using Diploma.JWT;
using EFCoreConfiguration.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Diploma.DependencyInjection
{
    internal static class ServicesExtensions
    {
        public static void AddServicesCustom(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<SubjectRepository>();
        }
    }
}
