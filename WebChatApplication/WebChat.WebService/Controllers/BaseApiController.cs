using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Services.Controllers
{
    using System.Web.Http;
    using WebChat.Data;

    public class BaseApiController : ApiController
    {
        public BaseApiController()
            : this(new WebChatContext())
        {
        }

        public BaseApiController(WebChatContext data)
        {
            this.Data = data;
        }

        protected WebChatContext Data { get; set; }
    }
}