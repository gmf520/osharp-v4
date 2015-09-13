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
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void IsMatchTest()
        {
            const string pattern = @"\d.*";
            Assert.IsFalse(((string)null).IsMatch(pattern));
            Assert.IsFalse("abc".IsMatch(pattern));
            Assert.IsTrue("abc123".IsMatch(pattern));
        }

        [TestMethod()]
        public void MatchTest()
        {
            const string pattern = @"\d.*";
            Assert.IsNull(((string)null).Match(pattern));
            Assert.AreEqual("abc".Match(pattern), string.Empty);
            Assert.AreEqual("abc123".Match(pattern), "123");
        }

        [TestMethod()]
        public void MatchesTest()
        {
            const string pattern = @"\d";
            Assert.AreEqual(((string)null).Matches(pattern).Count(), 0);
            Assert.AreEqual("abc".Matches(pattern).Count(), 0);
            Assert.AreEqual("abc123".Matches(pattern).Count(), 3);
        }

        [TestMethod()]
        public void StrLengthTest()
        {
            Assert.AreEqual("".TextLength(), 0);
            Assert.AreEqual("123".TextLength(), 3);
            Assert.AreEqual("abc".TextLength(), 3);
            Assert.AreEqual("$%^*&".TextLength(), 5);
            Assert.AreEqual("汉字测试".TextLength(), 8);
        }

        [TestMethod()]
        public void IsEmailTest()
        {
            string value = null;
            Assert.IsFalse(value.IsEmail());
            value = "123";
            Assert.IsFalse(value.IsEmail());
            value = "abc123.fds";
            Assert.IsFalse(value.IsEmail());
            value = "abc.yeah.net";
            Assert.IsFalse(value.IsEmail());
            value = "abc@yeah.net";
            Assert.IsTrue(value.IsEmail());
        }
    }
}
