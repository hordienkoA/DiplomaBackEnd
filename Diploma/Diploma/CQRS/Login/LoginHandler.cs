using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Diploma.JWT;
using Diploma.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Login
{
    public class LoginHandler: IRequestHandler<LoginQuery,string>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public readonly IJwtGenerator _generator;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager,IJwtGenerator generator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _generator = generator;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user ?? new User(), request.Password, false);

            if (result.Succeeded)
            {
                return _generator.CreateToken(user);
            }

            return null;
        }
    }
}
