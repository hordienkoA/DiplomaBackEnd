using System.Collections.Generic;
using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Lessons
{
    public class GetLessonsRequest : IRequest<ResultView>
    {
        [FromRoute]
        public int? LessonId { get; set; }
        [FromQuery]
        public int? SubjectId { get; set; }
        [FromQuery]
        public string Filter { get; set; }
    }
}
