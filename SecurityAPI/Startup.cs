using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationServer;
using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;
using DatabaseAccessor.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UsersAPI.Models;

namespace SecurityAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //  public IConfiguration Configuration { get; }
        private IConfiguration Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
        private IConfiguration Configuration1 = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                    .AddRazorViewEngine()
                    .AddAuthorization()
                    .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:5000";
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "Users";
                    });
            services.AddSingleton(new Repo<UserInformation>(
                new MapInfo(this.Configuration["Mappers:Users"]),
                new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));
            services.AddSingleton(new Repo<AdminPublicInfo>(
              new MapInfo(this.Configuration["Mappers:Users"]),
              new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));
            services.AddSingleton(new Repo<AdminInfo>(
             new MapInfo(this.Configuration["Mappers:Users"]),
             new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));
            services.AddSingleton(new Repo<SellerPublicInfo>(
             new MapInfo(this.Configuration["Mappers:Users"]),
             new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));
            services.AddSingleton(new Repo<SellerInfo>(
             new MapInfo(this.Configuration["Mappers:Users"]),
             new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));
            services.AddSingleton(new Repo<CustomerInfo>(
              new MapInfo(this.Configuration["Mappers:Users"]),
             new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));

            services.AddSingleton(new Repo<CustomerPublicInfo>(
       new MapInfo(this.Configuration["Mappers:Users"]),
       new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));

            services.AddSingleton(new Repo<UserPublicInfo>(
                new MapInfo(this.Configuration["Mappers:Users"]),
                new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));

            services.AddSingleton(new Repo<UserInformation>(
             new MapInfo(this.Configuration["Mappers:Users"]),
             new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
