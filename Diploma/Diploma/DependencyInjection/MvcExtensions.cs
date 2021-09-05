using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Diploma.DependencyInjection
{
    internal static class MvcExtensions
    {
        public static void AddMvcCustom(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                //options.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }
    }
}
