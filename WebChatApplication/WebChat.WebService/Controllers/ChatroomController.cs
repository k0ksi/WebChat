using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SocialNetwork.Services.Controllers;
using WebChat.Models;
using WebChat.WebService.Models;

namespace WebChat.WebService.Controllers
{
    [Authorize]
    [RoutePrefix("api/chatroom")]
    public class ChatroomController : BaseApiController
    {
        // POST api/Chatroom
        [HttpPost]
        public IHttpActionResult CreateChatroom(ChatroomBindingModel model)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var doesChatroomAlreadyExist = this.Data.ChatRooms.FirstOrDefault(c => c.Name == model.Name);

            if (doesChatroomAlreadyExist != null)
            {
                return this.BadRequest("There is already a chatroom with the same name.");
            }

            var chatroom = new ChatRoom()
            {
                Name = model.Name
            };

            chatroom.Users.Add(user);
            this.Data.ChatRooms.Add(chatroom);
            this.Data.SaveChanges();

            return this.Ok(
                new
                {
                    Message = "Chatroom created succesfully.",
                    Chatroom = new ChatroomBindingModel()
                    {
                        Name = chatroom.Name
                    }
                });
        }

        // GET api/chatroom/GetByName?name={name}
        [HttpGet]
        [Route("GetByName")]
        public IHttpActionResult GetChatroomByName(string name)
        {
            var chatroom = this.Data.ChatRooms.FirstOrDefault(c => c.Name == name);

            if (chatroom != null)
            {
                return this.Ok(new ChatroomViewModel
                {
                    Id = chatroom.Id,
                    Name = chatroom.Name,
                    Users = chatroom.Users.Select(u => new UserInfoShortViewModel
                    {
                        FullName = u.FullName,
                        UserName = u.UserName,
                        Email = u.Email
                    }).ToList()
                });
            }

            return this.NotFound();
        }

        // GET api/chatroom/{id}
        [HttpGet]
        public IHttpActionResult GetChatroomById(int id)
        {
            var chatroom = this.Data.ChatRooms.Find(id);

            if (chatroom != null)
            {
                return this.Ok(new ChatroomViewModel()
                {
                    Id = chatroom.Id,
                    Name = chatroom.Name,
                    Users = chatroom.Users.Select(u => new UserInfoShortViewModel()
                    {
                        UserName = u.UserName,
                        Email = u.Email,
                        FullName = u.FullName
                    }).ToList()
                });
            }

            return this.NotFound();
        }

        // GET api/chatroom
        [HttpGet]
        public IHttpActionResult GetAllChatrooms()
        {
            var chatrooms = this.Data.ChatRooms
                .Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    UsersCount = c.Users.Count
                })
                .ToList();

            return this.Ok(chatrooms);
        }

        // GET api/chatroom/GetCount
        [HttpGet]
        [Route("GetCount")]
        public IHttpActionResult GetChatroomsCount()
        {
            var chatroomsCount = this.Data.ChatRooms.Count();

            return this.Ok(chatroomsCount);
        }

        // GET api/chatroom/GetUsersByChatroom?name={name}
        [HttpGet]
        [Route("GetUsersByChatroom")]
        public IHttpActionResult GetUsersByChatroomName(string name)
        {
            var chatroom = this.Data.ChatRooms.FirstOrDefault(c => c.Name == name);

            if (chatroom != null)
            {
                var users = new ChatroomViewModel()
                {
                    Id = chatroom.Id,
                    Name = chatroom.Name,
                    Users = chatroom.Users
                        .Select(u => new UserInfoShortViewModel()
                        {
                            UserName = u.UserName,
                            Email = u.Email,
                            FullName = u.FullName

                        }).ToList()
                };

                return this.Ok(users);
            }

            return this.NotFound();
        }

        // DELETE api/chatroom/{id}
        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            var chatroom = this.Data.ChatRooms.Find(id);

            if (chatroom != null)
            {
                this.Data.ChatRooms.Remove(chatroom);
                this.Data.SaveChanges();
                return this.Ok("Chatroom deleted.");
            }

            return this.NotFound();
        }

        // DELETE api/chatroom/DeleteByName?name={name}
        [HttpDelete]
        [Route("DeleteByName")]
        public IHttpActionResult DeleteByName(string name)
        {
            var chatroom = this.Data.ChatRooms.FirstOrDefault(c => c.Name == name);

            if (chatroom != null)
            {
                this.Data.ChatRooms.Remove(chatroom);
                this.Data.SaveChanges();
                return this.Ok("Chatroom deleted.");
            }

            return this.NotFound();
        }

        // POST api/chatroom/Join?name={name}
        [HttpPost]
        [Route("Join")]
        public IHttpActionResult JoinChatRoom(string name)
        {
            var chatroom = this.Data.ChatRooms.FirstOrDefault(c => c.Name == name);

            if (chatroom == null)
            {
                return this.NotFound();
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.Data.Users.Find(currentUserId);

            if (user == null || chatroom.Users.Contains(user))
            {
                return this.BadRequest("You've already joined this  chatroom.");
            }

            chatroom.Users.Add(user);
            this.Data.SaveChanges();

            return this.Ok(new
            {
                ChatroomId = chatroom.Id,
                ChatRoomName = chatroom.Name
            });
        }

        // POST api/chatroom/Leave?name={name}
        [HttpPost]
        [Route("Leave")]
        public IHttpActionResult LeaveChatroom(string name)
        {
            var chatroom = this.Data.ChatRooms
                .FirstOrDefault(c => c.Name == name);

            if (chatroom == null)
            {
                return this.NotFound();
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.Data.Users.Find(currentUserId);

            if (user == null || !chatroom.Users.Contains(user))
            {
                return this.BadRequest();
            }

            chatroom.Users.Remove(user);
            this.Data.SaveChanges();
            return this.Ok();
        }
    }
}