using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Task
{
    public class GetTaskRequest: IRequest<ResultView>
    {
        [FromQuery]
        public int? Id { get; set; }
        [FromQuery]
        public int? LessonId { get; set; }
    }
}
