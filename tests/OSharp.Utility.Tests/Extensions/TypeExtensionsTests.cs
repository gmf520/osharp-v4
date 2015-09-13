// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:05 2:53</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Extensions;

using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;


namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class TypeExtensionsTests
    {
        [TestMethod()]
        public void IsNullableTypeTest()
        {
            // ReSharper disable ConvertNullableToShortForm
            Assert.IsTrue(typeof(int?).IsNullableType());
            Assert.IsTrue(typeof(Nullable<int>).IsNullableType());

            Assert.IsFalse(typeof(int).IsNullableType());
        }

        [TestMethod()]
        public void IsEnumerableTest()
        {
            Assert.IsTrue(typeof(string[]).IsEnumerable());
            Assert.IsTrue(typeof(ICollection<string>).IsEnumerable());
            Assert.IsTrue(typeof(IEnumerable<string>).IsEnumerable());
            Assert.IsTrue(typeof(IList<string>).IsEnumerable());
            Assert.IsTrue(typeof(Hashtable).IsEnumerable());
            Assert.IsTrue(typeof(HashSet<string>).IsEnumerable());

            Assert.IsFalse(typeof(int).IsEnumerable());
            Assert.IsFalse(typeof(string).IsEnumerable());
        }

        [TestMethod()]
        public void GetNonNummableType()
        {
            Assert.AreEqual(typeof(int?).GetNonNummableType(), typeof(int));
            Assert.AreEqual(typeof(Nullable<int>).GetNonNummableType(), typeof(int));

            Assert.AreEqual(typeof(int).GetNonNummableType(), typeof(int));
        }

        [TestMethod()]
        public void GetUnNullableTypeTest()
        {
            Assert.AreEqual(typeof(int?).GetUnNullableType(), typeof(int));
            Assert.AreEqual(typeof(Nullable<int>).GetUnNullableType(), typeof(int));

            Assert.AreEqual(typeof(int).GetUnNullableType(), typeof(int));
        }

        [TestMethod()]
        public void ToDescriptionTest()
        {
            Type type = typeof(TestEntity);
            Assert.AreEqual(type.ToDescription(), "测试实体");
            PropertyInfo property = type.GetProperty("Id");
            Assert.AreEqual(property.ToDescription(), "编号");

            type = GetType();
            Assert.AreEqual(type.ToDescription(), "OSharp.Utility.Extensions.Tests.TypeExtensionsTests");
        }

        [TestMethod()]
        public void HasAttributeTest()
        {
            Type type = GetType();
            Assert.IsTrue(type.HasAttribute<TestClassAttribute>());
            MethodInfo method = type.GetMethod("HasAttributeTest");
            Assert.IsTrue(method.HasAttribute<TestMethodAttribute>());
        }

        [TestMethod()]
        public void GetAttributeTest()
        {
            Type type = typeof(TestEntity);
            Assert.AreEqual(type.GetAttribute<DescriptionAttribute>().Description, "测试实体");
            PropertyInfo property = type.GetProperty("Id");
            Assert.AreEqual(property.GetAttribute<DescriptionAttribute>().Description, "编号");
            MethodInfo method = GetType().GetMethod("GetAttributeTest");
            Assert.IsFalse(method.GetAttribute<TestMethodAttribute>() == null);
        }

        [TestMethod()]
        public void GetAttributesTest()
        {
            Type type = GetType();
            Assert.AreEqual(type.GetAttributes<DescriptionAttribute>().Length, 0);
            Assert.AreEqual(type.GetAttributes<TestClassAttribute>().Length, 1);
        }

        [TestMethod()]
        public void IsGenericAssignableFromTest()
        {
            Assert.IsTrue(typeof(IEnumerable<>).IsGenericAssignableFrom(typeof(List<>)));
            Assert.IsTrue(typeof(List<>).IsGenericAssignableFrom(typeof(List<string>)));

            ExceptionAssert.IsException<ArgumentException>(() =>
                (typeof(string)).IsGenericAssignableFrom(typeof(int)));
        }
    }
}