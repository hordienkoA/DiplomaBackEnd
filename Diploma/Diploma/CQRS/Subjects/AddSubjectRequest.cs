using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Diploma.CQRS.Subjects
{
    public class AddSubjectRequest: IRequest<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Course{ get; set; }
    }
}
