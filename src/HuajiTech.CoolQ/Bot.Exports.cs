using Autofac;
using Autofac.Util;
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
    public partial class Bot
    {
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                if (new StackTrace(ex)
                    .GetFrames()
                    .Any(frame => frame.GetMethod().Module.Assembly == Assembly.GetExecutingAssembly()))
                {
                    Instance.Logger.LogFatal(ex.ToString());
                }
            }
        }

        private static void LogPlugins(IEnumerable<IPlugin> plugins)
        {
            if (plugins is null)
            {
                throw new ArgumentNullException(nameof(plugins));
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
                .Register(context => PluginContext.Current)
                .As<PluginContext>();
        }

        private static void RegisterPlugins(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var defaultLoadStage = ((AppLifecycle?)assembly
                .GetCustomAttribute<DefaultPluginLoadStageAttribute>()?.LoadStage) ?? AppLifecycle.Enabled;

            var attributes = assembly.GetCustomAttributes<PluginLoadStageAttribute>();

            var types = assembly.GetLoadableTypes()
                .Where(type => !type.IsInterface && !type.IsAbstract && typeof(IPlugin).IsAssignableFrom(type));

            foreach (var type in types)
            {
                var loadStage = ((AppLifecycle?)attributes
                    .FirstOrDefault(attr => attr.Type == type)?.LoadStage) ?? defaultLoadStage;

                builder
                    .RegisterType(type)
                    .SingleInstance()
                    .Named<IPlugin>(loadStage.ToString())
                    .As<IPlugin>()
                    .AsSelf();
            }
        }

        [DllExport(EntryPoint = "AppInfo")]
        private static string GetAppInfo() => ApiVersion + "," + AppId;

        [DllExport]
        private static int Initialize(int authCode)
        {
            Instance = new Bot(authCode);
            PluginContext.Current = new CoolQPluginContext(Instance);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var builder = new ContainerBuilder();

            RegisterSdk(builder);
            RegisterPlugins(builder);

            _container = builder.Build();

            GetPlugins(AppLifecycle.Initializing);

            var source = BotEventSource.Instance;

            source.AppEnabled += (sender, e) => LogPlugins(GetPlugins(AppLifecycle.Enabled));
            source.BotStarted += (sender, e) => GetPlugins(AppLifecycle.BotStarted);
            source.AppDisabling += (sender, e) => GetPlugins(AppLifecycle.Disabling);
            source.BotStopping += (sender, e) => GetPlugins(AppLifecycle.BotStopping);

            return 0;
        }
    }
}