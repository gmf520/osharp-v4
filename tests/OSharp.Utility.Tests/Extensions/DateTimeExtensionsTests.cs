﻿// -----------------------------------------------------------------------
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

using Smocks;

using Xunit;


namespace OSharp.Utility.Extensions.Tests
{

    public class DateTimeExtensionsTests
    {
        [Fact()]
        public void IsWeekendTest()
        {
            DateTime dt = new DateTime(2015, 5, 2);
            Assert.True(dt.IsWeekend());
            dt = new DateTime(2015, 5, 3);
            Assert.True(dt.IsWeekend());
            for (int i = 0; i < 5; i++)
            {
                dt = new DateTime(2015, 5, 4 + i);
                Assert.False(dt.IsWeekend());
            }
        }

        [Fact()]
        public void IsWeekdayTest()
        {
            DateTime dt = new DateTime(2015, 5, 2);
            Assert.False(dt.IsWeekday());
            dt = new DateTime(2015, 5, 3);
            Assert.False(dt.IsWeekday());
            for (int i = 0; i < 5; i++)
            {
                dt = new DateTime(2015, 5, 4 + i);
                Assert.True(dt.IsWeekday());
            }
        }

        [Fact()]
        public void ToUniqueStringTest()
        {
            DateTime now = new DateTime(2015, 11, 4, 15, 10, 25);
            Assert.Equal(now.ToUniqueString(), "1530854625");
            Assert.Equal(now.ToUniqueString(true), "1530854625000");
        }
    }
}