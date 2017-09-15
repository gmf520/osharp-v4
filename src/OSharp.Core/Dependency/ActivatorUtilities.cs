// -----------------------------------------------------------------------
//  <copyright file="ActivatorUtilities.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-06 22:02</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;

using OSharp.Core.Properties;
using OSharp.Utility.Extensions;


namespace OSharp.Core.Dependency
{
    /// <summary>
    /// 创建实例辅助操作
    /// </summary>
    public static class ActivatorUtilities
    {
        private static readonly MethodInfo GetServiceInfo =
            GetMethodInfo<Func<IServiceProvider, Type, Type, bool, object>>((sp, t, r, c) => GetService(sp, t, r, c));

        /// <summary>
        /// 从服务提供者中创建指定类型与构造参数的实例
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <param name="instanceType">指定类型</param>
        /// <param name="parameters">构造参数</param>
        /// <returns></returns>
        public static object CreateInstance(IServiceProvider provider, Type instanceType, params object[] parameters)
        {
            int bestLength = -1;
            ConstructorMatcher bestMatcher = null;
            foreach (ConstructorMatcher matcher in instanceType.GetTypeInfo().DeclaredConstructors.Where(m => !m.IsStatic && m.IsPublic)
                .Select(m => new ConstructorMatcher(m)))
            {
                int length = matcher.Match(parameters);
                if (length == -1)
                {
                    continue;
                }
                if (bestLength >= length)
                {
                    continue;
                }
                bestLength = length;
                bestMatcher = matcher;
            }
            if (bestMatcher == null)
            {
                throw new InvalidOperationException(Resources.Ioc_NoConstructorMatch.FormatWith(instanceType));
            }
            return bestMatcher.CreateInstance(provider);
        }

        /// <summary>
        /// 从服务提供者中创建指定类型与构造参数的实例
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <param name="paramters">构造参数</param>
        /// <returns></returns>
        public static T CreateInstance<T>(IServiceProvider provider, params object[] paramters)
        {
            return (T)CreateInstance(provider, typeof(T), paramters);
        }

        /// <summary>
        /// 创建获取指定类型实例的委托
        /// </summary>
        /// <param name="instanceType">指定类型</param>
        /// <param name="argumentTypes">构造参数</param>
        /// <returns></returns>
        public static ObjectFactory CreateFactory(Type instanceType, Type[] argumentTypes)
        {
            ConstructorInfo constructor;
            int?[] parameterMap;

            FindApplicableConstructor(instanceType, argumentTypes, out constructor, out parameterMap);

            ParameterExpression provider = Expression.Parameter(typeof(IServiceProvider), "provider");
            ParameterExpression argumentArray = Expression.Parameter(typeof(object[]), "argumentArray");
            Expression factoryExpressionBody = BuildFactoryExpression(constructor, parameterMap, provider, argumentArray);

            Expression<Func<IServiceProvider, object[], object>> factoryLamda = Expression.Lambda<Func<IServiceProvider, object[], object>>(
                factoryExpressionBody, provider, argumentArray);

            Func<IServiceProvider, object[], object> result = factoryLamda.Compile();
            return result.Invoke;
        }

        /// <summary>
        /// 获取或创建指定类型的实例
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="provider">服务提供者</param>
        /// <returns></returns>
        public static T GetServiceOrCreateInstance<T>(IServiceProvider provider)
        {
            return (T)GetServiceOrCreateInstance(provider, typeof(T));
        }

        /// <summary>
        /// 获取或创建指定类型的实例
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public static object GetServiceOrCreateInstance(IServiceProvider provider, Type type)
        {
            return provider.GetService(type) ?? CreateInstance(provider, type);
        }
        private static MethodInfo GetMethodInfo<T>(Expression<T> expr)
        {
            MethodCallExpression mc = (MethodCallExpression)expr.Body;
            return mc.Method;
        }

        private static object GetService(IServiceProvider sp, Type type, Type requiredBy, bool isDefaultParameterRequired)
        {
            object service = sp.GetService(type);
            if (service == null && !isDefaultParameterRequired)
            {
                throw new InvalidOperationException(
                Resources.Ioc_CannotResolveService.FormatWith(requiredBy, type));
            }
            return service;
        }
        private static Expression BuildFactoryExpression(
            ConstructorInfo constructor,
            int?[] parameterMap,
            Expression serviceProvider,
            Expression factoryArgumentArray)
        {
            ParameterInfo[] constructorParameters = constructor.GetParameters();
            Expression[] constructorArguments = new Expression[constructorParameters.Length];

            for (int i = 0; i < constructorParameters.Length; i++)
            {
                Type parameterType = constructorParameters[i].ParameterType;

                if (parameterMap[i] != null)
                {
                    constructorArguments[i] = Expression.ArrayAccess(factoryArgumentArray, Expression.Constant(parameterMap[i]));
                }
                else
                {
                    bool constructorParameterHasDefault = constructorParameters[i].HasDefaultValue;
                    Expression[] parameterTypeExpression = new Expression[] { serviceProvider,
                        Expression.Constant(parameterType, typeof(Type)),
                        Expression.Constant(constructor.DeclaringType, typeof(Type)),
                        Expression.Constant(constructorParameterHasDefault) };
                    constructorArguments[i] = Expression.Call(GetServiceInfo, parameterTypeExpression);
                }

                // Support optional constructor arguments by passing in the default value
                // when the argument would otherwise be null.
                if (constructorParameters[i].HasDefaultValue)
                {
                    ConstantExpression defaultValueExpression = Expression.Constant(constructorParameters[i].DefaultValue);
                    constructorArguments[i] = Expression.Coalesce(constructorArguments[i], defaultValueExpression);
                }

                constructorArguments[i] = Expression.Convert(constructorArguments[i], parameterType);
            }

            return Expression.New(constructor, constructorArguments);
        }

