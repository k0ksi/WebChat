using System.Data.Entity.Migrations;

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
    }
}
