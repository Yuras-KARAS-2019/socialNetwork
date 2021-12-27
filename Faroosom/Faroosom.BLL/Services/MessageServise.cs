using Faroosom.BLL.DTO.Message;
using Faroosom.BLL.Interfaces;
using Faroosom.DAL;
using Faroosom.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faroosom.BLL.Services
{
    public class MessageServise : IMessageService
    {
        private readonly FaroosomContext _context;

        public MessageServise(FaroosomContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }
        public async Task<ICollection<MessageDto>> GetMessagesByUsersAcync(int userId1, int userId2)
        {
            return await _context.Messages
                .Where(x => (x.FromId == userId1 && x.ToId == userId2) || (x.FromId == userId2 && x.ToId == userId1))
                .OrderBy(x => x.CreatedData)
                .Select(x => new MessageDto
                {
                    Id = x.Id,
                    Text = x.Text,
                    FromId = x.FromId,
                    ToId = x.ToId,
                    CreatedData = x.CreatedData
                })
                .ToListAsync();
        }

        public async Task<MessageDto> SendMessageAsync(CreateMessageDto dto)
        {
            var message = new Message
            {
                Text = dto.Text,
                FromId = dto.FromId,
                ToId = dto.ToId
            };
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return new MessageDto
            {
                Id = message.Id,
                Text = message.Text,
                FromId = message.FromId,
                ToId = message.ToId,
                CreatedData = message.CreatedData
            };
        }
    }
}
