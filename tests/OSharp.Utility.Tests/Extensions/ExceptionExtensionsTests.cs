using Xunit;
using OSharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Utility.Exceptions;


namespace OSharp.Utility.Extensions.Tests
{
    public class ExceptionExtensionsTests
    {
        [Fact()]
        public void FormatMessageTest()
        {
            Exception ex = null;
            try
            {
                int num = 0;
                num = 1 / num;
            }
            catch (DivideByZeroException e)
            {
                ex = new OSharpException("服务器正忙，请稍候再尝试。", e);
            }
            Assert.True(ex != null);
            string msg = ex.FormatMessage();
            Assert.True(msg.Contains("内部异常"));
            Assert.True(msg.Contains("服务器正忙"));
        }
    }
}