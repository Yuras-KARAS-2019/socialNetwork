using Faroosom.BLL.DTO.Message;
using Faroosom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Faroosom.MVC.Controllers
{
    public class ActionController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public ActionController(IUserService userService, IMessageService messageService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        public async Task<IActionResult> ShowAllUsers()
        {
            return View(await _userService.GetAllUsersAsync());
        }

        public async Task<IActionResult> ShowMySubcriptions()
        {
            return View(await _userService.GetAllSubscriptionByIdAsync(Global.User.Id));
        }

        public async Task<IActionResult> ShowMySubcribers()
        {
            return View(await _userService.GetAllUserSubscribersByIdAsync(Global.User.Id));
        }

        public async Task<IActionResult> SubscribeToUser()
        {
            return View(await _userService.GetAllUsersAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(int id)
        {
            await _userService.SubscribeAsync(Global.User.Id, id);
            return RedirectToAction("ShowMySubcriptions");
        }
        public async Task<IActionResult> UnsubcribeFromUser()
        {
            return View(await _userService.GetAllUsersAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Unsubcribe(int id)
        {
            await _userService.UnsubscribeAsync(Global.User.Id, id);
            return RedirectToAction("ShowMySubcriptions");
        }

        public async Task<IActionResult> ShowChats()
        {
            return View(await _userService.GetAllUsersAsync());
        }
        public async Task<IActionResult> ShowChatWithUser(int id)
        {
            return View(await _messageService.GetMessagesByUsersAcync(Global.User.Id, id));
        }
        public async Task<IActionResult> SendMessageToUser(CreateMessageDto dto)
        {
            await _messageService.SendMessageAsync(dto);
            return RedirectToAction("ShowChatWithUser", new { id = dto.ToId });
        }
    }
}
