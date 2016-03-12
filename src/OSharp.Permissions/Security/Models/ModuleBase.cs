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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        : EntityBase<TKey>,
          IModule<TKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TModule : IModule<TKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TKey : struct
    {
        /// <summary>
        /// 初始化一个<see cref="ModuleBase"/>类型的新实例
        /// </summary>
        protected ModuleBase()
        {
            TreePathIds = new TKey[0];
            SubModules = new List<TModule>();
            Functions = new List<TFunction>();
            Roles = new List<TRole>();
            Users = new List<TUser>();
        }

        /// <summary>
        /// 获取或设置 树形路径，树链的Id以逗号分隔构成的字符串，编辑时更新
        /// </summary>
        public string TreePath { get; set; }

        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 模块描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 节点内排序码
        /// </summary>
        [Range(0, 999)]
        public int OrderCode { get; set; }

        /// <summary>
        /// 获取 树形路径编号数组，由<see cref="TreePath"/>属性转换
        /// </summary>
        [NotMapped]
        public TKey[] TreePathIds
        {
            get
            {
                return TreePath == null
                    ? new TKey[0]
                    : TreePath.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(m => m.CastTo<TKey>()).ToArray();
            }
            set { TreePath = value.ExpandAndToString(); }
        }

        /// <summary>
        /// 获取或设置 父模块信息
        /// </summary>
        public TModule Parent { get; set; }

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
        public ICollection<TUser> Users { get; set; }
    }
}