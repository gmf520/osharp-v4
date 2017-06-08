using System.Collections.Generic;
using System.Web.Http;


namespace OSharp.Demo.Web.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IDictionary<int, string> _values = new Dictionary<int, string>() { { 1, "No.1 value" }, { 2, "No.2 value" } };

        [HttpGet]
        [Route("api/values/value")]
        public IHttpActionResult Value()
        {
            return Ok("Hello My Api");
        }
        
        public IHttpActionResult Get(int id)
        {
            string value;
            if (!_values.TryGetValue(id, out value))
            {
                return NotFound();
            }
            return Ok(value);
        }
    }
}