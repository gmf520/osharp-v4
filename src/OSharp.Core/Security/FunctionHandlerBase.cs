// -----------------------------------------------------------------------
//  <copyright file="FunctionHandlerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 16:55</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using OSharp.Core.Data;
using OSharp.Core.Dependency;
using OSharp.Utility.Collections;
using OSharp.Utility.Logging;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 功能信息处理器基类
    /// </summary>
    /// <typeparam name="TFunction">功能信息类型</typeparam>
    /// <typeparam name="TKey">功能信息主键类型</typeparam>
    public abstract class FunctionHandlerBase<TFunction, TKey> : IFunctionHandler
        where TFunction : FunctionBase<TKey>, IFunction, new()
    {
        private ILogger _logger;

        /// <summary>
        /// 获取 日志对象
        /// </summary>
        protected ILogger Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(GetType())); }
        }

        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取 所有功能信息集合
        /// </summary>
        protected TFunction[] Functions { get; private set; }

        /// <summary>
        /// 获取或设置 功能信息类型查找器
        /// </summary>
        public IFunctionTypeFinder TypeFinder { get; set; }

        /// <summary>
        /// 获取或设置 功能信息方法查找器
        /// </summary>
        public IFunctionMethodInfoFinder MethodInfoFinder { get; set; }

        /// <summary>
        /// 获取 功能技术提供者，如Mvc/WebApi/SignalR等，用于区分功能来源，各技术更新功能时，只更新属于自己技术的功能
        /// </summary>
        protected abstract PlatformToken PlatformToken { get; }

        /// <summary>
        /// 从程序集中刷新功能数据，主要检索MVC的Controller-Action信息
        /// </summary>
        public void Initialize()
        {
            Type[] types = TypeFinder.FindAll();
            TFunction[] functions = GetFunctions(types);
            if (functions.Length == 0)
            {
                return;
            }
            UpdateToRepository(functions);
            RefreshCache();
        }

        /// <summary>
        /// 查找指定条件的功能信息
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">功能方法</param>
        /// <returns>符合条件的功能信息</returns>
        public virtual IFunction GetFunction(string area, string controller, string action)
        {
            if (Functions == null)
            {
                RefreshCache();
            }
            Debug.Assert(Functions != null, "Functions != null");
            return Functions.FirstOrDefault(m => m.Area == area && m.Controller == controller
                && m.Action == action && m.PlatformToken == PlatformToken);
        }

        /// <summary>
        /// 查找指定URL的功能信息
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>符合条件的功能信息</returns>
        public virtual IFunction GetFunction(string url)
        {
            if (Functions == null)
            {
                RefreshCache();
            }
            Debug.Assert(Functions != null, "Functions != null");
            return Functions.FirstOrDefault(m => m.Url != null && m.Url == url);
        }

        /// <summary>
        /// 刷新功能信息缓存
        /// </summary>
        public void RefreshCache()
        {
            Functions = GetLastestFunctions();
        }

        /// <summary>
        /// 从控制器类型中获取功能信息集合
        /// </summary>
        /// <param name="types">控制器类型</param>
        /// <returns>功能信息集合</returns>
        protected virtual TFunction[] GetFunctions(Type[] types)
        {
            List<TFunction> functions = new List<TFunction>();
            foreach (Type controllerType in types.OrderBy(m => m.FullName))
            {
                TFunction controller = GetFunction(controllerType);
                if (controller == null)
                {
                    continue;
                }
                if (!ExistsFunction(functions, controller))
                {
                    functions.Add(controller);
                }
                MethodInfo[] methods = MethodInfoFinder.FindAll(controllerType);
                foreach (MethodInfo method in methods)
                {
                    TFunction action = GetFunction(method);
                    if (action == null)
                    {
                        continue;
                    }
                    if (IsIgnoreMethod(action, method, functions))
                    {
                        continue;
                    }
                    functions.Add(action);
                }
            }
            return functions.ToArray();
        }

        /// <summary>
        /// 重写以实现从类型信息创建功能信息
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns></returns>
        protected abstract TFunction GetFunction(Type type);

        /// <summary>
        /// 重写以实现从方法信息创建功能信息
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns></returns>
        protected abstract TFunction GetFunction(MethodInfo method);

        /// <summary>
        /// 重写以判断指定功能信息是否存在
        /// </summary>
        /// <param name="functions">功能信息集合</param>
        /// <param name="function">要判断的功能信息</param>
        /// <returns></returns>
        protected virtual bool ExistsFunction(IEnumerable<TFunction> functions, TFunction function)
        {
            return functions.Any(m => m.Action == function.Action && m.Controller == function.Controller
                && m.Area == function.Area && m.Name == function.Name && m.PlatformToken == PlatformToken);
        }

        /// <summary>
        /// 重写以实现功能信息查找
        /// </summary>
        /// <param name="functions">功能信息集合</param>
        /// <param name="action">方法名称</param>
        /// <param name="controller">类型名称</param>
        /// <param name="area">区域名称</param>
        /// <param name="name">功能名称</param>
        /// <returns></returns>
        protected virtual TFunction GetFunction(IEnumerable<TFunction> functions, string action, string controller, string area, string name)
        {
            return functions.FirstOrDefault(m => m.Action == action && m.Controller == controller
                && m.Area == area && m.Name == name && m.PlatformToken == PlatformToken);
        }

        /// <summary>
        /// 重写以实现是否忽略指定方法的功能信息
        /// </summary>
        /// <param name="action">要判断的功能信息</param>
        /// <param name="method">功能相关的方法信息</param>
        /// <param name="functions">已存在的功能信息集合</param>
        /// <returns></returns>
        protected virtual bool IsIgnoreMethod(TFunction action, MethodInfo method, IEnumerable<TFunction> functions)
        {
            TFunction exist = GetFunction(functions, action.Action, action.Controller, action.Area, action.Name);
            return exist != null;
        }

        /// <summary>
        /// 更新功能信息到数据库中
        /// </summary>
        /// <param name="functions">功能信息集合</param>
        protected virtual void UpdateToRepository(TFunction[] functions)
        {
            if (functions.Length == 0)
            {
                return;
            }
            IRepository<TFunction, TKey> repository = ServiceProvider.GetService<IRepository<TFunction, TKey>>();
            if (repository == null)
            {
                return;
            }
            TFunction[] items = repository.TrackEntities.Where(m => m.PlatformToken == PlatformToken).ToArray();

            //删除的功能（排除自定义功能信息）
            TFunction[] removeItems = items.Where(m => !m.IsCustom).Except(functions,
                EqualityHelper<TFunction>.CreateComparer(m => m.Area + m.Controller + m.Action + m.PlatformToken)).ToArray();
            int removeCount = removeItems.Length;
            repository.UnitOfWork.BeginTransaction();
            int tmpCount = 0;
            foreach (TFunction removeItem in removeItems)
            {
                try
                {
                    removeItem.IsDeleted = true;
                    tmpCount += repository.Delete(removeItem);
                }
                catch (Exception)
                {
                    //无法物理删除，可能是外键约束，改为逻辑删除
                    tmpCount += repository.Recycle(removeItem);
                }
            }
            repository.UnitOfWork.Commit();
            if (tmpCount > 0)
            {
                items = repository.TrackEntities.ToArray();
            }

            repository.UnitOfWork.BeginTransaction();
            //处理新增的功能
            TFunction[] addItems = functions.Except(items,
                EqualityHelper<TFunction>.CreateComparer(m => m.Area + m.Controller + m.Action + m.PlatformToken)).ToArray();
            int addCount = addItems.Length;
            repository.Insert(addItems.AsEnumerable());

            //处理更新的功能
            int updateCount = 0;
            tmpCount = 0;
            foreach (TFunction item in items)
            {
                if (item.IsCustom)
                {
                    continue;
                }
                bool isUpdate = false;
                TFunction function = functions.SingleOrDefault(m => m.Area == item.Area && m.Controller == item.Controller
                    && m.Action == item.Action && m.PlatformToken == PlatformToken);
                if (function == null)
                {
                    continue;
                }
                if (item.Name != function.Name)
                {
                    item.Name = function.Name;
                    isUpdate = true;
                }
                if (item.IsAjax != function.IsAjax)
                {
                    item.IsAjax = function.IsAjax;
                    isUpdate = true;
                }
                if (!item.IsTypeChanged && item.FunctionType != function.FunctionType)
                {
                    item.FunctionType = function.FunctionType;
                    isUpdate = true;
                }
                if (isUpdate)
                {
                    tmpCount += repository.Update(item);
                    updateCount++;
                }
            }
            repository.UnitOfWork.Commit();
            if (removeCount > 0 || tmpCount > 0)
            {
                string message = "刷新功能信息";
                if (addCount > 0)
                {
                    message += "，添加功能信息 " + addCount + " 个";
                }
                if (updateCount > 0)
                {
                    message += "，更新功能信息 " + updateCount + " 个";
                }
                if (removeCount > 0)
                {
                    message += "，移除功能信息 " + removeCount + " 个";
                }
                Logger.Info(message);
            }
        }

        /// <summary>
        /// 重写以实现从类型中获取功能的区域信息
        /// </summary>
        protected abstract string GetArea(Type type);

        /// <summary>
        /// 获取最新功能信息
        /// </summary>
        /// <returns></returns>
        protected virtual TFunction[] GetLastestFunctions()
        {
            IRepository<TFunction, TKey> repository = ServiceProvider.GetService<IRepository<TFunction, TKey>>();
            if (repository == null)
            {
                return new TFunction[0];
            }
            return repository.Entities.Where(m => m.PlatformToken == PlatformToken).ToArray();
        }
    }
}