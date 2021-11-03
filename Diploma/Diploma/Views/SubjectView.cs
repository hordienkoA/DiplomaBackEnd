using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Views
{
    public class SubjectView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Course { get; set; }
        public List<LessonView> Lessons { get; set; }
    }
}