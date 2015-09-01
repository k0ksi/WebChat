using SocialNetwork.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebChat.Models;
using WebChat.WebService.Models;

namespace WebChat.WebService.Controllers
{

    [Authorize]
    public class MessageController : BaseApiController
    {
        //POST api/messages
        [HttpPost]
        public IHttpActionResult SendMessage(
            [FromBody]MessageBindingModels model)
        {
            if (model == null)
            {
                return this.BadRequest("Model cannot be null (no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var reciever = this.Data.Users.FirstOrDefault(u => u.UserName == model.Reciever);

            if (reciever == null)
            {
                return this.BadRequest(string.Format(
                    "User {0} does not exist",
                    model.Reciever));
            }

            string senderName = this.User.Identity.Name;

            var sender = this.Data.Users.FirstOrDefault(u => u.UserName == senderName);

            var message = new Message()
            {
                SenderId = sender.Id,
                ReceiverId = reciever.Id,
                MessageDateTime = DateTime.Now,
                Content = model.Content

            };


            this.Data.Messages.Add(message);
            this.Data.SaveChanges();

            var data = this.Data.Messages
                .Where(p => p.Id == message.Id)
                .Select(MessageViewModels.Create)
                .FirstOrDefault();

            return this.Ok(data);
        }

        //GET api/messages
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllMessages()
        {            
            var data = this.Data.Messages
                .OrderBy(m => m.MessageDateTime)
                .Select(MessageViewModels.Create);

            return this.Ok(data);
        }

        //POST api/messages/{roomID}
        [HttpPost]
        public IHttpActionResult SendMessage(
            [FromBody]MessageBindingModels model,int id)
        {
            if (model == null)
            {
                return this.BadRequest("Model cannot be null (no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            

            string senderName = this.User.Identity.Name;

            var sender = this.Data.Users.FirstOrDefault(u => u.UserName == senderName);

            var message = new Message()
            {
                 SenderId = sender.Id,
                 ChatRoomId = id,
                 MessageDateTime = DateTime.Now,
                 Content = model.Content

            };


            this.Data.Messages.Add(message);
            this.Data.SaveChanges();

            var data = this.Data.Messages
                .Where(p => p.Id == message.Id)
                .Select(MessageViewModels.Create)
                .FirstOrDefault();

            return this.Ok(data);
        }

        //GET api/messages/{roomID}
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllMessages(int roomId)
        {
            var data = this.Data.Messages
                .Where(m => m.ChatRoomId == roomId)
                .OrderBy(m => m.MessageDateTime)
                .Select(MessageViewModels.Create);

            return this.Ok(data);
        }
    }
}