// -----------------------------------------------------------------------
//  <copyright file="AuthenticationResult.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 18:39</last-date>
// -----------------------------------------------------------------------

using System.Dynamic;

using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 权限检查结果
    /// </summary>
    public sealed class AuthorizationResult : OSharpResult<AuthorizationResultType>
    {
        static AuthorizationResult()
        {
            Allowed = new AuthorizationResult(AuthorizationResultType.Allowed);
        }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationResultType type)
            : this(type, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationResultType type, string message)
            : this(type, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationResultType type, string message, object data)
            : base(type, message, data)
        { }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public override string Message
        {
            get { return _message ?? ResultType.ToDescription(); }
            set { _message = value; }
        }

        /// <summary>
        /// 获取或设置 允许的检查结果
        /// </summary>
        public static AuthorizationResult Allowed { get; set; }
    }
}