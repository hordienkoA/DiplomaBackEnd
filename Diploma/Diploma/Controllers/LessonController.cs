using System.Threading.Tasks;
using Diploma.CQRS.Lessons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("api/lessons")]
    public class LessonController: Controller
    {
        private readonly IMediator _mediator;

        public LessonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator,Teacher,Student")]
        [HttpGet]
        public async Task<IActionResult> GetLessons([FromQuery] GetLessonsRequest model)
        {
            var result = await _mediator.Send(model);
            return result != null ? Json(result) : NotFound();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] AddLessonRequest model)
        {
            var result = await _mediator.Send(model);
            return result ? Ok() : BadRequest();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLesson([FromBody] RemoveLessonRequest model)
        {
            var result = await _mediator.Send(model);
            return result ? Ok() : NotFound();
        }
    }
}
