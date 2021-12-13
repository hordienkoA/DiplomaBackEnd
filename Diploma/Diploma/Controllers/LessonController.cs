using System.Linq;
using System.Threading.Tasks;
using Diploma.CQRS.LessonInfo;
using Diploma.CQRS.Lessons;
using EFCoreConfiguration.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("api/lessons")]
    public class LessonController: Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _manager;

        public LessonController(
            IMediator mediator,
            UserManager<User> manager)
        {
            _mediator = mediator;
            _manager = manager;
        }

        [Authorize(Roles = "Administrator,Teacher,Student")]
        [HttpGet]
        [Route("{id:int?}")]
        public async Task<IActionResult> GetLessons(int? id, [FromQuery] string filter, int? SubjectId)
        {
            var user = await _manager.FindByNameAsync(HttpContext.User.Identity.Name);
            var roles = await _manager.GetRolesAsync(user);
            //if (roles.Contains("Teacher"))
            //{
                var model = new GetLessonsRequest { LessonId = id, Filter = filter, SubjectId = SubjectId };
                var result = await _mediator.Send(model);
            //}
            
            if (result.Error != null)
            {
                Response.StatusCode = result.Error.Code;
                return Json(result.Error.Message);
            }
            return Json(result.Views);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("info/{id:int?}")]
        public async Task<IActionResult> GetLessonInfos(int? id)
        {
            var user = await _manager.FindByNameAsync(HttpContext.User.Identity.Name);
            var roles = await _manager.GetRolesAsync(user);
            var model = new GetLessonInfoRequest { LessonId = id };
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
        public async Task<IActionResult> CreateLesson([FromBody] AddLessonRequest model)
        {
            var result = await _mediator.Send(model);
            if(result.Error != null)
            {
                Response.StatusCode = result.Error.Code;
                return Json(result.Error.Message);
            }
            return Ok();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var model = new RemoveLessonRequest { LessonId = id };
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

        [Authorize(Roles = "Administrator, Teacher")]
        [HttpPut]
        public async Task<IActionResult> EditLesson([FromBody] EditLessonRequest model)
        {
            var result = await _mediator.Send(model);
            if (result.Error != null)
            {
                Response.StatusCode = result.Error.Code;
                return Json(result.Error.Message);
            }
            return Json(result.Views);
        }
    }
}
