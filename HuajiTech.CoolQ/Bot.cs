using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供与机器人交互的方法、事件和属性的静态类。
    /// </summary>
    public static partial class Bot
    {
        /// <summary>
        /// 获取 AppID。
        /// </summary>
        public static readonly string AppId = GetApp(out AppConstructor);

        internal const string ApiVersion = "9";
        private static readonly ConstructorInfo AppConstructor;

        private static bool? _canSendImage;
        private static bool? _canSendRecord;
        private static DirectoryInfo _dataDirectory;

        static Bot()
        {
            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();

            AppDomain.CurrentDomain.ResourceResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// 获取一个值，指示是否可以发送图片。
        /// </summary>
        public static bool CanSendImage =>
            _canSendImage ??= NativeMethods.GetCanSendImage(AuthCode);

        /// <summary>
        /// 获取一个值，指示是否可以发送录音。
        /// </summary>
        public static bool CanSendRecord =>
            _canSendRecord ??= NativeMethods.GetCanSendRecord(AuthCode);

        internal static object App { get; private set; }

        internal static int AuthCode { get; private set; }

        /// <summary>
        /// 在应用被禁用时引发。
        /// </summary>
        public static event EventHandler AppDisabling;

        /// <summary>
        /// 在应用被启用时引发。
        /// </summary>
        public static event EventHandler AppEnabled;

        /// <summary>
        /// 在机器人启动时引发。
        /// </summary>
        public static event EventHandler Started;

        /// <summary>
        /// 在机器人停止时引发。
        /// </summary>
        public static event EventHandler Stopping;

        /// <summary>
        /// 获取应用的数据目录。
        /// </summary>
        public static DirectoryInfo DataDirectory =>
            _dataDirectory ??= new DirectoryInfo(NativeMethods.GetDataDirectory(AuthCode));

        /// <summary>
        /// 记录一条日志。
        /// </summary>
        /// <param name="level">日志的等级。</param>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void Log(LogLevel level, string type, string message)
        {
            NativeMethods.Log(AuthCode, level, type, message).CheckError();
        }

        /// <summary>
        /// 以异步操作记录一条日志。
        /// </summary>
        /// <param name="level">日志的等级。</param>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static Task LogAsync(LogLevel level, string type, string message)
        {
            return Task.Run(() => Log(level, type, message));
        }

        /// <summary>
        /// 记录一个致命错误。
        /// </summary>
        /// <param name="message">致命错误的消息。</param>
        public static void LogFatal(string message)
        {
            NativeMethods.LogFatal(AuthCode, message).CheckError();
        }

        /// <summary>
        /// 以异步操作记录一个致命错误。
        /// </summary>
        /// <param name="message">致命错误的消息。</param>
        public static Task LogFatalAsync(string message)
        {
            return Task.Run(() => LogFatal(message));
        }

        /// <summary>
        /// 请求图片。
        /// </summary>
        /// <param name="fileName">图片的文件名。</param>
        /// <returns>图片的文件信息。</returns>
        public static FileInfo RequestImage(string fileName)
        {
            return new FileInfo(NativeMethods.RequestImage(AuthCode, fileName) ??
                throw new CoolQException(Resources.NullReturnValue));
        }

        /// <summary>
        /// 以异步操作请求图片。
        /// </summary>
        /// <param name="fileName">图片的文件名。</param>
        /// <returns>图片的文件信息。</returns>
        public static Task<FileInfo> RequestImageAsync(string fileName)
        {
            return Task.Run(() => RequestImage(fileName));
        }

        /// <summary>
        /// 请求录音。
        /// </summary>
        /// <param name="fileName">录音的文件名。</param>
        /// <param name="fileFormat">录音的格式。</param>
        /// <returns>录音的文件信息。</returns>
        public static FileInfo RequestRecord(string fileName, string fileFormat)
        {
            return new FileInfo(NativeMethods.RequestRecord(AuthCode, fileName, fileFormat) ??
                throw new CoolQException(Resources.NullReturnValue));
        }

        /// <summary>
        /// 以异步操作请求录音。
        /// </summary>
        /// <param name="fileName">录音的文件名。</param>
        /// <param name="fileFormat">录音的格式。</param>
        /// <returns>录音的文件信息。</returns>
        public static Task<FileInfo> RequestRecordAsync(string fileName, string fileFormat)
        {
            return Task.Run(() => RequestRecord(fileName, fileFormat));
        }

        private static string GetApp(out ConstructorInfo constructorInfo)
        {
            var apps = from asm in AppDomain.CurrentDomain.GetAssemblies()
                       from type in asm.GetTypes()
                       let attr = type.GetCustomAttribute<AppAttribute>()
                       where !(attr is null)
                       where !type.IsAbstract
                       from ctor in type.GetConstructors()
                       where ctor.IsPublic && !ctor.GetParameters().Any()
                       select new
                       {
                           Constructor = ctor,
                           attr.Id
                       };

            if (apps.Count() != 1)
            {
                constructorInfo = null;
                return Resources.AppNotFound;
            }

            var app = apps.ElementAt(0);
            constructorInfo = app.Constructor;
            return app.Id;
        }
    }
}