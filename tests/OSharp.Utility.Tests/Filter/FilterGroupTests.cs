using Xunit;
using OSharp.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSharp.Utility.Filter.Tests
{
    public class FilterGroupTests
    {
        [Fact()]
        public void FilterGroupTest()
        {
            FilterGroup group = new FilterGroup();
            Assert.Equal(group.Operate, FilterOperate.And);
            Assert.NotEqual(group.Rules, null);
            Assert.NotEqual(group.Groups, null);
        }
    }
}