using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConfiguration.Repositories
{
    public class LessonInfoRepository : Repository<ApplicationContext, LessonInfo>
    {
        public LessonInfoRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<LessonInfo> GetLessonInfoAsync(int? lessonId = null, string username = null, int? subjectId = null)
        {
            return  await Source
                .Include(li => li.Lesson)
                .ThenInclude(l => l.Subject)
                .ThenInclude(l => l.Users)
                .FirstOrDefaultAsync(el =>
                    el.Lesson.SubjectId == (subjectId ?? el.Lesson.SubjectId) &&
                    el.LessonId == (lessonId ?? el.LessonId) &&
                    el.Lesson.Subject.Users.Any(u => u.UserName.Equals(username)));
            
        }


        public void AddLessonInfo(LessonInfo lessonInfo)
        {
            Add(lessonInfo);
            Context.SaveChanges();
        }

        public void EditLessonInfo(LessonInfo LessonInfo)
        {
            Context.Update(LessonInfo);
            Context.SaveChanges();
        }

        public void DeleteLessonInfo(LessonInfo lesson)
        {
            Remove(lesson);
            Context.SaveChanges();
        }
    }
}
