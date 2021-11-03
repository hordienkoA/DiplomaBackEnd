using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Diploma.CQRS.Lessons
{
    public class AddLessonHandler: IRequestHandler<AddLessonRequest, bool>
    {
        private readonly LessonRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpContext _context;

        public AddLessonHandler(
            LessonRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(AddLessonRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _repository.AddLesson(new()
                {
                    Name= request.Name,
                    Description = request.Description,
                    Status = request.Status,
                    ValidTill = request.ValidTill,
                    SubjectId = request.SubjectId
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
