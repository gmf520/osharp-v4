// -----------------------------------------------------------------------
//  <copyright file="ExpirableBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 22:44</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 可过期实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ExpirableBase<TKey> : EntityBase<TKey>, IExpirable 
    {
        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 验证时间有效性
        /// </summary>
        public void ThrowIfTimeInvalid()
        {
            if (!BeginTime.HasValue || !EndTime.HasValue || BeginTime.Value <= EndTime.Value)
            {
                return;
            }
            throw new IndexOutOfRangeException("生效时间不能大于过期时间");
        }
    }
}