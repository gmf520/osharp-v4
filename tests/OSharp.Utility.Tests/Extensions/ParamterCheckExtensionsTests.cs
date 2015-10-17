using System;
using System.IO;

using Smocks;

using Xunit;


namespace OSharp.Utility.Extensions.Tests
{

    public class ParamterCheckExtensionsTests
    {
        [Fact()]
        public void RequiredTest()
        {
            Assert.Throws<Exception>(() => "abc".Required(str => str.Length > 3, "message"));
            "abc".Required(str => str.Length == 3, "message");
        }

        [Fact()]
        public void CheckNotNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((object)null).CheckNotNull("param"));
            new object().CheckNotNull("value");
        }

        [Fact()]
        public void CheckNotEmptyTest()
        {
            Assert.Throws<ArgumentException>(() => Guid.Empty.CheckNotEmpty("param"));
            Guid.NewGuid().CheckNotEmpty("guid");
        }

        [Fact()]
        public void CheckNotNullOrEmptyTest_String()
        {
            Assert.Throws<ArgumentNullException>(() => ((string)null).CheckNotNullOrEmpty("param"));
            Assert.Throws<ArgumentException>(() => string.Empty.CheckNotNullOrEmpty("param"));
            "abc".CheckNotNullOrEmpty("param");
        }

        [Fact()]
        public void CheckNotNullOrEmptyTest_Collection()
        {
            Assert.Throws<ArgumentNullException>(() => ((object[])null).CheckNotNullOrEmpty("param"));
            Assert.Throws<ArgumentException>(() => new object[] { }.CheckNotNullOrEmpty("param"));
            new object[] { "abc" }.CheckNotNullOrEmpty("param");
        }

        [Fact()]
        public void CheckLessThanTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckLessThan("param", 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckLessThan("param", 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckLessThan("param", 4, true));
            5.CheckLessThan("param", 6);
            5.CheckLessThan("param", 5, true);
        }

        [Fact()]
        public void CheckGreaterThanTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckGreaterThan("param", 6));
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckGreaterThan("param", 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckGreaterThan("param", 6, true));
            5.CheckGreaterThan("param", 4);
            5.CheckGreaterThan("param", 5, true);
        }

        [Fact()]
        public void CheckBetweenTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 1, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 6, 9));
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 1, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.CheckBetween("param", 5, 9));
            5.CheckBetween("param", 1, 9);
            5.CheckBetween("param", 1, 5, false, true);
            5.CheckBetween("param", 5, 9, true, false);
            5.CheckBetween("param", 5, 5, true, true);
        }

        [Fact()]
        public void CheckDirectoryExistsTest()
        {
            Smock.Run(context =>
            {
                Assert.Throws<ArgumentNullException>(() => ((string)null).CheckDirectoryExists("param"));
                context.Setup(() => Directory.Exists("path")).Returns(false);
                Assert.Throws<DirectoryNotFoundException>(() => "path".CheckDirectoryExists("param"));
                context.Setup(() => Directory.Exists("path")).Returns(true);
                "path".CheckDirectoryExists("param");
            });
        }

        [Fact()]
        public void CheckFileExistsTest()
        {
            Smock.Run(context =>
            {
                Assert.Throws<ArgumentNullException>(() => ((string)null).CheckFileExists("param"));
                context.Setup(() => File.Exists("filename")).Returns(false);
                Assert.Throws<FileNotFoundException>(() => "filename".CheckFileExists("param"));
                context.Setup(() => File.Exists("filename")).Returns(true);
                "filename".CheckFileExists("param");
            });
        }

    }
}
