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
                    Age = 22,
                    ConcurrencyStamp = "e74bf818-e556-461f-bfa7-e315f5606380",
                    PasswordHash = "AQAAAAEAACcQAAAAEJ6DrokQy6byZ1LJF52mA6ayRxsSTRGymDFgV5YIisRZzpLxqOM8vhojBg/B9+xmFg==",
                    SecurityStamp = "92f4cf69-021a-4057-a8ed-7bf15ca820cb"
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
                    Age = 20,
                    ConcurrencyStamp = "ee4aa45c-ce6c-4571-bf99-5a8eb7c44aa6",
                    PasswordHash = "AQAAAAEAACcQAAAAEGAcwTwwCiQem7FH84SZWc7aaEe1mWFnp1mTX0Cc2EYKqy8+FMBfYwwYsqw1FY9Sfw==",
                    SecurityStamp = "3f4d0152-71cd-4e40-b599-4f61552a5de9"
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
                    Age = 76,
                    ConcurrencyStamp = "d42270e6-7738-4bdf-84fe-51ac742b592d",
                    PasswordHash = "AQAAAAEAACcQAAAAEHMad7fbBpFdRem2cnL7/pfYQNh0NQqew74V92RGlCi/MK+q6GjiT7zqYFJPaH7ZoQ==",
                    SecurityStamp = "e2f4703a-893b-49b9-ac06-40ae983af9b9"
                };

            //admin.PasswordHash = PassGenerate(admin, "BillyH1488$");
            //student.PasswordHash = PassGenerate(student, "Idirabotai1488$");
            //teacher.PasswordHash = PassGenerate(teacher, "PredmetDermo232$");
            builder.HasData(admin, student, teacher);
        }

        public string PassGenerate(User user, string password)
        {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, password);
        }
    }
}
