using Diploma.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Diploma.DependencyInjection
{
    internal static class ServicesExtensions
    {
        public static void AddServicesCustom(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }
    }
}
