using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfxApi.Setups
{
    public static class VersionSetup 
    {
        public static void ConfigureVersions(this IServiceCollection services)
        {
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = false;
                x.ReportApiVersions = true;
            });
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(2, 0);
                x.AssumeDefaultVersionWhenUnspecified = false;
                x.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = $"'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            //services.AddTransient<Controllers.v1.SaveDataController>();
            //services.AddTransient<Controllers.v2.SaveDataController>();
        }
    }
}
