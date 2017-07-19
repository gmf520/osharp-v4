// -----------------------------------------------------------------------
//  <copyright file="ModuleBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 0:17</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Security.Models
{
    /// <summary>
    /// 模块信息基类
    /// </summary>
    /// <typeparam name="TKey">模块编号类型</typeparam>
    /// <typeparam name="TModule">模块信息类型</typeparam>
    /// <typeparam name="TFunction">功能信息类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TRole">角色信息类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户信息类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public abstract class ModuleBase<TKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        : EntityBase<TKey>, IModule<TKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TModule : IModule<TKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TKey : IEquatable<TKey>
        where TFunctionKey : IEquatable<TFunctionKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 初始化一个<see cref="ModuleBase"/>类型的新实例
        /// </summary>
        protected ModuleBase()
        {
            SubModules = new List<TModule>();
            Functions = new List<TFunction>();
            Roles = new List<TRole>();
            Users = new List<TUser>();
        }

        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        [Required, DisplayName("模块名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 模块描述
        /// </summary>
        [DisplayName("模块描述")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 节点内排序码
        /// </summary>
        [DisplayName("排序码")]
        public double OrderCode { get; set; }

        /// <summary>
        /// 获取 从根结点到当前结点的树形路径编号数组，由<see cref="TreePathString"/>属性转换，此属性仅支持在内存中使用
        /// </summary>
        [NotMapped]
        public TKey[] TreePathIds
        {
            get
            {
                return TreePathString == null
                    ? new TKey[0]
                    : TreePathString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(m => m.Trim('$').CastTo<TKey>()).ToArray();
            }
        }

        /// <summary>
        /// 获取或设置 父节点树形路径，父级树链Id根据一定格式构建的字符串，形如："$1$,$3$,$4$,$7$"，编辑时更新
        /// </summary>
        public string TreePathString { get; set; }

        /// <summary>
        /// 获取或设置 父模块信息
        /// </summary>
        public virtual TModule Parent { get; set; }

        /// <summary>
        /// 获取或设置 子模块集合，子模块自动拥有父模块的功能
        /// </summary>
        public virtual ICollection<TModule> SubModules { get; set; }

        /// <summary>
        /// 获取或设置 模块功能集合
        /// </summary>
        public virtual ICollection<TFunction> Functions { get; set; }

        /// <summary>
        /// 获取或设置 拥有此模块的角色信息集合
        /// </summary>
        public virtual ICollection<TRole> Roles { get; set; }

        /// <summary>
        /// 获取或设置 拥有此模块的用户信息集合
        /// </summary>
        public virtual ICollection<TUser> Users { get; set; }

        /// <summary>
        /// 获取实体的TreePath，即由根结点Id到当前结点Id构成的字符串
        /// </summary>
        public virtual string GetTreePath()
        {
            const string itemFormat = "${0}$";
            List<string> keys = new List<string> { itemFormat.FormatWith(Id) };
            TModule parent = Parent;
            while (parent != null)
            {
                keys.Add(itemFormat.FormatWith(parent.Id));
                parent = parent.Parent;
            }
            string[] ids = keys.ToArray();
            Array.Reverse(ids); //将收集的Id倒序排序，根结点在前
            return ids.ExpandAndToString();
        }
    }
}