// -----------------------------------------------------------------------
//  <copyright file="IFunctionHandler.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-13 9:31</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能信息处理器
    /// </summary>
    public interface IFunctionHandler
    {
        /// <summary>
        /// 从程序集中刷新功能数据，主要检索MVC的Controller-Action信息
        /// </summary>
        void Initialize();

        /// <summary>
        /// 查找指定条件的功能信息
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">功能方法</param>
        /// <returns>符合条件的功能信息</returns>
        IFunction GetFunction(string area, string controller, string action);

        /// <summary>
        /// 查找指定URL的功能信息
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>符合条件的功能信息</returns>
        IFunction GetFunction(string url);

        /// <summary>
        /// 刷新功能信息缓存
        /// </summary>
        void RefreshCache();
    }
}