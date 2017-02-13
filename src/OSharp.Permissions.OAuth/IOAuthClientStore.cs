// -----------------------------------------------------------------------
//  <copyright file="IClientStore.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-04 15:55</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using OSharp.Core.Security.Dtos;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义客户端信息存储
    /// </summary>
    public interface IOAuthClientStore<in TClientInputDto, in TKey, in TClientSecretInputDto, in TClientSecretKey> : IOAuthClientValidator
        where TClientInputDto : OAuthClientBaseInputDto<TKey>
        where TClientSecretInputDto : OAuthClientSecretBaseInputDto<TClientSecretKey, TKey>
    {
        /// <summary>
        /// 新增客户端信息
        /// </summary>
        /// <param name="dto">客户端信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateClient(TClientInputDto dto);

        /// <summary>
        /// 编辑客户端信息
        /// </summary>
        /// <param name="dto">客户端信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateClient(TClientInputDto dto);

        /// <summary>
        /// 删除客户端信息
        /// </summary>
        /// <param name="id">客户端编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteClient(TKey id);

        /// <summary>
        /// 新增客户端密钥信息
        /// </summary>
        /// <param name="dto">客户端密钥信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateClientSecret(TClientSecretInputDto dto);

        /// <summary>
        /// 编辑客户端密钥信息
        /// </summary>
        /// <param name="dto">客户端密钥信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateClientSecret(TClientSecretInputDto dto);

        /// <summary>
        /// 删除客户端密钥信息
        /// </summary>
        /// <param name="id">客户端密钥编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteClientSecret(TClientSecretKey id);
    }
}