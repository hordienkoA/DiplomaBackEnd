using System;
using System.Collections.Generic;
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
    public class AddSubjectHandler: IRequestHandler<AddSubjectRequest,ResultView>
    {
        private readonly SubjectRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _accessor;

        public AddSubjectHandler(
            SubjectRepository repository,
            UserManager<User> userManager,
            IUserAccessor accessor)
        {
            _repository = repository;
            _userManager = userManager;
            _accessor = accessor;
        }
        public async Task<ResultView> Handle(AddSubjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_accessor.User.Identity.Name);
                _repository.AddSubject(new()
                {
                    Name = request.Name,
                    Course = request.Course,
                    Description = request.Description,
                    Users = new List<User>{user}
                });
                return new ResultView();
            }
            catch(Exception ex)
            {
                return new(){Error = new(400, ex.Message)};
            }

        }
    }
}
