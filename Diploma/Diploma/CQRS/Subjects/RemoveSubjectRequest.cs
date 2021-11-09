using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Subjects
{
    public class RemoveSubjectRequest: IRequest<ResultView>
    {
        [FromRoute]
        public int SubjectId { get; set; }
    }
}
