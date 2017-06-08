// -----------------------------------------------------------------------
//  <copyright file="TypeHubProxyExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-20 15:52</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.AspNet.SignalR.Client;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OSharp.Utility.Reflection;


namespace OSharp.Web.SignalR.Client
{
    /// <summary>
    /// 强类型<see cref="IHubProxy"/>扩展辅助类
    /// </summary>
    public static class TypeHubProxyExtensions
    {
        /// <summary>
        /// 将强类型实例附加到<see cref="IHubProxy"/>上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proxy"></param>
        /// <param name="instance">强类型的实例</param>
        /// <returns></returns>
        public static IDisposable Subscribe<T>(this IHubProxy proxy, T instance)
        {
            Disposer disposer = new Disposer();
            foreach (var method in typeof(T).GetMethods())
            {
                Subscribe(proxy, instance, method, disposer);
            }
            return disposer;
        }

        private static void Subscribe<T>(IHubProxy proxy, T instance, MethodInfo method, ICollection<Action> disposer)
        {
            var subscription = proxy.Subscribe(method.Name);
            var parmTypes = method.GetParameters().Select(i => i.ParameterType).ToArray();
            var fastMethod = FastInvokeHandler.Create(method);
            var invokeHandler = new Action<object[]>(parm => fastMethod.Invoke(instance, parm));
            Action<IList<JToken>> handler = args => OnData(parmTypes, args, proxy.JsonSerializer, invokeHandler);
            subscription.Received += handler;
            disposer.Add(() => subscription.Received -= handler);
        }

        private static void OnData(ICollection<Type> parmType, ICollection<JToken> data, JsonSerializer serializer, Action<object[]> invokeHandler)
        {
            if (parmType.Count != data.Count)
            {
                throw new InvalidOperationException("参数类型数量与实例数量不一致。");
            }
            var parm = data.Zip(parmType, (i1, i2) => i1.ToObject(i2)).ToArray();
            invokeHandler(parm);
        }


        internal class Disposer : List<Action>, IDisposable
        {
            #region Implementation of IDisposable

            /// <summary>
            /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
            /// </summary>
            public void Dispose()
            {
                foreach (var handler in this)
                {
                    handler();
                }
            }

            #endregion
        }
    }
}