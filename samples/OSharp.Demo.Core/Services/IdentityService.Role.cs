// -----------------------------------------------------------------------
//  <copyright file="IdentityService.Role.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-24 17:10</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;

using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Demo.Services
{
    public partial class IdentityService
    {
        #region Implementation of IIdentityContract

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        public IQueryable<Role> Roles
        {
            get { return RoleRepository.Entities; }
        }

        /// <summary>
        /// 检查角色信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的角色信息编号</param>
        /// <returns>角色信息是否存在</returns>
        public bool CheckRoleExists(Expression<Func<Role, bool>> predicate, int id = 0)
        {
            return RoleRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加角色信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddRoles(params RoleInputDto[] inputDtos)
        {
            return RoleRepository.Insert(inputDtos,
                dto =>
                {
                    if (dto.OrganizationId.HasValue)
                    {
                        if (RoleRepository.CheckExists(m => m.Name == dto.Name && m.Organization != null && m.Organization.Id == dto.OrganizationId.Value))
                        {
                            throw new Exception("同组织机构中名称为“{0}”的角色已存在，不能重复添加。".FormatWith(dto.Name));
                        }
                    }
                    else if (RoleRepository.CheckExists(m => m.Name == dto.Name && m.Organization == null))
                    {
                        throw new Exception("无组织机构的名称为“{0}”的角色已存在，不能重复添加".FormatWith(dto.Name));
                    }
                },
                (dto, entity) =>
                {
                    if (dto.OrganizationId.HasValue && dto.OrganizationId.Value > 0)
                    {
                        Organization organization = OrganizationRepository.GetByKey(dto.OrganizationId.Value);
                        if (organization == null)
                        {
                            throw new Exception("要加入的组织机构不存在。");
                        }
                        entity.Organization = organization;
                    }
                    else
                    {
                        entity.Organization = null;
                    }
                    return entity;
                });
        }

        /// <summary>
        /// 更新角色信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的角色信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditRoles(params RoleInputDto[] inputDtos)
        {
            return RoleRepository.Update(inputDtos,
                (dto, entity) =>
                {
                    if (entity.IsSystem)
                    {
                        throw new Exception("角色“{0}”为系统角色，不能编辑".FormatWith(dto.Name));
                    }
                    if (dto.OrganizationId.HasValue)
                    {
                        if (RoleRepository.CheckExists(m => m.Name == dto.Name && m.Organization != null && m.Organization.Id == dto.OrganizationId.Value, dto.Id))
                        {
                            throw new Exception("同组织机构中名称为“{0}”的角色已存在，不能重复添加。".FormatWith(dto.Name));
                        }
                    }
                    else if (RoleRepository.CheckExists(m => m.Name == dto.Name && m.Organization == null, dto.Id))
                    {
                        throw new Exception("无组织机构的名称为的角色已存在，不能重复添加".FormatWith(dto.Name));
                    }
                },
                (dto, entity) =>
                {
                    if (dto.OrganizationId.HasValue && dto.OrganizationId.Value > 0)
                    {
                        Organization organization = OrganizationRepository.GetByKey(dto.OrganizationId.Value);
                        if (organization == null)
                        {
                            throw new Exception("要加入的组织机构不存在。");
                        }
                        entity.Organization = organization;
                    }
                    else
                    {
                        entity.Organization = null;
                    }
                    return entity;
                });
        }

        /// <summary>
        /// 删除角色信息信息
        /// </summary>
        /// <param name="ids">要删除的角色信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteRoles(params int[] ids)
        {
            return RoleRepository.Delete(ids);
        }

        #endregion
    }
}