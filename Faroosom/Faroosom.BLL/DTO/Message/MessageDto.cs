using System;

namespace Faroosom.BLL.DTO.Message
{
    public record MessageDto
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedData { get; set; }
    }
}
