// -----------------------------------------------------------------------
//  <copyright file="ClientStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-04 17:13</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
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
        where TClientSecret : ClientSecretBase<TClientSecretKey, TClient, TClientKey>
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
        /// 获取或设置 客户端编号生成器
        /// </summary>
        public IClientIdProvider ClientIdProvider { get; set; }

        /// <summary>
        /// 获取或设置 客户端密钥生成器
        /// </summary>
        public IClientSecretProvider ClientSecretProvider { get; set; }

        /// <summary>
        /// 新增客户端信息
        /// </summary>
        /// <param name="dto">客户端信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> AddClient(TClientInputDto dto)
        {
            dto.CheckNotNull("dto");
            return ClientRepository.InsertAsync(new[] { dto }, null,
                (_, entity) =>
                {
                    entity.ClientId = ClientIdProvider.CreateClientId(_.ClientType);
                    return Task.FromResult(entity);
                });
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
                    return Task.FromResult(entity);
                });
        }

        /// <summary>
        /// 验证客户端编号与客户端密钥有效性
        /// </summary>
        /// <param name="clientId">客户端编号</param>
        /// <param name="clientSecret">客户端密钥</param>
        /// <returns>是否验证通过</returns>
        public Task<bool> Validate(string clientId, string clientSecret)
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
        /// 获取指定客户端的重定向地址
        /// </summary>
        /// <param name="clientId">客户端编号</param>
        /// <returns></returns>
        public Task<string> GetRedirectUrl(string clientId)
        {
            clientId.CheckNotNull("clientId");
            return Task.Run(() =>
            {
                return ClientRepository.Entities.Where(m => m.ClientId == clientId).Select(m => m.RedirectUrl).SingleOrDefault();
            });
        }

        /// <summary>
        /// 新增客户端密钥信息
        /// </summary>
        /// <param name="dto">客户端密钥信息输入DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> AddClientSecret(TClientSecretInputDto dto)
        {
            dto.CheckNotNull("dto");
            return ClientSecretRepository.InsertAsync(new[] { dto },
                null,
                async (_, entity) =>
                {
                    //生成密钥
                    entity.Value = ClientSecretProvider.CreateSecret();
                    TClient client = await ClientRepository.GetByKeyAsync(dto.ClientId);
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
            dto.CheckNotNull("dto");
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