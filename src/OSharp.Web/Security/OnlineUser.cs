// -----------------------------------------------------------------------
//  <copyright file="OnlineUser.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-15 10:54</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;


namespace OSharp.Web.Security
{
    /// <summary>
    /// 在线用户信息
    /// </summary>
    [Serializable]
    public class OnlineUser
    {
        /// <summary>
        /// 初始化一个<see cref="OnlineUser"/>类型的新实例
        /// </summary>
        public OnlineUser()
        {
            RoleNames = new List<string>();
            UserData = new Dictionary<string, object>();
        }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 角色集合
        /// </summary>
        public IEnumerable<string> RoleNames { get; set; }

        /// <summary>
        /// 获取或设置 是否管理角色
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 获取或设置 客户端标识
        /// </summary>
        public string ClientToken { get; set; }

        /// <summary>
        /// 获取或设置 客户端版本
        /// </summary>
        public string ClientVersion { get; set; }

        /// <summary>
        /// 获取或设置 在线标识，客户端在线凭证
        /// </summary>
        public string OnlineToken { get; set; }

        /// <summary>
        /// 获取或设置 在线类型
        /// </summary>
        public OnlineType OnlineType { get; set; }

        /// <summary>
        /// 获取或设置 用户数据
        /// </summary>
        public Dictionary<string, object> UserData { get; set; }

        /// <summary>
        /// 获取或设置 IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 获取或设置 最后活动地址
        /// </summary>
        public string LastActivityUrl { get; set; }

        /// <summary>
        /// 获取或设置 最后活动时间
        /// </summary>
        public DateTime LastActivityTime { get; set; }
    }
}