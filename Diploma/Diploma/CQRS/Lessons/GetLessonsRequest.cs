using System.Collections.Generic;
using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Lessons
{
    public class GetLessonsRequest : IRequest<List<LessonView>>
    {
        public int? LessonId { get; set; }
        public string Filter { get; set; }
    }
}
