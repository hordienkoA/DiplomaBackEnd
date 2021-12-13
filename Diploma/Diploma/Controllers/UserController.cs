using System.Linq;
using System.Threading.Tasks;
using Diploma.CQRS.AdminManagement;
using Diploma.CQRS.Login;
using Diploma.CQRS.Register;
using LocaleData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Diploma.Controllers
{
    [Route("/api/users")]
    public class UserController: Controller
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<Messages> _localization;
        public UserController(
            IMediator mediator,
            IStringLocalizer<Messages> localization)
        {
            _mediator = mediator;
            _localization = localization;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(el => el.ErrorMessage));
            }
            var result = await _mediator.Send(query);
            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(el => el.ErrorMessage));
            }
            var result = await _mediator.Send(query);
            if (result != null)
            {
                return Ok(result);
            }

            return Unauthorized(_localization["Login_Unauthorized"]);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Teacher,Student")]
        public async Task<IActionResult> GetUsers(GetUsersQuery model)
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
