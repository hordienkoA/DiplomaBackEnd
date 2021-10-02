using System.Threading.Tasks;
using Diploma.CQRS.Subjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [Route("api/subjects")]
    public class SubjectController: Controller
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost]
        public async Task<IActionResult> GetSubjects([FromBody] AddSubjectRequest model)
        {
            var result = await _mediator.Send(new AddSubjectRequest());
            return result ? Ok() : BadRequest();
        }
    }
}
