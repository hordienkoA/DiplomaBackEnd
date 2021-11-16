using Diploma.CQRS.Task;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("api/tasks")]
    public class TaskController: Controller
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetTasks(GetTaskRequest model)
        {
            var result = await _mediator.Send(model);
            if (result.Error != null)
            {
                Response.StatusCode = result.Error.Code;
                return Json(result.Error.Message);
            }
            return Json(result);
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] AddTaskRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(el => el.ErrorMessage));
            }
            
            var result = await _mediator.Send(model);
            if (result.Error != null)
            {
                Response.StatusCode = result.Error.Code;
                return Json(result.Error.Message);
            }

            return Ok();
        }
    }
}
