using System.Collections.Concurrent;


namespace OSharp.Web.Http.Caching
{
    /// <summary>
    /// 基于内存的限流信息仓储
    /// </summary>
    public class InMemoryThrottleStore : IThrottleStore
    {
        private readonly ConcurrentDictionary<string, ThrottleEntry> _throttleStore = new ConcurrentDictionary<string, ThrottleEntry>();

        /// <summary>
        /// 尝试获取指定标记的限流记录
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out ThrottleEntry entry)
        {
            return _throttleStore.TryGetValue(key, out entry);
        }

        /// <summary>
        /// 更新指定标记的限流记录
        /// </summary>
        /// <param name="key"></param>
        public void IncrementRequests(string key)
        {
            _throttleStore.AddOrUpdate(key,
                k => new ThrottleEntry { Requests = 1 },
                (k, e) =>
                {
                    e.Requests++;
                    return e;
                });
        }

        /// <summary>
        /// 销毁指定标记的限流记录
        /// </summary>
        /// <param name="key"></param>
        public void Rollover(string key)
        {
            ThrottleEntry dummy;
            _throttleStore.TryRemove(key, out dummy);
        }

        /// <summary>
        /// 清空仓储
        /// </summary>
        public void Clear()
        {
            _throttleStore.Clear();
        }
    }
}
