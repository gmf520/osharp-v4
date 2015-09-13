// -----------------------------------------------------------------------
//  <copyright file="DeliverErrorCode.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-12-23 20:59</last-date>
// -----------------------------------------------------------------------

// ReSharper disable InconsistentNaming
namespace OSharp.Web.Net.Alipay
{
    /// <summary>
    /// 确认发货业务错误码
    /// </summary>
    public enum DeliverErrorCode
    {
        /// <summary>
        /// 参数不正确
        /// </summary>
        ILLEGAL_ARGUMENT,

        /// <summary>
        /// 指定交易信息不存在
        /// </summary>
        TRADE_NOT_EXIST,

        /// <summary>
        /// 交易状态不正确
        /// </summary>
        TRADE_STATUS_NOT_AVAILD,

        /// <summary>
        /// 签名不正确
        /// </summary>
        ILLEGAL_SIGN
    }
}