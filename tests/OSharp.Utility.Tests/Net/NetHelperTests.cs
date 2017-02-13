using Xunit;
using OSharp.Utility.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSharp.Utility.Net.Tests
{
    public class NetHelperTests
    {
        [Fact()]
        public void PingTest()
        {
            bool flag = NetHelper.Ping("114.55.24.30");
            Assert.True(flag);
        }

        [Fact()]
        public void IsInternetConnectedTest()
        {
            bool flag = NetHelper.IsInternetConnected();
            Assert.True(flag);
        }
    }
}