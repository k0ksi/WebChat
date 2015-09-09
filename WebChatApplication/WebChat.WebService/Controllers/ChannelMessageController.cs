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
    public class ChannelMessageController : BaseApiController
    {


        //POST api/ChannelMessage?channelName={channelName}
        [HttpPost]
        public IHttpActionResult SendMessage(
            [FromBody]MessageBindingModels model, string channelName)
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
            var channelId = this.Data.ChatRooms.FirstOrDefault(c => c.Name == channelName).Id;
            var sender = this.Data.Users.FirstOrDefault(u => u.UserName == senderName);

            var message = new Message()
            {
                SenderId = sender.Id,
                ChatRoomId = channelId,
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

        //GET  api/ChannelMessage?channelName={channelName}
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllMessages(string channelName)
        {
            var data = this.Data.Messages
                .Where(m => m.ChatRoom.Name == channelName)
                .OrderBy(m => m.MessageDateTime)
                .Select(MessageViewModels.Create);

            return this.Ok(data);
        }
    }
}