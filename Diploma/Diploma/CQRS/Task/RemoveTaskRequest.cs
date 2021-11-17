using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Diploma.CQRS.Task
{
    public class RemoveTaskRequest: IRequest<ResultView>
    {
        [FromRoute]
        [Required(ErrorMessage = "RemoveTask_Id_Required")]
        public int TaskId { get; set; }
    }
}
