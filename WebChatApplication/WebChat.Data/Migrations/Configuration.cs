using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebChat.Models;

namespace WebChat.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebChatContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WebChatContext context)
        {
            if (!context.Users.Any())
            {
                List<ApplicationUser> users = SeedUsers(context);
                foreach (var applicationUser in users)
                {
                    context.Users.Add(applicationUser);
                }
                context.SaveChanges();
            }

            if (!context.ChatRooms.Any())
            {
                var rooms = new string[]
                {
                    "gaming",
                    "programming",
                    "lifestyle",
                    "football",
                    "cooking"
                };
                foreach (var room in rooms)
                {
                    context.ChatRooms.Add(new ChatRoom() {Name = room});
                }

                context.SaveChanges();
            }

            if (!context.Messages.Any())
            {
                var messages = new Message[]
                {
                    new Message()
                    {
                        Content = "Hi! How are you?",
                        ChatRoomId = 1,
                        MessageDateTime = DateTime.Now.AddDays(-1),
                        SenderId = "0f896c71-e0d7-4fc6-92cc-8d17e5c3f4c5",
                        ReceiverId = "9689e7d4-9ae4-4e90-a1d5-37c8d3db20b9",
                    },
                    new Message()
                    {
                        Content = "I'm fine, just a bit tired.",
                        ChatRoomId = 1,
                        MessageDateTime = DateTime.Now,
                        SenderId = "9689e7d4-9ae4-4e90-a1d5-37c8d3db20b9",
                        ReceiverId = "0f896c71-e0d7-4fc6-92cc-8d17e5c3f4c5",
                    },
                    new Message()
                    {
                        Content = "Did you watched the latest lecture about advanced tree structures?",
                        ChatRoomId = 2,
                        MessageDateTime = DateTime.Now.AddDays(-3),
                        SenderId = "2f13ac24-7174-44ac-82b3-2dd914a8f858",
                        ReceiverId = "fc46186a-9059-44f6-9a05-30776acfef80",
                    },
                    new Message()
                    {
                        Content = "Yes I did!",
                        ChatRoomId = 2,
                        MessageDateTime = DateTime.Now.AddDays(-2),
                        SenderId = "fc46186a-9059-44f6-9a05-30776acfef80",
                        ReceiverId = "2f13ac24-7174-44ac-82b3-2dd914a8f858",
                    },
                    new Message()
                    {
                        Content = "Wow! Feeling so special.",
                        ChatRoomId = 3,
                        MessageDateTime = DateTime.Now.AddDays(-4),
                        SenderId = "d69b5d4b-4e11-4008-a354-914fef18803b",
                        ReceiverId = "9689e7d4-9ae4-4e90-a1d5-37c8d3db20b9",
                    },
                    new Message()
                    {
                        Content = "Nananananananananna!!!",
                        ChatRoomId = 3,
                        MessageDateTime = DateTime.Now.AddDays(-3),
                        SenderId = "9689e7d4-9ae4-4e90-a1d5-37c8d3db20b9",
                        ReceiverId = "d69b5d4b-4e11-4008-a354-914fef18803b",
                    },
                    new Message()
                    {
                        Content = "What a game it was yesterday! Did you watched Valencia against Deportivo?",
                        ChatRoomId = 4,
                        MessageDateTime = DateTime.Now.AddDays(0),
                        SenderId = "0f896c71-e0d7-4fc6-92cc-8d17e5c3f4c5",
                        ReceiverId = "2f13ac24-7174-44ac-82b3-2dd914a8f858",
                    },
                    new Message()
                    {
                        Content = "I was too tired and went to sleep :(!!",
                        ChatRoomId = 4,
                        MessageDateTime = DateTime.Now.AddDays(0),
                        SenderId = "2f13ac24-7174-44ac-82b3-2dd914a8f858",
                        ReceiverId = "0f896c71-e0d7-4fc6-92cc-8d17e5c3f4c5",
                    },
                    new Message()
                    {
                        Content = "I cooked musaka yesterday. What have you cooked lately?",
                        ChatRoomId = 5,
                        MessageDateTime = DateTime.Now.AddDays(-10),
                        SenderId = "2f13ac24-7174-44ac-82b3-2dd914a8f858",
                        ReceiverId = "9689e7d4-9ae4-4e90-a1d5-37c8d3db20b9",
                    },
                    new Message()
                    {
                        Content = "I cooked cream brulle.",
                        ChatRoomId = 5,
                        MessageDateTime = DateTime.Now.AddDays(-9),
                        SenderId = "9689e7d4-9ae4-4e90-a1d5-37c8d3db20b9",
                        ReceiverId = "2f13ac24-7174-44ac-82b3-2dd914a8f858",
                    }
                };

                foreach (var message in messages)
                {
                    context.Messages.Add(message);
                }

                context.SaveChanges();
            }
        }

        private static List<ApplicationUser> SeedUsers(WebChatContext context)
        {
            var user = new ApplicationUser()
            {
                UserName = "dimityr.jechev",
                Email = "mitko@abv.bg"
            };
            var user1 = new ApplicationUser()
            {
                UserName = "HristoVutov",
                Email = "ico@abv.bg"
            };
            var user2 = new ApplicationUser()
            {
                UserName = "manito_17711",
                Email = "manito@abv.bg"
            };
            var user3 = new ApplicationUser()
            {
                UserName = "Nichigo",
                Email = "Nichigo@abv.bg"
            };
            var user4 = new ApplicationUser()
            {
                UserName = "zh.stoqnov",
                Email = "zh.stoqnov@abv.bg"
            };

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                user4,
                user3,
                user1,
                user2,
                user
            };

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string password = "TestPass_1";

            var userCreateResult = userManager.Create(user, password);
            var userCreateResult1 = userManager.Create(user1, password);
            var userCreateResult2 = userManager.Create(user2, password);
            var userCreateResult3 = userManager.Create(user3, password);
            var userCreateResult4 = userManager.Create(user4, password);

            if (!userCreateResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(
                    Environment.NewLine,
                    userCreateResult.Errors));
            }
            if (!userCreateResult1.Succeeded)
            {
                throw new InvalidOperationException(string.Join(
                    Environment.NewLine,
                    userCreateResult.Errors));
            }
            if (!userCreateResult2.Succeeded)
            {
                throw new InvalidOperationException(string.Join(
                    Environment.NewLine,
                    userCreateResult.Errors));
            }
            if (!userCreateResult3.Succeeded)
            {
                throw new InvalidOperationException(string.Join(
                    Environment.NewLine,
                    userCreateResult.Errors));
            }
            if (!userCreateResult4.Succeeded)
            {
                throw new InvalidOperationException(string.Join(
                    Environment.NewLine,
                    userCreateResult.Errors));
            }

            return users;
        } 
    }
}
