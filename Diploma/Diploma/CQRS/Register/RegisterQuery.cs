﻿using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Register
{
    public class RegisterQuery: IRequest<IdentityResult>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
