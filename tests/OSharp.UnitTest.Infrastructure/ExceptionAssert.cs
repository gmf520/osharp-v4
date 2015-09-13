using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.UnitTest.Infrastructure
{
    /// <summary>
    /// 用于异常Exception的断言
    /// </summary>
    public class ExceptionAssert
    {
        /// <summary>
        /// 获取指定代码执行所引发的异常，无异常返回null
        /// </summary>
        /// <param name="action">要执行的功能</param>
        /// <returns>引发的异常，无异常返回null</returns>
        public static Exception GetException(Action action)
        {
            try
            {
                action();
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        /// <summary>
        /// 检测指定代码是否引发指定类型的异常
        /// </summary>
        /// <param name="action">要执行的功能</param>
        /// <param name="exceptionType">预期异常类型</param>
        public static void IsException(Action action, Type exceptionType)
        {
            Exception e = GetException(action);
            if (e == null)
            {
                return;
            }
            Assert.AreEqual(e.GetType(), exceptionType);
        }

        /// <summary>
        /// 检测指定代码是否引发指定类型的异常
        /// </summary>
        /// <param name="action">要执行的功能</param>
        /// <typeparam name="TException">预期异常类型</typeparam>
        public static void IsException<TException>(Action action)
        {
            IsException(action, typeof(TException));
        }
    }
}
