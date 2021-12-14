using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects.AssignUsers
{
    public class AssignUsersRequest: IRequest<ResultView>
    {
        public int SubjectId { get; set; }
        public List<string> UserNames { get; set; }
        public List<int> GroupIds { get; set; }
    }
}
