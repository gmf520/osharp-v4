// -----------------------------------------------------------------------
//  <copyright file="SecurityHelper.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-02-27 22:38</last-date>
// -----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Web;

using OSharp.Core;


namespace OSharp.Web.Mvc.Security
{
    /// <summary>
    /// 安全相关辅助操作
    /// </summary>
    public class SecurityHelper
    {
        /// <summary>
        /// 检测验证码有效性
        /// </summary>
        public static bool CheckVerify(string code, bool cleanIfFited = false)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            const string sessionName = Constants.VerifyCodeSession;
            object sessionCode = HttpContext.Current.Session[sessionName];
            bool fited = sessionCode != null && string.Equals(code, sessionCode.ToString(), StringComparison.CurrentCultureIgnoreCase);
            if (fited && cleanIfFited)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }
            return fited;
        }

        /// <summary>
        /// 设置验证码到SESSION中
        /// </summary>
        /// <param name="code">要设置的验证码</param>
        public static void SetVerify(string code)
        {
            const string sessionName = Constants.VerifyCodeSession;
            HttpContext.Current.Session[sessionName] = code.ToUpper();
        }
    }
}