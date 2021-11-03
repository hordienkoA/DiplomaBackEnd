using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Diploma.Views;
using EFCoreConfiguration.Repositories;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class GetSubjectHandler : IRequestHandler<GetSubjectsRequest, List<SubjectView>>
    {
        private readonly SubjectRepository _repository;
        private readonly IMapper _mapper;
        public GetSubjectHandler(
            SubjectRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<SubjectView>> Handle(GetSubjectsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await _repository.GetSubjectsAsync(request.SubjectId, request.Filter);
                return subjects.Select(el => new SubjectView()
                {
                    Id = el.Id,
                    Name = el.Name,
                    Course = el.Course,
                    Description = el.Description,
                    Lessons = el.Lessons
                        .Select(e => _mapper.Map<LessonView>(e)).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
