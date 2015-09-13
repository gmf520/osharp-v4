// -----------------------------------------------------------------------
//  <copyright file="SecurityService.EntityInfo.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-15 2:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Context;
using OSharp.Core.Security;
using OSharp.Demo.Dtos.Security;
using OSharp.Utility.Data;


namespace OSharp.Demo.Services
{
    public partial class SecurityService
    {
        #region Implementation of ISecurityContract

        /// <summary>
        /// 获取 实体数据信息查询数据集
        /// </summary>
        public IQueryable<EntityInfo> EntityInfos
        {
            get { return EntityInfoRepository.Entities; }
        }

        /// <summary>
        /// 更新实体数据信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的实体数据信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditEntityInfos(params EntityInfoDto[] dtos)
        {
            OperationResult result = EntityInfoRepository.Update(dtos);

            if (result.ResultType == OperationResultType.Success)
            {
                OSharpContext.Current.EntityInfoHandler.RefreshCache();
            }
            return result;
        }

        #endregion
    }
}