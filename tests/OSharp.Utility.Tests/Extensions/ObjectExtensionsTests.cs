// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:05 3:17</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class ObjectExtensionsTests
    {
        [TestMethod()]
        public void CastToTest()
        {
            Assert.AreEqual(((object)null).CastTo<object>(), null);
            Assert.AreEqual("123".CastTo<int>(), 123);
            Assert.AreEqual(123.CastTo<string>(), "123");
            Assert.AreEqual(true.CastTo<string>(), "True");
            Assert.AreEqual("true".CastTo<bool>(), true);
            Assert.AreEqual("56D768A3-3D74-43B4-BD7B-2871D675CC4B".CastTo<Guid>(), new Guid("56D768A3-3D74-43B4-BD7B-2871D675CC4B"));
            Assert.AreEqual(1.CastTo<UriKind>(), UriKind.Absolute);
            Assert.AreEqual("RelativeOrAbsolute".CastTo<UriKind>(), UriKind.RelativeOrAbsolute);

            Assert.AreEqual("abc".CastTo<int>(123), 123);
            
            ExceptionAssert.IsException<FormatException>(() => "abc".CastTo<int>());
        }

        [TestMethod()]
        public void IsBetweenTest()
        {
            const int num = 5;
            Assert.IsTrue(num.IsBetween(0, 10));
            Assert.IsFalse(num.IsBetween(5, 10));
            Assert.IsTrue(num.IsBetween(5, 10, true));
            Assert.IsFalse(num.IsBetween(0, 5));
            Assert.IsTrue(num.IsBetween(0, 5, false, true));
            Assert.IsFalse(num.IsBetween(5, 5));
            Assert.IsFalse(num.IsBetween(5, 5, true));
            Assert.IsFalse(num.IsBetween(5, 5, false, true));
            Assert.IsTrue(num.IsBetween(5, 5, true, true));
        }

        [TestMethod()]
        public void ToDynamicTest()
        {
            var obj1 = new { Name = "GMF" };
            Assert.IsTrue(obj1.ToDynamic().Name == "GMF");
            var obj2 = new { Name = "GMF", Value = new { IsLocked = true } };
            Assert.IsTrue(obj2.ToDynamic().Value.IsLocked);
        }
    }
}