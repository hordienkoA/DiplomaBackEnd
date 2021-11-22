using EFCoreConfiguration.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Group = EFCoreConfiguration.Models.Group;
using User = EFCoreConfiguration.Models.User;
namespace EFCoreConfiguration.Repositories
{
    public class GroupRepository : Repository<ApplicationContext, Group>
    {
        public GroupRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Group>> GetGroupsAsync(string filter = null)
        {
            return await Source
                .Where(el =>
                 el.Name.Contains(filter ?? el.Name))
                .ToListAsync();
        }

        public async Task AddGroup(Group group)
        {
            Add(group);
            await Context.SaveChangesAsync();
        }

        public async Task<Group> EditGroup(Group group)
        {
            Context.Update(group);
            await Context.SaveChangesAsync();
            return await Source.FirstOrDefaultAsync(g => g.Name == group.Name);
        }

        public async Task DeleteGroup(Group group)
        {
            Remove(group);
            await Context.SaveChangesAsync();
        }

        public async Task AddToGroup(int groupId, List<User> users)
        {
            var group = Source.Include(g=>g.Users).FirstOrDefault(g => g.Id == groupId);
            group.Users.AddRange(users);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveFromGroup(int groupId, List<User> users)
        {
            var group = Source.Include(g => g.Users).FirstOrDefault(g => g.Id == groupId);
            group.Users.RemoveAll(u=>users.Any(el=>el.UserName.Equals(u.UserName)));
            await Context.SaveChangesAsync();
        }
    }
}
