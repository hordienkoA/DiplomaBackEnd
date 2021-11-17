using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using LocaleData;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Diploma.CQRS.Task
{
    public class RemoveTaskHandler : IRequestHandler<RemoveTaskRequest, ResultView>
    {
        private readonly TaskRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IStringLocalizer<Messages> _localization;

        public RemoveTaskHandler(
            TaskRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _localization = localization;
        }
        public async Task<ResultView> Handle(RemoveTaskRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var tasks = await _repository.GetTasksAsync(request.TaskId, null, user);
            if(tasks.Count == 0)
            {
                return new() { Error = new(403, _localization["RemoveTask_AccessError"]) };
            }

            _repository.DeleteTask(tasks.FirstOrDefault());
            return new ResultView();
        }
    }
}
