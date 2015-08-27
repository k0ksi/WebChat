using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebChat.Data.Migrations;
using WebChat.Models;

namespace WebChat.Data
{
    public class WebChatContext : IdentityDbContext<ApplicationUser>
    {
        public WebChatContext()
            : base("WebChatContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebChatContext, Configuration>());
        }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<ChatRoom> ChatRooms { get; set; }

        public static WebChatContext Create()
        {
            return new WebChatContext();
        }
    }
}