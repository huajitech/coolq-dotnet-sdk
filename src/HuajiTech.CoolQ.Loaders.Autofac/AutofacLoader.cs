using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Util;
using HuajiTech.CoolQ.Events;

namespace HuajiTech.CoolQ.Loaders
{
    public class AutofacLoader : ILoader
    {
        private readonly ContainerBuilder _builder;

        public AutofacLoader(ContainerBuilder builder)
            => _builder = builder ?? throw new ArgumentNullException(nameof(builder));

        public IContainer? Container { get; private set; }

        private static void LogPlugins(IEnumerable<object> plugins)
        {
            if (plugins is null)
            {
                throw new ArgumentNullException(nameof(plugins));
            }

            foreach (var plugin in plugins)
            {
                PluginContext.Current.Bot.Logger.LogDebug(
                    AutofacLoaderResources.PluginLoadTitle,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        AutofacLoaderResources.PluginLoadMessage,
                        plugin.GetType().FullName));
            }
        }

        public AutofacLoader RegisterSdk()
        {
            if (!(Container is null))
            {
                return this;
            }

            _builder
                .RegisterInstance(CurrentUserEventSource.Instance)
                .AsImplementedInterfaces();

            _builder
                .RegisterInstance(GroupEventSource.Instance)
                .AsImplementedInterfaces();

            _builder
                .RegisterInstance(BotEventSource.Instance)
                .AsImplementedInterfaces();

            _builder
                .Register(context => PluginContext.Current)
                .As<PluginContext>();

            _builder
                .Register(context => PluginContext.Current.Bot)
                .AsImplementedInterfaces();

            _builder
                .Register(context => PluginContext.Current.Bot.Logger)
                .As<ILogger>();

            _builder
                .Register(context => PluginContext.Current.Bot.CurrentUser)
                .As<ICurrentUser>();

            return this;
        }

        public AutofacLoader RegisterPlugins(Assembly assembly)
        {
            if (!(Container is null))
            {
                return this;
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var attributes = assembly.GetCustomAttributes<PluginAttribute>();

            foreach (var type in assembly.GetLoadableTypes().Where(type => type.IsClass && !type.IsAbstract))
            {
                var attr = type.GetCustomAttribute<PluginAttribute>();

                if (attr is null)
                {
                    continue;
                }

                _builder
                    .RegisterType(type)
                    .SingleInstance()
                    .Named<object>(attr.LoadTiming.ToString())
                    .AsSelf();
            }

            return this;
        }

        public AutofacLoader Build()
        {
            Container = _builder.Build();
            return this;
        }

        public AutofacLoader Init()
        {
            GetPlugins(AppLifecycle.Initializing);

            var source = BotEventSource.Instance;

            var isFirstLoad = true;
            source.AppEnabled += (sender, e) =>
            {
                var plugins = GetPlugins(AppLifecycle.Enabled);

                if (isFirstLoad)
                {
                    LogPlugins(plugins);
                    isFirstLoad = false;
                }
            };

            source.BotStarted += (sender, e) => GetPlugins(AppLifecycle.BotStarted);

            source.AppDisabling += (sender, e) => GetPlugins(AppLifecycle.Disabling);

            source.BotStopping += (sender, e) => GetPlugins(AppLifecycle.BotStopping);

            return this;
        }

        public TPlugin GetPlugin<TPlugin>()
            where TPlugin : notnull
        {
            if (Container is null)
            {
                throw new InvalidOperationException(AutofacLoaderResources.NotBuilt);
            }

            try
            {
                return Container.Resolve<TPlugin>();
            }
            catch (Exception ex)
            {
                throw new PluginLoadException(AutofacLoaderResources.FailedToLoadPlugin, ex);
            }
        }

        public object GetPlugin(Type pluginType)
        {
            if (Container is null)
            {
                throw new InvalidOperationException(AutofacLoaderResources.NotBuilt);
            }

            try
            {
                return Container.Resolve(pluginType);
            }
            catch (Exception ex)
            {
                throw new PluginLoadException(AutofacLoaderResources.FailedToLoadPlugin, ex);
            }
        }

        public IReadOnlyCollection<object> GetPlugins(AppLifecycle loadTiming)
        {
            if (Container is null)
            {
                throw new InvalidOperationException(AutofacLoaderResources.NotBuilt);
            }

            try
            {
                return Container.ResolveNamed<IReadOnlyCollection<object>>(loadTiming.ToString());
            }
            catch (Exception ex)
            {
                throw new PluginLoadException(AutofacLoaderResources.FailedToLoadPlugin, ex);
            }
        }
    }
}