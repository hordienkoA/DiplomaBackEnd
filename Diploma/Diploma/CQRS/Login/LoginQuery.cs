using System.ComponentModel.DataAnnotations;
using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Login
{
    public class LoginQuery: IRequest<LoginDetails>
    {
        [Required(ErrorMessage = "Login_UserNameOrEmail_Required")]
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Login_Password_Required")]
        public string Password { get; set; }
    }
}
