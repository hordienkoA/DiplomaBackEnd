using System;
using System.Collections.Generic;
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

        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await Source.ToListAsync();
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
