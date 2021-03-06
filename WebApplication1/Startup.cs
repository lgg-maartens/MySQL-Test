using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Database;

namespace WebApplication1
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
      // Replace with your connection string.      
      string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=fastfood;Uid=lgg;Pwd=;";

      // Replace with your server version and type.
      // Use 'MariaDbServerVersion' for MariaDB.
      // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
      // For common usages, see pull request #1233.
      var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

      // Replace 'YourDbContext' with the name of your own DbContext derived class.
      services.AddDbContext<FastFoodContext>(
          dbContextOptions => dbContextOptions
              .UseMySql(connectionString, serverVersion)
              .EnableSensitiveDataLogging() // <-- These two calls are optional but help
              .EnableDetailedErrors()       // <-- with debugging (remove for production).
      );

      services.AddControllersWithViews();
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
        app.UseExceptionHandler("/Home/Error");
      }
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
