using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreConfiguration.Models.Contexts.Configuration
{
    class UsersWithRoleConfiguration: IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F1",
                    UserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5E1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F2",
                    UserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5E2"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F3",
                    UserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5E3"
                });

        }
    }
}
