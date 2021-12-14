using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Diploma.CQRS.AdminManagement
{
    public class AssignRoleQuery: IRequest<IdentityResult>
    {
        [Required(ErrorMessage = "AssignRole_UserName_Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "AssignRole_Roles_Required")]
        public List<string> Roles { get; set; }
    }
}
