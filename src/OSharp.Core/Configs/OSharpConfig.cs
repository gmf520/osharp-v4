using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs.ConfigFile;


namespace OSharp.Core.Configs
{
    /// <summary>
    /// OSharp配置类
    /// </summary>
    public sealed class OSharpConfig
    {
        private static readonly Lazy<OSharpConfig> InstanceLazy
            = new Lazy<OSharpConfig>(() => new OSharpConfig());
        private const string OSharpSectionName = "osharp";

        /// <summary>
        /// 初始化一个心得<see cref="OSharpConfig"/>实例
        /// </summary>
        private OSharpConfig()
        {
            OSharpFrameworkSection section = (OSharpFrameworkSection)ConfigurationManager.GetSection(OSharpSectionName);
            if (section == null)
            {
                DataConfig = new DataConfig();
                LoggingConfig = new LoggingConfig();
                return;
            }
            DataConfig = new DataConfig(section.Data);
            LoggingConfig = new LoggingConfig(section.Logging);
        }

        /// <summary>
        /// 获取 配置类的单一实例
        /// </summary>
        public static OSharpConfig Instance
        {
            get { return InstanceLazy.Value; }
        }

        /// <summary>
        /// 获取或设置 数据配置信息
        /// </summary>
        public DataConfig DataConfig { get; set; }

        /// <summary>
        /// 获取或设置 日志配置信息
        /// </summary>
        public LoggingConfig LoggingConfig { get; set; }
    }
}
