using WebChat.Data.Repositories;
using WebChat.Models;

namespace WebChat.Data
{
    public interface IWebChatData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<ChatRoom> Chatrooms { get; }

        IRepository<Message> Messages { get; }

        int SaveChanges();
    }
}