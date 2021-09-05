using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreConfiguration.Models.Contexts.Configuration
{
    class UsersConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var admin =
                new User()
                {
                    Id = "B22698B8-42A2-4115-9631-1C2D1E2AC5E1",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "test1488228@gmail.com",
                    NormalizedEmail = "TEST1488228@gmail.com",
                    FirstName = "Dungeon",
                    SecondName = "Master",
                    Age = 22
                };
            var student =
                new User()
                {
                    Id = "B22698B8-42A2-4115-9631-1C2D1E2AC5E2",
                    UserName = "Chell",
                    NormalizedUserName = "CHELL",
                    Email = "rabotiaga@zavod.com",
                    NormalizedEmail = "RABOTIAGA@ZAVOD.COM",
                    FirstName = "Rabotiaga",
                    SecondName = "Default",
                    Age = 20
                };

            var teacher =
                new User()
                {
                    Id = "B22698B8-42A2-4115-9631-1C2D1E2AC5E3",
                    UserName = "Prepod",
                    NormalizedUserName = "PREPOD",
                    Email = "prepod@univer.com",
                    NormalizedEmail = "PREPOD@UNIVER.COM",
                    FirstName = "Prepod",
                    SecondName = "NeVali",
                    Age = 76
                };

            admin.PasswordHash = PassGenerate(admin, "BillyH1488$");
            student.PasswordHash = PassGenerate(student, "Idirabotai1488$");
            teacher.PasswordHash = PassGenerate(teacher, "PredmetDermo232$");
            builder.HasData(admin, student, teacher);
        }

        public string PassGenerate(User user, string password)
        {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, password);
        }
    }
}
