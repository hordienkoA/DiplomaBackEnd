using LocaleData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
            }).AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(Messages));
                })
                .AddNewtonsoftJson();
        }
    }
}
