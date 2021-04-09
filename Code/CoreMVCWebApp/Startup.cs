using CoreMVCWebApp.Data;
using CoreMVCWebApp.Extensions;
using CoreMVCWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreMVCWebApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            //services.AddDirectoryBrowser();
            //services.AddDirectoryBrowser();

            //inject services;

            services.AddSingleton<IInjectService1, InjectService1>();
            services.AddScoped<IInjectService2, InjectService2>();
            services.AddTransient<IInjectService3, InjectService3>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseExceptionHandler("/Error");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //use folder broswer UseFileServer
            app.UseStaticFiles(new StaticFileOptions(){
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\images")),
                RequestPath=new Microsoft.AspNetCore.Http.PathString("/MyImages")
                });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/MyImages2")
            });


            loggerFactory.AddLog4Net();//log4net

            ////short circuiting
            //byte[] body = Encoding.Default.GetBytes("Hello! the is OK!");
            //app.Run(async context => {
            //    await context.Response.Body.WriteAsync(body,0,body.Length);
            //});

            //Mapwhen Useage
            ConfigureMapWhenHello(app);

            app.UseRequestIP();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        #region MapWhen methods 
        private static void HandleHelloBranch(IApplicationBuilder app)
        {
            //short circuiting
            byte[] body = Encoding.Default.GetBytes("Hello! this is an test MapWhen url!");
            app.Run(async context =>
            {
                await context.Response.Body.WriteAsync(body, 0, body.Length);
            });
        }

        public void ConfigureMapWhenHello(IApplicationBuilder app)
        {
            app.MapWhen(context => {
                return context.Request.Query.ContainsKey("hello"); 
            }, HandleHelloBranch);
        }
        #endregion
    }
}
