using Diploma.Views;
using EFCoreConfiguration.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diploma.CQRS.AdminManagement
{
    public class GetUsersHandler: IRequestHandler<GetUsersQuery, List<UserView>>
    {
        private readonly UserManager<User> _userManager;

        public GetUsersHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<UserView>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Select(el => new UserView()
            {
                Email = el.Email,
                UserName = el.UserName,
                FirstName = el.FirstName,
                SecondName = el.SecondName,
                Age = el.Age
            }).ToListAsync(cancellationToken);
            return users;
        }
    }
}
