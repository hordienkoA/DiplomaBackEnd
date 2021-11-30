using System.Net.Mail;
using Diploma.JWT;
using Diploma.Views;
using EFCoreConfiguration.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Login
{
    public class LoginHandler: IRequestHandler<LoginQuery, LoginDetails>
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

        public async Task<LoginDetails> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var emailChecker =  MailAddress.TryCreate(request.UserNameOrEmail, out var temp);
            var user = emailChecker 
                ? await _userManager.FindByEmailAsync(request.UserNameOrEmail) 
                : await _userManager.FindByNameAsync(request.UserNameOrEmail) ;
            var result = await _signInManager.CheckPasswordSignInAsync(user ?? new User(), request.Password, false);

            if (result.Succeeded)
            {
                return new LoginDetails() {
                    Token = await _generator.CreateToken(user),
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                };
            }

            return null;
        }
    }
}
