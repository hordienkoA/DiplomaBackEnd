using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects.ResignUsers
{
    public class ResignUsersRequest: IRequest<ResultView>
    {
        public int SubjectId { get; set; }
        public List<int> UserNames { get; set; }
        public List<int> GroupIds { get; set; }
    }
}
