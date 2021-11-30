using Diploma.CQRS.Atempt;
using Diploma.CQRS.Task;
using EFCoreConfiguration.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _manager;

        public TaskController(
            IMediator mediator,
            UserManager<User> manager)
        {
            _mediator = mediator;
            _manager = manager;
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
            return Json(result.Views);
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

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPut]
        public async Task<IActionResult> EditTask([FromBody] EditTaskRequest model)
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


        [Authorize(Roles = "Administrator,Teacher")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> RemoveTask(int id)
        {
            var model = new RemoveTaskRequest { TaskId = id };
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

        [Authorize(Roles = "Teacher,Student")]
        [HttpGet("get-task-infos")]
        public async Task<IActionResult> GetTaskInfos(StartAtemptRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(el => el.ErrorMessage));
            }
            var user = await _manager.FindByNameAsync(User.Identity.Name);
            var roles = await _manager.GetRolesAsync(user);
            if(roles.Contains("Teacher") || (roles.Contains("Student") && user.UserName == model.UserName))
            {
                var result = await _mediator.Send(model);
                if (result.Error != null)
                {
                    Response.StatusCode = result.Error.Code;
                    return Json(result.Error.Message);
                }
                return Ok(result.Views);
            }
            
            return Forbid();
        }

        [Authorize(Roles ="Student")]
        [HttpPost("submit-answer")]
        public async Task<IActionResult> SubmitAnswer([FromBody]SubmitAnswerRequest model)
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
