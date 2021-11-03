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
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Lessons
{
    public class GetLessonsHandler : IRequestHandler<GetLessonsRequest, List<LessonView>>
    {
        private readonly LessonRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        public GetLessonsHandler(
            LessonRepository repository,
            IMapper mapper,
            UserManager<User> userManager,
            IUserAccessor accessor)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _accessor = accessor;
        }

        public async Task<List<LessonView>> Handle(GetLessonsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(user);
                var lessons = await _repository.GetLessonsAsync(request.LessonId, request.Filter, _accessor.User.Identity.Name, roles.ToList());

                return lessons.Select(e => new LessonView()
                {
                    Name = e.Name,
                    Description = e.Description,
                    ValidTill = e.ValidTill,
                    Status = e.Status
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
