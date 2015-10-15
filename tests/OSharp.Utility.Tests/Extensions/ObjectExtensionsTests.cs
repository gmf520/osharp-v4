﻿// -----------------------------------------------------------------------
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

using Xunit;


namespace OSharp.Utility.Extensions.Tests
{
    
    public class ObjectExtensionsTests
    {
        [Fact]
        public void CastToTest()
        {
            Assert.Equal(((object)null).CastTo<object>(), null);
            Assert.Equal("123".CastTo<int>(), 123);
            Assert.Equal(123.CastTo<string>(), "123");
            Assert.Equal(true.CastTo<string>(), "True");
            Assert.Equal("true".CastTo<bool>(), true);
            Assert.Equal("56D768A3-3D74-43B4-BD7B-2871D675CC4B".CastTo<Guid>(), new Guid("56D768A3-3D74-43B4-BD7B-2871D675CC4B"));
            Assert.Equal(1.CastTo<UriKind>(), UriKind.Absolute);
            Assert.Equal("RelativeOrAbsolute".CastTo<UriKind>(), UriKind.RelativeOrAbsolute);

            Assert.Equal("abc".CastTo<int>(123), 123);
            
            Assert.Throws<FormatException>(() => "abc".CastTo<int>());
        }

        [Fact]
        public void IsBetweenTest()
        {
            const int num = 5;
            Assert.True(num.IsBetween(0, 10));
            Assert.False(num.IsBetween(5, 10));
            Assert.True(num.IsBetween(5, 10, true));
            Assert.False(num.IsBetween(0, 5));
            Assert.True(num.IsBetween(0, 5, false, true));
            Assert.False(num.IsBetween(5, 5));
            Assert.False(num.IsBetween(5, 5, true));
            Assert.False(num.IsBetween(5, 5, false, true));
            Assert.True(num.IsBetween(5, 5, true, true));
        }

        [Fact]
        public void ToDynamicTest()
        {
            var obj1 = new { Name = "GMF" };
            Assert.True(obj1.ToDynamic().Name == "GMF");
            var obj2 = new { Name = "GMF", Value = new { IsLocked = true } };
            Assert.True(obj2.ToDynamic().Value.IsLocked);
        }
    }
}