using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Json;

using OSharp.Utility;


namespace OSharp.Web.SignalR.Hubs
{
    /// <summary>
    /// 可变的参数解析器，主要是将默认的只读集合更改为可变集合，以符合通信加密的需要
    /// </summary>
    public class VariableParameterResolver : DefaultParameterResolver
    {
        /// <summary>
        /// 重写以将Default中的Array更改为List，使Args能修改
        /// </summary>
        public override IList<object> ResolveMethodParameters(MethodDescriptor method, IList<IJsonValue> values)
        {
            method.CheckNotNull("method" );
            if (values != null && values.Count > 0)
            {
                object value = values.First().ConvertTo(typeof(string));
                return new List<object>() { value };
            }
            return method.Parameters.Zip(values, (descriptor, value) => ResolveParameter(descriptor, value)).ToList();
        }

    }
}
