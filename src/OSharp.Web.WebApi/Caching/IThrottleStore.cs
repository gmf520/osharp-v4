namespace OSharp.Web.Http.Caching
{
    /// <summary>
    /// 定义限流信息的仓储
    /// </summary>
    public interface IThrottleStore
    {
        /// <summary>
        /// 尝试获取指定标记的限流记录
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        bool TryGetValue(string key, out ThrottleEntry entry);

        /// <summary>
        /// 更新指定标记的限流记录
        /// </summary>
        /// <param name="key"></param>
        void IncrementRequests(string key);

        /// <summary>
        /// 销毁指定标记的限流记录
        /// </summary>
        /// <param name="key"></param>
        void Rollover(string key);

        /// <summary>
        /// 清空仓储
        /// </summary>
        void Clear();
    }
}
