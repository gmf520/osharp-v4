using Xunit;

using System;
using System.Runtime.Caching;

using OSharp.UnitTest.Infrastructure;


namespace OSharp.Utility.Extensions.Tests
{
    public class MemoryCacheExtensionsTests
    {
        [Fact()]
        public void GetTest()
        {
            const string key = "MemoryCacheExtensionsTests_GetTest";
            MemoryCache cache = MemoryCache.Default;
            TestEntity value1 = new TestEntity() { Id = 1000, Name = "Name1000" };
            cache.Set(key, value1, DateTimeOffset.Now.AddMinutes(1));
            TestEntity value2 = cache.Get<TestEntity>(key);
            Assert.True(value2.Id == value1.Id);
            Assert.True(value2.Name == value1.Name);
            Assert.True(value2.AddDate == value1.AddDate);
        }
    }
}