// -----------------------------------------------------------------------
//  <copyright file="DateTimeExtensionsTests.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-05-05 11:49</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Utility.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class DateTimeExtensionsTests
    {
        [TestMethod()]
        public void IsWeekendTest()
        {
            DateTime dt = new DateTime(2015, 5, 2);
            Assert.IsTrue(dt.IsWeekend());
            dt = new DateTime(2015, 5, 3);
            Assert.IsTrue(dt.IsWeekend());
            for (int i = 0; i < 5; i++)
            {
                dt = new DateTime(2015, 5, 4 + i);
                Assert.IsFalse(dt.IsWeekend());
            }
        }

        [TestMethod()]
        public void IsWeekdayTest()
        {
            DateTime dt = new DateTime(2015, 5, 2);
            Assert.IsFalse(dt.IsWeekday());
            dt = new DateTime(2015, 5, 3);
            Assert.IsFalse(dt.IsWeekday());
            for (int i = 0; i < 5; i++)
            {
                dt = new DateTime(2015, 5, 4 + i);
                Assert.IsTrue(dt.IsWeekday());
            }
        }
    }
}