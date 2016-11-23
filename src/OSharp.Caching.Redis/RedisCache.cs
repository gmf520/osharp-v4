using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Caching;


namespace OSharp.Caching.Redis
{
    public class RedisCache : CacheBase
    {
        /// <summary>
        /// 获取 缓存区域名称，可作为缓存键标识，给缓存管理带来便利
        /// </summary>
        public override string Region { get; }

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        public override object Get(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        public override T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        public override IEnumerable<object> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        public override IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        public override void Set(string key, object value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加或替换缓存项并设置绝对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        public override void Set(string key, object value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public override void Remove(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
