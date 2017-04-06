using System;


namespace OSharp.Web.Http.Caching
{
    /// <summary>
    /// 限流信息类
    /// </summary>
    public class ThrottleEntry
    {
        /// <summary>
        /// 初始化一个<see cref="ThrottleEntry"/>类型的新实例
        /// </summary>
        public ThrottleEntry()
        {
            PeriodStart = DateTime.UtcNow;
            Requests = 0;
        }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime PeriodStart { get; set; }
        
        /// <summary>
        /// 请求数量
        /// </summary>
        public long Requests { get; set; }
    }
}
