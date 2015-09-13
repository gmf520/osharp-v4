// -----------------------------------------------------------------------
//  <copyright file="DataElement.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 15:41</last-date>
// -----------------------------------------------------------------------

using System.Configuration;


namespace OSharp.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据配置节点
    /// </summary>
    internal class DataElement : ConfigurationElement
    {
        private const string ContextKey = "contexts";

        /// <summary>
        /// 数据上下文配置节点集合
        /// </summary>
        [ConfigurationProperty(ContextKey)]
        public virtual ContextCollection Contexts
        {
            get { return (ContextCollection)base[ContextKey]; }
        }
    }
}