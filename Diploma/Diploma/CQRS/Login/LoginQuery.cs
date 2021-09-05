using MediatR;

namespace Diploma.CQRS.Login
{
    public class LoginQuery: IRequest<string>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
