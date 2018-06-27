using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestLogger.Helpers;

namespace RequestLogger
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<IConfigurationRoot>(Configuration);
            //services.AddSingleton<IMyService, MyService>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var sb = new StringBuilder();
                foreach (var header in context.Request.Headers)
                {
                    sb.AppendLine($"{header.Key}: {header.Value}");
                }
               
                //await context.Response.WriteAsync(sb.ToString());
                
                foreach (var header in context.Request.Headers)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{header.Key}: ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{header.Value}" + Environment.NewLine);
                    Console.ResetColor();
                }
                Console.WriteLine();

                var body = await RequestHelpers.GetBodyContentAsStringAsync(context.Request);
                if (!string.IsNullOrEmpty(body))
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine(body);
                    Console.ResetColor();
                    Console.WriteLine();
                    sb.AppendLine("=== BODY ===");
                    sb.AppendLine(body);
                }


                await context.Response.WriteAsync(sb.ToString());
            });
        }
    }
}