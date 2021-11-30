using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Atempt
{
    public class StartAtemptRequest: IRequest<ResultView>
    {
        [FromQuery]
        public int LessonId { get; set; }
        [FromQuery]
        public string UserName { get; set; }
    }
}
