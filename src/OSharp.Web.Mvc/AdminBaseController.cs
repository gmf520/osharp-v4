// -----------------------------------------------------------------------
//  <copyright file="AdminBaseController.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-25 2:39</last-date>
// -----------------------------------------------------------------------

using System.Web.Mvc;


namespace OSharp.Web.Mvc
{
    /// <summary>
    /// 管理控制器基类
    /// </summary>
    [Authorize]
    public abstract class AdminBaseController : BaseController
    { }
}
