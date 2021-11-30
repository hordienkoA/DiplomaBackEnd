using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.LessonInfo
{
    public class GetLessonInfoRequest: IRequest<ResultView>
    {
        [FromRoute]
        public int? LessonId { get; set; }
    }
}
