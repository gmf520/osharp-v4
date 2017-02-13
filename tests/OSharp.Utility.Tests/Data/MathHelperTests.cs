using Xunit;
using OSharp.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSharp.Utility.Data.Tests
{
    public class MathHelperTests
    {
        [Fact()]
        public void FourSpeciesTest()
        {
            string exp = "6*5";
            double num = MathHelper.FourSpecies(exp);
            Assert.Equal(num, 30);
        }
    }
}