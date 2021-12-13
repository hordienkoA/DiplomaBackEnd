using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Subjects.ResignUsers
{
    public class ResignUsersHandler: IRequestHandler<ResignUsersRequest, ResultView>
    {
        private readonly GroupRepository _groupRepository;
        private readonly UserManager<User> _manager;
        private readonly SubjectRepository _repository;

        public ResignUsersHandler(
            GroupRepository groupRepository,
            UserManager<User> manager,
            SubjectRepository repository)
        {
            _groupRepository = groupRepository;
            _manager = manager;
            _repository = repository;
        }

        public async Task<ResultView> Handle(ResignUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _manager.Users.Where(el => request.UserNames.Any(s => s.Equals(el.UserName))).ToList();
                var groups = await _groupRepository.GetGroupsAsync(groupIds: request.GroupIds);
                await _repository.ResignUsers(request.SubjectId, users, groups);
                return new ResultView();
            }
            catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
