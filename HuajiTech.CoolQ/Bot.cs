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
        /// 获取当前应用的 AppID。
        /// </summary>
        public static readonly string AppId = GetApp(out AppConstructor);

        internal const string ApiVersion = "9";
        private static readonly ConstructorInfo AppConstructor;

        private static readonly Lazy<bool> _canSendImage =
            new Lazy<bool>(() => NativeMethods.GetCanSendImage(AuthCode));

        private static readonly Lazy<bool> _canSendRecord =
            new Lazy<bool>(() => NativeMethods.GetCanSendRecord(AuthCode));

        private static readonly Lazy<CurrentUser> _currentUser =
            new Lazy<CurrentUser>(() => new CurrentUser());

        private static readonly Lazy<DirectoryInfo> _dataDirectory =
            new Lazy<DirectoryInfo>(() => new DirectoryInfo(NativeMethods.GetDataDirectory(AuthCode)));

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
        public static bool CanSendImage => _canSendImage.Value;

        /// <summary>
        /// 获取一个值，指示是否可以发送录音。
        /// </summary>
        public static bool CanSendRecord => _canSendRecord.Value;

        /// <summary>
        /// 获取机器人的当前用户。
        /// </summary>
        public static CurrentUser CurrentUser => _currentUser.Value;

        /// <summary>
        /// 获取应用的数据目录。
        /// </summary>
        public static DirectoryInfo DataDirectory => _dataDirectory.Value;

        /// <summary>
        /// 获取一个值，指示应用是否已被启用。
        /// </summary>
        public static bool IsAppEnabled { get; private set; }

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
        /// 请求图片。
        /// </summary>
        /// <param name="fileName">图片的文件名。</param>
        /// <returns>图片的文件信息。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static FileInfo RequestImage(string fileName)
        {
            return new FileInfo(NativeMethods.RequestImage(AuthCode, fileName).CheckError());
        }

        /// <summary>
        /// 以异步操作请求图片。
        /// </summary>
        /// <param name="fileName">图片的文件名。</param>
        /// <returns>图片的文件信息。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
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
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static FileInfo RequestRecord(string fileName, string fileFormat)
        {
            return new FileInfo(NativeMethods.RequestRecord(AuthCode, fileName, fileFormat).CheckError());
        }

        /// <summary>
        /// 以异步操作请求录音。
        /// </summary>
        /// <param name="fileName">录音的文件名。</param>
        /// <param name="fileFormat">录音的格式。</param>
        /// <returns>录音的文件信息。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static Task<FileInfo> RequestRecordAsync(string fileName, string fileFormat)
        {
            return Task.Run(() => RequestRecord(fileName, fileFormat));
        }

        private static string GetApp(out ConstructorInfo constructorInfo)
        {
            var apps = from type in typeof(Bot).Assembly.GetTypes()
                       where !type.IsAbstract
                       let attr = type.GetCustomAttribute<AppAttribute>()
                       where !(attr is null)
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