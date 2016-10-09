// -----------------------------------------------------------------------
//  <copyright file="IModule.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 0:16</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 定义模块信息
    /// </summary>
    /// <typeparam name="TKey">模块编号类型</typeparam>
    /// <typeparam name="TModule">模块信息类型</typeparam>
    /// <typeparam name="TFunction">功能信息类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TRole">角色信息类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户信息类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public interface IModule<out TKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey> : IEntity<TKey>
        where TModule : IModule<TKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
    {
        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置 模块备注
        /// </summary>
        string Remark { get; set; }

        /// <summary>
        /// 获取或设置 节点内排序码
        /// </summary>
        int OrderCode { get; set; }

        /// <summary>
        /// 获取 从根结点到当前结点的树形路径编号数组，由<see cref="TreePathString"/>属性转换，此属性仅支持在内存中使用
        /// </summary>
        TKey[] TreePathIds { get;}

        /// <summary>
        /// 获取或设置 父节点树形路径，从根结点Id到当前结点Id根据一定格式构建的字符串，形如："$1$,$3$,$4$,$7$"，编辑时更新
        /// </summary>
        string TreePathString { get; set; }

        /// <summary>
        /// 获取或设置 父模块信息
        /// </summary>
        TModule Parent { get; set; }

        /// <summary>
        /// 获取或设置 子模块集合
        /// </summary>
        ICollection<TModule> SubModules { get; set; }

        /// <summary>
        /// 获取或设置 功能信息集合
        /// </summary>
        ICollection<TFunction> Functions { get; set; }

        /// <summary>
        /// 获取或设置 拥有此模块的角色信息集合
        /// </summary>
        ICollection<TRole> Roles { get; set; }

        /// <summary>
        /// 获取或设置 拥有此模块的用户信息集合
        /// </summary>
        ICollection<TUser> Users { get; set; }

        /// <summary>
        /// 获取实体的TreePath，即由父级树链的Id构成的字符串
        /// </summary>
        /// <returns></returns>
        string GetTreePath();
    }
}