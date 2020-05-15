using Autofac;
using HuajiTech.CoolQ.Events;
using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    public partial class Bot : IBot
    {
        internal const string ApiVersion = "9";

        public static readonly string AppId = GetAppId();
        private static readonly ContainerBuilder _builder = new ContainerBuilder();
        private static readonly object _containerLock = new object();

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

            RegisterSdk(_builder);
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

        public ILogger Logger { get; } = new Logger();

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
                throw new TargetInvocationException(Resources.FailedToLoadPlugin, ex);
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
                throw new TargetInvocationException(Resources.FailedToLoadPlugin, ex);
            }
        }

        public static TPlugin LoadPlugin<TPlugin>()
            where TPlugin : notnull, IPlugin
        {
            _builder
                .RegisterType<TPlugin>()
                .AsSelf()
                .As<IPlugin>()
                .SingleInstance();

            lock (_containerLock)
            {
                _container = _builder.Build();
            }

            return GetPlugin<TPlugin>();
        }

        public static IPlugin LoadPlugin(Type pluginType)
        {
            if (!typeof(IPlugin).IsAssignableFrom(pluginType))
            {
                throw new ArgumentException(Resources.TypeIsNotPlugin, nameof(pluginType));
            }

            _builder
                .RegisterType(pluginType)
                .AsSelf()
                .As<IPlugin>()
                .SingleInstance();

            lock (_containerLock)
            {
                _container = _builder.Build();
            }

            return GetPlugin(pluginType);
        }

        [Conditional("DEBUG")]
        public static void LogPluginInfos(IEnumerable<IPlugin> plugins)
        {
            if (plugins is null)
            {
                throw new ArgumentNullException(nameof(plugins));
            }

            foreach (var plugin in plugins)
            {
                Instance.Logger.LogDebug(
                    Resources.PluginLoadTitle,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Resources.PluginLoadMessage,
                        plugin.GetType().FullName));
            }
        }

        public static ICollection<IPlugin> LoadPlugins(AppLifecycle loadStage)
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
                throw new TargetInvocationException(Resources.FailedToLoadPlugin, ex);
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

        private static void RegisterSdk(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(Instance)
                .AsImplementedInterfaces();

            builder
                .RegisterInstance(Instance.Logger)
                .As<ILogger>();

            builder
                .Register(context => Instance.CurrentUser)
                .As<ICurrentUser>();

            builder
                .RegisterInstance(CurrentUserEventSource.Instance)
                .AsImplementedInterfaces();
            builder
                .RegisterInstance(GroupEventSource.Instance)
                .AsImplementedInterfaces();
            builder
                .RegisterInstance(BotEventSource.Instance)
                .AsImplementedInterfaces();

            builder
                .Register(context => PluginContext.Current)
                .As<PluginContext>();
        }

        public FileInfo GetImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(fileName));
            }

            return new FileInfo(NativeMethods.Bot_GetImage(AuthCode, fileName).CheckError());
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

            return new FileInfo(NativeMethods.Bot_GetRecord(AuthCode, fileName, fileFormat).CheckError());
        }
    }
}