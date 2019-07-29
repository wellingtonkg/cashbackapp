using CashBackApp.Api;
using CashBackApp.Api.Core;
using CashBackApp.Domain.Exceptions;
using CashBackApp.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.Globalization;
using System.Linq;
using System.Net;

namespace CashBackApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opts =>
                {
                    //Camel Case to JSON
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            //Register Dependency Injection
            DependencyInjection.Startup.Start(services);
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, DatabaseDefault>();

            // Register Swagger  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "E-commerce Discos", Version = "v1", Description = "Cashback E-commerce Discos" });
            });

            //services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Define the brazilian culture: pt-BR
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.  
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),  
            // specifying the Swagger JSON endpoint.  
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseExceptionHandler(
                          builder =>
                          {
                              builder.Run(
                                async context =>
                                {
                                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                                    var error = context.Features.Get<IExceptionHandlerFeature>();
                                    if (error != null)
                                    {
                                        var type = error.Error.GetType();
                                        var messages = string.Join(" #-# ", error.Error.GetInnerExceptions().Select(x => x.Message));

                                        switch (type.Name)
                                        {
                                            case nameof(UserException):
                                                {
                                                    context.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                                                    context.Response.ContentType = "application/json";
                                                    await context.Response.WriteAsync(messages).ConfigureAwait(false);
                                                    break;
                                                }
                                            default:
                                                {
                                                    context.Response.AddApplicationError(messages);
                                                    if (env.IsDevelopment())
                                                    {
                                                        var errorMsg = new { Message = messages, StackTrace = error.Error.StackTrace };
                                                        context.Response.ContentType = "application/json";
                                                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorMsg)).ConfigureAwait(false);
                                                    }
                                                    else
                                                        await context.Response.WriteAsync(messages).ConfigureAwait(false);

                                                    break;
                                                }
                                        }
                                    }
                                });
                          });

            //app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
