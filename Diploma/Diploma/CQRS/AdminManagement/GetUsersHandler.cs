using Diploma.Views;
using EFCoreConfiguration.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diploma.CQRS.AdminManagement
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, ResultView>
    {
        private readonly UserManager<User> _userManager;

        public GetUsersHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResultView> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .Where(g => g.GroupId == (request.GroupId ?? g.GroupId))
                .Select(el => new UserView()
                {
                    Email = el.Email,
                    UserName = el.UserName,
                    FirstName = el.FirstName,
                    SecondName = el.SecondName,
                    Age = el.Age,
                    GroupId = el.GroupId
                }).ToListAsync(cancellationToken);
            if (!users.Any())
            {
                return new() { Error = new(404, "Не знайдено користувачів") };
            }

            return new() { Views = new List<IView>(users)};
        }
    }
}
