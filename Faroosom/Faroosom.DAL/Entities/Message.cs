using System;

namespace Faroosom.DAL.Entities
{
    public record Message
    {
        public int Id { get; set; }

        public int FromId { get; set; }
        public virtual User From { get; set; }

        public int ToId { get; set; }
        public virtual User To { get; set; }

        public string Text { get; set; }
        public DateTime CreatedData { get; set; } = DateTime.Now;
    }
}