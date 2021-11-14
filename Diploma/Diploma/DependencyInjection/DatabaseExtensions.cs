using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Diploma.DependencyInjection
{
    internal static class DatabaseExtensions
    {
        public static void AddSqlServerCustom(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void ApplyMigrations(this IApplicationBuilder app, ApplicationContext context)
        {
            context.Database.Migrate();
        }
    }
}
