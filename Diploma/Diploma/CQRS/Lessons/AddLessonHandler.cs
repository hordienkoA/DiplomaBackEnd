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

namespace Diploma.CQRS.Lessons
{
    public class AddLessonHandler: IRequestHandler<AddLessonRequest, ResultView>
    {
        private readonly LessonRepository _repository;
        public readonly SubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _accessor;
        private readonly UserManager<User> _userManager;

        public AddLessonHandler(
            LessonRepository repository,
            IMapper mapper,
            UserManager<User> userManager,
            IUserAccessor accessor,
            SubjectRepository subjectRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _accessor = accessor;
            _subjectRepository = subjectRepository;
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
                        Error = new(403, $"Ви не можете додати це завдання до предмета з id - {request.SubjectId}")
                    };
                }

                _repository.AddLesson(new()
                {
                    Name= request.Name,
                    Description = request.Description,
                    Status = request.Status,
                    ValidTill = request.ValidTill,
                    SubjectId = request.SubjectId,
                    
                });
                return new();
            }
            catch (Exception ex)
            {
                return new(){Error = new(400, ex.Message)} ;
            }
        }
    }
}
