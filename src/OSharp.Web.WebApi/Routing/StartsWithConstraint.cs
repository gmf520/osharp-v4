using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;


namespace OSharp.Web.Http.Routing
{
    public class StartsWithConstraint : IHttpRouteConstraint
    {
        private const string Id = "id";
        private readonly string[] _array;

        public StartsWithConstraint(string[] startswithArray = null)
        {
            if (startswithArray == null)
            {
                startswithArray = new[] { "GET", "PUT", "DELETE", "POST", "EDIT", "UPDATE", "AUDIT", "DOWNLOAD" };
            }
            _array = startswithArray;
        }

        #region Implementation of IHttpRouteConstraint

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (values == null)
            {
                return true;
            }
            if (!values.ContainsKey(parameterName) || !values.ContainsKey(Id))
            {
                return true;
            }
            string action = values[parameterName].ToString().ToLower();
            if (string.IsNullOrEmpty(action))
            {
                values[parameterName] = request.Method.ToString();
            }
            else if (string.IsNullOrEmpty(values[Id].ToString()))
            {
                bool isAction = _array.All(item => action.StartsWith(item.ToLower()));
                if (isAction)
                {
                    return true;
                }
                //values[Id] = values[parameterName];
                //values[parameterName] = request.Method.ToString();
            }
            return true;
        }

        #endregion
    }
}
