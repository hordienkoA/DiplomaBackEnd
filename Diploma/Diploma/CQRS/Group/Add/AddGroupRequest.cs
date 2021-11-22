using Diploma.Views;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Diploma.CQRS.Group.Add
{
    public class AddGroupRequest: IRequest<ResultView>
    {
        [Required(ErrorMessage = "AddGroup_Name_Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "AddGroup_CourseNumber_Required")]
        [Range(0,7,ErrorMessage = "AddGroup_CourseNumber_Range")]
        public int CourseNumber { get; set; }
    }
}
