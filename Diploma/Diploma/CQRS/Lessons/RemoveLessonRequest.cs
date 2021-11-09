using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Lessons
{
    public class RemoveLessonRequest: IRequest<ResultView>
    {
        [FromRoute]
        public int LessonId { get; set; }
    }
}
