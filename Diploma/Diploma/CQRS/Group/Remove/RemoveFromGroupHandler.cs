using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Group.Remove
{
    public class RemoveFromGroupHandler : IRequestHandler<RemoveFromGroupRequest, ResultView>
    {
        private readonly UserManager<User> _manager;
        private readonly GroupRepository _repository;

        public RemoveFromGroupHandler(
             UserManager<User> manager,
            GroupRepository repository)
        {
            _manager = manager;
            _repository = repository;
        }

        public async Task<ResultView> Handle(RemoveFromGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _manager.Users.Where(el => request.UserNames.Any(s => s.Equals(el.UserName))).ToList();
                await _repository.RemoveFromGroup(request.GroupId, users);
                return new();
            }
            catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
