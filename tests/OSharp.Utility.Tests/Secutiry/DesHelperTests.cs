// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:05 20:52</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Secutiry;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.Secutiry.Tests
{
    [TestClass()]
    public class DesHelperTests
    {
        [TestMethod()]
        public void DesHelperTest()
        {
            Assert.AreEqual(new DesHelper(false).Key.Length, 8);
            Assert.AreEqual(new DesHelper(true).Key.Length, 24);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            string key = "12345678";
            string actual = "TMR29YtnGPI=";
            DesHelper des = new DesHelper(Encoding.UTF8.GetBytes(key));
            Assert.AreEqual(des.Encrypt("admin"), actual);
            Assert.AreEqual(DesHelper.Encrypt("admin", key), actual);

            //弱密钥
            key = "123456781234567812345678";
            des = new DesHelper(Encoding.UTF8.GetBytes(key));
            ExceptionAssert.IsException<CryptographicException>(() => des.Encrypt("admin"));

            key = "!@#$%^&*QWERTYUI12345678";
            actual = "Qp4r67VJ8Z0=";
            des = new DesHelper(Encoding.UTF8.GetBytes(key));
            Assert.AreEqual(des.Encrypt("admin"), actual);
            Assert.AreEqual(DesHelper.Encrypt("admin", key), actual);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            string key = "12345678";
            string actual = "TMR29YtnGPI=";
            DesHelper des = new DesHelper(Encoding.UTF8.GetBytes(key));
            Assert.AreEqual(des.Decrypt(actual), "admin");
            Assert.AreEqual(DesHelper.Decrypt(actual, key), "admin");


            key = "!@#$%^&*QWERTYUI12345678";
            actual = "Qp4r67VJ8Z0=";
            des = new DesHelper(Encoding.UTF8.GetBytes(key));
            Assert.AreEqual(des.Decrypt(actual), "admin");
            Assert.AreEqual(DesHelper.Decrypt(actual, key), "admin");
        }

        public void EncryptAndDecryptTest()
        {
            DesHelper des = new DesHelper();
            Assert.AreEqual(des.Decrypt(des.Encrypt("admin")), "admin");
            des = new DesHelper(true);
            Assert.AreEqual(des.Decrypt(des.Encrypt("admin")), "admin");
        }
    }
}