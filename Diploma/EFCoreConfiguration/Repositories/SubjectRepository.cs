using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConfiguration.Repositories
{
    public class SubjectRepository: Repository<ApplicationContext, Subject>
    {
        public SubjectRepository(ApplicationContext context): base(context)
        {
            
        }

        public async Task<List<Subject>> GetSubjectsAsync(int? id, string filter)
        {
            return await Source
                .Where(el => el.Id == (id ?? el.Id) && el.Name.Contains(filter ?? el.Name))
                .Select(el => new Subject()
                {
                    Id= el.Id,
                    Name = el.Name,
                    Course = el.Course,
                    Description = el.Description,
                    Lessons = el.Lessons.Select(e=>new Lesson()
                    {
                        Id= e.Id,
                        Name = e.Name,
                        Status = e.Status,
                        ValidTill = e.ValidTill,
                        Description = e.Description
                    }).ToList()
                }
            ).ToListAsync();
        }

        public void AddSubject(Subject subject)
        {
            Add(subject);
            Context.SaveChanges();
        }

        public void EditSubject(Subject subject)
        {
            Context.Update(subject);
            Context.SaveChanges();
        }

        public void DeleteSubject(Subject subject)
        {
            Remove(subject);
            Context.SaveChanges();
        }
    }
}
