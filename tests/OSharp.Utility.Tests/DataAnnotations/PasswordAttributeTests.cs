// -----------------------------------------------------------------------
//  <copyright file="PasswordAttributeTests.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-26 14:38</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Utility.DataAnnotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.DataAnnotations.Tests
{
    [TestClass()]
    public class PasswordAttributeTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            PasswordAttribute attr = new PasswordAttribute()
            {
                RequiredLength = 6,
                CanOnlyDigit = true,
                RequiredDigit = true,
                RequiredLowercase = false,
                RequiredUppercase = false
            };
            Assert.IsTrue(attr.IsValid(null));
            Assert.IsFalse(attr.IsValid("213"));
            Assert.IsTrue(attr.IsValid("123456"));
            attr.CanOnlyDigit = false;
            Assert.IsFalse(attr.IsValid("12356"));
            Assert.IsTrue(attr.IsValid("123abc"));
            attr.RequiredLowercase = true;
            Assert.IsFalse(attr.IsValid("123ABC"));
            Assert.IsTrue(attr.IsValid("123abc"));
            attr.RequiredUppercase = true;
            Assert.IsFalse(attr.IsValid("123abc"));
            Assert.IsFalse(attr.IsValid("123ABC"));
            Assert.IsTrue(attr.IsValid("123AbC"));
        }
    }
}