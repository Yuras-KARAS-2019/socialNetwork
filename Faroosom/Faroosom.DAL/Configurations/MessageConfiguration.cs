using Faroosom.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faroosom.DAL.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(x => x.From)
                .WithMany(y => y.MessagesFrom)
                .HasForeignKey(x => x.FromId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.To)
                .WithMany(y => y.MessagesTo)
                .HasForeignKey(x => x.ToId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Text)
                .IsRequired();
        }
    }
}
