using Xunit;

using System;

using OSharp.Utility.Extensions;


namespace OSharp.Utility.Data.Tests
{
    public class CompressionTests
    {
        [Fact()]
        public void CompressTest()
        {
            string value = "admin";
            string temp = Compression.Compress(value);
            string result = Compression.Decompress(temp);
            Assert.Equal(value, result);

            Random rnd = new Random();
            byte[] sourceBytes = rnd.NextBytes(10000);
            byte[] tempBytes = Compression.Compress(sourceBytes);
            byte[] resultBytes = Compression.Decompress(tempBytes);
            Assert.Equal(sourceBytes, resultBytes);
        }
    }
}