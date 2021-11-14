using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.AdminManagement
{
    public class GetUsersQuery: IRequest<List<UserView>>
    {
    }
}
