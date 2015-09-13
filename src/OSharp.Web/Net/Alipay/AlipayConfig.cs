// -----------------------------------------------------------------------
//  <copyright file="AlipayConfig.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-12-23 21:00</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Web.Net.Alipay
{
    /// <summary>
    /// 支付宝基础配置类，用于设置账户有关信息及返回路径
    /// </summary>
    public static class AlipayConfig
    {
        static AlipayConfig()
        {
            Partner = string.Empty;
            Key = string.Empty;
            InputCharset = "utf-8";
            SignType = "MD5";
        }

        /// <summary>
        /// 获取或设置 合作者身份ID
        /// </summary>
        public static string Partner { get; set; }

        /// <summary>
        /// 获取或设置 安全校验码
        /// </summary>
        public static string Key { get; set; }

        /// <summary>
        /// 获取或设置 字符编码格式，当前支持 gbk 或 utf-8
        /// </summary>
        public static string InputCharset { get; set; }

        /// <summary>
        /// 获取或设置 签名方式，当前支持 RSA、DSA、MD5
        /// </summary>
        public static string SignType { get; set; }
    }
}