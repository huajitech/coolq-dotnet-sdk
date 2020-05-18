using Autofac;
using Autofac.Util;
using HuajiTech.CoolQ.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace HuajiTech.CoolQ.Loaders
{
    public class AutofacLoader : ILoader
    {
        private readonly IContainer _container;

        public AutofacLoader()
        {
            var builder = new ContainerBuilder();

            RegisterSdk(builder);
            RegisterPlugins(builder);

            _container = builder.Build();
        }

        public TPlugin GetPlugin<TPlugin>()
          where TPlugin : notnull, IPlugin
        {
            try
            {
                return _container.Resolve<TPlugin>();
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(AutofacLoaderResources.FailedToLoadPlugin, ex);
            }
        }

        public IPlugin GetPlugin(Type pluginType)
        {
            if (!typeof(IPlugin).IsAssignableFrom(pluginType))
            {
                throw new ArgumentException(AutofacLoaderResources.TypeIsNotPlugin, nameof(pluginType));
            }

            try
            {
                return (IPlugin)_container.Resolve(pluginType);
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(AutofacLoaderResources.FailedToLoadPlugin, ex);
            }
        }

        public ICollection<IPlugin> GetPlugins(AppLifecycle loadStage)
        {
            try
            {
                return _container.ResolveNamed<ICollection<IPlugin>>(loadStage.ToString());
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(AutofacLoaderResources.FailedToLoadPlugin, ex);
            }
        }

        public ICollection<IPlugin> GetPlugins()
        {
            try
            {
                return _container.Resolve<ICollection<IPlugin>>();
            }
            catch (Exception ex)
            {
                throw new TypeLoadException(AutofacLoaderResources.FailedToLoadPlugin, ex);
            }
        }

        private static void RegisterSdk(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(Bot.Instance)
                .AsImplementedInterfaces();

            builder
                .RegisterInstance(Bot.Instance.Logger)
                .As<ILogger>();

            builder
                .Register(context => Bot.Instance.CurrentUser)
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

        private static void RegisterPlugins(ContainerBuilder builder)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var defaultLoadStage = ((AppLifecycle?)executingAssembly
                .GetCustomAttribute<DefaultPluginLoadStageAttribute>()?.LoadStage) ?? AppLifecycle.Enabled;

            var attributes = executingAssembly.GetCustomAttributes<PluginLoadStageAttribute>();

            var types = from asm in AppDomain.CurrentDomain.GetAssemblies()
                        from type in asm.GetLoadableTypes()
                        where type.IsClass && !type.IsAbstract && typeof(IPlugin).IsAssignableFrom(type)
                        select type;

            foreach (var type in types)
            {
                var loadStage = ((AppLifecycle?)attributes
                    .FirstOrDefault(attr => attr.Type == type)?.LoadStage) ?? defaultLoadStage;

                builder
                    .RegisterType(type)
                    .SingleInstance()
                    .Named<IPlugin>(loadStage.ToString())
                    .As<IPlugin>()
                    .AsSelf();
            }
        }
    }
}