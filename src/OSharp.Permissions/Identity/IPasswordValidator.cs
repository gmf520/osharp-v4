// -----------------------------------------------------------------------
//  <copyright file="IPasswordValidator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-05 2:38</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using OSharp.Core.Dependency;


namespace OSharp.Core.Identity
{
    /// <summary>
    /// 定义用户密码验证器
    /// </summary>
    public interface IPasswordValidator : IScopeDependency
    {
        /// <summary>
        /// 验证用户名与密码是否匹配
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        Task<bool> Validate(string userName, string password);
    }
}