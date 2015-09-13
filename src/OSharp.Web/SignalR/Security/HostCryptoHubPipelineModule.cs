// -----------------------------------------------------------------------
//  <copyright file="HostCryptoHubPipelineModule.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-11-19 12:13</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;

using Newtonsoft.Json;

using OSharp.Utility;
using OSharp.Utility.Extensions;
using OSharp.Utility.Secutiry;
using OSharp.Web.Http.Internal;
using OSharp.Web.Properties;


namespace OSharp.Web.SignalR.Security
{
    /// <summary>
    /// 服务端Hub数据通信加密解密中间模块
    /// </summary>
    public class HostCryptoHubPipelineModule : HubPipelineModule
    {
        private readonly string _hashType;
        private readonly string _ownPrivateKey;
        private CommunicationCryptor _cryptor;
        private bool _canCrypto;

        /// <summary>
        /// 初始化一个<see cref="HostCryptoHubPipelineModule"/>类型的新实例
        /// </summary>
        /// <param name="ownPrivateKey">己方私钥</param>
        /// <param name="hashType">摘要哈希方式，可选值为MD5或SHA1</param>
        public HostCryptoHubPipelineModule(string ownPrivateKey, string hashType)
        {
            ownPrivateKey.CheckNotNullOrEmpty("ownPrivateKey");
            hashType.CheckNotNullOrEmpty("hashType");
            hashType = hashType.ToUpper();
            hashType.Required(str => hashType == "MD5" || hashType == "SHA1", Resources.Http_Security_RSA_Sign_HashType);
            _ownPrivateKey = ownPrivateKey;
            _hashType = hashType;
        }

        /// <summary>
        /// 是否开启通信加密功能
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual bool CanCrypto(HubCallerContext context)
        {
            return true;
        }

        /// <summary>
        /// 数据传到Hub之前进行数据解密
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            //_canCrypto = CanCrypto(context.Hub.Context);
            //if (!_canCrypto)
            //{
            //    return base.OnBeforeIncoming(context);
            //}
            //数据解密
            string facePublicKey = context.Hub.Context.Headers.Get(HttpHeaderNames.OSharpClientPublicKey);
            if (string.IsNullOrEmpty(facePublicKey))
            {
                return false;
            }
            _cryptor = new CommunicationCryptor(_ownPrivateKey, facePublicKey, _hashType);
            if (context.Args.Count == 1)
            {
                string encrypt = (string)context.Args[0];
                string json = _cryptor.DecryptAndVerifyData(encrypt);
                IList<object> args = JsonConvert.DeserializeObject<IList<object>>(json);
                context.Args.Clear();
                IList<object> values = context.MethodDescriptor.Parameters.Zip(args, (desc, arg) => ResolveParameter(desc, arg)).ToList();
                foreach (object arg in values)
                {
                    context.Args.Add(arg);
                }
            }
            return base.OnBeforeIncoming(context);
        }

        /// <summary>
        /// 将Hub处理完之后的数据加密以进行传输
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
        {
            //if (!_canCrypto)
            //{
            //    return base.OnBeforeOutgoing(context);
            //}
            //数据加密
            if (_cryptor == null)
            {
                return false;
            }
            object[] args = context.Invocation.Args;
            string encrypt = _cryptor.EncryptData(JsonConvert.SerializeObject(args));
            context.Invocation.Args = new object[] { encrypt };
            return base.OnBeforeOutgoing(context);
        }

        private object ResolveParameter(ParameterDescriptor descriptor, object value)
        {
            descriptor.CheckNotNull("descriptor" );
            if (value == null)
            {
                return null;
            }
            if (value.GetType() == descriptor.ParameterType)
            {
                return value;
            }
            return value.CastTo(descriptor.ParameterType);
        }
    }
}