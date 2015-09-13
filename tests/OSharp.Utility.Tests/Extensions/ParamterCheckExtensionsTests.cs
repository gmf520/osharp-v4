using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Fakes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class ParamterCheckExtensionsTests : UnitTestBase
    {
        [TestMethod()]
        public void RequiredTest()
        {
            ExceptionAssert.IsException<Exception>(()=>"abc".Required(str=>str.Length > 3, "message"));
            "abc".Required(str => str.Length == 3, "message");
        }

        [TestMethod()]
        public void CheckNotNullTest()
        {
            ExceptionAssert.IsException<ArgumentNullException>(() => ((object)null).CheckNotNull("param"));
            new object().CheckNotNull("value");
        }

        [TestMethod()]
        public void CheckNotEmptyTest()
        {
            ExceptionAssert.IsException<ArgumentException>(() => Guid.Empty.CheckNotEmpty("param"));
            Guid.NewGuid().CheckNotEmpty("guid");
        }

        [TestMethod()]
        public void CheckNotNullOrEmptyTest_String()
        {
            ExceptionAssert.IsException<ArgumentNullException>(() => ((string)null).CheckNotNullOrEmpty("param"));
            ExceptionAssert.IsException<ArgumentException>(() => string.Empty.CheckNotNullOrEmpty("param"));
            "abc".CheckNotNullOrEmpty("param");
        }

        [TestMethod()]
        public void CheckNotNullOrEmptyTest_Collection()
        {
            ExceptionAssert.IsException<ArgumentNullException>(() => ((object[])null).CheckNotNullOrEmpty("param"));
            ExceptionAssert.IsException<ArgumentException>(() => new object[] { }.CheckNotNullOrEmpty("param"));
            new object[] { "abc" }.CheckNotNullOrEmpty("param");
        }

        [TestMethod()]
        public void CheckLessThanTest()
        {
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckLessThan("param", 4));
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckLessThan("param", 5));
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckLessThan("param", 4, true));
            5.CheckLessThan("param", 6);
            5.CheckLessThan("param", 5, true);
        }

        [TestMethod()]
        public void CheckGreaterThanTest()
        {
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckGreaterThan("param", 6));
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckGreaterThan("param", 5));
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckGreaterThan("param", 6, true));
            5.CheckGreaterThan("param", 4);
            5.CheckGreaterThan("param", 5, true);
        }

        [TestMethod()]
        public void CheckBetweenTest()
        {
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 1, 4));
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 6, 9));
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 1, 5));
            ExceptionAssert.IsException<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 5, 9));
            5.CheckBetween("param", 1, 9);
            5.CheckBetween("param", 1, 5, false, true);
            5.CheckBetween("param", 5, 9, true, false);
            5.CheckBetween("param", 5, 5, true, true);
        }

        [TestMethod()]
        public void CheckDirectoryExistsTest()
        {
            ExceptionAssert.IsException<ArgumentNullException>(() => ((string)null).CheckDirectoryExists("param"));
            ShimDirectory.ExistsString = m => false;
            ExceptionAssert.IsException<DirectoryNotFoundException>(() => "path".CheckDirectoryExists("param"));
            ShimDirectory.ExistsString = m => true;
            "path".CheckDirectoryExists("param");
        }

        [TestMethod()]
        public void CheckFileExistsTest()
        {
            ExceptionAssert.IsException<ArgumentNullException>(()=>((string)null).CheckFileExists("param"));
            ShimFile.ExistsString = m => false;
            ExceptionAssert.IsException<FileNotFoundException>(()=>"filename".CheckFileExists("param"));
            ShimFile.ExistsString = m => true;
            "filename".CheckFileExists("param");
        }

    }
}
