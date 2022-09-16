using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using MetricsConversion.Data.Core;
using MetricsConversion.Data.Configuration;
using MetricsConversion.Data.Infrastructure;


namespace MetricsConversion.WebApi
{
    public class Startup
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IEngine _engine;
        private Config _defaultConfig;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _webHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<HeaderFilter>();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Metric Conversion API",
                    Version = "BNPL API",
                    Description = "API for SimpleMarket BNPL MicroService",
                    Contact = new OpenApiContact
                    {
                        Name = "SimpleMarket Inc",
                        Email = "devmail@simplmarket.app"
                    },
                });

                //Set the comment path for the Swagger JSON and UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });



            services.AddCors()
                .AddScoped<IMigrationManager, MigrationManager>();

            services.AddMvc(c =>
            {
                c.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // JsonNamingPolicy.CamelCase;
            });

            services.ConfigureStartupConfig<HostingConfig>(Configuration.GetSection("Hosting"));

            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = false,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});



            //add distributed memory cache
            services.AddDistributedMemoryCache();
            (_engine, _defaultConfig) = services.ConfigureAppServices(Configuration, _webHostEnvironment);
        }




        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();

        //    services.AddCors();

        //    services.AddMvc(c =>
        //    {
        //        c.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
        //    }).AddJsonOptions(options =>
        //    {
        //        options.JsonSerializerOptions.PropertyNamingPolicy = null; // JsonNamingPolicy.CamelCase;
        //    });

        //    services.ConfigureStartupConfig<HostingConfig>(Configuration.GetSection("Hosting"));


        //    //add distributed memory cache
        //    services.AddDistributedMemoryCache();
        //    (_engine, _defaultConfig) = services.ConfigureAppServices(Configuration, _webHostEnvironment);
        //}

        public void ConfigureContainer(ContainerBuilder builder)
        {
            _engine.RegisterDependencies(builder, _defaultConfig);
        }

        // ApiExplorerGroupPerVersionConvention.cs
        public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
        {
            public void Apply(ControllerModel controller)
            {
                var controllerNamespace = controller.ControllerType.Namespace; // e.g. "Controllers.V1"
                var apiVersion = controllerNamespace.Split('.').Last().ToLower();

                controller.ApiExplorer.GroupName = apiVersion;
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _webHostEnvironment = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleMarket BNPL API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "SimpleMarket BNPL API v2");
            });

            app.UseRouting();
            //TODO: Whitelist Domain
            app.UseCors(option => option
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.ConfigureRequestPipeline();

            app.StartEngine();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseHttpsRedirection();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });

        //    app.ConfigureRequestPipeline();

        //    app.StartEngine();
        //}
    }
}
