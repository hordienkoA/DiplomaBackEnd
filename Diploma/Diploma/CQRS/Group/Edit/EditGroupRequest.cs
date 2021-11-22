using Diploma.Views;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Diploma.CQRS.Group.Edit
{
    public class EditGroupRequest: IRequest<ResultView>
    {
        [Required(ErrorMessage = "EditGroup_Id_Required")]
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(0, 7, ErrorMessage = "EditGroup_CourseNumber_Range")]
        public int CourseNumber { get; set; }
    }
}
