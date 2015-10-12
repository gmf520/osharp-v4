using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OSharp.Utility.Extensions;


namespace OSharp.Utility.Tests
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test01()
        {
            Type t1 = typeof(List<>);
            Assert.IsTrue(t1.IsGenericType);
            Assert.IsTrue(t1.IsGenericTypeDefinition);

            Type info1 = t1.GetTypeInfo();
            Assert.IsTrue(info1.IsGenericType);
            Assert.IsTrue(info1.IsGenericTypeDefinition);
            Assert.IsTrue(t1 == info1);

            Type definition1 = t1.GetGenericTypeDefinition();
            Assert.IsTrue(definition1.IsGenericType);
            Assert.IsTrue(definition1.IsGenericTypeDefinition);
            Assert.IsTrue(t1 == definition1);
            Assert.IsTrue(info1 == definition1);


            //==================================
            Type t2 = typeof(List<string>);
            Assert.IsTrue(t2.IsGenericType);
            Assert.IsFalse(t2.IsGenericTypeDefinition);

            Type info2 = t2.GetTypeInfo();
            Assert.IsTrue(info2.IsGenericType);
            Assert.IsFalse(info2.IsGenericTypeDefinition);
            Assert.IsTrue(t2 == info2);

            Type definition2 = t2.GetGenericTypeDefinition();
            Assert.IsTrue(definition2.IsGenericType);
            Assert.IsTrue(definition2.IsGenericTypeDefinition);
            Assert.IsFalse(t2 == definition2);
            Assert.IsFalse(info2 == definition2);
            Assert.IsFalse(t1 == info2);
            Assert.IsTrue(t1 == definition2);
        }
    }
}
