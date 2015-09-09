using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SocialNetwork.Services.Controllers;
using WebChat.Models;

namespace WebChat.WebService.Controllers
{
    public class UsersController : BaseApiController
    {
        // Filter by letters from username
        // get api/users/filter
        [HttpGet]
        [Route("api/users/filter")]
        public IHttpActionResult ListUsersByFilter([FromUri] string keyword = null)
        {
            IQueryable<ApplicationUser> users = null;
            if (keyword != null)
            {
                users = this.Data.Users
                    .Where(u => u.UserName.Contains(keyword));
            }

            if (users != null)
            {
                var result = users
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.UserName,
                    u.FullName,
                    u.BirthDate
                });

                return this.Ok(result);
            }
            return this.Ok("No users to be shown with the current filter");
        }

        // Search by particular username
        // get api/users/{username}
        [HttpGet]
        [Route("api/users/{username}")]
        public IHttpActionResult GetUserByName(string username)
        {
            var user = this.Data.Users
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                FullName = user.FullName,
                ImageDataUrl = user.ImageUrl
            });
        }

        // get api/chatrooms
        [HttpGet]
        [Route("api/chatrooms")]
        public IHttpActionResult GetUserChatrooms()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users
                .FirstOrDefault(u => u.Id == currentUserId);

            if (currentUser != null)
            {
                var chatrooms = currentUser.JoinedChatRooms.Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                });

                return this.Ok(chatrooms);
            }

            return this.BadRequest("No user is logged in the system");
        }
    }
}