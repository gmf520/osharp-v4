// -----------------------------------------------------------------------
//  <copyright file="ContextElement.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 2:06</last-date>
// -----------------------------------------------------------------------

using System.Configuration;


namespace OSharp.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据上下文配置节点
    /// </summary>
    internal class ContextElement : ConfigurationElement
    {
        private const string NameKey = "name";
        private const string EnabledKey = "enabled";
        private const string DataLoggingEnabledKey = "dataLoggingEnabled";
        private const string ConnectionStringNameKey = "connectionStringName";
        private const string TypeKey = "type";
        private const string DbContextInitializerKey = "initializer";

        /// <summary>
        /// 获取或设置 节点名称
        /// </summary>
        [ConfigurationProperty(NameKey, IsRequired = true, IsKey = true)]
        public virtual string Name
        {
            get { return (string)this[NameKey]; }
            set { this[NameKey] = value; }
        }

        /// <summary>
        /// 获取或设置 是否启用数据上下文
        /// </summary>
        [ConfigurationProperty(EnabledKey, DefaultValue = true)]
        public virtual bool Enabled
        {
            get { return (bool)this[EnabledKey]; }
            set { this[EnabledKey] = value; }
        }

        /// <summary>
        /// 获取或设置 是否开启数据日志记录
        /// </summary>
        [ConfigurationProperty(DataLoggingEnabledKey, DefaultValue = false)]
        public virtual bool DataLoggingEnabled
        {
            get { return (bool)this[DataLoggingEnabledKey]; }
            set { this[DataLoggingEnabledKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据库连接串名称
        /// </summary>
        [ConfigurationProperty(ConnectionStringNameKey, IsRequired = true)]
        public virtual string ConnectionStringName
        {
            get { return (string)this[ConnectionStringNameKey]; }
            set { this[ConnectionStringNameKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据上下文类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string ContextTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据上下文初始化配置
        /// </summary>
        [ConfigurationProperty(DbContextInitializerKey, IsRequired = true)]
        public virtual DbContextInitializerElement DbContextInitializer
        {
            get { return (DbContextInitializerElement)this[DbContextInitializerKey]; }
            set { this[DbContextInitializerKey] = value; }
        }
    }
}