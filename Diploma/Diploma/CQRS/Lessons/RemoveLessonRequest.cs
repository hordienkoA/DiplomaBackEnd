using System.ComponentModel.DataAnnotations;
using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Lessons
{
    public class RemoveLessonRequest: IRequest<ResultView>
    {
        [FromRoute]
        [Required(ErrorMessage = "RemoveLesson_LessonId_Required")]
        public int LessonId { get; set; }
    }
}
