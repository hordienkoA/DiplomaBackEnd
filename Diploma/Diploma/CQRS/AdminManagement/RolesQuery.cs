﻿using System.Collections.Generic;
using MediatR;

namespace Diploma.CQRS.AdminManagement
{
    public class RolesQuery: IRequest<List<string>>
    {
    }
}
