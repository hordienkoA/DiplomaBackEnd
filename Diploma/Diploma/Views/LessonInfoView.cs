using EFCoreConfiguration.Models.Enums;

namespace Diploma.Views
{
    public class LessonInfoView: IView
    {
        public StatusEnum Status { get; set; }
        public DateTime ValidTill { get; set; }
        public int Attemts { get; set; }
        public bool IsPassed { get; set; }
        public int Mark { get; set; }
    }
}
