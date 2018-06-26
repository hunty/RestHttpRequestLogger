using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace RequestLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Logging server ***");
            // WebHostBuilder wb = new WebHostBuilder().UseStartup<Startup>()
            CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
