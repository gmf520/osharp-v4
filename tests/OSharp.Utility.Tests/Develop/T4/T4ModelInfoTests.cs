using Xunit;
using OSharp.Utility.Develop.T4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;


namespace OSharp.Utility.Develop.T4.Tests
{
    public class T4ModelInfoTests
    {
        [Fact()]
        public void T4ModelInfoTest()
        {
            //OSharp.UnitTest.Infrastructure
            const string pattern = "(?<=OSharp.).*(?=.Infrastructure)";
            Type type = typeof(TestEntity);
            T4ModelInfo modelInfo = new T4ModelInfo(type, pattern);
            Assert.Equal("UnitTest", modelInfo.ModuleName);
        }
    }
}