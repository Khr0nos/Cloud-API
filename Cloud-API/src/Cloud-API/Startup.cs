using System.IO;
using CloudAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.Swagger.Model;

namespace CloudAPI {
    /// <summary>
    /// Startup class. Provides startup configuration for the API
    /// </summary>
    public class Startup {
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="env">Provides information about the web hosting environment</param>
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Method called by the runtime on startup. Used for configuration of framework services including database connections, formatters, among others 
        /// </summary>
        /// <param name="services">Collection provided for defining framework services</param>
        public void ConfigureServices(IServiceCollection services) {
            // Add framework services.
            services.AddDbContext<DatabaseContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("Azure")));

            //services.Configure<MvcOptions>(opt => opt.Filters.Add(new RequireHttpsAttribute()));
            services.AddMvc()
                .AddXmlSerializerFormatters();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(opt => {
                opt.SingleApiVersion(new Info {
                    Version = "v1",
                    Title = "IoT Cloud API",
                    Description = "ASP.NET Core Web service using a REST API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Xavi Garcia", Email = "xgarcia@mcr.es", Url = "" },
                    License = new License { Name = "" }
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Cloud-API.xml");
                opt.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Method called by the runtime on startup. Used for configuring the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Provides mechanisms to configure the pipeline</param>
        /// <param name="env">Provides information about the web hosting environment</param>
        /// <param name="loggerFactory">Provides configuration for the logging system of the API</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddNLog();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            var options = new JwtBearerOptions {
                Audience = Configuration["Auth0:ApiIdentifier"],
                Authority = $"https://{Configuration["Auth0:Domain"]}/"
            };
            app.UseJwtBearerAuthentication(options);
            
            env.ConfigureNLog("nlog.config");
            app.AddNLogWeb();

            app.UseStaticFiles();   
            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUi("api/help");
        }
    }
}
