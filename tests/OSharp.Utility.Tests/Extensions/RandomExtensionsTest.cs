using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Extensions;

namespace OSharp.Utility.Extensions.Tests
{
    [TestClass]
    public class RandomExtensionsTest
    {
        [TestMethod()]
        public void NextBooleanTest()
        {
            Random rnd = new Random();
            bool value = rnd.NextBoolean();
            Assert.IsTrue(new[] { true, false }.Contains(value));
        }

        [TestMethod]
        public void NextEnumTest()
        {
            Random rnd = new Random();
            UriKind kind = rnd.NextEnum<UriKind>();
            Assert.IsTrue(new[] { UriKind.Absolute, UriKind.Relative, UriKind.RelativeOrAbsolute }.Contains(kind));
        }

        [TestMethod()]
        public void NextBytesTest()
        {
            Random rnd = new Random();
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => rnd.NextBytes(-5));

            Byte[] bytes = rnd.NextBytes(10);
            Assert.IsTrue(bytes.Length == 10);
            Assert.IsTrue(bytes.Distinct().Count() > 8);
        }

        [TestMethod()]
        public void NextItemTest()
        {
            Random rnd = new Random();
            int[] array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int item = rnd.NextItem(array);
            CollectionAssert.Contains(array, item);
        }

        [TestMethod()]
        public void NextDateTimeTest()
        {
            Random rnd = new Random();
            DateTime dt = rnd.NextDateTime();
            Assert.IsTrue(dt >= DateTime.MinValue);
            Assert.IsTrue(dt <= DateTime.MaxValue);

            DateTime dtNow = DateTime.Now;
            DateTime dtMin = dtNow.AddMinutes(-10);
            DateTime dtMax = dtNow.AddMinutes(10);
            dt = rnd.NextDateTime(dtMin, dtMax);
            Assert.IsTrue(dt >= dtMin);
            Assert.IsTrue(dt <= dtMax);
        }

        [TestMethod()]
        public void GetRandomNumberStringTest()
        {
            Random rnd = new Random();
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => rnd.GetRandomNumberString(-5));
            string rndNum = rnd.GetRandomNumberString(10);
            Assert.IsTrue(rndNum.Length == 10);
        }

        [TestMethod()]
        public void GetRandomLetterStringTest()
        {
            Random rnd = new Random();
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => rnd.GetRandomNumberString(-5));
            string rndNum = rnd.GetRandomLetterString(10);
            Assert.IsTrue(rndNum.Length == 10);
        }

        [TestMethod()]
        public void GetRandomLetterAndNumberString()
        {
            Random rnd = new Random();
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => rnd.GetRandomNumberString(-5));
            string rndNum = rnd.GetRandomLetterAndNumberString(10);
            Assert.IsTrue(rndNum.Length == 10);
        }
    }
}
