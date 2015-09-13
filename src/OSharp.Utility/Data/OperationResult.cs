// -----------------------------------------------------------------------
//  <copyright file="OperationResult.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-03 18:31</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Utility.Data
{
    /// <summary>
    /// 业务操作结果信息类，对操作结果进行封装
    /// </summary>
    public class OperationResult : OperationResult<object>
    {
        static OperationResult()
        {
            Success = new OperationResult(OperationResultType.Success);
            NoChanged = new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult()
            : this(OperationResultType.Success)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType)
            : this(resultType, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message, object data)
            : base(resultType, message, data)
        { }

        /// <summary>
        /// 获取或设置 成功的操作结果
        /// </summary>
        public static OperationResult Success { get; set; }

        /// <summary>
        /// 获取或设置 未变更的操作结果
        /// </summary>
        public static OperationResult NoChanged { get; set; }
    }


    /// <summary>
    /// 泛型版本的业务操作结果信息类，对操作结果进行封装
    /// </summary>
    /// <typeparam name="TData">返回数据的类型</typeparam>
    public class OperationResult<TData> : OSharpResult<OperationResultType, TData>
    {
        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult()
            : this(OperationResultType.Success)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType)
            : this(resultType, null, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType, message, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message, TData data)
            : base(resultType, message, data)
        { }
    }
}