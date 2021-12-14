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
    public class EditTaskHandler : IRequestHandler<EditTaskRequest, ResultView>
    {
        private readonly TaskRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly LessonRepository _lessonRepository;
        private readonly IMapper _mapper;


        public EditTaskHandler(
            TaskRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            LessonRepository lessonRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<ResultView> Handle(EditTaskRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                var lesson = await _lessonRepository.GetLessonAsync(request.LessonId, user);
                if (lesson != null)
                {
                    _repository.EditTask(_mapper.Map<TaskM>(request));
                    return new ResultView();
                }

                return new ResultView() { Error = new(400, "Localize later") };

            }
            catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
