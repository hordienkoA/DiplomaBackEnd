using System.ComponentModel.DataAnnotations;
using Diploma.Views;
using MediatR;

namespace Diploma.CQRS.Subjects
{
    public class AddSubjectRequest: IRequest<ResultView>
    {
        [Required(ErrorMessage = "AddSubject_Name_Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "AddSubject_Course_Required")]
        [Range(1,7, ErrorMessage = "AddSubject_Course_Range")]
        public int Course{ get; set; }
    }
}
