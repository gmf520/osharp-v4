using OSharp.Core.Data;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OSharp.Core.Dependency;


namespace OSharp.Demo.Web.Controllers
{
    public class ValuesController : ApiController
    {
        public IRepository<User, int> UserRepo { get; set; }
        public IRepository<Role, int> RoleRepo { get; set; }

        private readonly IDictionary<int, string> _values = new Dictionary<int, string>() { { 1, "No.1 value" }, { 2, "No.2 value" } };

        [HttpGet]
        [Route("api/values/value")]
        public IHttpActionResult Value()
        {
            const string format = "{0}: {1}";
            List<string> lines = new List<string>()
            {
                format.FormatWith("IRepository<User,int>", UserRepo.UnitOfWork.GetHashCode()),
                format.FormatWith("IRepository<Role,int>", RoleRepo.UnitOfWork.GetHashCode()),
            };
            return Ok(lines.ExpandAndToString("<br>"));
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