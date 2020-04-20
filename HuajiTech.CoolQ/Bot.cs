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
        /// 请求获取指定文件名的图片的文件。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名</param>
        /// <returns>指定文件名的图片文件。</returns>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static FileInfo RequestImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            return new FileInfo(NativeMethods.RequestImage(AuthCode, fileName).CheckError());
        }

        /// <summary>
        /// 以异步操作请求获取指定文件名的图片的文件。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名</param>
        /// <returns>指定文件名的图片文件。</returns>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static Task<FileInfo> RequestImageAsync(string fileName)
        {
            return Task.Run(() => RequestImage(fileName));
        }

        /// <summary>
        /// 请求获取指定文件名的录音的文件。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名</param>
        /// <param name="fileFormat">返回的文件的格式。</param>
        /// <returns>指定文件名的录音文件。</returns>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="ArgumentException"><paramref name="fileFormat"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static FileInfo RequestRecord(string fileName, string fileFormat)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(fileFormat))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileFormat));
            }

            return new FileInfo(NativeMethods.RequestRecord(AuthCode, fileName, fileFormat).CheckError());
        }

        /// <summary>
        /// 以异步操作请求获取指定文件名的录音的文件。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名</param>
        /// <param name="fileFormat">返回的文件的格式。</param>
        /// <returns>指定文件名的录音文件。</returns>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="ArgumentException"><paramref name="fileFormat"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
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