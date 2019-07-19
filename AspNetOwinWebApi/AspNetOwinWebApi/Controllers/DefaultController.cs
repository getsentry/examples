using System.Collections.Generic;
using System.Web.Http;

namespace AspNetOwinWebApi.Controllers
{
    public class DefaultController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
