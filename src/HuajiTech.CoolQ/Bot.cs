using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    internal partial class Bot : IBot
    {
        internal const string ApiVersion = "9";

        public static readonly string AppId = GetAppId();

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

        public bool CanSendImage => _canSendImage.Value;

        public bool CanSendRecord => _canSendRecord.Value;

        public DirectoryInfo DataDirectory => _dataDirectory.Value;

        public QQ.CurrentUser CurrentUser => _currentUser.Value;

        public QQ.Logger Logger { get; }

        internal ICollection<Plugin> Apps { get; private set; }

        private static string GetAppId()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AppIdAttribute>();

            if (attr is null)
            {
                throw new EntryPointNotFoundException(Resources.AppIdNotFound);
            }

            return attr.Id;
        }

        public FileInfo RequestImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            return new FileInfo(NativeMethods.RequestImage(AuthCode, fileName).CheckError());
        }

        public Task<FileInfo> RequestImageAsync(string fileName)
        {
            return Task.Run(() => RequestImage(fileName));
        }

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

        public Task<FileInfo> RequestRecordAsync(string fileName, string fileFormat)
        {
            return Task.Run(() => RequestRecord(fileName, fileFormat));
        }
    }
}