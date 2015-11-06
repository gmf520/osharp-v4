/********************************************************************************
** 命名空间:  Byone.APIStore
** 文 件 名:  DownloadEventArgs
** 作    者： Hxjhaker
** 生成日期： 2015-11-05 17:55:05
** 版 本 号:  V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byone.APIStore
{
    public class DownloadEventArgs : EventArgs
    {
        private int bytesReceived;
        private int totalBytes;
        private byte[] receivedData;
        /// <summary>    
        /// 已接收的字节数    
        /// </summary>    
        public int BytesReceived
        {
            get { return bytesReceived; }
            set { bytesReceived = value; }
        }
        /// <summary>    
        /// 总字节数    
        /// </summary>    
        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }
        /// <summary>    
        /// 当前缓冲区接收的数据    
        /// </summary>    
        public byte[] ReceivedData
        {
            get { return receivedData; }
            set { receivedData = value; }
        }
    }
}
