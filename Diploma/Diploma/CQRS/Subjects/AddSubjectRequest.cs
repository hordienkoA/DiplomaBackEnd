using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Subjects
{
    public class AddSubjectRequest: IRequest<bool>
    {
        public string Name { get; set; }
        public int CourseNumber { get; set; }
    }
}
