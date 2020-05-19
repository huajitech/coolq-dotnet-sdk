using HuajiTech.CoolQ.Events;
using HuajiTech.UnmanagedExports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    public partial class Bot : IBot
    {
        internal const string ApiVersion = "9";
        public static readonly string AppId = GetAppId();

        private static Bot? _instance;

        private readonly Lazy<CurrentUser> _currentUser = new Lazy<CurrentUser>(() => new CurrentUser());
        private readonly Lazy<bool> _canSendImage;
        private readonly Lazy<bool> _canSendRecord;
        private readonly Lazy<DirectoryInfo> _dataDirectory;

        private IPacker? _packer;
        private ILoader? _loader;

        private Bot(int authCode)
        {
            AuthCode = authCode;
            _canSendImage = new Lazy<bool>(() => NativeMethods.Bot_GetCanSendImage(AuthCode));
            _canSendRecord = new Lazy<bool>(() => NativeMethods.Bot_GetCanSendRecord(AuthCode));
            _dataDirectory = new Lazy<DirectoryInfo>(() => new DirectoryInfo(
                NativeMethods.Bot_GetDataDirectory(AuthCode).CheckError()));
        }

        public static Bot Instance
        {
            get => _instance ?? throw new InvalidOperationException(CoreResources.BotNotInitialized);
            private set => _instance = value;
        }

        public IPacker Packer
        {
            get => _packer ?? throw new InvalidOperationException(CoreResources.BotNotInitialized);
            private set => _packer = value;
        }

        public ILoader Loader
        {
            get => _loader ?? throw new InvalidOperationException(CoreResources.BotNotInitialized);
            private set => _loader = value;
        }

        public bool CanSendImage => _canSendImage.Value;

        public bool CanSendRecord => _canSendRecord.Value;

        public DirectoryInfo DataDirectory => _dataDirectory.Value;

        public ICurrentUser CurrentUser => _currentUser.Value;

        public ILogger Logger { get; } = new CoolQLogger();

        internal int AuthCode { get; }

        [DllExport(EntryPoint = "AppInfo")]
        private static string GetAppInfo() => ApiVersion + "," + AppId;

        [DllExport]
        private static int Initialize(int authCode)
        {
            Instance = new Bot(authCode);

            Instance.Packer = GetInstance<IPacker>();
            Instance.Loader = GetInstance<ILoader>();

            PluginContext.Current = new CoolQPluginContext(Instance);

            Instance.Loader.GetPlugins(AppLifecycle.Initializing);

            var source = BotEventSource.Instance;

            var isFirstLoad = true;
            source.AppEnabled += (sender, e) =>
            {
                var plugins = Instance.Loader.GetPlugins(AppLifecycle.Enabled);

                if (isFirstLoad)
                {
                    LogPlugins(plugins);
                    isFirstLoad = false;
                }
            };

            source.BotStarted += (sender, e) => Instance.Loader.GetPlugins(AppLifecycle.BotStarted);
            source.AppDisabling += (sender, e) => Instance.Loader.GetPlugins(AppLifecycle.Disabling);
            source.BotStopping += (sender, e) => Instance.Loader.GetPlugins(AppLifecycle.BotStopping);

            return 0;
        }

        private static string GetAppId()
        {
            var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AppIdAttribute>();

            if (attr is null)
            {
                throw new InvalidOperationException(CoreResources.AppIdNotFound);
            }

            return attr.Id;
        }

        private static T GetInstance<T>()
        {
            return (from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && !type.IsAbstract && typeof(T).IsAssignableFrom(type)
                    select (T)Activator.CreateInstance(type)).Single();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                if (new StackTrace(ex)
                    .GetFrames()
                    .Any(frame => frame.GetMethod().Module.Assembly == Assembly.GetExecutingAssembly()))
                {
                    Instance.Logger.LogFatal(ex.ToString());
                }
            }
        }

        private static void LogPlugins(IEnumerable<IPlugin> plugins)
        {
            if (plugins is null)
            {
                throw new ArgumentNullException(nameof(plugins));
            }

            foreach (var plugin in plugins)
            {
                Instance.Logger.LogDebug(
                    CoreResources.PluginLoadTitle,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        CoreResources.PluginLoadMessage,
                        plugin.GetType().FullName));
            }
        }

        public FileInfo GetImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            return new FileInfo(NativeMethods.Bot_GetImage(AuthCode, fileName).CheckError());
        }

        public FileInfo GetRecord(string fileName, string fileFormat)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(fileFormat))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileFormat));
            }

            return new FileInfo(NativeMethods.Bot_GetRecord(AuthCode, fileName, fileFormat).CheckError());
        }
    }
}