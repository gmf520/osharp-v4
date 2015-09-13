// -----------------------------------------------------------------------
//  <copyright file="LogisticsType.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-12-23 19:48</last-date>
// -----------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace OSharp.Web.Net.Alipay
{
    /// <summary>
    /// 表示物流类型的枚举
    /// </summary>
    public enum LogisticsType
    {
        /// <summary>
        /// 平邮
        /// </summary>
        POST = 0,

        /// <summary>
        /// 快递
        /// </summary>
        EXPRESS = 1,

        /// <summary>
        /// EMS
        /// </summary>
        EMS = 2,

        /// <summary>
        /// 无需物流，在发货时使用
        /// </summary>
        DIRECT = 3
    }
}