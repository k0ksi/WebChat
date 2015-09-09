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
    public class PersonalMessagesController : BaseApiController
    {
        //POST api/PersonalMessages
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

        //GET api/PersonalMessages
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllMessages()
        {
            string senderName = this.User.Identity.Name;
            var data = this.Data.Messages
                .Where(m => m.Sender.UserName == senderName)
                .OrderBy(m => m.MessageDateTime)
                .Select(MessageViewModels.Create);

            return this.Ok(data);
        }

        //GET api/PersonalMessages/{messageId}
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetMessages(int id)
        {
            string senderName = this.User.Identity.Name;
            var data = this.Data.Messages
                .Where(m => m.Sender.UserName == senderName && m.Id == id)
                .Select(MessageViewModels.Create);

            return this.Ok(data);
        }
    }
}