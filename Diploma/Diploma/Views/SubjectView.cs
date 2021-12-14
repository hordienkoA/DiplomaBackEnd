namespace Diploma.Views
{
    public class SubjectView: IView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Course { get; set; }
        public List<LessonView> Lessons { get; set; }
    }
}