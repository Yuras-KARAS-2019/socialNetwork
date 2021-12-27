using Faroosom.BLL.DTO.User;
using Faroosom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Faroosom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserDto>>> GetAll()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(id));
            }
            catch (KeyNotFoundException)
            {

                return NotFound();
            }
        }

        [HttpGet("{id}/subscribers")]
        public async Task<ActionResult<UserDto>> GetSubscribersById(int id)
        {
            return Ok(await _userService.GetAllUserSubscribersByIdAsync(id));
        }

        [HttpGet("{id}/subscriptions")]
        public async Task<ActionResult<UserDto>> GetSubscriptionsById(int id)
        {
            return Ok(await _userService.GetAllSubscriptionByIdAsync(id));
        }

        [HttpPost("byCreds")]
        public async Task<ActionResult<UserDto>> GetByCredentials([FromBody] CredentialsDto dto)
        {
            try
            {
                return Ok(await _userService.GetUserByCredentialsAsync(dto));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("subscribe/{subscriberId}/{publisherId}")]
        public async Task<ActionResult<UserDto>> Subscribe(int subscriberId, int publisherId)
        {
            await _userService.SubscribeAsync(subscriberId, publisherId);
            return Ok();
        }

        [HttpPost("unsubscribe/{subscriberId}/{publisherId}")]
        public async Task<ActionResult<UserDto>> Unsubscribe(int subscriberId, int publisherId)
        {
            await _userService.UnsubscribeAsync(subscriberId, publisherId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            return Ok(await _userService.CreateUserAsync(dto));
        }
    }
}
