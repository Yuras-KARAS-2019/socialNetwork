using Faroosom.BLL.DTO.User;
using Faroosom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Faroosom.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CredentialsDto dto)
        {
            try
            {
                Global.User = await _userService.GetUserByCredentialsAsync(dto);
                return View();
            }
            catch (AuthenticationException)
            {

                ModelState.AddModelError("", "Incorrect login and/or password");
                return View(dto);
            }
        }

        [HttpPost]
        new public IActionResult SignOut()
        {
            Global.User = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
