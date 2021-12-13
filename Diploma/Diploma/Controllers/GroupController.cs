using Diploma.CQRS.Group.Add;
using Diploma.CQRS.Group.Edit;
using Diploma.CQRS.Group.Get;
using Diploma.CQRS.Group.Remove;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("/api/groups")]
    public class GroupController: Controller
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles="Administrator,Teacher")]
        public async Task<IActionResult> GetGroups(GetGroupRequest model)
        {
            var result = await _mediator.Send(model);
            if (result.Error != null)
            {
                Response.StatusCode = result.Error.Code;
                return Json(result.Error.Message);
            }

            return Json(result.Views);
        }

        [HttpPost]
        [Authorize(Roles ="Administrator,Teacher")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupRequest model)
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

        [HttpPut]
        [Authorize(Roles = "Administrator,Teacher")]
        public async Task<IActionResult> EditGroup([FromBody] EditGroupRequest model)
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

        [HttpDelete]
        [Authorize(Roles = "Administrator,Teacher")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var model = new RemoveGroupRequest { GroupId = id };
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

        [HttpPost("add-users")]
        [Authorize(Roles = "Administrator, Teacher")]
        public async Task<IActionResult> AddToGroup([FromBody] AddToGroupRequest model)
        {
            var result = await _mediator.Send(model);
            if(result.Error != null)
            {
                Response.StatusCode=result.Error.Code;
                return Json(result.Error.Message);
            }
            return Ok();
        }

        [HttpPost("remove-users")]
        [Authorize(Roles = "Administrator, Teacher")]
        public async Task<IActionResult> AddToGroup([FromBody] RemoveFromGroupRequest model)
        {
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
