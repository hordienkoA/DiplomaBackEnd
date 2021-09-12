using System.Threading.Tasks;
using Diploma.CQRS.AdminManagement;
using Diploma.CQRS.Login;
using Diploma.CQRS.Register;
using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("/api/users")]
    public class UserController: Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterQuery query)
        {
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
            var token=await _mediator.Send(query);
            if (token != null)
            {
                return Ok(new LoginDetails{Token = token});
            }

            return Unauthorized("Login failed");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Json(users);
        }

        [Route("test")]
        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok($"Вы авторизированы,{User.Identity.Name}.");
        }
    }
}
