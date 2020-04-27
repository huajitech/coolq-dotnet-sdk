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
        [DllExport(EntryPoint = "AppInfo")]
        private static string GetAppInfo()
        {
            return ApiVersion + "," + AppId;
        }

        [DllExport]
        private static int Initialize(int authCode)
        {
            Instance = new Bot(authCode);
            QQ.AppContext.CurrentContext = new DefaultAppContext(Instance);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
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
            };

            var builder = new ContainerBuilder();

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
                .Register(context => QQ.AppContext.CurrentContext)
                .As<QQ.AppContext>();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<App>()
                .SingleInstance()
                .As<App>();

            var container = builder.Build();

            try
            {
                Instance.Apps = container.Resolve<ICollection<App>>();
            }
            catch (Exception ex)
            {
                throw new EntryPointNotFoundException(Resources.AppNotFound, ex);
            }

            return 0;
        }
    }
}