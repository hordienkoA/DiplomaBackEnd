using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class GetSubjectsRequest:  IRequest<ResultView>
    {
        public int? SubjectId { get; set; } = null;
        public string Filter { get; set; }
    }
}
