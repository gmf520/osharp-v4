// -----------------------------------------------------------------------
//  <copyright file="ClientSecretBaseInputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-04 15:45</last-date>
// -----------------------------------------------------------------------

using OSharp.Core.Data;


namespace OSharp.Core.Security.Dtos
{
    /// <summary>
    /// 客户端密钥输入DTO基类
    /// </summary>
    public class OAuthClientSecretBaseInputDto<TKey, TClientKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }
        
        /// <summary>
        /// 获取或设置 密钥类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 客户端信息编号
        /// </summary>
        public TClientKey ClientId { get; set; }
    }
}