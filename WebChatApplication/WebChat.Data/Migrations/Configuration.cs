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
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (!context.Users.Any())
            {
                List<ApplicationUser> users = SeedUsers(context);
                foreach (var applicationUser in users)
                {
                    context.Users.Add(applicationUser);
                    context.SaveChanges();
                    var userId = context.Users.Where(u => u.Email == applicationUser.Email).Select(u => u.Id).FirstOrDefault();
                    userManager.UpdateSecurityStamp(userId);
                }
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
                    context.ChatRooms.Add(new ChatRoom() { Name = room });
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
                        MessageDateTime = DateTime.Now.AddDays(-1),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "I'm fine, just a bit tired.",
                        MessageDateTime = DateTime.Now,
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "Did you watched the latest lecture about advanced tree structures?",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(-3),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "Yes I did!",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(-2),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "Wow! Feeling so special.",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(-4),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "Nananananananananna!!!",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(-3),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id,
                    },
                    new Message()
                    {
                        Content = "What a game it was yesterday! Did you watched Valencia against Deportivo?",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(0),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "I was too tired and went to sleep :(!!",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(0),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "I cooked musaka yesterday. What have you cooked lately?",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(-10),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                    new Message()
                    {
                        Content = "I cooked cream brulle.",
                        ChatRoomId = null,
                        MessageDateTime = DateTime.Now.AddDays(-9),
                        SenderId = context.Users.First().Id,
                        ReceiverId = context.Users.OrderBy(u=>u.Id).Skip(2).FirstOrDefault().Id
                    },
                      new Message()
                    {
                        Content = "Wassup dudes",
                        ChatRoomId = 1,
                        MessageDateTime = DateTime.Now.AddDays(-9),
                        SenderId = context.Users.First().Id,
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
          
            var passwordHash = new PasswordHasher();
            var user = new ApplicationUser()
            {
                UserName = "dimityr.jechev",
                Email = "mitko@abv.bg",
                PasswordHash = passwordHash.HashPassword("Password@123")
            };
            var user1 = new ApplicationUser()
            {
                UserName = "HristoVutov",
                Email = "ico@abv.bg",
                PasswordHash = passwordHash.HashPassword("Password@123")
            };
            var user2 = new ApplicationUser()
            {
                UserName = "manito_17711",
                Email = "manito@abv.bg",
                PasswordHash = passwordHash.HashPassword("Password@123")
            };
            var user3 = new ApplicationUser()
            {
                UserName = "Nichigo",
                Email = "Nichigo@abv.bg",
                PasswordHash = passwordHash.HashPassword("Password@123")
            };
            var user4 = new ApplicationUser()
            {
                UserName = "zh.stoqnov",
                Email = "zh.stoqnov@abv.bg",
                PasswordHash = passwordHash.HashPassword("Password@123")
            };

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                user4,
                user3,
                user1,
                user2,
                user
            };

         

           

            return users;
        } 
    }
}
