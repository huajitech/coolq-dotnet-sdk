using HuajiTech.UnmanagedExports;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    public static partial class Bot
    {
        [DllExport(EntryPoint = "AppInfo")]
        private static string GetAppInfo()
        {
            return ApiVersion + "," + AppId;
        }

        [DllExport]
        private static int Initialize(int authCode)
        {
            AuthCode = authCode;

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    var frames = new StackTrace(ex)
                        .GetFrames()
                        .Where(frame => frame.GetMethod().Module.Assembly == Assembly.GetExecutingAssembly());

                    if (frames.Any())
                    {
                        Logger.LogFatal(ex.ToString());
                    }
                }
            };

            App = AppConstructor.Invoke(null);

            return 0;
        }

        [DllExport]
        private static int OnAppDisabling()
        {
            IsAppEnabled = false;
            AppDisabling?.Invoke(null, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        private static int OnAppEnabled()
        {
            Logger.LogInfo(
                Resources.TestingNotificationTitle,
                string.Format(CultureInfo.InvariantCulture, Resources.TestingNotificationContent, AppId));

            IsAppEnabled = true;
            AppEnabled?.Invoke(null, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        private static int OnStarted()
        {
            Started?.Invoke(null, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        private static int OnStopping()
        {
            Stopping?.Invoke(null, EventArgs.Empty);
            return 0;
        }
    }
}