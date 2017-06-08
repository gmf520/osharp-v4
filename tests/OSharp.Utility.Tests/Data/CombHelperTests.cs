using Xunit;

using System;

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