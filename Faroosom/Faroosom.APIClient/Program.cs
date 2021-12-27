using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Faroosom.APIClient
{
    class Program
    {
        static HttpClient _client;

        static User _user;
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
                            await ShawAllUsers();
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
            await _client.PostAsJsonAsync("api/messages", new Message { FromId = _user.Id, Text = text, ToId = id });
        }

        static async Task ShowChatWithUser()
        {
            Console.WriteLine("Ented user id");
            int id = int.Parse(Console.ReadLine());
            var messages = await _client.GetFromJsonAsync<ICollection<Message>>($"api/messages/{_user.Id}/{id}");
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
            await _client.PostAsync($"api/users/unsubscribe/{_user.Id}/{id}", null);
        }

        static async Task SubscribeToUser()
        {
            Console.WriteLine("Ented user id");
            int id = int.Parse(Console.ReadLine());
            await _client.PostAsync($"api/users/subscribe/{_user.Id}/{id}", null);
        }

        static async Task ShawAllUsers()
        {
            var users = await _client.GetFromJsonAsync<ICollection<User>>($"api/users");
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }
        static async Task ShowMySubcriptions()
        {
            var users = await _client.GetFromJsonAsync<ICollection<User>>($"/api/Users/{_user.Id}/subscriptions");
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }

        static async Task ShowMySubcribers()
        {
            var users = await _client.GetFromJsonAsync<ICollection<User>>($"/api/Users/{_user.Id}/subscribers");
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
                var response = await _client.PostAsJsonAsync("api/users/byCreds", new Credentionals
                {
                    Login = login,
                    Password = password
                });

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Incorent login and/or password");
                }
                else
                {
                    _user = await response.Content.ReadFromJsonAsync<User>();
                    Console.WriteLine($"Welcome {_user.Name} {_user.LastName}!");
                    logined = true;
                }
            }
        }

        static void Init()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:58655")
            };
        }
    }
}
