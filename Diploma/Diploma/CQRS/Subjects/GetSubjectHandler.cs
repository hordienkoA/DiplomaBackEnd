using System;
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

namespace Diploma.CQRS.Subjects
{
    public class GetSubjectHandler : IRequestHandler<GetSubjectsRequest, ResultView>
    {
        private readonly SubjectRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IStringLocalizer<Messages> _localization;

        public GetSubjectHandler(
            SubjectRepository repository,
            IMapper mapper,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _accessor = accessor;
            _localization = localization;
        }
        public async Task<ResultView> Handle(GetSubjectsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(user);
                var subjects = await _repository.GetSubjectsAsync(request.SubjectId, request.Filter, _accessor.User.Identity.Name, roles.ToList());
                if (!subjects.Any())
                {
                    return new()
                    {
                        Error = new(404, _localization["GetSubjects_NotFound"])
                    };
                }

                return new()
                {
                    Views = new List<IView>(subjects.Select(el => new SubjectView()
                    {
                        Id = el.Id,
                        Name = el.Name,
                        Course = el.Course,
                        Description = el.Description,
                        Lessons = el.Lessons
                            .Select(e => _mapper.Map<LessonView>(e)).ToList()
                    }))
                };
            }
            catch (Exception ex)
            {
                return new() { Error = new(400, ex.Message) };
            }
        }
    }
}
