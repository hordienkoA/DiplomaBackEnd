using Diploma.Views;
using EFCoreConfiguration.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Diploma.CQRS.Task
{
    public class EditTaskRequest: IRequest<ResultView>
    {
        [Required(ErrorMessage = "EditTask_Question_Required")]
        public string Question { get; set; }
        public TaskType Type { get; set; }
        [Required(ErrorMessage = "EditTask_Answer_Required")]
        public string Answer { get; set; }
        public int LessonId { get; set; }
    }
}
