﻿namespace EFCoreConfiguration.Models
{
    public class Work
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public Subject Subject { get; set; }
        public uint? SubjectId { get; set; }
        public string Test { get; set; }
    }
}