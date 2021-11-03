using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diploma.DependencyInjection;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Lessons
{
    public class RemoveLessonHandler: IRequestHandler<RemoveLessonRequest, bool>
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
        }
        public async Task<bool> Handle(RemoveLessonRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            var lessons = await _repository.GetLessonsAsync(request.LessonId, null, _accessor.User.Identity.Name, roles.ToList());
            if (lessons.Count == 0)
            {
                return false;
            }

            _repository.DeleteLesson(lessons.FirstOrDefault());
            return true;
        }
    }
}
