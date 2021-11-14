using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Register
{
    public class RegisterQuery: IRequest<IdentityResult>
    {
        [Required(ErrorMessage = "Register_FirstName_Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Register_SecondName_Required")]
        public string SecondName { get; set; }
        [Range(0,120, ErrorMessage = "Register_Age_Range")]
        public int Age { get; set; }
        [EmailAddress(ErrorMessage = "Register_Email_Validation")]
        [Required(ErrorMessage = "Register_Email_Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Register_UserName_Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Register_Password_Required")]
        public string Password { get; set; }
    }
}
