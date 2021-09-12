using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.AdminManagement
{
    public class AssignRoleQuery: IRequest<IdentityResult>
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
