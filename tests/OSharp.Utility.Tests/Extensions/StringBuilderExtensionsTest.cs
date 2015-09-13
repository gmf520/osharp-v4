using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OSharp.Utility.Extensions;


namespace OSharp.Utility.Tests.Extensions
{
    [TestClass]
    public class StringBuilderExtensionsTest
    {
        [TestMethod]
        public void TrimTest()
        {
            StringBuilder sb = null;
            sb = new StringBuilder("   asd sdf  ");
            Assert.AreEqual(sb.Trim().ToString(), "asd sdf");
        }

        [TestMethod]
        public void TrimStartTest()
        {
            StringBuilder sb = new StringBuilder("asdfgef");
            Assert.AreEqual(sb.TrimStart('a').ToString(), "sdfgef");
            sb.Insert(0, "   ");
            Assert.AreEqual(sb.TrimStart().ToString(), "sdfgef");
            Assert.AreEqual(sb.TrimStart("sdf").ToString(), "gef");
            Assert.AreEqual(sb.TrimStart("gef").ToString(), string.Empty);
        }

        [TestMethod]
        public void TriemEndTest()
        {
            StringBuilder sb;
            sb = new StringBuilder("asdfgef");
            Assert.AreEqual(sb.TrimEnd('a').ToString(), "asdfgef");
            Assert.AreEqual(sb.TrimEnd('f').ToString(), "asdfge");
            sb.Append("   ");
            Assert.AreEqual(sb.TrimEnd().ToString(), "asdfge");
            Assert.AreEqual(sb.TrimEnd(new[] { 'g', 'e' }).ToString(), "asdf");
            Assert.AreEqual(sb.TrimEnd("asdf").ToString(), string.Empty);
        }
    }
}
