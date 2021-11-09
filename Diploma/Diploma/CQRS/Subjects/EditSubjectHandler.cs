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

namespace Diploma.CQRS.Subjects
{
    public class EditSubjectHandler: IRequestHandler<EditSubjectRequest, ResultView>
    {
        private readonly SubjectRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;
        private readonly IMapper _mapper;

        public EditSubjectHandler(
            SubjectRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor,
            IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
            _mapper = mapper;
        }
        public async Task<ResultView> Handle(EditSubjectRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            var subjects =
                await _repository.GetSubjectsAsync(request.Id, null, _accessor.User.Identity.Name, roles.ToList());
            if (subjects.Count == 0)
            {
                return new()
                    { Error = new(403, "Ви не маєте доступу до данного предмета або його не існує в системі ") };
            }

            _repository.EditSubject(_mapper.Map<Subject>(request));

            return new()
            {
                Views = new()
                {
                    _mapper.Map<SubjectView>(await _repository.FindAsync(request.Id))
                }
            };
        }
    }
}
