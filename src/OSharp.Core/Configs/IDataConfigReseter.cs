// -----------------------------------------------------------------------
//  <copyright file="IDataConfigReseter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-26 0:58</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Core.Configs
{
    /// <summary>
    /// OSharp数据配置信息重置类
    /// </summary>
    public interface IDataConfigReseter
    {
        /// <summary>
        /// 重置数据配置信息
        /// </summary>
        /// <param name="config">原始数据配置信息</param>
        /// <returns>重置后的数据配置信息</returns>
        DataConfig Reset(DataConfig config);
    }
}