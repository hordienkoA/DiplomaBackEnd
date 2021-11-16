using Diploma.Views;
using EFCoreConfiguration.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Diploma.CQRS.Task
{
    public class AddTaskRequest: IRequest<ResultView>
    {
        [Required(ErrorMessage = "AddTask_Question_Required")]
        public string Question { get; set; }
        [Required(ErrorMessage = "AddTask_Type_Required")]
        public TaskType Type { get; set; }
        public int AnswerNumber { get; set; }
        public int LessonId { get; set; }
    }
}
