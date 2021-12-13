using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskM = EFCoreConfiguration.Models.Task;
namespace Diploma.CQRS.Task
{
    public class AddTaskHandler : IRequestHandler<AddTaskRequest, ResultView>
    {
        private readonly TaskRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly LessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        public AddTaskHandler(
            TaskRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IMapper mapper,
            LessonRepository lessonRepository)
        {
            _repository= repository;
            _userManager= userManager;
            _accessor= accessor;
            _mapper= mapper;
            _lessonRepository = lessonRepository;
        }
        public async Task<ResultView> Handle(AddTaskRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                var lesson = await _lessonRepository.GetLessonAsync(request.LessonId, user);
                if (lesson != null)
                {
                    _repository.AddTask(_mapper.Map<TaskM>(request));
                    return new ResultView();
                }
                return new ResultView() { Error = new(400,"Localize later")};

            }catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
