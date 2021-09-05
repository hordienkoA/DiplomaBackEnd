﻿using Microsoft.AspNetCore.Builder;

namespace Diploma.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorsCustom(this IApplicationBuilder app)
        {
            //because of localhost 
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
    }
}
