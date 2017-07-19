// -----------------------------------------------------------------------
//  <copyright file="IdentityService.User.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-03-24 17:25</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity;
using OSharp.Core.Mapping;
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
        /// 获取 用户信息查询数据集
        /// </summary>
        public IQueryable<User> Users
        {
            get { return UserRepository.Entities; }
        }

        /// <summary>
        /// 获取或设置 用户管理器
        /// </summary>
        public UserManager<User, int> UserManager { get; set; }

        /// <summary>
        /// 检查用户信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户信息编号</param>
        /// <returns>用户信息是否存在</returns>
        public bool CheckUserExists(Expression<Func<User, bool>> predicate, int id = 0)
        {
            return UserRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加用户信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> AddUsers(params UserInputDto[] dtos)
        {
            List<string> names = new List<string>();
            UserRepository.UnitOfWork.BeginTransaction();
            foreach (UserInputDto dto in dtos)
            {
                IdentityResult result;
                User user = dto.MapTo<User>();
                //密码单独处理
                if (!dto.Password.IsNullOrEmpty())
                {
                    result = await UserManager.PasswordValidator.ValidateAsync(dto.Password);
                    if (!result.Succeeded)
                    {
                        return result.ToOperationResult();
                    }
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(dto.Password);
                }
                user.Extend = new UserExtend() { RegistedIp = dto.RegistedIp };
                result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return new OperationResult(OperationResultType.Error, result.Errors.ExpandAndToString());
                }
                names.Add(user.UserName);
            }
            UserRepository.UnitOfWork.Commit();
            return new OperationResult(OperationResultType.Success, "用户“{0}”创建成功".FormatWith(names.ExpandAndToString()));
        }

        /// <summary>
        /// 更新用户信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> EditUsers(params UserInputDto[] dtos)
        {
            List<string> names = new List<string>();
            UserRepository.UnitOfWork.BeginTransaction();
            foreach (UserInputDto dto in dtos)
            {
                IdentityResult result;
                User user = UserManager.FindById(dto.Id);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull);
                }
                user = dto.MapTo(user);
                //密码单独处理
                if (!dto.Password.IsNullOrEmpty())
                {
                    result = await UserManager.PasswordValidator.ValidateAsync(dto.Password);
                    if (!result.Succeeded)
                    {
                        return result.ToOperationResult();
                    }
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(dto.Password);
                }
                result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new OperationResult(OperationResultType.Error, result.Errors.ExpandAndToString());
                }
                names.Add(dto.UserName);
            }
            UserRepository.UnitOfWork.Commit();
            return new OperationResult(OperationResultType.Success, "用户“{0}”更新成功".FormatWith(names.ExpandAndToString()));
        }

        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="ids">要删除的用户信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteUsers(params int[] ids)
        {
            OperationResult result = UserRepository.Delete(ids, null,
                entity =>
                {
                    //先删除所有用户相关信息
                    UserExtendRepository.Delete(entity.Extend);
                    return entity;
                });
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 设置用户的角色
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="roleIds">角色编号集合</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> SetUserRoles(int id, int[] roleIds)
        {
            User user = await UserRepository.GetByKeyAsync(id);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的用户信息不存在");
            }
            int[] existIds = UserRoleMapRepository.Entities.Where(m => m.User.Id == id).Select(m => m.Role.Id).ToArray();
            int[] addIds = roleIds.Except(existIds).ToArray();
            int[] removeIds = existIds.Except(roleIds).ToArray();
            UserRoleMapRepository.UnitOfWork.BeginTransaction();
            int count = 0;
            foreach (int addId in addIds)
            {
                Role role = await RoleRepository.GetByKeyAsync(addId);
                if (role == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
                }
                UserRoleMap map = new UserRoleMap() { User = user, Role = role };
                count += await UserRoleMapRepository.InsertAsync(map);
            }
            count += await UserRoleMapRepository.DeleteAsync(m => m.User.Id == id && removeIds.Contains(m.Role.Id));
            UserRoleMapRepository.UnitOfWork.Commit();
            return count > 0
                ? new OperationResult(OperationResultType.Success, "用户“{0}”指派角色操作成功".FormatWith(user.UserName))
                : OperationResult.NoChanged;
        }

        #endregion
    }
}