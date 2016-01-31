namespace OSharp.Utility.Windows
{
    /// <summary>
    /// 系统信息类
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 获取或设置 CPU型号
        /// </summary>
        public string CpuName { get; set; }

        /// <summary>
        /// 获取或设置 CPU编号
        /// </summary>
        public string CpuId { get; set; }

        /// <summary>
        /// 获取或设置 主板型号
        /// </summary>
        public string BoardName { get; set; }

        /// <summary>
        /// 获取或设置 主板编号
        /// </summary>
        public string BoardId { get; set; }

        /// <summary>
        /// 获取或设置 硬盘型号
        /// </summary>
        public string DiskName { get; set; }

        /// <summary>
        /// 获取或设置 硬盘编号
        /// </summary>
        public string DiskId { get; set; }

        /// <summary>
        /// 获取或设置 操作系统名称
        /// </summary>
        public string OSName { get; set; }

        /// <summary>
        /// 获取或设置 操作系统补丁版本
        /// </summary>
        public string OSCsdVersion { get; set; }

        /// <summary>
        /// 获取或设置 是否64位操作系统
        /// </summary>
        public bool OSIs64Bit { get; set; }

        /// <summary>
        /// 获取或设置 操作系统版本
        /// </summary>
        public string OSVersion { get; set; }

        /// <summary>
        /// 获取或设置 操作系统路径
        /// </summary>
        public string OSPath { get; set; }

        /// <summary>
        /// 获取或设置 可用物理内存，单位：MB
        /// </summary>
        public double PhysicalMemoryFree { get; set; }

        /// <summary>
        /// 获取或设置 总共物理内存，单位：MB
        /// </summary>
        public double PhysicalMemoryTotal { get; set; }

        /// <summary>
        /// 获取或设置 屏幕分辨率宽
        /// </summary>
        public int ScreenWith { get; set; }

        /// <summary>
        /// 获取或设置 屏幕分辨率高
        /// </summary>
        public int ScreenHeight { get; set; }

        /// <summary>
        /// 获取或设置 屏幕色深
        /// </summary>
        public int ScreenColorDepth { get; set; }
    }
}
