using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OSharp.Utility.Extensions;


namespace OSharp.UnitTest.Infrastructure
{
    [TestClass]
    public abstract class UnitTestBase
    {
        protected IDisposable Shims;
        protected readonly IEnumerable<TestEntity> Entities; 

        protected UnitTestBase()
        {
            List<TestEntity>entities = new List<TestEntity>();
            DateTime dt = DateTime.Now;
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                entities.Add(new TestEntity()
                {
                    Id = i + 1,
                    Name = "Name" + (i + 1),
                    AddDate = rnd.NextDateTime(dt.AddDays(-7), dt.AddDays(7)),
                    IsDeleted = rnd.NextBoolean()
                });
            }
            Entities = entities;
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Shims = ShimsContext.Create();            
        }
        
        [TestCleanup]
        public virtual void TestCleanup()
        {
            Shims.Dispose();
        }
    }
}
