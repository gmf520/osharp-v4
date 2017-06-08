using System;
using System.Collections.Generic;
using System.Reflection;

using Xunit;


namespace OSharp.Utility.Tests
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    
    public class UnitTest1
    {
        [Fact()]
        public void Test01()
        {
            Type t1 = typeof(List<>);
            Assert.True(t1.IsGenericType);
            Assert.True(t1.IsGenericTypeDefinition);

            Type info1 = t1.GetTypeInfo();
            Assert.True(info1.IsGenericType);
            Assert.True(info1.IsGenericTypeDefinition);
            Assert.True(t1 == info1);

            Type definition1 = t1.GetGenericTypeDefinition();
            Assert.True(definition1.IsGenericType);
            Assert.True(definition1.IsGenericTypeDefinition);
            Assert.True(t1 == definition1);
            Assert.True(info1 == definition1);


            //==================================
            Type t2 = typeof(List<string>);
            Assert.True(t2.IsGenericType);
            Assert.False(t2.IsGenericTypeDefinition);

            Type info2 = t2.GetTypeInfo();
            Assert.True(info2.IsGenericType);
            Assert.False(info2.IsGenericTypeDefinition);
            Assert.True(t2 == info2);

            Type definition2 = t2.GetGenericTypeDefinition();
            Assert.True(definition2.IsGenericType);
            Assert.True(definition2.IsGenericTypeDefinition);
            Assert.False(t2 == definition2);
            Assert.False(info2 == definition2);
            Assert.False(t1 == info2);
            Assert.True(t1 == definition2);
        }

        [Fact]
        public void Test02()
        {
            int? num = null;
            Assert.True(num.HasValue == false);
            Assert.True(num == null);
            num = 1;
            Assert.True(num.HasValue);
            Assert.True(num != null);
        }
    }
}
