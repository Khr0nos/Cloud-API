using System.IO;
using Cloud_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using Swashbuckle.Swagger.Model;

namespace Cloud_API {
    public class Startup {
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
        public void ConfigureServices(IServiceCollection services) {
            // Add framework services.
            //services.AddRouting();
            services.AddDbContext<DatabaseContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("Azure")));
            services.AddMvc();

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(opt => {
                opt.SingleApiVersion(new Info {
                    Version = "v1",
                    Title = "IoT Cloud API",
                    Description = "ASP.NET Core Web service using a REST API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Xavi Garcia", Email = "", Url = "" },
                    License = new License { Name = "" }
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Cloud-API.xml");
                opt.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddNLog();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            env.ConfigureNLog("nlog.config");

            app.UseStaticFiles();   
            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
