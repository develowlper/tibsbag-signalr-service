using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace tibsbag_signalr_service
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configure)
    {
        Configuration = configure;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSignalR()
            .AddAzureSignalR(Configuration["Azure:SignalR:ConnectionString"]); //Azure:SignalR:ConnectionString
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseAzureSignalR(routes =>
      {
        routes.MapHub<Chat>("/chat");
      });

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseFileServer();
    }
  }
}
