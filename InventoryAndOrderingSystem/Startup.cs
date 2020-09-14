using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Repositories.CustomerRepositories;
using InventoryAndOrderingSystem.Repositories.LoginRepositories;
using InventoryAndOrderingSystem.Repositories.OrderRepositories;
using InventoryAndOrderingSystem.Repositories.ProductRepositories;
using InventoryAndOrderingSystem.Services.CustomerServices;
using InventoryAndOrderingSystem.Services.LoginServices;
using InventoryAndOrderingSystem.Services.OrderServices;
using InventoryAndOrderingSystem.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OSMS.CustomHandler;

namespace InventoryAndOrderingSystem
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();  //razor runtime compilatio to render html changes on page refresh, need nuget package install
            services.AddRazorPages();

            services.AddDbContext<IOSContext>(options =>
                                              options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddAuthentication("CookieAuthentication")
                    .AddCookie("CookieAuthentication", config =>
                    {
                        config.Cookie.Name = "UserLoginCookie"; // Name of cookie   
                        config.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
                        config.LoginPath = "/Login/Index"; // Path for the redirect to user login page  
                        config.AccessDeniedPath = "/Login/Index";
                    });

            services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

            services.AddScoped<LoginService>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();
            services.AddScoped<CustomerService>();

            services.AddScoped<LoginRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<CustomerRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
