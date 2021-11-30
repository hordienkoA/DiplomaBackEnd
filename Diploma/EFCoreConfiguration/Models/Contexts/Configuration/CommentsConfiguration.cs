using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreConfiguration.Models.Contexts.Configuration
{
    internal class CommentsConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.Receiver).WithMany(u => u.Incomming).HasForeignKey(c => c.ReceiverId);
            builder.HasOne(c => c.Sender).WithMany(u => u.Outgoing).HasForeignKey(c => c.SenderId);
        }
    }
}
