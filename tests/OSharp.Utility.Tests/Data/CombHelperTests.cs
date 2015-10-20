using Xunit;
using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smocks;


namespace OSharp.Utility.Data.Tests
{
    public class CombHelperTests
    {
        [Fact()]
        public void NewCombTest()
        {
            DateTime now = DateTime.Now;
            Smock.Run(context =>
            {
                context.Setup(() => DateTime.Now).Returns(now);
                Guid id = CombHelper.NewComb();
                DateTime time = CombHelper.GetDateFromComb(id);
                Assert.True(time.Subtract(now).TotalSeconds < 1);
            });
        }
    }
}