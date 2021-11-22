using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Diploma.CQRS.Group.Remove
{
    public class RemoveGroupRequest: IRequest<ResultView>
    {
        [FromRoute]
        [Required(ErrorMessage = "RemoveGroup_GroupId_Required")]
        public int GroupId { get; set; }
    }
}
