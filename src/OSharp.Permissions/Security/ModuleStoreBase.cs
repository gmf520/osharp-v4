// -----------------------------------------------------------------------
//  <copyright file="ModuleStoreBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-13 1:01</last-date>
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


namespace OSharp.Core.Security
{
    /// <summary>
    /// 模块存储功能基类
    /// </summary>
    public class ModuleStoreBase<TModule, TModuleKey, TModuleInputDto, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey> :
        IModuleStore<TModule, TModuleKey, TModuleInputDto, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>
        where TModule : IModule<TModuleKey, TModule, TFunction, TFunctionKey, TRole, TRoleKey, TUser, TUserKey>, IEntity<TModuleKey>
        where TModuleInputDto : ModuleBaseInputDto<TModuleKey>
        where TFunction : IFunction, IEntity<TFunctionKey>
        where TRole : IRole<TRoleKey>, IEntity<TRoleKey>
        where TUser : IUser<TUserKey>, IEntity<TUserKey>
        where TModuleKey : struct
    {
        /// <summary>
        /// 获取或设置 模块仓储对象
        /// </summary>
        public IRepository<TModule, TModuleKey> ModuleRepository { get; set; }

        #region 私有方法

        private static TModuleKey[] GetTreePathIds(TModule module)
        {
            List<TModuleKey> keys = new List<TModuleKey>();
            TModule parent = module.Parent;
            while (!parent.Equals(default(TModule)))
            {
                keys.Add(parent.Id);
                parent = parent.Parent;
            }
            keys.Reverse();
            return keys.ToArray();
        }

        #endregion

        #region Implementation of IModuleStore<TModule,TModuleKey,TModuleInputDto,TFunction,TFunctionKey,TRole,TRoleKey>

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
        public Task<bool> CheckModuleExists(Expression<Func<TModule, bool>> predicate, TModuleKey id = default(TModuleKey))
        {
            predicate.CheckNotNull("predicate");
            return ModuleRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加模块信息信息
        /// </summary>
        /// <param name="dto">要添加的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> CreateTModule(TModuleInputDto dto)
        {
            dto.CheckNotNull("dto");
            if (await ModuleRepository.CheckExistsAsync(m => m.Name == dto.Name))
            {
                return new OperationResult(OperationResultType.Error, "名称为“{0}”的模块已存在，不能重复添加".FormatWith(dto.Name));
            }
            TModule module = dto.MapTo<TModule>();
            if (dto.ParentId.HasValue)
            {
                TModule parent = await ModuleRepository.GetByKeyAsync(dto.ParentId.Value);
                if (parent == null)
                {
                    return new OperationResult(OperationResultType.Error, "编号为“{0}”的父模块信息不存在".FormatWith(dto.ParentId.Value));
                }
                module.Parent = parent;
            }
            else
            {
                module.Parent = default(TModule);
            }
            module.TreePathIds = GetTreePathIds(module);
            await ModuleRepository.InsertAsync(module);
            return OperationResult.Success;
        }

        /// <summary>
        /// 更新模块信息信息
        /// </summary>
        /// <param name="dto">包含更新信息的模块信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateTModule(TModuleInputDto dto)
        {
            dto.CheckNotNull("dto");
            if (await ModuleRepository.CheckExistsAsync(m => m.Name == dto.Name, dto.Id))
            {
                return new OperationResult(OperationResultType.Error, "名称为“{0}”的模块已存在，不能重复添加".FormatWith(dto.Name));
            }
            TModule module = await ModuleRepository.GetByKeyAsync(dto.Id);
            if (module == null)
            {
                return new OperationResult(OperationResultType.Error, "编号为“{0}”的模块信息不存在".FormatWith(dto.Id));
            }
            module = dto.MapTo(module);
            if (dto.ParentId.HasValue)
            {
                if (module.Parent == null || !module.Parent.Id.Equals(dto.ParentId))
                {
                    TModule parent = await ModuleRepository.GetByKeyAsync(dto.ParentId.Value);
                    if (parent == null)
                    {
                        return new OperationResult(OperationResultType.Error, "编号为“{0}”的父模块信息不存在".FormatWith(dto.ParentId.Value));
                    }
                    module.Parent = parent;
                }
            }
            else
            {
                module.Parent = default(TModule);
            }
            module.TreePathIds = GetTreePathIds(module);
            await ModuleRepository.UpdateAsync(module);
            return OperationResult.Success;
        }

        /// <summary>
        /// 删除模块信息信息
        /// </summary>
        /// <param name="id">要删除的模块信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteTModule(TModuleKey id)
        {
            TModule module = await ModuleRepository.GetByKeyAsync(id);
            if (module == null)
            {
                return OperationResult.Success;
            }
            if (module.SubModules.Count > 0)
            {
                return new OperationResult(OperationResultType.Error, "模块“{0}”的子模块不为空，不能删除".FormatWith(module.Name));
            }
            module.Functions.Clear();
            await ModuleRepository.DeleteAsync(module);
            return OperationResult.Success;
        }

        #endregion
    }
}