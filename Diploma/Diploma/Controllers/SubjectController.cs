using Diploma.CQRS.Subjects;
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
        public async Task<IActionResult> GetSubjects(int? id, [FromQuery]string filter)
        {
            var model = new GetSubjectsRequest { SubjectId = id, Filter = filter };
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
    }
}
