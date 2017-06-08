using Xunit;

using System;
using System.IO;

using Smocks;


namespace OSharp.Utility.IO.Tests
{
    public class DirectoryHelperTests
    {
        [Fact()]
        public void CopyTest()
        {
            Assert.Throws<ArgumentNullException>(() => DirectoryHelper.Copy(null, "targetPath"));
            Assert.Throws<ArgumentException>(() => DirectoryHelper.Copy(string.Empty, "targetPath"));
            Assert.Throws<ArgumentNullException>(() => DirectoryHelper.Copy("sourcePath", null));
            Assert.Throws<ArgumentException>(() => DirectoryHelper.Copy("sourcePath", string.Empty));

            Smock.Run(context =>
            {
                context.Setup(() => Directory.Exists("sourcePath1")).Returns(false);
                Assert.Throws<DirectoryNotFoundException>(() => DirectoryHelper.Copy("sourcePath1", "targetPath"));
            });

            string[] paths = { "sourcePath", "sourcePath\\123", "sourcePath\\234", "sourcePath\\345" };
            foreach (string path in paths)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            DirectoryHelper.Copy("sourcePath", "targetPath");

            string[] dirs = Directory.GetDirectories("targetPath");
            Assert.Equal(dirs.Length, 3);
            foreach (string dir in dirs)
            {
                Assert.True(Directory.Exists(dir));
            }
            //顺便测试删除
            DirectoryHelper.Delete("sourcePath");
            DirectoryHelper.Delete("targetPath");
            foreach (string dir in dirs)
            {
                Assert.False(Directory.Exists(dir));
            }
        }

        [Fact()]
        public void SetAttributesTest()
        {
            Smock.Run(context =>
            {
                context.Setup(() => Directory.Exists("sourcePath1")).Returns(false);
                Assert.Throws<DirectoryNotFoundException>(() => DirectoryHelper.SetAttributes("sourcePath1", FileAttributes.Archive, true));
            });
            const string dir = "sourcePath";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            DirectoryHelper.SetAttributes(dir, FileAttributes.System, true);
            Assert.True(new DirectoryInfo(dir).Attributes.HasFlag(FileAttributes.System));
            DirectoryHelper.SetAttributes(dir, FileAttributes.System, false);
            Assert.False(new DirectoryInfo(dir).Attributes.HasFlag(FileAttributes.System));
            DirectoryHelper.Delete("sourcePath");
        }
    }
}