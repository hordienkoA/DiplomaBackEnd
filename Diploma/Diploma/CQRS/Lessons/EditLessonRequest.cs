using Diploma.Views;
using EFCoreConfiguration.Models.Enums;
using MediatR;

namespace Diploma.CQRS.Lessons
{
    public class EditLessonRequest: IRequest<ResultView>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime ValidTill { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
    }
}
