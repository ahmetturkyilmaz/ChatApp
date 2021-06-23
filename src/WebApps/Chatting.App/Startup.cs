using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Chatting.App
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
            services.AddRazorPages();
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

            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                WriteRequestParam(context);
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    Console.WriteLine("WebSocket Connected");
                }
                else
                {
                    Console.WriteLine("Hello from the 2nd request delegate");
                    await next();
                }
            });
            app.Run(async context =>
            {
                Console.WriteLine("Hello from the 3rd request delegate");
                await context.Response.WriteAsync("Hello from the 3rd request delegate");
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }

        public void WriteRequestParam(HttpContext context)
        {
            Console.WriteLine("Request Method" + context.Request.Method);
            Console.WriteLine("Request Protocol: " + context.Request.Protocol);
            if (context.Request.Headers != null)
            {
                foreach (var h in context.Request.Headers)
                {
                    Console.WriteLine("-->" + h.Key + ":" + h.Value);
                }
            }
        }
    }
}