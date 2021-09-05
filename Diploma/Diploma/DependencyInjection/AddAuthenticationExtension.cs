using System;
using System.Text;
using Diploma.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Diploma.DependencyInjection
{
    internal static class AddAuthenticationExtension
    {
        public static void AddAuthenticationCustom(this IServiceCollection services, IConfiguration configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("diploma_secret"));
            services.AddAuthentication(options=>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(
                    options =>
                    {
                        AuthOptions.ISSUER = configuration.GetSection("JWTConf:ISSUER").Value;
                        AuthOptions.AUDIENCE = configuration.GetSection("JWTConf:AUDIENCE").Value;
                        AuthOptions.KEY = configuration.GetSection("JWTConf:KEY").Value;
                        AuthOptions.Lifetime = Int32.Parse(configuration.GetSection("JWTConf:Lifetime").Value);

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,

                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,

                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetySymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
        }
    }
}
