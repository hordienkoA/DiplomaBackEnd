using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diploma.Exceptions;
using EFCoreConfiguration.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.AdminManagement
{
    public class AssignRoleHandler: IRequestHandler<AssignRoleQuery, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AssignRoleHandler(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(AssignRoleQuery request, CancellationToken cancellationToken)
        {
            var wrongRoles = request.Roles.Where(el => !_roleManager.Roles.Any(r => r.Name == el));
                if (wrongRoles.Any())
                {
                    throw new BusinessException($"These roles don`t exist: {String.Join(',', wrongRoles)}");
                }
                var user = await _userManager.FindByNameAsync(request.UserName);
                var result = await _userManager.AddToRolesAsync(user, request.Roles);
                return result;
            }
            
        }
    }

