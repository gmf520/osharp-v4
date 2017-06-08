// -----------------------------------------------------------------------
//  <copyright file="IOperateLogWriter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-06 2:15</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Dependency;


namespace OSharp.Core.Logging
{
    /// <summary>
    /// 操作日志输出接口
    /// </summary>
    public interface IOperateLogWriter : IScopeDependency
    {
        /// <summary>
        /// 输出操作日志
        /// </summary>
        /// <param name="operateLog">操作日志信息</param>
        void Write(OperateLog operateLog);
    }
}