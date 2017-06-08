// -----------------------------------------------------------------------
//  <copyright file="StringTrimModelBinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-04 17:37</last-date>
// -----------------------------------------------------------------------

using System.Web.Mvc;


namespace OSharp.Web.Mvc.Binders
{
    /// <summary>
    /// 模型字符串处理类，处理传入字符串的前后空格
    /// </summary>
    public class StringTrimModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// 使用指定的控制器上下文和结合上下文约束模型。
        /// </summary>
        /// <param name="controllerContext">控制器操作的上下文。上下文信息，包括控制器，HTTP请求的内容，背景，和路由数据。</param>
        /// <param name="bindingContext">模型在该模型中的约束。上下文包括信息，如模型对象、模型名称、模型类型、属性筛选器和值提供程序。</param>
        /// <returns>绑定对象</returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object value = base.BindModel(controllerContext, bindingContext);
            if (value is string)
            {
                return (value as string).Trim();
            }
            return value;
        }
    }
}