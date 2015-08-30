using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebChat.Models;

namespace WebChat.WebService.Models
{
    public class MessageViewModels
    {
        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime sendOn { get; set; }

        public static Expression<Func<Message, MessageViewModels>> Create
        {
            get
            {
                return p => new MessageViewModels
                {
                    
                    Content = p.Content,
                    Author = p.Sender.UserName,
                    sendOn = p.MessageDateTime
                    
                   
                };
            }
        }
    }
}