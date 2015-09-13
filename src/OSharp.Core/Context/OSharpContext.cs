// -----------------------------------------------------------------------
//  <copyright file="OSharpContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-28 0:41</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using OSharp.Core.Security;


namespace OSharp.Core.Context
{
    /// <summary>
    /// OSharp框架上下文，用于构造OSharp框架运行环境
    /// </summary>
    [Serializable]
    public class OSharpContext : Dictionary<string, object>
    {
        private static readonly Lazy<OSharpContext> ContextLazy = new Lazy<OSharpContext>(() => new OSharpContext());

        private OSharpContext()
        { }

        /// <summary>
        /// 获取 当前上下文
        /// </summary>
        public static OSharpContext Current
        {
            get { return ContextLazy.Value; }
        }

        /// <summary>
        /// 获取或设置 功能信息处理器
        /// </summary>
        public IFunctionHandler FunctionHandler { get; set; }

        /// <summary>
        /// 获取或设置 实体数据信息处理器
        /// </summary>
        public IEntityInfoHandler EntityInfoHandler { get; set; }
    }
}