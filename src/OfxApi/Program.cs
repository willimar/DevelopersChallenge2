using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OfxApi
{
    public class Program
    {
        public const string API_V_1_0 = "1";
        public const string API_V_2_0 = "2";
        public const string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
