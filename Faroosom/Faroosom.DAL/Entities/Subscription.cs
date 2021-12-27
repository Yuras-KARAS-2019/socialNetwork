using System;

namespace Faroosom.DAL.Entities
{
    public record Subscription
    {
        public int Id { get; set; }

        public int SubscriberId { get; set; }
        public virtual User Subscriber { get; set; }

        public int PublisherId { get; set; }
        public virtual User Publisher { get; set; }

        public DateTime CreatedData { get; set; } = DateTime.Now;
    }
}
