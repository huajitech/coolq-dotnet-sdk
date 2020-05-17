using Autofac;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    public partial class Bot : IBot
    {
        internal const string ApiVersion = "9";

        public static readonly string AppId = GetAppId();

        private static Bot? _instance;
        private static IContainer? _container;

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

        private Bot(int authCode)
        {
            AuthCode = authCode;
            _canSendImage = new Lazy<bool>(() => NativeMethods.Bot_GetCanSendImage(AuthCode));
            _canSendRecord = new Lazy<bool>(() => NativeMethods.Bot_GetCanSendRecord(AuthCode));
            _dataDirectory = new Lazy<DirectoryInfo>(() => new DirectoryInfo(
                NativeMethods.Bot_GetDataDirectory(AuthCode).CheckError()));
        }

        internal static Bot Instance
        {
            get => _instance ?? throw new InvalidOperationException(Resources.BotNotInitialized);
            private set => _instance = value;
        }

        public bool CanSendImage => _canSendImage.Value;

        public bool CanSendRecord => _canSendRecord.Value;

        public DirectoryInfo DataDirectory => _dataDirectory.Value;

        public ICurrentUser CurrentUser => _currentUser.Value;

        public ILogger Logger { get; } = new CoolQLogger();

        internal int AuthCode { get; }

        public static TPlugin GetPlugin<TPlugin>()
            where TPlugin : notnull, IPlugin
        {
            if (_container is null)
            {
                throw new InvalidOperationException(Resources.BotNotInitialized);
            }

            try
            {
                return _container.Resolve<TPlugin>();
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(Resources.FailedToLoadPlugin, ex);
            }
        }

        public static IPlugin GetPlugin(Type pluginType)
        {
            if (_container is null)
            {
                throw new InvalidOperationException(Resources.BotNotInitialized);
            }

            if (!typeof(IPlugin).IsAssignableFrom(pluginType))
            {
                throw new ArgumentException(Resources.TypeIsNotPlugin, nameof(pluginType));
            }

            try
            {
                return (IPlugin)_container.Resolve(pluginType);
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(Resources.FailedToLoadPlugin, ex);
            }
        }

        public static ICollection<IPlugin> GetPlugins(AppLifecycle loadStage)
        {
            if (_container is null)
            {
                throw new InvalidOperationException(Resources.BotNotInitialized);
            }

            try
            {
                return _container.ResolveNamed<ICollection<IPlugin>>(loadStage.ToString());
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(Resources.FailedToLoadPlugin, ex);
            }
        }

        public static ICollection<IPlugin> GetPlugins()
        {
            if (_container is null)
            {
                throw new InvalidOperationException(Resources.BotNotInitialized);
            }

            try
            {
                return _container.Resolve<ICollection<IPlugin>>();
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(Resources.FailedToLoadPlugin, ex);
            }
        }

        private static string GetAppId()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AppIdAttribute>();

            if (attr is null)
            {
                throw new InvalidOperationException(Resources.AppIdNotFound);
            }

            return attr.Id;
        }

        public System.IO.FileInfo GetImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            return new System.IO.FileInfo(NativeMethods.Bot_GetImage(AuthCode, fileName).CheckError());
        }

        public System.IO.FileInfo GetRecord(string fileName, string fileFormat)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(fileFormat))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileFormat));
            }

            return new System.IO.FileInfo(NativeMethods.Bot_GetRecord(AuthCode, fileName, fileFormat).CheckError());
        }
    }
}