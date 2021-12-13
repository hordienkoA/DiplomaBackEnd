using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class RemoveSubjectHandler: IRequestHandler<RemoveSubjectRequest, ResultView>
    {
        private readonly SubjectRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IStringLocalizer<Messages> _localization;

        public RemoveSubjectHandler(
            SubjectRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IStringLocalizer<Messages> localization)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _localization = localization;
        }
        public async Task<ResultView> Handle(RemoveSubjectRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            var subjects = await _repository.GetSubjectsAsync(request.SubjectId, null, _accessor.User.Identity.Name,
                roles.ToList());
            if (subjects.Count == 0)
            {
                return new() { Error = new(403, _localization["RemoveSubject_AccessError"]) };
            }

            _repository.DeleteSubject(subjects.FirstOrDefault());
            return new ResultView();
        }
    }
}
