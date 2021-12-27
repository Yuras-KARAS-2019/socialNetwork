using Faroosom.BLL.DTO.User;
using Faroosom.BLL.Interfaces;
using Faroosom.DAL;
using Faroosom.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Faroosom.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly FaroosomContext _context;

        public UserService(FaroosomContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }

        public async Task<ICollection<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users.Select(x => new UserDto
            {
                Id = x.Id,
                Age = x.Age,
                Name = x.Name,
                LastName = x.LastName,
                Login = x.Login
            }).ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) ??
                throw new KeyNotFoundException($"Subcrciption does not exist with");
            var result = new UserDto
            {
                Id = user.Id,
                Age = user.Age,
                Name = user.Name,
                LastName = user.LastName,
                Login = user.Login
            };
            return result;
        }

        public async Task SubscribeAsync(int subscriberId, int publisherId)
        {
            var subscription = new Subscription()
            {
                SubscriberId = subscriberId,
                PublisherId = publisherId,
            };
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UnsubscribeAsync(int subscriberId, int publisherId)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x =>
                                   x.PublisherId == publisherId &&
                                   x.SubscriberId == subscriberId) 
                ?? throw new KeyNotFoundException($"Subcrciption does not exist with");
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserDto>> GetAllUserSubscribersByIdAsync(int publisherId)
        {
            return await _context.Subscriptions
                .Where(x => x.PublisherId == publisherId)
                .Select(x => new UserDto
                {
                    Id = x.Subscriber.Id,
                    Age = x.Subscriber.Age,
                    Name = x.Subscriber.Name,
                    LastName = x.Subscriber.LastName,
                    Login = x.Subscriber.Login
                }).ToListAsync();
        }

        public async Task<ICollection<UserDto>> GetAllSubscriptionByIdAsync(int userId)
        {
            return await _context.Subscriptions
                .Where(x => x.SubscriberId == userId)
                .Select(x => new UserDto
                {
                    Id = x.Publisher.Id,
                    Age = x.Publisher.Age,
                    Name = x.Publisher.Name,
                    LastName = x.Publisher.LastName,
                    Login = x.Publisher.Login
                }).ToListAsync();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Age = dto.Age,
                LastName = dto.LastName,
                Login = dto.Login,
                Name = dto.Name,
                Password = dto.Password
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Id = user.Id,
                Age = user.Age,
                Name = user.Name,
                LastName = user.LastName,
                Login = user.Login
            };
        }

        public async Task<UserDto> GetUserByCredentialsAsync(CredentialsDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == dto.Login && x.Password == dto.Password)
                ?? throw new AuthenticationException("Incorrect login or password");
            return new UserDto
            {
                Id = user.Id,
                Age = user.Age,
                Name = user.Name,
                LastName = user.LastName,
                Login = user.Login
            };

        }
    }
}
