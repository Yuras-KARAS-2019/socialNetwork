using Faroosom.BLL.DTO.Message;
using Faroosom.BLL.DTO.User;
using Faroosom.BLL.Interfaces;
using Faroosom.BLL.Services;
using Faroosom.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Faroosom.ConsoleApp
{
    class Program
    {
        static IUserService _userService;
        static IMessageService _messageService;

        static UserDto _user;

        static async Task Main(string[] args)
        {
            Init();
            await Login();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(
                    "1 - Show all users\n" +
                    "2 - Show my subcriptions\n" +
                    "3 - Show my subcribers\n" +
                    "4 - Subscribe to user\n" +
                    "5 - Unsubscribe from user\n" +
                    "6 - Show chat with user\n" +
                    "7 - Write message to user\n" +
                    "8 - Exit");
                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            await ShowAllUsers();
                            break;
                        case "2":
                            await ShowMySubcriptions();
                            break;
                        case "3":
                            await ShowMySubcribers();
                            break;
                        case "4":
                            await SubscribeToUser();
                            break;
                        case "5":
                            await UnsubcribeFromUser();
                            break;
                        case "6":
                            await ShowChatWithUser();
                            break;
                        case "7":
                            await SendMessageToUser();
                            break;
                        case "8":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Incorrect input");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Усе погано");
                }
            }
        }

        static async Task SendMessageToUser()
        {
            Console.WriteLine("Ented user id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Ented message text");
            string text = Console.ReadLine();
            await _messageService.SendMessageAsync(new CreateMessageDto { FromId = _user.Id, Text = text, ToId = id });
        }

        static async Task ShowChatWithUser()
        {
            Console.WriteLine("Ented user id");
            int id = int.Parse(Console.ReadLine());
            var messages = await _messageService.GetMessagesByUsersAcync(_user.Id, id);
            foreach (var message in messages)
            {
                Console.WriteLine("====================");
                Console.Write(message.FromId == _user.Id ? "From me" : "To me");
                Console.WriteLine($" ({message.CreatedData}) :");
                Console.WriteLine("====================");
                Console.WriteLine(message.Text);
                Console.WriteLine("====================");
            }
        }

        static async Task UnsubcribeFromUser()
        {
            Console.WriteLine("Ented user id");
            int id = int.Parse(Console.ReadLine());
            await _userService.UnsubscribeAsync(_user.Id, id);
        }

        static async Task SubscribeToUser()
        {
            Console.WriteLine("Ented user id");
            int id = int.Parse(Console.ReadLine());
            await _userService.SubscribeAsync(_user.Id, id);
        }

        static async Task ShowAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }
        static async Task ShowMySubcriptions()
        {
            var users = await _userService.GetAllSubscriptionByIdAsync(_user.Id);
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }

        static async Task ShowMySubcribers()
        {
            var users = await _userService.GetAllUserSubscribersByIdAsync(_user.Id);
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }


        static async Task Login()
        {
            bool logined = false;
            while (!logined)
            {
                Console.WriteLine("Please enter login:");
                var login = Console.ReadLine();
                Console.WriteLine("Please enter password:");
                var password = Console.ReadLine();
                try
                {
                    _user = await _userService.GetUserByCredentialsAsync(new CredentialsDto
                    {
                        Login = login,
                        Password = password
                    });
                    Console.WriteLine($"Welcome {_user.Name} {_user.LastName}!");
                    logined = true;

                }
                catch (AuthenticationException)
                {
                    Console.WriteLine("Incorent login and/or password");
                }
            }
        }

        static void Init()
        {
            var options = new DbContextOptionsBuilder<FaroosomContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Faroosomdb;Trusted_Connection=True;").Options;
            var context = new FaroosomContext(options);
            _userService = new UserService(context);
            _messageService = new MessageServise(context);
        }
    }
}
