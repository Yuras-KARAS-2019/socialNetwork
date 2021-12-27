using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Faroosom.BLL.DTO.User;
using Faroosom.BLL.Services;
using Faroosom.DAL;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Faroosom.BLL.Tests
{
    public class UserServiceTests : IDisposable
    {
        private readonly FaroosomContext _context;
        private readonly UserService _service;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<FaroosomContext>().UseInMemoryDatabase("TestUserDatabase").Options;
            _context = new FaroosomContext(options);
            _context.Database.EnsureCreated();

            _service = new UserService(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }


        [Fact]
        public async Task GetUserByIdAsync_NotExists_ThrowsKeyNotFoundException()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetUserByIdAsync(-1));
        }

        [Fact]
        public async Task GetUserByIdAsync_Exists_Success()
        {
            var user1 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "Test",
                LastName = "Test",
                Login = "Test",
                Password = "pass"
            });

            var user2 = await _service.GetUserByIdAsync(user1.Id);
            Assert.Equal(user1, user2);
        }

        [Fact]
        public async Task SubscribeAsync_Valid_Success()
        {
            var user1 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "user1",
                LastName = "Test1",
                Login = "Test1",
                Password = "Test1"
            });
            var user2 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "user2",
                LastName = "Test2",
                Login = "Test2",
                Password = "Test2"
            });
            await _service.SubscribeAsync(user1.Id, user2.Id);
            var actualSubscribtion = await _context.Subscriptions.FirstOrDefaultAsync(s => s.PublisherId == user2.Id && s.SubscriberId == user1.Id);
            Assert.NotNull(actualSubscribtion);
            Assert.Equal(user2.Id, actualSubscribtion.PublisherId);
            Assert.Equal(user1.Id, actualSubscribtion.SubscriberId);
        }

        [Fact]
        public async Task UnsubscribeAsync_Valid_Success()
        {
            // Arrange 
            var user1 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "user1",
                LastName = "Test1",
                Login = "Test1",
                Password = "Test1"
            });
            var user2 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "user2",
                LastName = "Test2",
                Login = "Test2",
                Password = "Test2"
            });
            await _service.SubscribeAsync(user1.Id, user2.Id);
            // To ensure that subscription was created
            var actualSubscribtion = await _context.Subscriptions.FirstOrDefaultAsync(s => s.PublisherId == user2.Id && s.SubscriberId == user1.Id);
            Assert.NotNull(actualSubscribtion);
            //Act
            await _service.UnsubscribeAsync(user1.Id, user2.Id);
            var result = await _context.Subscriptions.AnyAsync(s => s.PublisherId == user2.Id && s.SubscriberId == user1.Id);
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllSubscriptiosAsync_Success()
        {
            // Arrange
            var user1 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "user1",
                LastName = "Test1",
                Login = "Test1",
                Password = "Test1"
            });
            var user2 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "user2",
                LastName = "Test2",
                Login = "Test2",
                Password = "Test2"
            });
            var user3 = await _service.CreateUserAsync(new CreateUserDto
            {
                Name = "user3",
                LastName = "Test3",
                Login = "Test3",
                Password = "Test3"
            });
            await _service.SubscribeAsync(user1.Id, user2.Id);
            await _service.SubscribeAsync(user1.Id, user3.Id);

            //Act
            var subscribtions = await _service.GetAllSubscriptionByIdAsync(user1.Id);

            //Assert
            Assert.Contains(user2, subscribtions);
            Assert.Contains(user3, subscribtions);
        }

    }
}
