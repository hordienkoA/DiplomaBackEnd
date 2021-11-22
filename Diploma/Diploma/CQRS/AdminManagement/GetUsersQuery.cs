using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.AdminManagement
{
    public class GetUsersQuery: IRequest<ResultView>
    {
        public int? GroupId { get; set; }
    }
}
