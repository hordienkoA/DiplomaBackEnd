using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Diploma.DependencyInjection
{
    internal static class IdentityExtensions
    {
        public static void AddIdentityCustom(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
        }
    }
}
