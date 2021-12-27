using System.Collections.Generic;
using System.Threading.Tasks;
using Faroosom.BLL.DTO.User;

namespace Faroosom.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> GetUserByCredentialsAsync(CredentialsDto dto);

        Task SubscribeAsync(int subscriberId, int publisherId);
        Task UnsubscribeAsync(int subscriberId, int publisherId);

        Task<ICollection<UserDto>> GetAllUserSubscribersByIdAsync(int publisherId);
        Task<ICollection<UserDto>> GetAllSubscriptionByIdAsync(int userId);

        Task<UserDto> CreateUserAsync(CreateUserDto dto);
    }
}
