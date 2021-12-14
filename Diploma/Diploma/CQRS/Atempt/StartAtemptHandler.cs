using AutoMapper;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Atempt
{
    public class StartAtemptHandler : IRequestHandler<StartAtemptRequest, ResultView>
    {
        private readonly TaskInfoRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly TaskRepository _taskRepository;
        public StartAtemptHandler(
            TaskInfoRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IMapper mapper,
            TaskRepository taskRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<ResultView> Handle(StartAtemptRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(user);
                var taskInfos = await _repository.GetTaskInfo(request.LessonId, request.UserName);
                if (taskInfos.Count == 0 && !roles.Contains("Teacher"))
                {
                    var tasks = await _taskRepository.GetTasksAsync(null, lessonId: request.LessonId, user: user);
                    foreach (var task in tasks)
                    {
                        _repository.AddTaskInfo(new()
                        {
                            ValidTill = DateTime.Now.AddMinutes(30),
                            TaskId = task.Id,
                        });
                    }
                    taskInfos = await _repository.GetTaskInfo(request.LessonId, request.UserName);
                }

                var result = taskInfos.Select(el => new TasksInfoView
                {
                    TaskId = el.TaskId,
                    TaskInfoId = el.Id,
                    Question = el.Task.Question,
                    Type = el.Task.Type,
                    Answer = el.Answer,
                    ValidTill = el.ValidTill,
                    Comments = el.Comments.Select(c=>new MessageView {Id=c.Id, Message=c.Message, Sender=c.Sender.UserName }).ToList(),
                }).ToList();

                return new()
                {
                    Views = new List<IView>(result)
                };

            }
            catch (Exception ex)
            {
                return new ResultView() { Error = new(400, ex.Message) };
            }
        }
    }
}
