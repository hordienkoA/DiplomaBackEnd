using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diploma.DependencyInjection;
using Diploma.Views;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Subjects
{
    public class RemoveSubjectHandler: IRequestHandler<RemoveSubjectRequest, ResultView>
    {
        private readonly SubjectRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;

        public RemoveSubjectHandler(
            SubjectRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
        }
        public async Task<ResultView> Handle(RemoveSubjectRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            var subjects = await _repository.GetSubjectsAsync(request.SubjectId, null, _accessor.User.Identity.Name,
                roles.ToList());
            if (subjects.Count == 0)
            {
                return new() { Error = new(403, "Ви не маєте прав для видалення даного предмета") };
            }

            _repository.DeleteSubject(subjects.FirstOrDefault());
            return new ResultView();
        }
    }
}
