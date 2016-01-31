// -----------------------------------------------------------------------
//  <copyright file="IDataHandlerInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-29 13:18</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 定义数据处理初始化器，反射程序集进行功能信息，实体信息的数据初始化
    /// </summary>
    public interface IDataHandlerInitializer
    {
        /// <summary>
        /// 执行数据处理初始化
        /// </summary>
        void Initialize();
    }
}