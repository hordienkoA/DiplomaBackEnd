using System.ComponentModel.DataAnnotations;
using Diploma.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.CQRS.Subjects
{
    public class RemoveSubjectRequest: IRequest<ResultView>
    {
        [FromRoute]
        [Required(ErrorMessage = "RemoveSubject_Id_Required")]
        public int SubjectId { get; set; }
    }
}
