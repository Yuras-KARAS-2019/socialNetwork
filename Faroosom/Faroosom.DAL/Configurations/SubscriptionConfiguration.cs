using Faroosom.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faroosom.DAL.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasOne(x => x.Subscriber)
                .WithMany(y => y.Subscriptions)
                .HasForeignKey(x => x.SubscriberId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Publisher)
                .WithMany(y => y.SubscriptionsToUser)
                .HasForeignKey(x => x.PublisherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasAlternateKey(x => new {x.SubscriberId, x.PublisherId});
        }
    }
}
