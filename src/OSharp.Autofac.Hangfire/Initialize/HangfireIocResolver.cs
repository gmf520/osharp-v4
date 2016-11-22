using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

using Hangfire;

using OSharp.Core.Dependency;


namespace OSharp.Autofac.Hangfire.Initialize
{
    public class HangfireIocResolver : IIocResolver
    {
        /// <summary>
        /// 获取或设置 带生命周期作用域的Job依赖注入对象解析委托
        /// </summary>
        public static Func<Type, object> LifetimeResolveFunc { private get; set; }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            if (LifetimeResolveFunc != null)
            {
                object obj = LifetimeResolveFunc(type);
                if (obj != null)
                {
                    return obj;
                }
            }
            return JobActivator.Current.ActivateJob(type);
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return Resolves(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            Type typeToResolve = typeof(IEnumerable<>).MakeGenericType(type);
            Array array;
            if (LifetimeResolveFunc != null)
            {
                array = LifetimeResolveFunc(typeToResolve) as Array;
            }
            else
            {
                array = Resolve(typeToResolve) as Array;
            }
            if (array != null)
            {
                return array.Cast<object>();
            }
            return new object[0];
        }
    }
}
