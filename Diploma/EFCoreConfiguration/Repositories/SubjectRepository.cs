using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreConfiguration.Models;
using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace EFCoreConfiguration.Repositories
{
    public class SubjectRepository: Repository<ApplicationContext, Subject>
    {
        public SubjectRepository(ApplicationContext context): base(context)
        {
            
        }

        public async Task<List<Subject>> GetSubjectsAsync(int? id, string filter = null, string userName = null, List<string> roles = null)
        {
            return await Source
                .Include(el=>el.Groups)
                .ThenInclude(g=>g.Users)
                .Where(el => 
                    el.Id == (id ?? el.Id) &&
                    el.Name.Contains(filter ?? el.Name) &&
                    (el.Users.Any(u=>u.UserName.Equals(userName)) || 
                    el.Groups.Any(g=>g.Users.Any(u=>u.UserName.Equals(userName)))
                    ||  roles.Contains("Administrator")))
                .Select(el => new Subject()
                    {
                        Id = el.Id,
                        Name = el.Name,
                        Course = el.Course,
                        Description = el.Description,
                        Lessons = el.Lessons.Select(e => new Lesson()
                        {
                            Id = e.Id,
                            Name = e.Name,
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

        public override async Task<Subject> FindAsync(int id)
        {
            return await Source.Include(s => s.Users).FirstOrDefaultAsync(el => el.Id == id);
        }

        public async Task AssignToUsers(int subjectId, List<User> users, List<Group> groups)
        {
            var subject = await Source.Include(el => el.Groups).Include(el => el.Users).FirstOrDefaultAsync(s => s.Id == subjectId);
            if (groups != null)
            {
                subject.Groups.AddRange(groups);
            }
            if (users != null)
            {
                subject.Users.AddRange(users);

            }
            await Context.SaveChangesAsync();
        }

        public async Task ResignUsers(int subjectId, List<User> users, List<Group> groups)
        {
            var subject = await Source.Include(el => el.Groups).Include(el => el.Users).FirstOrDefaultAsync(s => s.Id == subjectId);
            if (groups != null)
            {
                subject.Groups.RemoveAll(el => groups.Any(g => g == el));
            }

            if (users != null)
            {
                subject.Users.RemoveAll(el => users.Any(u => u == el));

            }
            await Context.SaveChangesAsync();
        }
    }
}
