using Autofac;
using HuajiTech.CoolQ.Events;
using HuajiTech.QQ;
using HuajiTech.UnmanagedExports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "CodeQuality", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    internal partial class Bot
    {
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                var frames = new StackTrace(ex)
                    .GetFrames()
                    .Where(frame => frame.GetMethod().Module.Assembly == Assembly.GetExecutingAssembly());

                if (frames.Any())
                {
                    Instance.Logger.LogFatal(ex.ToString());
                }
            }
        }

        private static void RegisterSdk(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(Instance)
                .AsImplementedInterfaces();

            builder
                .RegisterInstance(Instance.Logger)
                .As<ILogger>();

            builder
                .Register(context => Instance.CurrentUser)
                .As<ICurrentUser>();

            builder
                .RegisterInstance(CurrentUserEventSource.Instance)
                .AsImplementedInterfaces();
            builder
                .RegisterInstance(GroupEventSource.Instance)
                .AsImplementedInterfaces();
            builder
                .RegisterInstance(BotEventSource.Instance)
                .AsImplementedInterfaces();

            builder
                .Register(context => QQ.PluginContext.CurrentContext)
                .As<QQ.PluginContext>();
        }

        private static Dictionary<AppLifecycle, Type> RegisterPlugins(ContainerBuilder builder)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var defaultLoadStage = ((AppLifecycle?)executingAssembly
                .GetCustomAttribute<PluginLoadStageAttribute>()?.LoadStage) ?? AppLifecycle.Enabled;

            var infos = new Dictionary<AppLifecycle, Type>();

            var types = executingAssembly.GetTypes().Where(type => !type.IsInterface && !type.IsAbstract && type.IsAssignableTo<IPlugin>());

            foreach (var type in types)
            {
                var loadStage = ((AppLifecycle?)type
                    .GetCustomAttribute<PluginLoadStageAttribute>()?.LoadStage) ?? defaultLoadStage;

                infos.Add(loadStage, type);

                builder
                    .RegisterType(type)
                    .SingleInstance()
                    .Named<IPlugin>(loadStage.ToString());
            }

            return infos;
        }

        private static void LoadPlugins(IContainer container, AppLifecycle loadStage, bool logInfo = false)
        {
            try
            {
                var plugins = container.ResolveNamed<IEnumerable<IPlugin>>(loadStage.ToString());

                if (!logInfo)
                {
                    return;
                }

                foreach (var plugin in plugins)
                {
                    Instance.Logger.LogDebug(
                        Resources.PluginLoadTitle,
                        string.Format(
                            CultureInfo.InvariantCulture,
                            Resources.PluginLoadMessage,
                            plugin.GetType().FullName));
                }
            }
            catch (Exception ex)
            {
                throw new TargetInvocationException(Resources.FailedToLoadPlugin, ex);
            }
        }

        [DllExport(EntryPoint = "AppInfo")]
        private static string GetAppInfo() => ApiVersion + "," + AppId;

        [DllExport]
        private static int Initialize(int authCode)
        {
            Instance = new Bot(authCode);
            QQ.PluginContext.CurrentContext = new PluginContext(Instance);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var builder = new ContainerBuilder();

            RegisterSdk(builder);
            var pluginInfos = RegisterPlugins(builder);

            var container = builder.Build();

            LoadPlugins(container, AppLifecycle.Initializing);

            var source = BotEventSource.Instance;

            var isFirstTimeEnabled = true;

            source.AppEnabled += (sender, e) =>
            {
                if (!isFirstTimeEnabled)
                {
                    return;
                }

                LoadPlugins(container, AppLifecycle.Enabled, true);

                isFirstTimeEnabled = false;
            };

            source.BotStarted += (sender, e) => LoadPlugins(container, AppLifecycle.BotStarted);
            source.AppDisabling += (sender, e) => LoadPlugins(container, AppLifecycle.Disabling);
            source.BotStopping += (sender, e) => LoadPlugins(container, AppLifecycle.BotStopping);

            return 0;
        }
    }
}