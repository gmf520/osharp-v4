// -----------------------------------------------------------------------
//  <copyright file="HtmlHelperExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-18 13:19</last-date>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Web.Mvc;


namespace OSharp.Web.Mvc.Extensions
{
    /// <summary>
    /// <see cref="System.Web.Mvc.HtmlHelper"/>辅助操作扩展类
    /// </summary>
    public static class HtmlHelperExtensions
    {
        private static readonly string RecordWriterKey = Guid.NewGuid().ToString();

        /// <summary>
        /// View页面片断缓存
        /// </summary>
        /// <param name="html"></param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <param name="func">缓存无效时获取页面内容的委托</param>
        /// <returns></returns>
        public static string Cache(this HtmlHelper html,
            string cacheKey,
            DateTime absoluteExpiration,
            Func<object> func)
        {
            return html.Cache(cacheKey, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, func);
        }

        /// <summary>
        /// View页面片断缓存
        /// </summary>
        /// <param name="html"></param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="slidingExpiration">滑动过期时间</param>
        /// <param name="func">缓存无效时获取页面内容的委托</param>
        /// <returns></returns>
        public static string Cache(this HtmlHelper html,
            string cacheKey,
            TimeSpan slidingExpiration,
            Func<object> func)
        {
            return html.Cache(cacheKey, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration, func);
        }

        /// <summary>
        /// View页面片断缓存
        /// </summary>
        /// <param name="html"></param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheDependency">缓存依赖项</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <param name="slidingExpiration">滑动过期时间</param>
        /// <param name="func">缓存无效时获取页面内容的委托</param>
        /// <returns></returns>
        public static string Cache(this HtmlHelper html,
            string cacheKey,
            CacheDependency cacheDependency,
            DateTime absoluteExpiration,
            TimeSpan slidingExpiration,
            Func<object> func)
        {
            Cache cache = html.ViewContext.HttpContext.Cache;
            string content = cache.Get(cacheKey) as string;
            if (content == null)
            {
                content = func().ToString();
                cache.Insert(cacheKey, content, cacheDependency, absoluteExpiration, slidingExpiration);
            }
            return content;
        }

        /// <summary>
        /// View页面片断缓存
        /// </summary>
        /// <param name="html"></param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <param name="action">缓存无效时执行的页面内容片断</param>
        public static void Cache(this HtmlHelper html,
            string cacheKey,
            DateTime absoluteExpiration,
            Action action)
        {
            html.Cache(cacheKey, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, action);
        }
        
        /// <summary>
        /// View页面片断缓存
        /// </summary>
        /// <param name="html"></param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="slidingExpiration">滑动过期时间</param>
        /// <param name="action">缓存无效时执行的页面内容片断</param>
        public static void Cache(this HtmlHelper html,
            string cacheKey,
            TimeSpan slidingExpiration,
            Action action)
        {
            html.Cache(cacheKey, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration, action);
        }
        
        /// <summary>
        /// View页面片断缓存
        /// </summary>
        /// <param name="html"></param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheDependency">缓存依赖项</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <param name="slidingExpiration">滑动过期时间</param>
        /// <param name="action">缓存无效时执行的页面内容片断</param>
        public static void Cache(this HtmlHelper html,
            string cacheKey,
            CacheDependency cacheDependency,
            DateTime absoluteExpiration,
            TimeSpan slidingExpiration,
            Action action)
        {
            Cache cache = html.ViewContext.HttpContext.Cache;
            string content = cache.Get(cacheKey) as string;
            if (content == null)
            {
                RecordWriter writer = html.GetRecordWriter();
                StringBuilder recorder = new StringBuilder();
                writer.AddRecorder(recorder);
                action();
                writer.RemoveRecorder(recorder);
                content = recorder.ToString();
                cache.Insert(cacheKey, content, cacheDependency, absoluteExpiration, slidingExpiration);
            }
            html.ViewContext.Writer.Write(content);
        }

        /// <summary>
        /// 创建用于片断缓存的 <see cref="RecordWriter"/>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        public static TextWriter CreateCacheWriter(this HtmlHelper html, TextWriter writer)
        {
            RecordWriter recordWriter = new RecordWriter(writer);
            html.SetRecordWriter(recordWriter);
            return recordWriter;
        }

#region 私有方法

        private static void SetRecordWriter(this HtmlHelper html, RecordWriter writer)
        {
            RecordWriter existWriter = html.ViewContext.HttpContext.Items[RecordWriterKey] as RecordWriter;
            if (existWriter != null)
            {
                throw new InvalidOperationException("RecordWriter 已经存在，可能页面已经被缓存了");
            }
            html.ViewContext.HttpContext.Items[RecordWriterKey] = writer;
        }

        private static RecordWriter GetRecordWriter(this HtmlHelper html)
        {
            RecordWriter writer = html.ViewContext.HttpContext.Items[RecordWriterKey] as RecordWriter;
            if (writer == null)
            {
                throw new InvalidOperationException("RecordWriter 不存在，请使用CreateCacheWriter方法进行初始化");
            }
            return writer;
        }

#endregion
    }
}