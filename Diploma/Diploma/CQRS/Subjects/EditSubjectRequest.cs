using System.ComponentModel.DataAnnotations;
using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class EditSubjectRequest: IRequest<ResultView>
    {
        [Required(ErrorMessage = "EditSubject_Id_Required")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, 7, ErrorMessage = "EditSubject_Course_Range")]
        public int Course { get; set; }
    }
}
