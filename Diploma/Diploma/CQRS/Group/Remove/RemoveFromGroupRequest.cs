using System.Collections.Generic;
using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Group.Remove
{
    public class RemoveFromGroupRequest: IRequest<ResultView>
    {
        public int GroupId { get; set;}
        public List<string> UserNames { get; set; }
    }
}
