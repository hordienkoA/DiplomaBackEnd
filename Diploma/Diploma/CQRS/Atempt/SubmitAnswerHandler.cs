using System;
using System.Threading;
using System.Threading.Tasks;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Atempt
{
    public class SubmitAnswerHandler : IRequestHandler<SubmitAnswerRequest, ResultView>
    {
        private readonly TaskInfoRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;

        public SubmitAnswerHandler(
            TaskInfoRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
        }
        public async Task<ResultView> Handle(SubmitAnswerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(user);
                var taskInfo = await _repository.FindAsync(request.TaskInfoId);
                taskInfo.Answer = request.Answer;
                _repository.EditTaskInfo(taskInfo);
                return new ResultView();
            }
            catch (Exception ex)
            {
                return new ResultView() { Error = new(400, ex.Message) };
            }
        }
    }
}
