// -----------------------------------------------------------------------
//  <copyright file="FunctionHandlerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-12 23:55</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

using OSharp.Core.Data;
using OSharp.Core.Reflection;
using OSharp.Core.Security;
using OSharp.Utility;
using OSharp.Utility.Collections;
using OSharp.Utility.Extensions;
using OSharp.Utility.Logging;
using OSharp.Web.Mvc.Security;


namespace OSharp.SiteBase.Security
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
        /// 初始化一个<see cref="FunctionHandlerBase{TFunction, TKey}"/>类型的新实例
        /// </summary>
        protected FunctionHandlerBase()
        {
            ControllerTypeFinder = new ControllerTypeFinder();
            ActionInfoFinder = new MvcActionMethodInfoFinder();
        }

        /// <summary>
        /// 获取 日志对象
        /// </summary>
        protected ILogger Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(GetType())); }
        }

        /// <summary>
        /// 获取 所有功能信息集合
        /// </summary>
        protected TFunction[] Functions { get; private set; }

        /// <summary>
        /// 获取或设置 控制器类型查找器
        /// </summary>
        public ITypeFinder ControllerTypeFinder { get; set; }

        /// <summary>
        /// 获取或设置 功能查找器
        /// </summary>
        public IMethodInfoFinder ActionInfoFinder { get; set; }

        /// <summary>
        /// 从程序集中刷新功能数据，主要检索MVC的Controller-Action信息
        /// </summary>
        public void Initialize()
        {
            Type[] controllerTypes = ControllerTypeFinder.FindAll();
            TFunction[] functions = GetFunctions(controllerTypes);
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
            if (Functions == null || Functions.Length == 0)
            {
                RefreshCache();
            }
            Debug.Assert(Functions != null, "Functions != null");
            return Functions.FirstOrDefault(m => m.Area == area && m.Controller == controller && m.Action == action);
        }
    
        /// <summary>
        /// 查找指定URL的功能信息
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>符合条件的功能信息</returns>
        public virtual IFunction GetFunction(string url)
        {
            if (Functions.Length == 0)
            {
                RefreshCache();
            }
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
        /// <param name="controllerTypes">控制器类型</param>
        /// <returns>功能信息集合</returns>
        protected virtual TFunction[] GetFunctions(Type[] controllerTypes)
        {
            List<TFunction> functions = new List<TFunction>();
            foreach (Type controllerType in controllerTypes.OrderBy(m => m.FullName))
            {
                TFunction controller = GetFunction(controllerType);
                if (!ExistsFunction(functions, controller))
                {
                    functions.Add(controller);
                }
                MethodInfo[] methods = ActionInfoFinder.FindAll(controllerType);
                    //controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    //.Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)).ToArray();
                foreach (MethodInfo method in methods)
                {
                    TFunction action = GetFunction(method);
                    TFunction existAction = GetFunction(functions, action.Action, action.Controller, action.Area, action.Name);
                    //忽略同名的[HttpPost]功能
                    if (existAction != null && method.HasAttribute<HttpPostAttribute>())
                    {
                        continue;
                    }
                    if (existAction == null)
                    {
                        functions.Add(action);
                    }
                }
            }
            return functions.ToArray();
        }

        private TFunction GetFunction(Type type)
        {
            if (!typeof(Controller).IsAssignableFrom(type))
            {
                throw new InvalidOperationException("类型“{0}”不是控制器类型".FormatWith(type.FullName));
            }
            TFunction function = new TFunction()
            {
                Name = type.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("Controller", string.Empty),
                IsController = true,
                FunctionType = FunctionType.Anonymouse
            };
            return function;
        }

        private TFunction GetFunction(MethodInfo method)
        {
            if (method.ReturnType != typeof(ActionResult) && method.ReturnType != typeof(Task<ActionResult>))
            {
                throw new InvalidOperationException("方法“{0}”不是MVC的Action功能".FormatWith(method.Name));
            }

            FunctionType functionType = FunctionType.Anonymouse;
            if (method.HasAttribute<AllowAnonymousAttribute>(true))
            {
                functionType = FunctionType.Anonymouse;
            }
            else if (method.HasAttribute<LoginedAttribute>(true))
            {
                functionType = FunctionType.Logined;
            }
            else if (method.HasAttribute<RoleLimitAttribute>(true))
            {
                functionType = FunctionType.RoleLimit;
            }
            Type type = method.DeclaringType;
            if (type == null)
            {
                throw new InvalidOperationException("声明功能“{0}”的类型为空".FormatWith(method.Name));
            }
            TFunction function = new TFunction()
            {
                Name = method.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("Controller", string.Empty),
                Action = method.Name,
                FunctionType = functionType,
                IsController = false,
                IsAjax = method.HasAttribute<AjaxOnlyAttribute>(),
                IsChild = method.HasAttribute<ChildActionOnlyAttribute>()
            };
            return function;
        }

        private static bool ExistsFunction(IEnumerable<TFunction> functions, TFunction function)
        {
            return functions.Any(m => m.Action == function.Action && m.Controller == function.Controller
                && m.Area == function.Area && m.Name == function.Name);
        }

        private static TFunction GetFunction(IEnumerable<TFunction> functions, string action, string controller, string area, string name)
        {
            return functions.SingleOrDefault(m => m.Action == action && m.Controller == controller && m.Area == area && m.Name == name);
        }

        /// <summary>
        /// 更新功能信息到数据库中
        /// </summary>
        /// <param name="functions">功能信息集合</param>
        protected virtual void UpdateToRepository(TFunction[] functions)
        {
            IRepository<TFunction, TKey> repository = DependencyResolver.Current.GetService<IRepository<TFunction, TKey>>();
            TFunction[] items = repository.GetByPredicate(m => true).ToArray();

            //删除的功能（排除自定义功能信息）
            TFunction[] removeItems = items.Where(m => !m.IsCustom).Except(functions,
                EqualityHelper<TFunction>.CreateComparer(m => m.Area + m.Controller + m.Action)).ToArray();
            int removeCount = removeItems.Length;
            if (repository.Delete(removeItems) > 0)
            {
                items = repository.GetByPredicate(m => true).ToArray();
            }

            repository.UnitOfWork.TransactionEnabled = true;
            //处理新增的功能
            TFunction[] addItems = functions.Except(items,
                EqualityHelper<TFunction>.CreateComparer(m => m.Area + m.Controller + m.Action)).ToArray();
            int addCount = addItems.Length;
            repository.Insert(addItems.AsEnumerable());

            //处理更新的功能
            int updateCount = 0;
            foreach (TFunction item in items)
            {
                if (item.IsCustom)
                {
                    continue;
                }
                bool isUpdate = false;
                TFunction function = functions.Single(m => m.Area == item.Area && m.Controller == item.Controller && m.Action == item.Action);
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
                    repository.Update(item);
                    updateCount++;
                }
            }
            int count = repository.UnitOfWork.SaveChanges();
            if (removeCount > 0 || count > 0)
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
        /// 获取控制器类型所在的区域名称，无区域返回null
        /// </summary>
        protected virtual string GetArea(Type controllerType)
        {
            controllerType.CheckNotNull("controllerType");
            controllerType.Required<Type, InvalidOperationException>(m => typeof(Controller).IsAssignableFrom(m) && !m.IsAbstract,
                "类型“{0}”不是控制器类型".FormatWith(controllerType.FullName));
            string @namespace = controllerType.Namespace;
            if (@namespace == null)
            {
                return null;
            }
            int index = @namespace.IndexOf("Areas", StringComparison.Ordinal) + 6;
            string area = index > 6 ? @namespace.Substring(index, @namespace.IndexOf(".Controllers", StringComparison.Ordinal) - index) : null;
            return area;
        }

        /// <summary>
        /// 获取最新功能信息
        /// </summary>
        /// <returns></returns>
        protected virtual TFunction[] GetLastestFunctions()
        {
            IRepository<TFunction, TKey> repository = DependencyResolver.Current.GetService<IRepository<TFunction, TKey>>();
            return repository.Entities.ToArray();
        }
    }
}