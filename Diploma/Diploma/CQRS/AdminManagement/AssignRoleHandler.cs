using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diploma.Exceptions;
using EFCoreConfiguration.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using LocaleData;

namespace Diploma.CQRS.AdminManagement
{
    public class AssignRoleHandler: IRequestHandler<AssignRoleQuery, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<Messages> _localization;
        public AssignRoleHandler(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IStringLocalizer<Messages> localization)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _localization = localization;
        }

        public async Task<IdentityResult> Handle(AssignRoleQuery request, CancellationToken cancellationToken)
        {
            var wrongRoles = request.Roles.Where(el => !_roleManager.Roles.Any(r => r.Name == el));
                if (wrongRoles.Any())
                {
                    throw new BusinessException($"{_localization["AssignRole_WrongRoles"]}: {String.Join(',', wrongRoles)}");
                }
                var user = await _userManager.FindByNameAsync(request.UserName);
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                var result = await _userManager.AddToRolesAsync(user, request.Roles);
                return result;
            }
            
        }
    }

