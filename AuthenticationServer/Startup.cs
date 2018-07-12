using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using AuthenticationServer.Services;
using AuthenticationServer.Validators;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationServer
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddMvc();
            var policy1 = new AuthorizationPolicyBuilder()
  .AddAuthenticationSchemes("Cookie, Bearer")
  .RequireAuthenticatedUser()
  .RequireRole("")
  .RequireAssertion(ctx =>
  {
      return ctx.User.HasClaim("Role", "2");
             
  })
  .Build();
            services.AddIdentityServer().AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddProfileService<ProfileService>();
            services.AddAuthorization(options =>
    {
        options.AddPolicy("ContentsEditor", policy =>
        {
            policy.AddAuthenticationSchemes("Cookie, Bearer");
            policy.RequireAuthenticatedUser();
            policy.RequireRole("Seller");
            policy.RequireClaim("Role", "2");
        });
    });

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
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
