using System;
using System.Collections.Generic;

namespace Faroosom.DAL.Entities
{
    public record User
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedData { get; set; } = DateTime.Now;

        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public virtual ICollection<Subscription> SubscriptionsToUser { get; set; } = new List<Subscription>();

        public virtual ICollection<Message> MessagesFrom { get; set; } = new List<Message>();

        public virtual ICollection<Message> MessagesTo { get; set; } = new List<Message>();
    }
}
