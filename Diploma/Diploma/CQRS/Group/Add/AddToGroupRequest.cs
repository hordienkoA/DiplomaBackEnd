using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Group.Add
{
    public class AddToGroupRequest: IRequest<ResultView>
    {
        public int GroupId { get; set; }
        public List<string> UserNames { get; set; }
    }
}
