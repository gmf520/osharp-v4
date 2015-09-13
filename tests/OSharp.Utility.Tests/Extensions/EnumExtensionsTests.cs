// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:08 17:11</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OSharp.Utility.Extensions;


namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class EnumExtensionsTests
    {
        [TestMethod()]
        public void ToDescriptionTest()
        {
            TestEnum value = TestEnum.EnumItemA;
            Assert.AreEqual(value.ToDescription(), "枚举项A");

            value = TestEnum.EnumItemB;
            Assert.AreEqual(value.ToDescription(), "EnumItemB");
        }


        private enum TestEnum
        {
            [System.ComponentModel.Description("枚举项A")] 
            EnumItemA,

            EnumItemB
        }


    }
}