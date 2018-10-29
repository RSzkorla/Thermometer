using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Thermometer.BLL;
using Thermometer.Hubs;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Thermometer
{
  public class Startup
  {
    public static IHostingEnvironment Environment;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddSignalR();
      services.AddSingleton<IHostedService, MonitorService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      Environment = env;
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
      else
        app.UseExceptionHandler("/Home/Error");

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          "default",
          "{controller=Home}/{action=Index}/{id?}");
      });

      app.UseSignalR(routes => { routes.MapHub<UpdateHub>("/hubs/update"); });
      
    }
  }
}