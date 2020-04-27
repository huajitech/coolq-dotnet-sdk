using HuajiTech.QQ.Events;
using HuajiTech.UnmanagedExports;
using System;
using System.Globalization;

namespace HuajiTech.CoolQ.Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "CodeQuality", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    internal class BotEventSource : IBotEventSource
    {
        public static readonly BotEventSource Instance = new BotEventSource();

        private BotEventSource()
        {
        }

        public event EventHandler AppDisabling;

        public event EventHandler AppEnabled;

        public event EventHandler BotStarted;

        public event EventHandler BotStopping;

        [DllExport]
        private static int OnAppDisabling()
        {
            Instance.AppDisabling?.Invoke(null, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        private static int OnAppEnabled()
        {
            Bot.Instance.Logger.LogInfo(
                Resources.TestingNotificationTitle,
                string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.TestingNotificationContent,
                    Bot.Instance.AppId));

            foreach (var app in Bot.Instance.Apps)
            {
                Bot.Instance.Logger.LogDebug(
                    Resources.AppLoadTitle,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Resources.AppLoadMessage,
                        app.GetType().FullName));
            }

            Instance.AppEnabled?.Invoke(null, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        private static int OnStarted()
        {
            Instance.BotStarted?.Invoke(null, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        private static int OnStopping()
        {
            Instance.BotStopping?.Invoke(null, EventArgs.Empty);
            return 0;
        }
    }
}