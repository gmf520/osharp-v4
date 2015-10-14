// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:04 9:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Utility.Extensions;

using Xunit;


namespace OSharp.Utility.Extensions.Tests
{
    
    public class BooleanExtensionsTests
    {
        [Fact]
        public void ToLowerTest()
        {
            Assert.Equal(true.ToLower(), "true");
            Assert.Equal(false.ToLower(), "false");
            Assert.NotEqual(true.ToLower(), "True");
            Assert.NotEqual(false.ToLower(), "False");
        }
    }
}