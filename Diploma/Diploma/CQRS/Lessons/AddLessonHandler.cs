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
    public class AddLessonHandler: IRequestHandler<AddLessonRequest, ResultView>
    {
        private readonly LessonRepository _repository;
        private readonly SubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _accessor;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<Messages> _localization;

        public AddLessonHandler(
            LessonRepository repository,
            IMapper mapper,
            UserManager<User> userManager,
            IUserAccessor accessor,
            SubjectRepository subjectRepository,
            IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _accessor = accessor;
            _subjectRepository = subjectRepository;
            _localization = localization;
        }
        public async Task<ResultView> Handle(AddLessonRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var subject=await _subjectRepository.FindAsync(request.SubjectId);
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                if (subject == null || !subject.Users.Contains(user))
                {
                    return new()
                    {
                        Error = new(403, $"{_localization["AddLesson_WrongSubject"]} - {request.SubjectId}")
                    };
                }

                _repository.AddLesson(_mapper.Map<Lesson>(request));
                return new();
            }
            catch (Exception ex)
            {
                return new(){Error = new(400, ex.Message)} ;
            }
        }
    }
}
