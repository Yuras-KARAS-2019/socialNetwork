
namespace Faroosom.BLL.DTO.Message
{
    public class CreateMessageDto
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Text { get; set; }
    }
}
