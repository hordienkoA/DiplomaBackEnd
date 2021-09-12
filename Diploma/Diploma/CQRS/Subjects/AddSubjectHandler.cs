using System;
using System.Threading;
using System.Threading.Tasks;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class AddSubjectHandler: IRequestHandler<AddSubjectRequest,bool>
    {
        private readonly SubjectRepository _repository;

        public AddSubjectHandler(SubjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(AddSubjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _repository.AddSubject(new()
                {
                    Name = request.Name,
                    CourseNumber = request.CourseNumber
                });
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}
