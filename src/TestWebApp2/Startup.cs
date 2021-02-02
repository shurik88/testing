using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TestWebApp2.Converters;
using TestWebApp2.DataAccess;
using TestWebApp2.DataAccess.Mongo;
using TestWebApp2.Filters;
using TestWebApp2.gServices;
using TestWebApp2.Infrastructure.Features;
using TestWebApp2.Interceptors;
using TestWebApp2.Model;
using TestWebApp2.Validators;

namespace TestWebApp2
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
            services.AddControllersWithViews()
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(typeof(ApiExceptionFilterAttribute));
                })
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                })
                .AddFluentValidation(fv => {
                    fv.LocalizationEnabled = false;
                    fv.RegisterValidatorsFromAssemblyContaining<AssignerDtoValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<EditTodoValidator>();
                });



            var mongoUrl = new MongoUrl(Configuration.GetValue<string>("mongo:connectionString"));
            var database = mongoUrl.DatabaseName;
            var mongoClient = new MongoClient(mongoUrl);

            services
                .AddSingleton(mongoClient)
                .AddSingleton(mongoClient.GetDatabase(database))
                .AddTransient<IQueryable<ToDo>, MongoQueryable<ToDo>>(x => new MongoQueryable<ToDo>(x.GetRequiredService<IMongoDatabase>(), "todos"))
                .AddTransient<IRepository<ToDo, Guid>, MongoRepository<ToDo, Guid>>(x => new MongoRepository<ToDo, Guid>(x.GetRequiredService<IMongoDatabase>(), "todos"));

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddFluentValidationRules();
            });

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<ExceptionInterceptor>();
            });
            services.AddGrpcReflection();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            //// Add after existing UseGrpcWeb
            app.Use((c, next) =>
            {
                if (c.Request.ContentType == "application/grpc")
                {
                    var current = c.Features.Get<IHttpResponseFeature>();
                    c.Features.Set<IHttpResponseFeature>(new HttpSysWorkaroundHttpResponseFeature(current));
                }
                return next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapGrpcService<ToDoExecutionService>();

                endpoints.MapGrpcReflectionService();

                //if (env.IsDevelopment())
                //{
                //    endpoints.MapGrpcReflectionService();
                //}
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
