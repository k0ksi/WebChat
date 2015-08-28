using System.Linq;
using System.Web.Http;
using WebChat.Data;

namespace WebChat.WebService.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        [AllowAnonymous]
        // GET api/values
        public int Get()
        {
            var ctx = new WebChatContext();
            var usersCount = ctx.Users.Count();
            return usersCount;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
