using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            
        }

        private IList<ApplicationUser> SeedUsers(WebChatContext context)
        {
            var usernames = new string[]
            {
                "dimityr.jechev",
                "HristoVutov",
                "manito_17711",
                "Nichigo",
                "zh.stoqnov"
            };
        }
    }
}
