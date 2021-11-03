using System;
using EFCoreConfiguration.Models.Enums;

namespace Diploma.Views
{
    public class LessonView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime ValidTill { get; set; }
        public string Description { get; set; }
    }
}
