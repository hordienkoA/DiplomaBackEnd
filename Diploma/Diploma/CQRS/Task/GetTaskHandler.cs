using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
    public class GetTaskHandler : IRequestHandler<GetTaskRequest, ResultView>
    {
        private readonly TaskRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly GroupRepository _groupRepository;
        private readonly IUserAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Messages> _localization;


        public GetTaskHandler(
            TaskRepository repository,
            UserManager<User> userManager,
            GroupRepository groupRepository,
            IUserAccessor accessor,
            IMapper mapper,
            IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _userManager = userManager;
            _groupRepository = groupRepository;
            _accessor = accessor;
            _mapper = mapper;
            _localization = localization;
        }
        public async Task<ResultView> Handle(GetTaskRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var groups = await _groupRepository.GetGroupsAsync(user: user);
            var tasks = await _repository.GetTasksAsync(request.Id, request.LessonId, user, null);
            if (!tasks.Any())
            {
                return new()
                {
                    Error = new(404, _localization["GetLessons_NotFound"])
                };
            }

            return new()
            {
                Views = new List<IView>(tasks.Select(el => _mapper.Map<TaskView>(el)))
            };
        }
        }
    }