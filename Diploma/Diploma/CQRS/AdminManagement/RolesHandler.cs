using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diploma.CQRS.AdminManagement
{
    public class RolesHandler: IRequestHandler<RolesQuery, List<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        public async Task<List<string>> Handle(RolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.Select(el => el.Name).ToListAsync(cancellationToken);
        }
    }
}
