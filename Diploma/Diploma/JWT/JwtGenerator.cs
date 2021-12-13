using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EFCoreConfiguration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Diploma.JWT
{
    public class JwtGenerator:IJwtGenerator
    {
        private readonly UserManager<User> _manager;
        public JwtGenerator(UserManager<User> manager)
        {
            _manager = manager;
        }
        public async Task<string> CreateToken(User user)
        {
            var roles = (await _manager.GetRolesAsync(user)).Select(el=>new Claim(ClaimsIdentity.DefaultRoleClaimType, el));
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            };

            claims.AddRange(roles);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetySymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
