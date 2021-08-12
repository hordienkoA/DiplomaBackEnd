using Diploma.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Register
{
    public class RegisterQuery: IRequest<IdentityResult>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public Sex Sex { get; set; }
        public string Password { get; set; }
    }
}
