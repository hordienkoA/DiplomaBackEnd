using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Diploma.DependencyInjection
{
    internal static class AddAuthorizationExtensions
    {
        public static void AddAuthorizationCustom(this IServiceCollection services)
        {
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("roleassign", policy =>
            //    {
            //        policy.RequireClaim(ClaimTypes.Role, "Administrator");
            //    });
            //});
        }
    }
}
