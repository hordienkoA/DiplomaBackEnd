using Diploma.JWT;
using EFCoreConfiguration.Repositories;

namespace Diploma.DependencyInjection
{
    internal static class ServicesExtensions
    {
        public static void AddServicesCustom(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddHttpContextAccessor();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<SubjectRepository>();
            services.AddScoped<LessonRepository>();
        }
    }
}
