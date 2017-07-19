// -----------------------------------------------------------------------
//  <copyright file="SecurityManagerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 1:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Data;
using OSharp.Core.Mapping;
using OSharp.Core.Security.Dtos;
using OSharp.Core.Security.Models;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using OSharp.Utility.Filter;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 权限安全管理基类
    /// </summary>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    /// <typeparam name="TModule">模块类型</typeparam>
    /// <typeparam name="TModuleKey">模块编号类型</typeparam>
    /// <typeparam name="TModuleInputDto">模块输入DTO类型</typeparam>
    /// <typeparam name="TFunctionInputDto"></typeparam>
    /// <typeparam name="TEntityInfo">实体数据信息类型</typeparam>
    /// <typeparam name="TEntityInfoKey">实体数据信息编号类型</typeparam>
    /// <typeparam name="TEntityInfoInputDto">实体数据输入DTO类型</typeparam>
    /// <typeparam name="TEntityRoleMap">数据角色映射类型</typeparam>
    /// <typeparam name="TEntityRoleMapInputDto">数据角色映射输入DTO类型</typeparam>
    /// <typeparam name="TEntityRoleMapKey">数据角色映射编号类型</typeparam>
    /// <typeparam name="TEntityUserMap">数据用户映射类型</typeparam>
    /// <typeparam name="TEntityUserMapInputDto">数据用户映射输入DTO类型</typeparam>
    /// <typeparam name="TEntityUserMapKey">数据用户映射编号类型</typeparam>
    public abstract class SecurityManagerBase<TRole, TRoleKey, TUser, TUserKey, TModule, TModuleInputDto, TModuleKey, TFunction, TFunctionInputDto, TFunctionKey,
        TEntityInfo, TEntityInfoInputDto, TEntityInfoKey, TEntityRoleMap, TEntityRoleMapInputDto,
        TEntityRoleMapKey, TEntityUserMap, TEntityUserMapInputDto, TEntityUserMapKey>
        : ISecurityManager<TRole, TRoleKey, TUser, TUserKey, TFunction, TFunctionKey, TModuleKey>,
        IModuleStore<TModule, TModuleKey, TModuleInputDto, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>,
        IFunctionStore<TFunction, TFunctionKey, TFunctionInputDto>,
        IEntityInfoStore<TEntityInfo, TEntityInfoKey, TEntityInfoInputDto>,
        IEntityRoleStore<TEntityRoleMapInputDto, TEntityRoleMapKey, TEntityInfoKey, TRoleKey>,
        IEntityUserStore<TEntityUserMapInputDto, TEntityUserMapKey, TEntityInfoKey, TUserKey>
        where TRole : EntityBase<TRoleKey>, IRole<TRoleKey>
        where TUser : EntityBase<TUserKey>, IUser<TUserKey>
        where TModule : class, IModule<TModuleKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TModuleInputDto : ModuleBaseInputDto<TModuleKey>
        where TFunction : class, IFunction, IEntity<TFunctionKey>
        where TFunctionInputDto : FunctionBaseInputDto<TFunctionKey>
        where TEntityInfo : class, IEntityInfo, IEntity<TEntityInfoKey>
        where TEntityInfoInputDto : EntityInfoBaseInputDto<TEntityInfoKey>
        where TEntityRoleMap : class, IEntityRoleMap<TEntityRoleMapKey, TEntityInfo, TEntityInfoKey, TRole, TRoleKey>
        where TEntityRoleMapInputDto : EntityRoleMapBaseInputDto<TEntityRoleMapKey, TEntityInfoKey, TRoleKey>
        where TEntityUserMap : class, IEntityUserMap<TEntityUserMapKey, TEntityInfo, TEntityInfoKey, TUser, TUserKey>
        where TEntityUserMapInputDto : EntityUserMapBaseInputDto<TEntityUserMapKey, TEntityInfoKey, TUserKey>
        where TModuleKey : IEquatable<TModuleKey>
        where TRoleKey : IEquatable<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
        where TFunctionKey : IEquatable<TFunctionKey>
        where TEntityInfoKey : IEquatable<TEntityInfoKey>
        where TEntityRoleMapKey : IEquatable<TEntityRoleMapKey>
        where TEntityUserMapKey : IEquatable<TEntityUserMapKey>
    {
        #region 仓储对象属性

        /// <summary>
        /// 获取或设置 角色仓储对象
        /// </summary>
        public IRepository<TRole, TRoleKey> RoleRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<TUser, TUserKey> UserRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 功能仓储对象
        /// </summary>
        public IRepository<TFunction, TFunctionKey> FunctionRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 实体数据仓储对象
        /// </summary>
        public IRepository<TEntityInfo, TEntityInfoKey> EntityInfoRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 模块仓储对象
        /// </summary>
        public IRepository<TModule, TModuleKey> ModuleRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 数据角色映射仓储对象
        /// </summary>
        public IRepository<TEntityRoleMap, TEntityRoleMapKey> EntityRoleMapRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 数据用户映射仓储对象
        /// </summary>
        public IRepository<TEntityUserMap, TEntityUserMapKey> EntityUserMapRepository { protected get; set; }

        /// <summary>
        ///权限缓存对象
        /// </summary>
        public IFunctionAuthCache<TFunctionKey> FunctionAuthCache { get; set; }

        #endregion

        #region Implementation of ISecurityRole<in TRole,TRoleKey,TFunction,TFunctionKey,in TModuleKey>

        /// <summary>
        /// 获取指定角色的允许功能集合
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>允许的功能集合</returns>
        public virtual IEnumerable<TFunction> GetRoleAllFunctions(TRole role)
        {
            role.CheckNotNull("role");
            List<TModuleKey> moduleIds = ModuleRepository.Entities.Where(m => m.Roles.Select(n => n.Id).Contains(role.Id)).Select(m => m.Id).ToList();
            return moduleIds.SelectMany(GetAllFunctions).DistinctBy(m => m.Id);
        }

        /// <summary>
        /// 给角色添加模块权限
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <param name="moduleIds">要添加的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> SetRoleModules(TRole role, params TModuleKey[] moduleIds)
        {
            role.CheckNotNull("role");

            TModuleKey[] existModuleIds = ModuleRepository.Entities.Where(m => m.Roles.Select(n => n.Id).Contains(role.Id)).Select(m => m.Id).ToArray();
            TModuleKey[] addModuleIds = moduleIds.Except(existModuleIds).ToArray();
            TModuleKey[] removeModuleIds = existModuleIds.Except(moduleIds).ToArray();
            List<string> addNames = new List<string>(), removeNames = new List<string>();

            foreach (TModuleKey moduleId in addModuleIds)
            {
                TModule module = await ModuleRepository.GetByKeyAsync(moduleId);
                if (module == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "编号为“{0}”的模块信息不存在".FormatWith(moduleId));
                }
                module.Roles.Add(role);
                addNames.Add(module.Name);
            }
            foreach (TModuleKey moduleId in removeModuleIds)
            {
                TModule module = await ModuleRepository.GetByKeyAsync(moduleId);
                if (module == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "编号为“{0}”的模块信息不存在".FormatWith(moduleId));
                }
                module.Roles.Remove(role);
                removeNames.Add(module.Name);
            }

            //todo:确认更新 role 是否有效
            int count = await RoleRepository.UpdateAsync(role);

            if (count > 0)
            {
                //移除 变更模块涉及到的功能权限缓存，使其更新
                TModuleKey[] relateModuleIds = addModuleIds.Union(removeModuleIds).Distinct().ToArray();
                TFunctionKey[] functionIds = ModuleRepository.Entities.Where(m => relateModuleIds.Contains(m.Id))
                    .SelectMany(m => m.Functions.Select(n => n.Id)).Distinct().ToArray();
                FunctionAuthCache.RemoveFunctionCaches(functionIds);

                return new OperationResult(OperationResultType.Success,
                    "角色“{0}”添加模块“{1}”，移除模块“{2}”操作成功".FormatWith(role.Name, addNames.ExpandAndToString(), removeNames.ExpandAndToString()));
            }
            return OperationResult.NoChanged;
        }

        #endregion

        #region Implementation of ISecurityUser<in TUser,TUserKey,TFunction,TFunctionKey,in TModuleKey>

        /// <summary>
        /// 获取赋予给用户的功能集合，不包含用户拥有的角色赋予的功能集合
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>允许的功能集合</returns>
        public virtual IEnumerable<TFunction> GetUserAllFunctions(TUser user)
        {
            user.CheckNotNull("user");
            List<TModuleKey> moduleIds = ModuleRepository.Entities.Where(m => m.Users.Select(n => n.Id).Contains(user.Id)).Select(m => m.Id).ToList();
            return moduleIds.SelectMany(GetAllFunctions).DistinctBy(m => m.Id);
        }

        /// <summary>
        /// 给用户添加模块权限
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="moduleIds">要添加的模块编号集合</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> SetUserModules(TUser user, params TModuleKey[] moduleIds)
        {
            user.CheckNotNull("user");

            TModuleKey[] existModuleIds = ModuleRepository.Entities.Where(m => m.Users.Select(n => n.Id).Contains(user.Id)).Select(m => m.Id).ToArray();
            TModuleKey[] addModuleIds = moduleIds.Except(existModuleIds).ToArray();
            TModuleKey[] removeModuleIds = existModuleIds.Except(moduleIds).ToArray();
            List<string> addNames = new List<string>(), removeNames = new List<string>();

            ModuleRepository.UnitOfWork.BeginTransaction();
            foreach (TModuleKey moduleId in addModuleIds)
            {
                TModule module = await ModuleRepository.GetByKeyAsync(moduleId);
                if (module == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "编号为“{0}”的模块信息不存在".FormatWith(moduleId));
                }
                module.Users.Add(user);
                addNames.Add(module.Name);
            }
            foreach (TModuleKey moduleId in removeModuleIds)
            {
                TModule module = await ModuleRepository.GetByKeyAsync(moduleId);
                if (module == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "编号为“{0}”的模块信息不存在".FormatWith(moduleId));
                }
                module.Users.Remove(user);
                removeNames.Add(module.Name);
            }
            //todo:确认更新 user 是否有效
            int count = await UserRepository.UpdateAsync(user);

            if (count > 0)
            {
                //移除 当前用户的功能权限缓存，使其更新
                FunctionAuthCache.RemoveUserCaches(new[] { user.UserName });

                return new OperationResult(OperationResultType.Success,
                    "用户“{0}”添加模块“{1}”，移除模块“{2}”操作成功".FormatWith(user.UserName, addNames.ExpandAndToString(), removeNames.ExpandAndToString()));
            }
            return OperationResult.NoChanged;
        }

        #endregion

        #region Implementation of IModuleStore<TModule,in TModuleKey,in TModuleInputDto,TFunction,TFunctionKey,TRole,TRoleKey,TUser,TUserKey>

        /// <summary>
        /// 获取 模块信息查询数据集
        /// </summary>
        public IQueryable<TModule> Modules
        {
            get { return ModuleRepository.Entities; }
        }

        /// <summary>
        /// 检查模块信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的模块信息编号</param>
        /// <returns>模块信息是否存在</returns>
        public virtual Task<bool> CheckModuleExists(Expression<Func<TModule, bool>> predicate, TModuleKey id = default(TModuleKey))
        {
            return ModuleRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加模块信息信息
        /// </summary>
        /// <param name="dto">要添加的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> CreateModule(TModuleInputDto dto)
        {
            dto.CheckNotNull("dto");
            var exist = ModuleRepository.Entities.Where(m => m.Name == dto.Name && m.Parent != null && m.Parent.Id.Equals(dto.ParentId))
                .Select(m => new { ParentName = m.Parent.Name }).FirstOrDefault();
            if (exist != null)
            {
                return new OperationResult(OperationResultType.Error, "模块“{0}”中已存在名称为“{1}”的子模块，不能重复添加".FormatWith(exist.ParentName, dto.Name));
            }

            TModule entity = dto.MapTo<TModule>();
            //排序码，不存在为1，否则同级最大+1
            var peerModules = ModuleRepository.Entities.Where(m => m.Parent.Id.Equals(dto.ParentId)).Select(m => new { m.OrderCode }).ToArray();
            if (peerModules.Length == 0)
            {
                entity.OrderCode = 1;
            }
            else
            {
                double maxCode = peerModules.Max(m => m.OrderCode);
                entity.OrderCode = maxCode + 1;
            }
            //父模块
            if (!dto.ParentId.Equals(default(TModuleKey)))
            {
                TModule parent = await ModuleRepository.GetByKeyAsync(dto.ParentId);
                if (parent == null)
                {
                    return new OperationResult(OperationResultType.Error, "编号为“{0}”的父模块信息不存在".FormatWith(dto.ParentId));
                }
                entity.Parent = parent;
            }
            else
            {
                entity.Parent = default(TModule);
            }
            ModuleRepository.UnitOfWork.BeginTransaction();
            int count = await ModuleRepository.InsertAsync(entity);
            entity.TreePathString = entity.GetTreePath();
            await ModuleRepository.UpdateAsync(entity);

            return count > 0
                ? new OperationResult(OperationResultType.Success, "模块“{0}”创建成功".FormatWith(dto.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新模块信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> UpdateModule(TModuleInputDto dto)
        {
            dto.CheckNotNull("dto");
            var exist = ModuleRepository.Entities.Where(m => m.Name == dto.Name && m.Parent != null && m.Parent.Id.Equals(dto.ParentId))
                .Select(m => new { m.Id, ParentName = m.Parent.Name }).FirstOrDefault();
            if (exist != null && !exist.Id.Equals(dto.Id))
            {
                return new OperationResult(OperationResultType.Error, "模块“{0}”中已存在名称为“{1}”的子模块，不能重复添加".FormatWith(exist.ParentName, dto.Name));
            }
            TModule entity = await ModuleRepository.GetByKeyAsync(dto.Id);
            if (entity == null)
            {
                return new OperationResult(OperationResultType.Error, "编号为“{0}”的模块信息不存在".FormatWith(dto.Id));
            }
            entity = dto.MapTo(entity);
            if (!dto.ParentId.Equals(default(TModuleKey)))
            {
                if (entity.Parent == null || !entity.Parent.Id.Equals(dto.ParentId))
                {
                    TModule parent = await ModuleRepository.GetByKeyAsync(dto.ParentId);
                    if (parent == null)
                    {
                        return new OperationResult(OperationResultType.Error, "编号为“{0}”的父模块信息不存在".FormatWith(dto.ParentId));
                    }
                    entity.Parent = parent;
                }
            }
            else
            {
                entity.Parent = default(TModule);
            }
            entity.TreePathString = entity.GetTreePath();
            return await ModuleRepository.UpdateAsync(entity) > 0
                ? new OperationResult(OperationResultType.Success, "模块“{0}”更新成功".FormatWith(dto.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除模块信息信息
        /// </summary>
        /// <param name="id">要删除的模块信息编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteModule(TModuleKey id)
        {
            TModule entity = await ModuleRepository.GetByKeyAsync(id);
            if (entity == null)
            {
                return OperationResult.Success;
            }
            if (entity.SubModules.Count > 0)
            {
                return new OperationResult(OperationResultType.Error, "模块“{0}”的子模块不为空，不能删除".FormatWith(entity.Name));
            }
            entity.Functions.Clear();
            return await ModuleRepository.DeleteAsync(entity) > 0
                ? new OperationResult(OperationResultType.Success, "模块“{0}”删除成功".FormatWith(entity.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 获取指定模块及其父模块的所有可用功能集合
        /// </summary>
        /// <param name="id">要查询的顶模块信息</param>
        /// <returns>允许的功能集合</returns>
        public virtual IEnumerable<TFunction> GetAllFunctions(TModuleKey id)
        {
            TModule module = ModuleRepository.GetByKey(id);
            if (module == null)
            {
                throw new InvalidOperationException("编号为“{0}”的模块信息不存在".FormatWith(id));
            }
            TModuleKey[] keys = module.TreePathIds;
            return ModuleRepository.Entities.Where(m => keys.Contains(m.Id)).SelectMany(m => m.Functions).DistinctBy(m => m.Id);
        }

        /// <summary>
        /// 设置模块拥有的功能
        /// </summary>
        /// <param name="id">模块编号</param>
        /// <param name="functionIds">功能编号集合</param>
        /// <returns></returns>
        public virtual async Task<OperationResult> SetModuleFunctions(TModuleKey id, TFunctionKey[] functionIds)
        {
            var module = ModuleRepository.TrackEntities.Where(m => m.Id.Equals(id)).Select(m => new
            {
                Data = m,
                FunctionIds = m.Functions.Select(n => n.Id)
            }).FirstOrDefault();
            if (module == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "编号为“{0}”的模块信息不存在".FormatWith(id));
            }
            TModule entity = module.Data;
            TFunctionKey[] addFunctionIds = functionIds.Except(module.FunctionIds).Distinct().ToArray();
            if (addFunctionIds.Length > 0)
            {
                TFunction[] functions = FunctionRepository.TrackEntities.Where(m => addFunctionIds.Contains(m.Id)).ToArray();
                foreach (TFunction function in functions)
                {
                    entity.Functions.Add(function);
                }
            }
            TFunctionKey[] removeFunctionIds = module.FunctionIds.Except(functionIds).Distinct().ToArray();
            if (removeFunctionIds.Length > 0)
            {
                TFunction[] functions = FunctionRepository.TrackEntities.Where(m => removeFunctionIds.Contains(m.Id)).ToArray();
                foreach (TFunction function in functions)
                {
                    entity.Functions.Remove(function);
                }
            }
            int count = await ModuleRepository.UpdateAsync(entity);

            if (count > 0)
            {
                //移除 涉及功能的功能权限缓存，使其更新
                TFunctionKey[] relateFunctionIds = addFunctionIds.Union(removeFunctionIds).Distinct().ToArray();
                FunctionAuthCache.RemoveFunctionCaches(relateFunctionIds);

                return new OperationResult(OperationResultType.Success,
                    "模块“{0}”设置功能成功，共添加 {1} 个功能，移除 {2} 个功能。".FormatWith(entity.Name, addFunctionIds.Length, removeFunctionIds.Length));
            }
            return OperationResult.NoChanged;
        }

        #endregion

        #region Implementation of IFunctionStore<TFunction,in TFunctionKey,in TFunctionInputDto>

        /// <summary>
        /// 获取 功能信息查询数据集
        /// </summary>
        public IQueryable<TFunction> Functions
        {
            get { return FunctionRepository.Entities; }
        }

        /// <summary>
        /// 检查功能信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的功能信息编号</param>
        /// <returns>功能信息是否存在</returns>
        public virtual Task<bool> CheckFunctionExists(Expression<Func<TFunction, bool>> predicate, TFunctionKey id = default(TFunctionKey))
        {
            return FunctionRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加功能信息信息
        /// </summary>
        /// <param name="dto">要添加的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> CreateFunction(TFunctionInputDto dto)
        {
            if (dto.Url.IsMissing())
            {
                return new OperationResult(OperationResultType.Error, "自定义功能的URL不能为空");
            }
            if (await FunctionRepository.CheckExistsAsync(m => m.Name == dto.Name))
            {
                return new OperationResult(OperationResultType.Error, "名称为“{0}”的功能信息已存在".FormatWith(dto.Name));
            }
            if (dto.Url == null && FunctionRepository.CheckExists(m => m.Area == dto.Area && m.Controller == dto.Controller && m.Action == dto.Action))
            {
                return new OperationResult(OperationResultType.Error,
                    "区域“{0}”控制器“{1}”方法“{2}”的功能信息已存在".FormatWith(dto.Area, dto.Controller, dto.Action));
            }
            TFunction entity = dto.MapTo<TFunction>();
            entity.IsCustom = true;
            if (entity.Url.IsMissing())
            {
                entity.Url = null;
            }
            return await FunctionRepository.InsertAsync(entity) > 0
                ? new OperationResult(OperationResultType.Success, "功能“{0}”创建成功".FormatWith(dto.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新功能信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> UpdateFunction(TFunctionInputDto dto)
        {
            if (await FunctionRepository.CheckExistsAsync(m => m.Name == dto.Name, dto.Id))
            {
                return new OperationResult(OperationResultType.Error, "名称为“{0}”的功能信息已存在".FormatWith(dto.Name));
            }
            TFunction entity = await FunctionRepository.GetByKeyAsync(dto.Id);
            if (entity == null)
            {
                return new OperationResult(OperationResultType.QueryNull);
            }
            FunctionType oldType = entity.FunctionType;
            if (dto.DataLogEnabled && !dto.OperateLogEnabled && !entity.OperateLogEnabled && !entity.DataLogEnabled)
            {
                dto.OperateLogEnabled = true;
            }
            else if (!dto.OperateLogEnabled && dto.DataLogEnabled && entity.OperateLogEnabled && entity.DataLogEnabled)
            {
                dto.DataLogEnabled = false;
            }
            entity = dto.MapTo(entity);
            if (entity.Url.IsNullOrEmpty())
            {
                entity.Url = null;
            }
            if (oldType != entity.FunctionType)
            {
                entity.IsTypeChanged = true;
            }
            return await FunctionRepository.UpdateAsync(entity) > 0
                ? new OperationResult(OperationResultType.Success, "功能“{0}”更新成功".FormatWith(dto.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除功能信息信息
        /// </summary>
        /// <param name="id">要删除的功能信息编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteFunction(TFunctionKey id)
        {
            TFunction entity = await FunctionRepository.GetByKeyAsync(id);
            if (entity == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "编号为“{0}”的功能信息不存在".FormatWith(id));
            }
            if (!entity.IsCustom && !entity.IsDeleted)
            {
                return new OperationResult(OperationResultType.Error, "功能“{0}”不是自定义功能，并且未被回收，不能删除".FormatWith(entity.Name));
            }
            return await FunctionRepository.DeleteAsync(entity) > 0
                ? new OperationResult(OperationResultType.Success, "功能“{0}”删除成功".FormatWith(entity.Name))
                : OperationResult.NoChanged;
        }

        #endregion

        #region Implementation of IEntityInfoStore<TEntityInfo,in TEntityInfoKey,in TEntityInfoInputDto>

        /// <summary>
        /// 获取 实体数据信息查询数据集
        /// </summary>
        public IQueryable<TEntityInfo> EntityInfos
        {
            get { return EntityInfoRepository.Entities; }
        }

        /// <summary>
        /// 检查实体数据信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的实体数据信息编号</param>
        /// <returns>实体数据信息是否存在</returns>
        public virtual Task<bool> CheckEntityInfoExists(Expression<Func<TEntityInfo, bool>> predicate, TEntityInfoKey id = default(TEntityInfoKey))
        {
            return EntityInfoRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 更新实体数据信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的实体数据信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> UpdateEntityInfo(TEntityInfoInputDto dto)
        {
            TEntityInfo entity = await EntityInfoRepository.GetByKeyAsync(dto.Id);
            if (entity == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "编号为“{0}”的实体数据信息不存在".FormatWith(dto.Id));
            }
            entity = dto.MapTo(entity);
            return await EntityInfoRepository.UpdateAsync(entity) > 0
                ? new OperationResult(OperationResultType.Success, "实体“{0}”更新成功".FormatWith(dto.Name))
                : OperationResult.NoChanged;
        }

        #endregion

        #region Implementation of IEntityRoleStore<in TEntityRoleMapInputDto,in TEntityRoleMapKey,in TEntityInfoKey,in TRoleKey>

        /// <summary>
        /// 增加数据角色映射信息
        /// </summary>
        /// <param name="dto">数据角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> CreateEntityRoleMapAsync(TEntityRoleMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            if (await EntityRoleMapRepository.CheckExistsAsync(m => m.EntityInfo.Id.Equals(dto.EntityInfoId) && m.Role.Id.Equals(dto.RoleId)))
            {
                return OperationResult.Success;
            }
            TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
            if (entityInfo == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
            }
            TRole role = await RoleRepository.GetByKeyAsync(dto.RoleId);
            if (role == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
            }
            TEntityRoleMap map = dto.MapTo<TEntityRoleMap>();
            map.EntityInfo = entityInfo;
            map.Role = role;
            if (dto.FilterGroup != null)
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityRoleMapRepository.InsertAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "角色“{0}”与数据“{1}”映射信息创建成功".FormatWith(role.Name, entityInfo.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 编辑数据角色映射信息
        /// </summary>
        /// <param name="dto">数据角色映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> UpdateEntityRoleMapAsync(TEntityRoleMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            TEntityRoleMap map = await EntityRoleMapRepository.GetByKeyAsync(dto.Id);
            if (map == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据角色映射信息不存在");
            }
            map = dto.MapTo(map);
            if (map.EntityInfo == null || !map.EntityInfo.Id.Equals(dto.EntityInfoId))
            {
                TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
                if (entityInfo == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
                }
                map.EntityInfo = entityInfo;
            }
            if (map.Role == null || !map.Role.Id.Equals(dto.RoleId))
            {
                TRole role = await RoleRepository.GetByKeyAsync(dto.RoleId);
                if (role == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
                }
                map.Role = role;
            }
            if (map.FilterGroupJson != dto.FilterGroup.ToJsonString())
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityRoleMapRepository.UpdateAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "角色“{0}”与实体“{1}”的映射信息更新成功".FormatWith(map.Role.Name, map.EntityInfo.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除数据角色映射信息
        /// </summary>
        /// <param name="id">数据角色映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteEntityRoleMapAsync(TEntityRoleMapKey id)
        {
            TEntityRoleMap map = await EntityRoleMapRepository.GetByKeyAsync(id);
            if (map == null)
            {
                return OperationResult.NoChanged;
            }
            return await EntityRoleMapRepository.DeleteAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "角色与实体的映射信息删除成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 查找指定数据与角色的查询条件组
        /// </summary>
        /// <param name="entityInfoId">数据实体编号</param>
        /// <param name="roleId">角色编号</param>
        /// <returns>过滤条件组</returns>
        public virtual Task<FilterGroup> GetRoleFilterGroup(TEntityInfoKey entityInfoId, TRoleKey roleId)
        {
            var result = EntityRoleMapRepository.Entities.Where(m => m.EntityInfo.Id.Equals(entityInfoId) && m.Role.Id.Equals(roleId))
                .Select(m => new { m.FilterGroup }).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return Task.FromResult(result.FilterGroup);
        }

        #endregion

        #region Implementation of IEntityUserStore<in TEntityUserMapInputDto,in TEntityUserMapKey,in TEntityInfoKey,in TUserKey>

        /// <summary>
        /// 增加数据用户映射信息
        /// </summary>
        /// <param name="dto">数据用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> CreateEntityUserMapAsync(TEntityUserMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            if (await EntityUserMapRepository.CheckExistsAsync(m => m.EntityInfo.Id.Equals(dto.EntityInfoId) && m.User.Id.Equals(dto.UserId)))
            {
                return OperationResult.Success;
            }
            TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
            if (entityInfo == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
            }
            TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
            if (user == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
            }
            TEntityUserMap map = dto.MapTo<TEntityUserMap>();
            map.EntityInfo = entityInfo;
            map.User = user;
            if (dto.FilterGroup != null)
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityUserMapRepository.InsertAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "用户“{0}”与实体“{1}”的映射信息创建成功".FormatWith(user.UserName, entityInfo.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 编辑数据用户映射信息
        /// </summary>
        /// <param name="dto">数据用户映射信息DTO</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> UpdateEntityUserMapAsync(TEntityUserMapInputDto dto)
        {
            dto.CheckNotNull("dto");
            TEntityUserMap map = await EntityUserMapRepository.GetByKeyAsync(dto.Id);
            if (map == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定编号的数据角色映射信息不存在");
            }
            map = dto.MapTo(map);
            if (map.EntityInfo == null || !map.EntityInfo.Id.Equals(dto.EntityInfoId))
            {
                TEntityInfo entityInfo = await EntityInfoRepository.GetByKeyAsync(dto.EntityInfoId);
                if (entityInfo == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的数据实体信息不存在");
                }
                map.EntityInfo = entityInfo;
            }
            if (map.User == null || !map.User.Id.Equals(dto.UserId))
            {
                TUser user = await UserRepository.GetByKeyAsync(dto.UserId);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定编号的角色信息不存在");
                }
                map.User = user;
            }
            if (map.FilterGroupJson != dto.FilterGroup.ToJsonString())
            {
                map.FilterGroupJson = dto.FilterGroup.ToJsonString();
            }
            return await EntityUserMapRepository.UpdateAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "用户“{0}”与实体“{1}”的映射信息更新成功".FormatWith(map.User.UserName, map.EntityInfo.Name))
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除数据用户映射信息
        /// </summary>
        /// <param name="id">数据用户映射编号</param>
        /// <returns>业务操作结果</returns>
        public virtual async Task<OperationResult> DeleteEntityUserMapAsync(TEntityUserMapKey id)
        {
            TEntityUserMap map = await EntityUserMapRepository.GetByKeyAsync(id);
            if (map == null)
            {
                return OperationResult.NoChanged;
            }
            return await EntityUserMapRepository.DeleteAsync(map) > 0
                ? new OperationResult(OperationResultType.Success, "用户与实体的映射信息删除成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 查找指定数据与用户的查询条件组
        /// </summary>
        /// <param name="entityInfoId">数据实体编号</param>
        /// <param name="roleId">用户编号</param>
        /// <returns>过滤条件组</returns>
        public virtual Task<FilterGroup> GetUserFilterGroup(TEntityInfoKey entityInfoId, TUserKey roleId)
        {
            var result = EntityUserMapRepository.Entities.Where(m => m.EntityInfo.Id.Equals(entityInfoId) && m.User.Id.Equals(roleId))
                .Select(m => new { m.FilterGroup }).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return Task.FromResult(result.FilterGroup);
        }

        #endregion
    }
}