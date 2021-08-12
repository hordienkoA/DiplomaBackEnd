using Diploma.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


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
    }
}
