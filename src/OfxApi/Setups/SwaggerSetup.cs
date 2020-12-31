using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;
using System.Reflection;

namespace OfxApi.Setups
{
    public static class SwaggerSetup
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            #region Assembly Info
            services.AddControllers();

            var assembly = typeof(Program).Assembly;
            var assemblyInfo = assembly.GetName();

            var descriptionAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                 .OfType<AssemblyDescriptionAttribute>()
                 .FirstOrDefault();
            var productAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
                 .OfType<AssemblyProductAttribute>()
                 .FirstOrDefault();
            var copyrightAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
                 .OfType<AssemblyCopyrightAttribute>()
                 .FirstOrDefault();
            #endregion

            var openApiInfo = new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = productAttribute.Product,
                Version = assemblyInfo.Version.ToString(),
                Description = descriptionAttribute.Description,
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = copyrightAttribute.Copyright,
                    Url = new Uri(@"https://github.com/willimar/DevelopersChallenge2"),
                    Email = "willimar@gmail.com",
                },
                TermsOfService = null,
                License = new Microsoft.OpenApi.Models.OpenApiLicense()
                {
                    Name = "GNU GENERAL PUBLIC LICENSE",
                    Url = new Uri(@"https://github.com/willimar/DevelopersChallenge2/blob/master/LICENSE")
                }
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{Program.API_V_1_0}", openApiInfo);
                c.EnableAnnotations();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{Program.API_V_2_0}", openApiInfo);
                c.EnableAnnotations();
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            #region Assembly Info
            var assembly = typeof(Program).Assembly;
            var productAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
                 .OfType<AssemblyProductAttribute>()
                 .FirstOrDefault();
            var assemblyInfo = assembly.GetName();
            #endregion

            var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    $"{productAttribute.Product} - {description.GroupName}");
                }

                options.DocExpansion(DocExpansion.List);
            });
        }
    }
}