        private static void FindApplicableConstructor(
            Type instanceType,
            Type[] argumentTypes,
            out ConstructorInfo matchingConstructor,
            out int?[] parameterMap)
        {
            matchingConstructor = null;
            parameterMap = null;

            foreach (ConstructorInfo constructor in instanceType.GetTypeInfo().DeclaredConstructors)
            {
                if (constructor.IsStatic || !constructor.IsPublic)
                {
                    continue;
                }

                int?[] tempParameterMap;
                if (TryCreateParameterMap(constructor.GetParameters(), argumentTypes, out tempParameterMap))
                {
                    if (matchingConstructor != null)
                    {
                        throw new InvalidOperationException(Resources.Ioc_NoConstructorMatch.FormatWith(instanceType));
                    }

                    matchingConstructor = constructor;
                    parameterMap = tempParameterMap;
                }
            }

            if (matchingConstructor == null)
            {
                throw new InvalidOperationException(Resources.Ioc_NoConstructorMatch.FormatWith(instanceType));
            }
        }

        // Creates an injective parameterMap from givenParameterTypes to assignable constructorParameters.
        // Returns true if each given parameter type is assignable to a unique; otherwise, false.
        private static bool TryCreateParameterMap(ParameterInfo[] constructorParameters, Type[] argumentTypes, out int?[] parameterMap)
        {
            parameterMap = new int?[constructorParameters.Length];

            for (int i = 0; i < argumentTypes.Length; i++)
            {
                bool foundMatch = false;
                TypeInfo givenParameter = argumentTypes[i].GetTypeInfo();

                for (int j = 0; j < constructorParameters.Length; j++)
                {
                    if (parameterMap[j] != null)
                    {
                        // This ctor parameter has already been matched
                        continue;
                    }

                    if (constructorParameters[j].ParameterType.GetTypeInfo().IsAssignableFrom(givenParameter))
                    {
                        foundMatch = true;
                        parameterMap[j] = i;
                        break;
                    }
                }

                if (!foundMatch)
                {
                    return false;
                }
            }

            return true;
        }


        private class ConstructorMatcher
        {
            private readonly ConstructorInfo _constructor;
            private readonly ParameterInfo[] _parameters;
            private readonly object[] _parameterValues;
            private readonly bool[] _parameterValuesSet;

            public ConstructorMatcher(ConstructorInfo constructor)
            {
                _constructor = constructor;
                _parameters = _constructor.GetParameters();
                _parameterValuesSet = new bool[_parameters.Length];
                _parameterValues = new object[_parameters.Length];
            }

            public int Match(object[] givenParameters)
            {

                int applyIndexStart = 0;
                int applyExactLength = 0;
                for (int givenIndex = 0; givenIndex != givenParameters.Length; ++givenIndex)
                {
                    TypeInfo givenType = givenParameters[givenIndex] == null ? null : givenParameters[givenIndex].GetType().GetTypeInfo();
                    bool givenMatched = false;

                    for (int applyIndex = applyIndexStart; givenMatched == false && applyIndex != _parameters.Length; ++applyIndex)
                    {
                        if (_parameterValuesSet[applyIndex] || !_parameters[applyIndex].ParameterType.GetTypeInfo().IsAssignableFrom(givenType))
                        {
                            continue;
                        }
                        givenMatched = true;
                        _parameterValuesSet[applyIndex] = true;
                        _parameterValues[applyIndex] = givenParameters[givenIndex];
                        if (applyIndexStart != applyIndex)
                        {
                            continue;
                        }
                        applyIndexStart++;
                        if (applyIndex == givenIndex)
                        {
                            applyExactLength = applyIndex;
                        }
                    }

                    if (givenMatched == false)
                    {
                        return -1;
                    }
                }
                return applyExactLength;
            }

            public object CreateInstance(IServiceProvider provider)
            {
                for (int index = 0; index != _parameters.Length; ++index)
                {
                    if (_parameterValuesSet[index] != false)
                    {
                        continue;
                    }
                    object value = provider.GetService(_parameters[index].ParameterType);
                    if (value == null)
                    {
                        if (!_parameters[index].HasDefaultValue)
                        {
                            throw new InvalidOperationException(Resources.Ioc_CannotResolveService.FormatWith(_constructor.DeclaringType, _parameters[index].ParameterType));
                        }
                        _parameterValues[index] = _parameters[index].DefaultValue;
                    }
                    else
                    {
                        _parameterValues[index] = value;
                    }
                }

                try
                {
                    return _constructor.Invoke(_parameterValues);
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                    // The above line will always throw, but the compiler requires we throw explicitly.
                    throw;
                }
            }
        }
    }
}