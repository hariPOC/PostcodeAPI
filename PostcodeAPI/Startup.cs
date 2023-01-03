using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Postcode.Common.ErrorHandling;
using Postcode.Common.Logging;

namespace PostcodeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get;  set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc(x => x.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddHttpContextAccessor();
            //
            services.AddOptions<LoggerOption>().Bind
            (Configuration.GetSection("Logger")).ValidateDataAnnotations();
            /*IOC*/
            services.AddSingleton<Postcode.Common.Logging.ILogger, Logger>();
            services.AddScoped<ILogModelCreator,
                               LogModelCreator>();
            services.AddSwaggerGen(
                c=>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Postcode API",
                        Description = "This is a wrapper API service to get UK Postcode Details.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Hari Kiran Thatha",
                            Email = "hari.thatha@mastek.com",
                            Url = new Uri("https://github.com/hariPOC/PostcodeAPI.git")
                        }

                    }); ;
                }
                );

        }

        /// <summary>
        /// Method to register the assembly types to create singleton instances using Autofac nuget.
        /// </summary>
        /// <param name="builder">Autofac Container Builder.</param>
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            var assembly = Assembly.Load("Postcode").GetTypes()
                .Where(t => t.Name.EndsWith("Service", StringComparison.InvariantCulture))

                .Select(t => t.Name);
          

            builder.RegisterAssemblyTypes(Assembly.Load("Postcode"))
                .Where(t => t.Name.EndsWith("Service", StringComparison.InvariantCulture))
                .AsImplementedInterfaces().PropertiesAutowired();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

               
            }
            app.UseMiddleware<GlobalErrorHandler>();
            app.UseMiddleware<LoggingMiddleware>();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();

            });
            app.UseMvc();

        }
    }
}

