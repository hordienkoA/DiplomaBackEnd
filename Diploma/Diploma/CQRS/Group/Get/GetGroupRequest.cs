using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Group.Get
{
    public class GetGroupRequest: IRequest<ResultView>
    {
        public string NameFilter { get; set; }
    }
}
