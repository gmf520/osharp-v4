// -----------------------------------------------------------------------
//  <copyright file="ClientStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-04 17:13</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;
using OSharp.Core.Security.Dtos;
using OSharp.Core.Security.Models;
using OSharp.Utility;
using OSharp.Utility.Data;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 客户端信息存储基类
    /// </summary>
    public abstract class ClientStoreBase<TClient, TClientKey, TClientSecret, TClientSecretKey, TClientInputDto, TClientSecretInputDto>
        : IClientStore<TClientInputDto, TClientKey, TClientSecretInputDto, TClientSecretKey>
        where TClient : ClientBase<TClientKey, TClientSecret, TClientSecretKey>
        where TClientSecret : ClientSecretBase<TClientSecretKey, TClient>
        where TClientInputDto : ClientBaseInputDto<TClientKey>
        where TClientSecretInputDto : ClientSecretBaseInputDto<TClientSecretKey, TClientKey>
    {
        /// <summary>
        /// 获取或设置 客户端仓储对象
        /// </summary>
        public IRepository<TClient, TClientKey> ClientRepository { private get; set; }

        /// <summary>
        /// 获取或设置 客户端密钥仓储对象
        /// </summary>
        public IRepository<TClientSecret, TClientSecretKey> ClientSecretRepository { private get; set; }

        /// <summary>
        /// 新增客户端信息
        /// </summary>
        /// <param name="dto">客户端信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> AddClient(TClientInputDto dto)
        {
            dto.CheckNotNull("dto");
            return ClientRepository.InsertAsync(new[] { dto });
        }

        /// <summary>
        /// 编辑客户端信息
        /// </summary>
        /// <param name="dto">客户端信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> EditClient(TClientInputDto dto)
        {
            dto.CheckNotNull("dto");
            return ClientRepository.UpdateAsync(new[] { dto });
        }

        /// <summary>
        /// 删除客户端信息
        /// </summary>
        /// <param name="id">客户端编号</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> DeleteClient(TClientKey id)
        {
            return ClientRepository.DeleteAsync(new[] { id },
                null,
                entity =>
                {
                    foreach (TClientSecret secret in entity.ClientSecrets)
                    {
                        ClientSecretRepository.Delete(secret);
                    }
                    return entity;
                });
        }

        /// <summary>
        /// 验证客户端
        /// </summary>
        /// <param name="clientId">客户端编号</param>
        /// <param name="clientSecret">客户端密钥</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<bool> ValidateClient(string clientId, string clientSecret)
        {
            clientId.CheckNotNull("clientId");
            clientSecret.CheckNotNull("clientSecret");
            return Task.Run(() =>
            {
                var data = ClientRepository.Entities.Where(m => m.ClientId == clientId).Select(m => new
                {
                    ClientSecrets = m.ClientSecrets.Where(n => !n.IsLocked).Select(n => n.Value)
                }).FirstOrDefault();
                return data != null && data.ClientSecrets.Contains(clientSecret);
            });
        }

        /// <summary>
        /// 新增客户端密钥信息
        /// </summary>
        /// <param name="dto">客户端密钥信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> AddClientSecret(TClientSecretInputDto dto)
        {
            dto.CheckNotNull("dto" );
            return ClientSecretRepository.InsertAsync(new[] { dto },
                null,
                (_, entity) =>
                {
                    TClient client = ClientRepository.GetByKey(dto.ClientId);
                    if (client == null)
                    {
                        throw new Exception("指定编号的客户端信息不存在");
                    }
                    entity.Client = client;
                    return entity;
                });
        }

        /// <summary>
        /// 编辑客户端密钥信息
        /// </summary>
        /// <param name="dto">客户端密钥信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> EditClientSecret(TClientSecretInputDto dto)
        {
            dto.CheckNotNull("dto" );
            return ClientSecretRepository.UpdateAsync(new[] { dto });
        }

        /// <summary>
        /// 删除客户端密钥信息
        /// </summary>
        /// <param name="id">客户端密钥编号</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> DeleteClientSecret(TClientSecretKey id)
        {
            return ClientSecretRepository.DeleteAsync(new[] { id });
        }
    }
}