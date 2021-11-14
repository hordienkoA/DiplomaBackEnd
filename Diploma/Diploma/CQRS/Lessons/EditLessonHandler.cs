using AutoMapper;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using LocaleData;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Diploma.CQRS.Lessons
{
    public class EditLessonHandler: IRequestHandler<EditLessonRequest, ResultView>
    {
        private readonly LessonRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Messages> _localization;

        public EditLessonHandler(
            LessonRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IMapper mapper,
            IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _mapper = mapper;
            _localization = localization;
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
                    { Error = new(403, _localization["EditLesson_SubjectAccessError"]) };
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
