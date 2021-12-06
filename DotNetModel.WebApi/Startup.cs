using DotNetModel.Business;
using DotNetModel.Business.Impl;
using DotNetModel.DataRepository;
using DotNetModel.DataRepository.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SimpleInjector;
using System;

namespace DotNetModel.WebApi
{
    public class Startup
    {
        private SimpleInjector.Container container;
        public Startup(IConfiguration configuration)
        {
            container = new SimpleInjector.Container();
            container.Options.ResolveUnregisteredConcreteTypes = false;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Modelo de Documento API",
                Description = "Modelo de documento para avaliação API",
                TermsOfService =new Uri("https://www.linkedin.com/in/igor-rodrigues-35252a56/"),
                Contact = new OpenApiContact
                {
                    Name ="Igor V. Rodrigues",
                    Email ="igor.ti.1983@gmail.com",
                    Url =new Uri("https://www.linkedin.com/in/igor-rodrigues-35252a56/")
                },
                License = new OpenApiLicense
                {
                    Name ="GPL",
                    Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html")
                }
            }));

            services.AddSimpleInjector(container, options => {
                options.AddAspNetCore().AddControllerActivation();

                options.AddLogging();
                //options.AddLocalization();
            });

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            Configuration.GetSection("ConnectionStrings:DBConecction");

            container.Register<IApplicationDao, ApplicationDao>(Lifestyle.Singleton);
            container.Register<IApplicationBusiness, ApplicationBusiness>(Lifestyle.Singleton);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // UseSimpleInjector() finalizes the integration process.
            app.ApplicationServices.UseSimpleInjector(container);

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Modelo de Documentação"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(next => async context => {
                if ("/".Equals(context.Request.Path))
                {
                    context.Response.Redirect("/swagger");
                }
                else
                {
                    await next(context);
                }
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            container.Verify();
        }
    }
}
