using Autofac;
using HuajiTech.CoolQ.Events;
using HuajiTech.QQ;
using HuajiTech.UnmanagedExports;
using System;
using System.Diagnostics;
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

        private static void RegisterPlugins()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var defaultLoadStage = ((AppLifecycle?)executingAssembly
                .GetCustomAttribute<PluginLoadStageAttribute>()?.LoadStage) ?? AppLifecycle.Enabled;

            var types = executingAssembly.GetTypes()
                .Where(type => !type.IsInterface && !type.IsAbstract && typeof(IPlugin).IsAssignableFrom(type));

            foreach (var type in types)
            {
                var loadStage = ((AppLifecycle?)type
                    .GetCustomAttribute<PluginLoadStageAttribute>()?.LoadStage) ?? defaultLoadStage;

                _builder
                    .RegisterType(type)
                    .SingleInstance()
                    .Named<IPlugin>(loadStage.ToString());
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

            RegisterPlugins();

            lock (_containerLock)
            {
                _container = _builder.Build();
            }

            LoadPlugins(AppLifecycle.Initializing);

            var source = BotEventSource.Instance;

            source.AppEnabled += (sender, e) => LogPluginInfos(LoadPlugins(AppLifecycle.Enabled));
            source.BotStarted += (sender, e) => LoadPlugins(AppLifecycle.BotStarted);
            source.AppDisabling += (sender, e) => LoadPlugins(AppLifecycle.Disabling);
            source.BotStopping += (sender, e) => LoadPlugins(AppLifecycle.BotStopping);

            return 0;
        }
    }
}