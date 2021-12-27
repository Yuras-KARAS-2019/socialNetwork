using Faroosom.BLL.DTO.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faroosom.BLL.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> SendMessageAsync(CreateMessageDto dto);
        Task<ICollection<MessageDto>> GetMessagesByUsersAcync(int userId1, int userId2);
    }
}
