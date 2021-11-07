using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Lessons
{
    public class RemoveLessonHandler: IRequestHandler<RemoveLessonRequest, ResultView>
    {
        private readonly LessonRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;

        public RemoveLessonHandler(
            LessonRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
        }
        public async Task<ResultView> Handle(RemoveLessonRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            var lessons = await _repository.GetLessonsAsync(request.LessonId, null, _accessor.User.Identity.Name, roles.ToList());
            if (lessons.Count == 0)
            {
                return new(){Error = new(403, "Ви не маєте доступу до даного завдання або його не існує в системі")};
            }

            _repository.DeleteLesson(lessons.FirstOrDefault());
            return new();
        }
    }
}
