using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConfiguration.Repositories
{
    public class LessonRepository : Repository<ApplicationContext, Lesson>
    {
        public LessonRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Lesson>> GetLessonsAsync(int? id, string filter = null, string username = null, List<string> roles = null, int? subjectId = null)
        {
            return await Source
                .Include(el => el.Subject)
                .ThenInclude(el => el.Users)
                .Where(el =>
                    el.Id == (id ?? el.Id) &&
                    el.Name.Contains(filter ?? el.Name) &&
                    el.SubjectId == (subjectId ?? el.SubjectId) &&
                    (el.Subject.Users.Any(u => u.UserName.Equals(username)) || roles.Contains("Administrator")))
                .Select(el => new Lesson()
                {
                    Id = el.Id,
                    Name = el.Name,
                    Description = el.Description,
                    Tasks = el.Tasks.Select(t => new Models.Task
                    {
                        Id = t.Id,
                        Question = t.Question,
                        Type = t.Type,
                        Answer = t.Answer,
                        LessonId = t.LessonId,
                    }).ToList(),
                }).ToListAsync();
        }

        public void AddLesson(Lesson lesson)
        {
            Add(lesson);
            Context.SaveChanges();
        }

        public void EditLesson(Lesson lesson)
        {
            Context.Update(lesson);
            Context.SaveChanges();
        }

        public void DeleteLesson(Lesson lesson)
        {
            Remove(lesson);
            Context.SaveChanges();
        }

        public async Task<Lesson> GetLessonAsync(int id, User user)
        {
            return await Context.Lessons.Include(l => l.Subject).ThenInclude(s => s.Users).FirstOrDefaultAsync(l => l.Id == id && l.Subject.Users.Contains(user));
        }
    }
}
