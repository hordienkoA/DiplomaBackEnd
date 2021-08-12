using Diploma.Models;
using MediatR;

namespace Diploma.CQRS.Login
{
    public class LoginQuery: IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
