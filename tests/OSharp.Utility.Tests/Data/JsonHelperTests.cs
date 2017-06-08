﻿using Xunit;

using System;

using OSharp.UnitTest.Infrastructure;


namespace OSharp.Utility.Data.Tests
{
    public class JsonHelperTests
    {
        [Fact()]
        public void ToJsonTest()
        {
            object value = null;
            Assert.Throws<ArgumentNullException>(() => JsonHelper.ToJson(value));
            value = "";
            Assert.Equal(JsonHelper.ToJson(value), "\"\"");
            value = "123";
            Assert.Equal(JsonHelper.ToJson(value), "\"123\"");
            value = 123;
            Assert.Equal(JsonHelper.ToJson(value), "123");

            DateTime now = new DateTime(2015, 10, 15, 13, 38, 51);
            TestEntity source = new TestEntity() { AddDate = now };
            string json = JsonHelper.ToJson(source);
            TestEntity result = JsonHelper.FromJson<TestEntity>(json);
            Assert.Equal(source.Id, result.Id);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.IsDeleted, result.IsDeleted);
            Assert.Equal(source.AddDate, result.AddDate);
        }
    }
}