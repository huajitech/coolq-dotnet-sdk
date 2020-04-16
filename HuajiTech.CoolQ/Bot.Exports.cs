using HuajiTech.UnmanagedExports;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    public static partial class Bot
    {
        private static Exception _constructorException;

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
                    var frames = new System.Diagnostics.StackTrace(ex)
                        .GetFrames()
                        .Where(frame => frame.GetMethod().Module.Assembly == App.GetType().Assembly);

                    if (frames.Any())
                    {
                        LogFatal(ex.ToString());
                    }
                }
            };

            try
            {
                App = AppConstructor.Invoke(null);
            }
            catch (TargetInvocationException ex)
            {
                _constructorException = ex.InnerException;
            }

            return 0;
        }

        [DllExport]
        private static int OnAppDisabling()
        {
            AppDisabling?.Invoke(null, EventArgs.Empty);
            return 0;
        }

        [DllExport]
        private static int OnAppEnabled()
        {
            Log(
                LogLevel.Info,
                Resources.TestingNotificationTitle,
                string.Format(CultureInfo.InvariantCulture, Resources.TestingNotificationContent, AppId));

            if (!(_constructorException is null))
            {
                LogFatal(Resources.UnhandledExceptionInConstructor + "\n" + _constructorException.ToString());
            }

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