using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Diploma.CQRS.Lessons
{
    public class RemoveLessonRequest: IRequest<bool>
    {
        public int LessonId { get; set; }
    }
}
