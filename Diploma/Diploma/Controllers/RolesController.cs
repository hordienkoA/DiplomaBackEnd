using System.Linq;
using System.Threading.Tasks;
using Diploma.CQRS.AdminManagement;
using Diploma.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("/api/roles")]
    [Authorize(Roles = "Administrator")]
    public class RolesController: Controller
    {
        private readonly IMediator _mediator;
        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles=await _mediator.Send(new RolesQuery());
            return Json(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AssignToRoles([FromBody] AssignRoleQuery model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(el => el.ErrorMessage));
                }
                var result = await _mediator.Send(model);
                if (result.Succeeded)
                {
                    return Ok();
                }

                throw new BusinessException(string.Join('\n', result.Errors.Select(el => el.Description)));
            }
            catch(BusinessException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
