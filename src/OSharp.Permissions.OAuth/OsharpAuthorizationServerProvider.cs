using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Owin.Security.OAuth;


namespace OSharp.Core.Security
{
    /// <summary>
    /// Osharp-OAuth验证实现服务提供者
    /// </summary>
    public class OsharpAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 初始化一个<see cref="OsharpAuthorizationServerProvider"/>类型的新实例
        /// </summary>
        public OsharpAuthorizationServerProvider(IServiceProvider serviceProvider)
        {
        }
    }
}
