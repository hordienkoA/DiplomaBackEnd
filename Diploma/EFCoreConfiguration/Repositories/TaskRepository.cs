using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConfiguration.Repositories
{
    public class TaskRepository : Repository<ApplicationContext, Models.Task>
    {
        public TaskRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Models.Task>> GetTasksAsync(int? id, int? lessonId)
        {
            return await Source.Where(el => el.Id == (id ?? el.Id) && el.LessonId == (lessonId ?? el.LessonId)).ToListAsync();
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
            return await Source.Include(t=>t.Lesson).FirstOrDefaultAsync(el => el.Id == id);
        }
    }
}
