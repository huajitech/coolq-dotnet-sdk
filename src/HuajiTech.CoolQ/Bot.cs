using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    internal partial class Bot : IBot
    {
        internal const string ApiVersion = "9";

        public static readonly string AppId = GetAppId();
        private static Bot? _instance;

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

        public static Bot Instance
        {
            get => _instance ?? throw new InvalidOperationException(Resources.BotNotInitialized);
            private set => _instance = value;
        }

        public bool CanSendImage => _canSendImage.Value;

        public bool CanSendRecord => _canSendRecord.Value;

        public DirectoryInfo DataDirectory => _dataDirectory.Value;

        public ICurrentUser CurrentUser => _currentUser.Value;

        public ILogger Logger { get; }

        internal List<IPlugin> Plugins { get; } = new List<IPlugin>();

        internal int AuthCode { get; }

        private static string GetAppId()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AppIdAttribute>();

            if (attr is null)
            {
                throw new InvalidOperationException(Resources.AppIdNotFound);
            }

            return attr.Id;
        }

        public FileInfo GetImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            return new FileInfo(NativeMethods.GetImage(AuthCode, fileName).CheckError());
        }

        public FileInfo GetRecord(string fileName, string fileFormat)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(fileFormat))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileFormat));
            }

            return new FileInfo(NativeMethods.GetRecord(AuthCode, fileName, fileFormat).CheckError());
        }
    }
}