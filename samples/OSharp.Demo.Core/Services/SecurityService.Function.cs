﻿// -----------------------------------------------------------------------
//  <copyright file="SecurityService.Function.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 23:26</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Context;
using OSharp.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Mapping;
using OSharp.Core.Security;
using OSharp.Demo.Dtos.Security;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Demo.Services
{
    public partial class SecurityService
    {
        #region Implementation of ISecurityContract

        /// <summary>
        /// 获取 功能信息查询数据集
        /// </summary>
        public IQueryable<Function> Functions
        {
            get { return FunctionRepository.Entities; }
        }

        /// <summary>
        /// 检查功能信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的功能信息编号</param>
        /// <returns>功能信息是否存在</returns>
        public bool CheckFunctionExists(Expression<Func<Function, bool>> predicate, Guid id = new Guid())
        {
            return FunctionRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加功能信息信息
        /// </summary>
        /// <param name="inputDtos">要添加的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddFunctions(params FunctionInputDto[] inputDtos)
        {
            inputDtos.CheckNotNull("dtos");
            OperationResult result = FunctionRepository.Insert(inputDtos,
                dto =>
                {
                    if (dto.Url.IsNullOrWhiteSpace())
                    {
                        throw new Exception("自定义功能的URL不能为空");
                    }
                    if (FunctionRepository.CheckExists(m => m.Name == dto.Name))
                    {
                        throw new Exception("名称为“{0}”的功能信息已存在".FormatWith(dto.Name));
                    }
                    if (dto.Url == null && FunctionRepository.CheckExists(m => m.Area == dto.Area && m.Controller == dto.Controller && m.Action == dto.Action))
                    {
                        throw new Exception("区域“{0}”控制器“{1}”方法“{2}”的功能信息已存在".FormatWith(dto.Area, dto.Controller, dto.Action));
                    }
                },
                (dto, entity) =>
                {
                    entity.IsCustom = true;
                    if (entity.Url.IsNullOrEmpty())
                    {
                        entity.Url = null;
                    }
                    return entity;
                });
            if (result.ResultType == OperationResultType.Success)
            {
                IFunctionHandler handler = ServiceProvider.GetService<IFunctionHandler>();
                handler.RefreshCache();
            }
            return result;
        }

        /// <summary>
        /// 更新功能信息信息
        /// </summary>
        /// <param name="inputDtos">包含更新信息的功能信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditFunctions(params FunctionInputDto[] inputDtos)
        {
            inputDtos.CheckNotNull("dtos");
            List<string> names = new List<string>();
            FunctionRepository.UnitOfWork.TransactionEnabled = true;
            foreach (FunctionInputDto dto in inputDtos)
            {
                if (FunctionRepository.CheckExists(m => m.Name == dto.Name, dto.Id))
                {
                    return new OperationResult(OperationResultType.Error, "名称为“{0}”的功能信息已存在".FormatWith(dto.Name));
                }
                Function entity = FunctionRepository.GetByKey(dto.Id);
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
                FunctionRepository.Update(entity);
                names.Add(entity.Name);
            }
            int count = FunctionRepository.UnitOfWork.SaveChanges();
            OperationResult result = count > 0
                ? new OperationResult(OperationResultType.Success, "功能“{0}”更新成功".FormatWith(names.ExpandAndToString()))
                : new OperationResult(OperationResultType.NoChanged);
            if (result.ResultType == OperationResultType.Success)
            {
                IFunctionHandler handler = ServiceProvider.GetService<IFunctionHandler>();
                handler.RefreshCache();
            }
            return result;
        }

        /// <summary>
        /// 删除功能信息信息
        /// </summary>
        /// <param name="ids">要删除的功能信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteFunctions(params Guid[] ids)
        {
            ids.CheckNotNull("ids");
            List<string>names = new List<string>();
            FunctionRepository.UnitOfWork.TransactionEnabled = true;
            foreach (Guid id in ids)
            {
                Function entity = FunctionRepository.GetByKey(id);
                if (entity == null)
                {
                    return new OperationResult(OperationResultType.QueryNull);
                }
                if (!entity.IsCustom)
                {
                    return new OperationResult(OperationResultType.Error, "功能“{0}”不是自定义功能，不能删除".FormatWith(entity.Name));
                }
                FunctionRepository.Delete(entity);
                names.Add(entity.Name);
            }
            int count = FunctionRepository.UnitOfWork.SaveChanges();
            OperationResult result = count > 0
                ? new OperationResult(OperationResultType.Success, "功能“{0}”删除成功".FormatWith(names.ExpandAndToString()))
                : new OperationResult(OperationResultType.NoChanged);
            if (result.ResultType == OperationResultType.Success)
            {
                IFunctionHandler handler = ServiceProvider.GetService<IFunctionHandler>();
                handler.RefreshCache();
            }
            return result;
        }

        #endregion
    }
}