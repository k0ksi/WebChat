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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.SentMessages)
                .WithOptional(m => m.Receiver)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.ReceivedMessages)
                .WithRequired(m => m.Sender)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.JoinedChatRooms).WithMany(c => c.Users).Map(uc =>
            {
                uc.MapLeftKey("ChatroomId");
                uc.MapRightKey("ApplicationUserId");
                uc.ToTable("UsersChatrooms");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}