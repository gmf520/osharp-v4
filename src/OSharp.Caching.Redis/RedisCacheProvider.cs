// -----------------------------------------------------------------------
//  <copyright file="RedisCacheProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-11-23 20:27</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Caching;

using StackExchange.Redis;


namespace OSharp.Caching.Redis
{
    /// <summary>
    /// Redis缓存提供者
    /// </summary>
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly ConnectionMultiplexer Multiplexer;

        /// <summary>
        /// 初始化一个<see cref="RedisCacheProvider"/>类型的新实例
        /// </summary>
        public RedisCacheProvider()
        {
            Multiplexer = MultiplexerInit();
        }

        private static ConnectionMultiplexer MultiplexerInit()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="regionName">缓存区域名称</param>
        /// <returns></returns>
        public ICache GetCache(string regionName)
        {
            




            throw new System.NotImplementedException();
        }
        
    }
}