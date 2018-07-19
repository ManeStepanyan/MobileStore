using System.IO;
using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;
using DatabaseAccessor.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                        options.ApiName = "UserAPI";
                    });
            // adding policies
            services.AddAuthorization(options => options.AddPolicy("Admin", policy => policy.RequireClaim("role", "1")));
            services.AddAuthorization(options => options.AddPolicy("Seller", policy => policy.RequireClaim("role", "2")));
            services.AddAuthorization(options => options.AddPolicy("Customer", policy => policy.RequireClaim("role", "3")));
            services.AddAuthorization(options => options.AddPolicy("Admin, Seller", policy => policy.RequireClaim("role", "1","2")));
            services.AddAuthorization(options => options.AddPolicy("Admin, Customer",policy=>policy.RequireClaim("role", "1", "3")));

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
