using System;

using OSharp.Utility.Extensions;

using Xunit;


namespace OSharp.Utility.Drawing.Tests
{
    public class ValidateCoderTests
    {
        [Fact()]
        public void ValidateCoderTest()
        {
            ValidateCoder coder = new ValidateCoder();
            Assert.True(coder.FontNames.Count > 0);
            Assert.True(coder.FontNamesForHanzi.Count > 0);
            Assert.True(coder.FontSize > 9);
            Assert.False(coder.HasBorder);
            Assert.False(coder.RandomPosition);
            Assert.False(coder.RandomItalic);
        }

        [Fact()]
        public void GetCodeTest()
        {
            ValidateCoder coder = new ValidateCoder();
            Assert.Throws<ArgumentOutOfRangeException>(() => coder.GetCode(0));
            string code = coder.GetCode(4);
            Assert.Equal(code.Length, 4);
            code = coder.GetCode(4, ValidateCodeType.Number);
            Assert.True(code.IsNumeric());
        }
    }
}