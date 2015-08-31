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
                    context.ChatRooms.Add(new ChatRoom() {Name = room,ChatroomCreator_Id = 0});
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
