// -----------------------------------------------------------------------
//  <copyright file="ExpressionTools.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-20 15:59</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace OSharp.Web.SignalR.Client.Reflection
{
    /// <summary>
    /// Tools for working with expressions
    /// </summary>
    internal static class ExpressionTools
    {
        /// <summary>
        /// Get a method name and its argument values from an expression
        /// <para>This is used for making dynamic invocations or turning .Net calls into wire-API calls</para>
        /// </summary>
        public static Invocation GetInvocation<T>(this Expression<Action<T>> action)
        {
            if (!(action.Body is MethodCallExpression))
            {
                throw new ArgumentException("Action must be a method call", "action");
            }

            var callExpression = (MethodCallExpression)action.Body;

            var i = new Invocation
            {
                ParameterValues = callExpression.Arguments.Select(ReduceToConstant).ToArray(),
                MethodName = callExpression.Method.Name
            };
            return i;
        }

        /// <summary>
        /// Get a method name and its argument values from an expression
        /// <para>This is used for making dynamic invocations or turning .Net calls into wire-API calls</para>
        /// </summary>
        public static Invocation GetInvocation<TInput, TResult>(this Expression<Func<TInput, TResult>> action)
        {
            if (!(action.Body is MethodCallExpression))
            {
                throw new ArgumentException("Action must be a method call", "action");
            }

            var callExpression = (MethodCallExpression)action.Body;

            var i = new Invocation
            {
                ParameterValues = callExpression.Arguments.Select(ReduceToConstant).ToArray(),
                MethodName = callExpression.Method.Name,
                ReturnType = typeof(TResult)
            };
            return i;
        }

        /// <summary>
        /// Get the name of a method and that method's parameter types from a lambda expression
        /// <para>This is used for event binding.</para>
        /// </summary>
        public static MethodCallInfo GetBinding(this LambdaExpression exp)
        {
            var unaryExpression = (UnaryExpression)exp.Body;
            var methodCallExpression = (MethodCallExpression)unaryExpression.Operand;

            var methodInfo = MethodInfo(methodCallExpression);

            return new MethodCallInfo
            {
                MethodName = methodInfo.Name,
                ParameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray()
            };
        }

        private static MethodInfo MethodInfo(MethodCallExpression methodCallExpression)
        {
            // This works on .Net 4.0 and 4.5
            var methodInfo = (methodCallExpression.Object == null)
                ? ((MethodInfo)((ConstantExpression)methodCallExpression.Arguments.Last()).Value)
                : ((MethodInfo)((ConstantExpression)methodCallExpression.Object).Value);
            return methodInfo;
        }

        private static object ReduceToConstant(Expression expression)
        {
            var objectMember = Expression.Convert(expression, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }
    }
}