// ReSharper disable InconsistentNaming
namespace OSharp.Utility.Windows
{
    /// <summary>
    /// 表示WMI地址的枚举
    /// </summary>
    public enum WMIPath
    {
        /// <summary>
        /// CPU 处理器
        /// </summary>
        Win32_Processor,
        /// <summary>
        /// 物理内存条
        /// </summary>
        Win32_PhysicalMemory,
        /// <summary>
        /// 键盘
        /// </summary>
        Win32_Keyboard,
        /// <summary>
        /// 点输入设备，包括鼠标。
        /// </summary>
        Win32_PointingDevice,
        /// <summary>
        /// 软盘驱动器
        /// </summary>
        Win32_FloppyDrive,
        /// <summary>
        /// 硬盘驱动器
        /// </summary>
        Win32_DiskDrive,
        /// <summary>
        /// 光盘驱动器
        /// </summary>
        Win32_CDROMDrive,
        /// <summary>
        /// 主板
        /// </summary>
        Win32_BaseBoard,
        /// <summary>
        /// BIOS 芯片
        /// </summary>
        Win32_BIOS,
        /// <summary>
        /// 并口
        /// </summary>
        Win32_ParallelPort,
        /// <summary>
        /// 串口
        /// </summary>
        Win32_SerialPort,
        /// <summary>
        /// 串口配置
        /// </summary>
        Win32_SerialPortConfiguration,
        /// <summary>
        /// 多媒体设置，一般指声卡。
        /// </summary>
        Win32_SoundDevice,
        /// <summary>
        /// 主板插槽 (ISA & PCI & AGP)
        /// </summary>
        Win32_SystemSlot,
        /// <summary>
        /// USB 控制器
        /// </summary>
        Win32_USBController,
        /// <summary>
        /// 网络适配器
        /// </summary>
        Win32_NetworkAdapter,
        /// <summary>
        /// 网络适配器设置
        /// </summary>
        Win32_NetworkAdapterConfiguration,
        /// <summary>
        /// 打印机
        /// </summary>
        Win32_Printer,
        /// <summary>
        /// 打印机设置
        /// </summary>
        Win32_PrinterConfiguration,
        /// <summary>
        /// 打印机任务
        /// </summary>
        Win32_PrintJob,
        /// <summary>
        /// 打印机端口
        /// </summary>
        Win32_TCPIPPrinterPort,
        /// <summary>
        /// MODEM
        /// </summary>
        Win32_POTSModem,
        /// <summary>
        /// MODEM 端口
        /// </summary>
        Win32_POTSModemToSerialPort,
        /// <summary>
        /// 显示器
        /// </summary>
        Win32_DesktopMonitor,
        /// <summary>
        /// 显卡
        /// </summary>
        Win32_DisplayConfiguration,
        /// <summary>
        /// 显卡设置
        /// </summary>
        Win32_DisplayControllerConfiguration,
        /// <summary>
        /// 显卡细节
        /// </summary>
        Win32_VideoController,
        /// <summary>
        /// 显卡支持的显示模式
        /// </summary>
        Win32_VideoSettings,

        // 操作系统
        /// <summary>
        /// 时区
        /// </summary>
        Win32_TimeZone,
        /// <summary>
        /// 驱动程序
        /// </summary>
        Win32_SystemDriver,
        /// <summary>
        /// 磁盘分区
        /// </summary>
        Win32_DiskPartition,
        /// <summary>
        /// 逻辑磁盘
        /// </summary>
        Win32_LogicalDisk,
        /// <summary>
        /// 逻辑磁盘所在分区及始末位置
        /// </summary>
        Win32_LogicalDiskToPartition,
        /// <summary>
        /// 逻辑内存配置
        /// </summary>
        Win32_LogicalMemoryConfiguration,
        /// <summary>
        /// 系统页文件信息
        /// </summary>
        Win32_PageFile,
        /// <summary>
        /// 页文件设置
        /// </summary>
        Win32_PageFileSetting,
        /// <summary>
        /// 系统启动配置
        /// </summary>
        Win32_BootConfiguration,
        /// <summary>
        /// 计算机信息简要
        /// </summary>
        Win32_ComputerSystem,
        /// <summary>
        /// 操作系统信息
        /// </summary>
        Win32_OperatingSystem,
        /// <summary>
        /// 系统自动启动程序
        /// </summary>
        Win32_StartupCommand,
        /// <summary>
        /// 系统安装的服务
        /// </summary>
        Win32_Service,
        /// <summary>
        /// 系统管理组
        /// </summary>
        Win32_Group,
        /// <summary>
        /// 系统组帐号
        /// </summary>
        Win32_GroupUser,
        /// <summary>
        /// 用户帐号
        /// </summary>
        Win32_UserAccount,
        /// <summary>
        /// 系统进程
        /// </summary>
        Win32_Process,
        /// <summary>
        /// 系统线程
        /// </summary>
        Win32_Thread,
        /// <summary>
        /// 共享
        /// </summary>
        Win32_Share,
        /// <summary>
        /// 已安装的网络客户端
        /// </summary>
        Win32_NetworkClient,
        /// <summary>
        /// 已安装的网络协议
        /// </summary>
        Win32_NetworkProtocol
    }
}