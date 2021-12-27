using Faroosom.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faroosom.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Login)
                .IsRequired();

            builder.HasAlternateKey(x => x.Login);

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();
        }
    }
}