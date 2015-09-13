// -----------------------------------------------------------------------
//  <copyright file="OSharpFrameworkSection.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-30 13:44</last-date>
// -----------------------------------------------------------------------

using System.Configuration;


namespace OSharp.Core.Configs.ConfigFile
{
    internal class OSharpFrameworkSection : ConfigurationSection
    {
        private const string XmlnsKey = "xmlns";
        private const string DataKey = "data";
        private const string LoggingKey = "logging";

        [ConfigurationProperty(XmlnsKey, IsRequired = false)]
        private string Xmlns
        {
            get { return (string)this[XmlnsKey]; }
            set { this[XmlnsKey] = value; }
        }

        [ConfigurationProperty(DataKey)]
        public virtual DataElement Data
        {
            get { return (DataElement)this[DataKey]; }
            set { this[DataKey] = value; }
        }

        [ConfigurationProperty(LoggingKey)]
        public virtual LoggingElement Logging
        {
            get { return (LoggingElement)this[LoggingKey]; }
            set { this[LoggingKey] = value; }
        }
    }
}