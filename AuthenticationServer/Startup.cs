using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using AuthenticationServer.Services;
using AuthenticationServer.Validators;
using DatabaseAccess;
using DatabaseAccess.SpExecuters;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccessor.Repository;
using Microsoft.Extensions.Configuration;
using System.IO;
using AuthenticationServer.UsersRepository;

namespace AuthenticationServer
{
    public class Startup
    {
        private IConfiguration Configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json").Build();
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMvc();

            services.AddIdentityServer().AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddProfileService<ProfileService>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();

            // adding singletons
            
            services.AddSingleton(new Repo<User>(
                new MapInfo(this.Configuration["Mappers:Users"]),
                new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));

         //   services.AddAuthorization(options => options.AddPolicy("Admin", policy => policy.RequireClaim("role_id", "1")));
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
