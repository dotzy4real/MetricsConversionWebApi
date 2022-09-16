using Microsoft.AspNetCore.Builder;
using MetricsConversion.Domain.Common;
using MetricsConversion.Data.Core;

namespace MetricsConversion.Data.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        public static void StartEngine(this IApplicationBuilder application)
        {
            var engine = EngineContext.Current;

            //further actions are performed only when the database is installed
            if (DataSettingsManager.DatabaseIsInstalled)
            {
                //initialize and start schedule tasks
                //Services.Tasks.TaskManager.Instance.Initialize();
                //Services.Tasks.TaskManager.Instance.Start();

                //log application start
              ////  engine.Resolve<ILogger>().Information("API started");

                /* var pluginService = engine.Resolve<IPluginService>();

                 //install plugins
                 pluginService.InstallPlugins();

                 //update plugins
                 pluginService.UpdatePlugins();*/
            }
        }

    }
}
