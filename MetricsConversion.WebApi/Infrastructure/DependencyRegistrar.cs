using Autofac;
using Autofac.Builder;
using Autofac.Core;
using System.Collections.Generic;
using System.Reflection;
using MetricsConversion.Data;
using MetricsConversion.Data.Configuration;
using MetricsConversion.Data.Core;
using MetricsConversion.Data.ComponentModel;
using MetricsConversion.Domain.Conversions;
using MetricsConversion.Services.Conversions;
using MetricsConversion.Services.Core;
using MetricsConversion.Data.Infrastructure;
using System;


namespace MetricsConversion.WebApi.Infrastructure
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, Config config)
        {
           
            RegisterServices(builder);

        }



        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UnitConversionService>().As<IUnitConversionService>().InstancePerLifetimeScope();

            if (DataSettingsManager.DatabaseIsInstalled)
            {

            }
        }
        public virtual int Order
        {
            get { return Int16.MaxValue; }
        }
    }

    /// <summary>
    /// Setting source
    /// </summary>
    public class SettingsSource : IRegistrationSource
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
                    }
                    catch
                    {
                        if (!DataSettingsManager.DatabaseIsInstalled)
                            store = null;
                        else
                            throw;
                    }

                    var currentStoreId = store?.Id ?? 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    try
                    {
                        return c.Resolve<MetricsConversion.Data.Core.ISettingService>().LoadSetting<TSettings>(currentStoreId);
                    }
                    catch
                    {
                        if (DataSettingsManager.DatabaseIsInstalled)
                            throw;
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
