// -----------------------------------------------------------------------
//  <copyright file="CreateDatabaseInitializerElement.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-29 23:57</last-date>
// -----------------------------------------------------------------------

using System.Configuration;


namespace OSharp.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据库创建策略配置节点
    /// </summary>
    internal class CreateDatabaseInitializerElement : ConfigurationElement
    {
        private const string TypeKey = "type";

        /// <summary>
        /// 获取或设置 数据库创建策略类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string InitializerTypeName
        {
            get { return (string)this[TypeKey]; } 
            set { this[TypeKey] = value; }
        }
    }
}