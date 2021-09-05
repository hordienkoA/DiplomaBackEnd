using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreConfiguration.Models.Contexts.Configuration
{
    class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "B22698B8-42A2-4115-9631-1C2D1E2AC5F1",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole()
                {
                    Id= "B22698B8-42A2-4115-9631-1C2D1E2AC5F2",
                    Name = "Student",
                    NormalizedName = "STUDENT"
                },
                new IdentityRole()
                {
                    Id= "B22698B8-42A2-4115-9631-1C2D1E2AC5F3",
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                });
        }
    }
}
