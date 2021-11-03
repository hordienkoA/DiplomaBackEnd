using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma.Views;
using EFCoreConfiguration.Models.Enums;
using MediatR;

namespace Diploma.CQRS.Lessons
{
    public class AddLessonRequest: IRequest<bool>
    {
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime ValidTill { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
    }
}
