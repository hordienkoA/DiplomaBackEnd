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
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "33fdb9f4-3938-45c9-8ce5-69642a9f1767"
                },
                new IdentityRole()
                {
                    Id= "B22698B8-42A2-4115-9631-1C2D1E2AC5F2",
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = "10520316-ac7a-48c3-b934-001431e4bf6a",
                },
                new IdentityRole()
                {
                    Id= "B22698B8-42A2-4115-9631-1C2D1E2AC5F3",
                    Name = "Teacher",
                    NormalizedName = "TEACHER",
                    ConcurrencyStamp = "59e27848-45e9-4926-aefe-2daf6678e6e7"
                });
        }
    }
}
