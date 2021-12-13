using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Diploma.DependencyInjection
{
    public static class ConfigureLocalizationExtension
    {
        public static void ConfigureLocalizationCustom(this IApplicationBuilder app)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new("uk"),
                new("en")
            };
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("uk"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };

            app.UseRequestLocalization(options);
        }
    }
}
