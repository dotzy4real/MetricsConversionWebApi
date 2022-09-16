using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MetricsConversion.Data.Configuration;
using MetricsConversion.Data.Core;
using MetricsConversion.Data.Caching;

namespace MetricsConversion.Data.Infrastructure
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, Config config)
        {
            builder.RegisterType<DefaultFileProvider>().As<IDefaultFileProvider>().InstancePerLifetimeScope();

            builder.RegisterType<DataProviderManager>().As<IDataProviderManager>().InstancePerDependency();
            builder.Register(context => context.Resolve<IDataProviderManager>().DataProvider).As<IDefaultDataProvider>().InstancePerDependency();

            //repositories
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(AuditEntityRepository<>)).As(typeof(IAuditRepository<>)).InstancePerLifetimeScope();


            //redis connection wrapper
            //if (config.RedisEnabled)
            //{
            //    builder.RegisterType<RedisConnectionWrapper>()
            //        .As<ILocker>()
            //        .As<IRedisConnectionWrapper>()
            //        .SingleInstance();
            //}

           //static cache manager
           // if (config.RedisEnabled && config.UseRedisForCaching)
           // {
           //     builder.RegisterType<RedisCacheManager>().As<IStaticCacheManager>().InstancePerLifetimeScope();
           //}
           // else
           // {
                builder.RegisterType<MemoryCacheManager>()
                    .As<ILocker>()
                    .As<IStaticCacheManager>()
                    .SingleInstance();
            //}


            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();

            //register all settings
            builder.RegisterSource(new SettingsSource());
            RegisterPluginServices(builder);
            //RegisterModelBinders(builder);

            RegisterServices(builder);

            //event consumers
            //var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            //foreach (var consumer in consumers)
            //{
            //    builder.RegisterType(consumer)
            //        .As(consumer.FindInterfaces((type, criteria) =>
            //        {
            //            var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
            //            return isMatch;
            //        }, typeof(IConsumer<>)))
            //        .InstancePerLifetimeScope();
            //}


            builder.RegisterType<MigrationManager>().As<IMigrationManager>().InstancePerLifetimeScope();
            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
        }

        //private void RegisterModelBinders(ContainerBuilder builder)
        //{
        //    builder.RegisterGeneric(typeof(ParametersModelBinder<>)).InstancePerLifetimeScope();
        //    builder.RegisterGeneric(typeof(JsonModelBinder<>)).InstancePerLifetimeScope();
        //}

        private void RegisterPluginServices(ContainerBuilder builder)
        {

            //builder.RegisterType<DTOHelper>().As<IDTOHelper>().InstancePerLifetimeScope();
            //builder.RegisterType<SimpleMarketConfigManagerHelper>().As<IConfigManagerHelper>().InstancePerLifetimeScope();


            //builder.RegisterType<Maps.JsonPropertyMapper>().As<Maps.IJsonPropertyMapper>().InstancePerLifetimeScope();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<Dictionary<string, object>>().SingleInstance();
        }

        private void RegisterServices(ContainerBuilder builder)
        {


        }

        public virtual int Order
        {
            get { return Int16.MaxValue; }
        }
    }

    /// <summary>
    /// Setting source
    /// </summary>
    public class SettingsSource: IRegistrationSource
    {
        private static readonly MethodInfo _buildMethod =
            typeof(SettingsSource).GetMethod("BuildRegistration", BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Registrations for
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="registrations">Registrations</param>
        /// <returns>Registrations</returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = _buildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        private static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    AppInstance store;

                    try
                    {
                        store = c.Resolve<IAppInstanceContext>().CurrentAppInstance;
                        //store = null;
                    }
                    catch (Exception e)
                    {
                        if (!DataSettingsManager.DatabaseIsInstalled)
                            store = null;
                        else
                            throw (e);
                    }

                    var currentStoreId = store?.Id ?? 0;

                    //uncomment the code below if you want load settings per store only when you have two stores installed.
                    //var currentStoreId = c.Resolve<IAppInstanceService>().GetAllStores().Count > 1
                    //    c.Resolve<IAppInstanceContext>().CurrentAppInstance.Id : 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    try
                    {
                        return c.Resolve<ISettingService>().LoadSetting<TSettings>(currentStoreId);
                    }
                    catch (Exception e)
                    {
                        if (DataSettingsManager.DatabaseIsInstalled)
                            throw (e);
                    }

                    return default;
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        /// <summary>
        /// Is adapter for individual components
        /// </summary>
        public bool IsAdapterForIndividualComponents => false;
    }

}

