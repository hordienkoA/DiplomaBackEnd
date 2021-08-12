using System.Threading;
using System.Threading.Tasks;
using Diploma.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Register
{
    public class RegisterHandler: IRequestHandler<RegisterQuery, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public RegisterHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> Handle(RegisterQuery request, CancellationToken cancellationToken)
        {
            var user = new User {Email = request.Email, UserName = request.UserName, Sex = request.Sex};
            var result = await _userManager.CreateAsync(user, request.Password);

            return result;
        }
    }
}
