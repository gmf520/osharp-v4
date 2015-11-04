// -----------------------------------------------------------------------
//  <copyright file="ExpirableBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-21 22:44</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Data
{
    /// <summary>
    /// 可过期实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ExpirableBase<TKey> : EntityBase<TKey>, IExpirable
    {
        private DateTime _beginTime;
        private DateTime? _endTime;

        /// <summary>
        /// 初始化一个<see cref="ExpirableBase{TKey}"/>类型的新实例
        /// </summary>
        protected ExpirableBase()
        {
            _beginTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        public DateTime BeginTime
        {
            get { return _beginTime; }
            set
            {
                if (EndTime != null && value > EndTime.Value)
                {
                    throw new InvalidOperationException("生效时间不能大于过期时间");
                }
                _beginTime = value;
            }
        }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                if (value != null && value < BeginTime)
                {
                    throw new InvalidOperationException("过期时间不能小于生效时间");
                }
                _endTime = value;
            }
        }
    }
}