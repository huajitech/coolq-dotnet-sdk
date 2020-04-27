using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供与机器人交互的方法、事件和属性的静态类。
    /// </summary>
    internal partial class Bot : IBot
    {
        internal const string ApiVersion = "9";

        private static string _appId;

        private readonly Lazy<CurrentUser> _currentUser = new Lazy<CurrentUser>(() => new CurrentUser());

        private readonly Lazy<bool> _canSendImage;
        private readonly Lazy<bool> _canSendRecord;
        private readonly Lazy<DirectoryInfo> _dataDirectory;

        static Bot()
        {
            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();

            AppDomain.CurrentDomain.ResourceResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();
        }

        public Bot(int authCode)
        {
            AuthCode = authCode;
            Logger = new Logger();
            _canSendImage = new Lazy<bool>(() => NativeMethods.GetCanSendImage(AuthCode));
            _canSendRecord = new Lazy<bool>(() => NativeMethods.GetCanSendRecord(AuthCode));
            _dataDirectory = new Lazy<DirectoryInfo>(() => new DirectoryInfo(
                NativeMethods.GetDataDirectory(AuthCode).CheckError()));
        }

        public static Bot Instance { get; private set; }

        public int AuthCode { get; }

        /// <summary>
        /// 获取一个值，指示是否可以发送图片。
        /// </summary>
        public bool CanSendImage => _canSendImage.Value;

        /// <summary>
        /// 获取一个值，指示是否可以发送录音。
        /// </summary>
        public bool CanSendRecord => _canSendRecord.Value;

        /// <summary>
        /// 获取应用的数据目录。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public DirectoryInfo DataDirectory => _dataDirectory.Value;

        public ICurrentUser CurrentUser => _currentUser.Value;

        public ILogger Logger { get; }

        internal ICollection<App> Apps { get; private set; }

        private static string GetAppId()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AppIdAttribute>();

            if (attr is null)
            {
                throw new EntryPointNotFoundException(Resources.AppIdNotFound);
            }

            return attr.Id;
        }

        /// <summary>
        /// 请求获取指定文件名的图片的文件。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名</param>
        /// <returns>指定文件名的图片文件。</returns>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public FileInfo RequestImage(string fileName)
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
        public Task<FileInfo> RequestImageAsync(string fileName)
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
        public FileInfo RequestRecord(string fileName, string fileFormat)
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
        public Task<FileInfo> RequestRecordAsync(string fileName, string fileFormat)
        {
            return Task.Run(() => RequestRecord(fileName, fileFormat));
        }
    }
}