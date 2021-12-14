using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConfiguration.Repositories
{
    public class TaskRepository : Repository<ApplicationContext, Models.Task>
    {
        public TaskRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Models.Task>> GetTasksAsync(int? id, int? lessonId, User user, List<Group> groups = null)
        {
            return await Source
                .Include(t => t.Lesson)
                .ThenInclude(l => l.Subject)
                .ThenInclude(s => s.Users)
                .Where(el => el.Id == (id ?? el.Id)
                && el.LessonId == (lessonId ?? el.LessonId)
                && el.Lesson.Subject.Users.Contains(user)
                && ((groups == null) || (el.Lesson.Subject.Groups.Any(g=>groups.Contains(g)))))
                .ToListAsync();
        }

        public void AddTask(Models.Task task)
        {
            Add(task);
            Context.SaveChanges();
        }

        public void EditTask(Models.Task task)
        {
            Context.Update(task);
            Context.SaveChanges();
        }

        public void DeleteTask(Models.Task task)
        {
            Remove(task);
            Context.SaveChanges();
        }

        public override async Task<Models.Task> FindAsync(int id)
        {
            return await Source.Include(t => t.Lesson).FirstOrDefaultAsync(el => el.Id == id);
        }
    }
}
