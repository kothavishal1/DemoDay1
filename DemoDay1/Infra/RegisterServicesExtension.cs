using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDay1.Infra
{
    public static class RegisterServicesExtension
    {
        public static void AddDataBaseService(this IServiceCollection services,IConfiguration Configuration)
        {
            //DB Connection
            services.AddDbContext<ContosoContext>(options =>
            {
                options.EnableDetailedErrors();
                options.UseSqlServer(Configuration.GetConnectionString("ContosoDBConnection"));
            });

            //Implementation Factory(Custom Factory)
            //services.AddSingleton(sp =>
            //{
            //    //Register Service
            //});

            //UnitofWork Injection
            //services.Add(new ServiceDescriptor(typeof(IUnitOfWork),typeof(UnitOfWork),ServiceLifetime.Scoped));
            //or
            //services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


        }
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Deloitte API",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Nauzad Kapadia",
                        Email = string.Empty,
                        Url = new Uri("http://www.quartzsystems.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows()
                //    {
                //        Implicit = new OpenApiOAuthFlow()
                //        {
                //            AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                //            TokenUrl = new Uri($"{identityUrl}/connect/token"),
                //            Scopes = new Dictionary<string, string>()
                //            {
                //                { "catalog.api", "Catalog API" }
                //            }
                //        }
                //    }
                //});
            });
        }
    }
}
