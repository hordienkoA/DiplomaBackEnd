using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace EFCoreConfiguration.Repositories
{
    public class TaskInfoRepository : Repository<ApplicationContext, TaskInfo>
    {
        public TaskInfoRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<TaskInfo>> GetTaskInfo(int? lessonId = null, string username = null)
        {
            return await Source
                .Include(ti=>ti.Comments)
                .Include(ti => ti.Task)
                .ThenInclude(t => t.Lesson)
                .ThenInclude(l => l.Subject)
                .ThenInclude(s => s.Users)
                .Where(el =>
                el.Task.LessonId == (lessonId ?? el.Task.LessonId) &&
                el.Task.Lesson.Subject.Users.Any(u => u.UserName == username))
                .ToListAsync();
        }

        public void AddTaskInfo(TaskInfo taskInfo)
        {
            Add(taskInfo);
            Context.SaveChanges();
        }

        public void EditTaskInfo(TaskInfo taskInfo)
        {
            Context.Update(taskInfo);
            Context.SaveChanges();
        }

        public void DeleteTaskInfo(TaskInfo taskInfo)
        {
            Remove(taskInfo);
            Context.SaveChanges();
        }

        public async Task<TaskInfo> FindTaskAsync(int id)
        {
            return await Source.Include(el=>el.Comments).FirstOrDefaultAsync(el=>el.Id == id);
        }

        public async Task AddComment(int id, Comment comment)
        {
            var taskInfo = await FindTaskAsync(id);
            taskInfo.Comments.Add(comment);
            Context.SaveChanges();
        }
    }
}
