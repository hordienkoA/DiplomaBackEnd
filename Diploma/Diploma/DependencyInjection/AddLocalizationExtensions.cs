using Microsoft.Extensions.DependencyInjection;

namespace Diploma.DependencyInjection
{
    public static class AddLocalizationExtensions
    {
        public static void AddLocalizationCustom(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
        }
    }
}
