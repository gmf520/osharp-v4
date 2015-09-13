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

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class BooleanExtensionsTests
    {
        [TestMethod()]
        public void ToLowerTest()
        {
            Assert.AreEqual(true.ToLower(), "true");
            Assert.AreEqual(false.ToLower(), "false");
            Assert.AreNotEqual(true.ToLower(), "True");
            Assert.AreNotEqual(false.ToLower(), "False");
        }
    }
}