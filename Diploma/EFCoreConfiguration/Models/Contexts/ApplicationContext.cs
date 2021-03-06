using EFCoreConfiguration.Models.Contexts.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EFCoreConfiguration.Models.Contexts
{
    public class ApplicationContext: IdentityDbContext<User>, IDbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LessonInfo> LessonsInfo { get; set; }
        public DbSet<SubjectInfo> SubjectInfo { get; set; }
        public DbSet<TaskInfo> TaskInfo { get; set; }
        public override DbSet<TEntity> Set<TEntity>() where TEntity : class =>
            (DbSet<TEntity>)((IDbSetCache)this).GetOrAddSet(this.GetDependencies().SetSource, typeof(TEntity));

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RolesConfiguration());
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new UsersWithRoleConfiguration());
            builder.ApplyConfiguration(new GroupsConfiguration());
            builder.ApplyConfiguration(new CommentsConfiguration());
        }
    }
}
