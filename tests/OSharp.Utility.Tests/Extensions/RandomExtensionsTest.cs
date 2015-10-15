﻿using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Extensions;

namespace OSharp.Utility.Extensions.Tests
{
    
    public class RandomExtensionsTest
    {
        [Fact]
        public void NextBooleanTest()
        {
            Random rnd = new Random();
            bool value = rnd.NextBoolean();
            Assert.True(new[] { true, false }.Contains(value));
        }

        [Fact]
        public void NextEnumTest()
        {
            Random rnd = new Random();
            UriKind kind = rnd.NextEnum<UriKind>();
            Assert.True(new[] { UriKind.Absolute, UriKind.Relative, UriKind.RelativeOrAbsolute }.Contains(kind));
        }

        [Fact]
        public void NextBytesTest()
        {
            Random rnd = new Random();
            Assert.Throws<ArgumentOutOfRangeException>(() => rnd.NextBytes(-5));

            Byte[] bytes = rnd.NextBytes(10);
            Assert.True(bytes.Length == 10);
            Assert.True(bytes.Distinct().Count() > 8);
        }

        [Fact]
        public void NextItemTest()
        {
            Random rnd = new Random();
            int[] array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int item = rnd.NextItem(array);
            Assert.Contains(item, array);
        }

        [Fact]
        public void NextDateTimeTest()
        {
            Random rnd = new Random();
            DateTime dt = rnd.NextDateTime();
            Assert.True(dt >= DateTime.MinValue);
            Assert.True(dt <= DateTime.MaxValue);

            DateTime dtNow = DateTime.Now;
            DateTime dtMin = dtNow.AddMinutes(-10);
            DateTime dtMax = dtNow.AddMinutes(10);
            dt = rnd.NextDateTime(dtMin, dtMax);
            Assert.True(dt >= dtMin);
            Assert.True(dt <= dtMax);
        }

        [Fact]
        public void GetRandomNumberStringTest()
        {
            Random rnd = new Random();
            Assert.Throws<ArgumentOutOfRangeException>(() => rnd.GetRandomNumberString(-5));
            string rndNum = rnd.GetRandomNumberString(10);
            Assert.True(rndNum.Length == 10);
        }

        [Fact]
        public void GetRandomLetterStringTest()
        {
            Random rnd = new Random();
            Assert.Throws<ArgumentOutOfRangeException>(() => rnd.GetRandomNumberString(-5));
            string rndNum = rnd.GetRandomLetterString(10);
            Assert.True(rndNum.Length == 10);
        }

        [Fact]
        public void GetRandomLetterAndNumberString()
        {
            Random rnd = new Random();
            Assert.Throws<ArgumentOutOfRangeException>(() => rnd.GetRandomNumberString(-5));
            string rndNum = rnd.GetRandomLetterAndNumberString(10);
            Assert.True(rndNum.Length == 10);
        }
    }
}
