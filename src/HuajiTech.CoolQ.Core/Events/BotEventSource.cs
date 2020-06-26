using System;
using HuajiTech.UnmanagedExports;

namespace HuajiTech.CoolQ.Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    public sealed class BotEventSource : IBotEventSource
    {
        public static readonly BotEventSource Instance = new BotEventSource();

        private BotEventSource()
        {
        }

        public event EventHandler? AppDisabling;

        public event EventHandler? AppEnabled;

        public event EventHandler? BotStarted;

        public event EventHandler? BotStopping;

        [DllExport]
        internal static int OnAppDisabling()
        {
            Instance.AppDisabling?.Invoke(Instance, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        internal static int OnAppEnabled()
        {
            try
            {
                Instance.AppEnabled?.Invoke(Instance, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
                return 1;
            }

            return 0;
        }

        [DllExport]
        internal static int OnBotStarted()
        {
            Instance.BotStarted?.Invoke(Instance, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        internal static int OnBotStopping()
        {
            Instance.BotStopping?.Invoke(Instance, EventArgs.Empty);
            return 0;
        }
    }
}