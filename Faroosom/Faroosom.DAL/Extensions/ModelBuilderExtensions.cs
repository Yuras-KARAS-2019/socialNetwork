using Faroosom.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Faroosom.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(FaroosomContext).Assembly);
        }

        public static void Seed(this ModelBuilder builder)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Roma",
                    LastName = "Durda",
                    Login = "Sniper2001",
                    Password = "QWERTY1234",
                },

                new User
                {
                    Id = 2,
                    Name = "Nadia",
                    LastName = "Durda",
                    Login = "Killer1995",
                    Password = "QWERTY1234",
                }
            };

            var subscriptions = new List<Subscription>
            {
                new Subscription
                {
                    Id = 1,
                    PublisherId = 1,
                    SubscriberId = 2
                },

                new Subscription
                {
                    Id = 2,
                    PublisherId = 2,
                    SubscriberId = 1
                }
            };
            builder.Entity<User>()
                .HasData(users);
            builder.Entity<Subscription>()
                .HasData(subscriptions);
        }
    }
}