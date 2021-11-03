﻿using System.Threading.Tasks;
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

        [Authorize(Roles = "Administrator,Teacher,Student")]
        [HttpGet]
        public async Task<IActionResult> GetSubjects([FromQuery] GetSubjectsRequest model)
        {
            var result = await _mediator.Send(model);
            return result != null ? Json(result) : NotFound();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] AddSubjectRequest model)
        {
            var result = await _mediator.Send(model);
            return result ? Ok() : BadRequest();
        }
    }
}
