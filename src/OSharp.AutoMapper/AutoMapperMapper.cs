// -----------------------------------------------------------------------
//  <copyright file="AutoMapperMapper.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-14 2:40</last-date>
// -----------------------------------------------------------------------

using AutoMapper;

using OSharp.Core.Mapping;


namespace OSharp.AutoMapper
{
    /// <summary>
    /// AutoMapper映射执行类
    /// </summary>
    public class AutoMapperMapper : IMapper
    {
        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget">要映射的目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标类型的对象</returns>
        public TTarget MapTo<TTarget>(object source)
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        /// 使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">待更新的目标对象</param>
        /// <returns>更新后的目标类型对象</returns>
        public TTarget MapTo<TSource, TTarget>(TSource source, TTarget target)
        {
            return Mapper.Map(source, target);
        }
    }
}