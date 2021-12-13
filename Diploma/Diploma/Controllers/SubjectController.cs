using System.Linq;
using System.Threading.Tasks;
using Diploma.CQRS.Subjects;
using Diploma.CQRS.Subjects.AssignUsers;
using Diploma.CQRS.Subjects.ResignUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("api/subjects")]
    public class SubjectController : Controller
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator,Teacher,Student")]
        [HttpGet]
        [Route("{id:int?}")]
        public async Task<IActionResult> GetSubjects([FromRoute]int? id = null)
        {
            var model = new GetSubjectsRequest { SubjectId = id };
            var result = await _mediator.Send(model);
            if (result.Error != null)
            {
                Response.StatusCode = result.Error.Code;
                return Json(result.Error.Message);
            }
            return Json(result.Views);
        }

        [Authorize(Roles = "Administrator,Teacher,Student")]
        [HttpGet]
        public async Task<IActionResult> GetSubjects( string filter = null)
        {
            var model = new GetSubjectsRequest {  Filter = filter };
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
        public async Task<IActionResult> CreateSubject([FromBody] AddSubjectRequest model)
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
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var model = new RemoveSubjectRequest { SubjectId = id };
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
        public async Task<IActionResult> EditSubject([FromBody] EditSubjectRequest model)
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

            return Json(result.Views);
        }

        [Authorize(Roles = "Administator,Teacher")]
        [HttpPost("assign-users")]
        public async Task<IActionResult> AssignUsers([FromBody] AssignUsersRequest model)
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

        [Authorize(Roles = "Administator,Teacher")]
        [HttpPost("resign-users")]
        public async Task<IActionResult> ResignUsers([FromBody] ResignUsersRequest model)
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
