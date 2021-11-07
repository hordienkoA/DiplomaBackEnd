using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Lessons
{
    public class EditLessonHandler: IRequestHandler<EditLessonRequest, ResultView>
    {
        private readonly LessonRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IMapper _mapper;

        public EditLessonHandler(
            LessonRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _mapper = mapper;
        }
        public async Task<ResultView> Handle(EditLessonRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            var lessons =
                await _repository.GetLessonsAsync(request.Id, null, _accessor.User.Identity.Name, roles.ToList());
            if (lessons.Count == 0)
            {
                return new()
                    { Error = new(403, "Ви не маєте доступу до данного завдання або його не існує в системі") };
            }

            _repository.EditLesson(_mapper.Map<Lesson>(request));

            return new()
            {
                Views = new()
                {
                    _mapper.Map<LessonView>(await _repository.FindAsync(request.Id))
                }
            };
        }
    }
}
