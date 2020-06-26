using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using HuajiTech.UnmanagedExports;

namespace HuajiTech.CoolQ
{
    internal class Bot : IBot
    {
        internal const string ApiVersion = "9";
        public static readonly string AppId = GetAppId();

        private static Bot? _instance;

        private readonly Lazy<CurrentUser> _currentUser = new Lazy<CurrentUser>(() => new CurrentUser());
        private readonly Lazy<bool> _canSendImage;
        private readonly Lazy<bool> _canSendRecord;
        private readonly Lazy<DirectoryInfo> _appDirectory;

        private Bot(int authCode)
        {
            AuthCode = authCode;
            _canSendImage = new Lazy<bool>(() => NativeMethods.Bot_GetCanSendImage(AuthCode));
            _canSendRecord = new Lazy<bool>(() => NativeMethods.Bot_GetCanSendRecord(AuthCode));
            _appDirectory = new Lazy<DirectoryInfo>(() => new DirectoryInfo(
                NativeMethods.Bot_GetAppDirectory(AuthCode).CheckError()));
        }

        public static Bot Instance
        {
            get => _instance ?? throw new InvalidOperationException(CoreResources.NotInitialized);
            private set => _instance = value;
        }

        public bool CanSendImage => _canSendImage.Value;

        public bool CanSendRecord => _canSendRecord.Value;

        public DirectoryInfo AppDirectory => _appDirectory.Value;

        public ICurrentUser CurrentUser => _currentUser.Value;

        public ILogger Logger { get; } = new Logger();

        internal int AuthCode { get; }

        [DllExport(EntryPoint = "AppInfo")]
        internal static string GetAppInfo() => ApiVersion + "," + AppId;

        [DllExport(EntryPoint = "Initialize")]
        internal static int Init(int authCode)
        {
            if (!(_instance is null))
            {
                throw new InvalidOperationException(CoreResources.AlreadyInitialized);
            }

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Instance = new Bot(authCode);
            PluginContext.Current = new PluginContextCore(Instance);

            var type = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<InitializerAttribute>()?.Initializer ??
                throw new AppInitializationException(CoreResources.InitializerNotFound);

            try
            {
                type.GetMethod("Init", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.Invoke(null, null);
            }
            catch (Exception ex)
            {
                throw new AppInitializationException(CoreResources.InitializationFailed, ex);
            }

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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                if (new StackTrace(ex)
                    .GetFrames()
                    .Any(frame => frame.GetMethod().Module.Assembly == Assembly.GetExecutingAssembly()))
                {
                    Instance.Logger.RaiseFatal(ex.ToString());
                }
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