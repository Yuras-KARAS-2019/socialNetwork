using Faroosom.BLL.DTO.Message;
using Faroosom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faroosom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        [HttpGet("{userId1}/{userId2}")]
        public async Task<ActionResult<ICollection<MessageDto>>> GetChat(int userId1, int userId2)
        {
            return Ok(await _messageService.GetMessagesByUsersAcync(userId1, userId2));
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> Create([FromBody] CreateMessageDto dto)
        {
            return Ok(await _messageService.SendMessageAsync(dto));
        }
    }
}
