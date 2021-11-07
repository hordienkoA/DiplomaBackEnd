using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Lessons
{
    public class RemoveLessonRequest: IRequest<ResultView>
    {
        public int LessonId { get; set; }
    }
}
