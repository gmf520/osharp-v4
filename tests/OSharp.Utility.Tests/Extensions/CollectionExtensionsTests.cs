// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:10 10:59</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class CollectionExtensionsTests
    {
        [TestMethod()]
        public void ExpandAndToStringTest_IEnumerable()
        {
            List<int> source = new List<int>();
            //当为空集合时，返回null
            Assert.AreEqual(source.ExpandAndToString(), null);

            source.AddRange(new List<int>() { 1, 2, 3, 4, 5, 6 });
            //没有分隔符时，默认为逗号
            Assert.AreEqual(source.ExpandAndToString(), "1,2,3,4,5,6");
            Assert.AreEqual(source.ExpandAndToString(null), "123456");
            Assert.AreEqual(source.ExpandAndToString(""), "123456");
            Assert.AreEqual(source.ExpandAndToString("|"), "1|2|3|4|5|6");
        }

        [TestMethod]
        public void ExpandAndToStringTest_IEnumerable2()
        {
            List<int> source = new List<int> { 1, 2, 3, 4, 5, 6 };

            //转换委托不能为空
            ExceptionAssert.IsException<ArgumentNullException>(() => source.ExpandAndToString(null));
            //没有分隔符时，默认为逗号
            Assert.AreEqual(source.ExpandAndToString(item => (item + 1).ToString()), "2,3,4,5,6,7");
            Assert.AreEqual(source.ExpandAndToString(item => (item + 1).ToString(), "|"), "2|3|4|5|6|7");
        }

        [TestMethod()]
        public void IsEmptyTest_IEnumerable()
        {
            List<int> source = new List<int>();
            Assert.IsTrue(source.IsEmpty());

            source.Add(1);
            Assert.IsFalse(source.IsEmpty());
        }

        [TestMethod()]
        public void WhereIfTest_IEnumerable()
        {
            List<int> source = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            CollectionAssert.AreEqual(source.WhereIf(m => m > 5, false).ToList(), source);
            List<int> actual = new List<int> { 6, 7 };
            CollectionAssert.AreEqual(source.WhereIf(m => m > 5, true).ToList(), actual);
        }

        [TestMethod()]
        public void DistinctByTest_IEnumerable()
        {
            List<int> source = new List<int> { 1, 2, 3, 3, 4, 4, 5, 6, 7, 7 };
            List<int> actual = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            CollectionAssert.AreEqual(source.DistinctBy(m => m).ToList(), actual);
        }

        [TestMethod]
        public void OrderByTest_IEnumerable()
        {
            IEnumerable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            };

            Assert.AreEqual(source.OrderBy("Id").ToArray()[1].Name, "hdg");
            Assert.AreEqual(source.OrderBy("Name", ListSortDirection.Descending).ToArray()[3].Id, 1);
            Assert.AreEqual(source.OrderBy(new SortCondition("Id")).ToArray()[1].Name, "hdg");
            Assert.AreEqual(source.OrderBy(new SortCondition<TestEntity>(m => m.Id)).ToArray()[1].Name, "hdg");
            Assert.AreEqual(source.OrderBy(new SortCondition<TestEntity>(m => m.Name.Length)).ToArray()[1].Name, "fda");
            Assert.AreEqual(source.OrderBy(new SortCondition("Name", ListSortDirection.Descending)).ToArray()[3].Id, 1);
        }

        [TestMethod()]
        public void ThenByTest_IEnumerable()
        {
            IEnumerable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            };
            Assert.AreEqual(source.OrderBy("IsDeleted").ThenBy("Id").ToArray()[2].Name, "fda");
            Assert.AreEqual(source.OrderBy("IsDeleted", ListSortDirection.Descending).ThenBy("Id", ListSortDirection.Descending).ToArray()[2].Name,
                "hdg");
        }
        [TestMethod()]
        public void WhereIfTest_IQueryable()
        {
            IQueryable<int> source = new List<int> { 1, 2, 3, 4, 5, 6, 7 }.AsQueryable();
            CollectionAssert.AreEqual(source.WhereIf(m => m > 5, false).ToList(), source.ToList());

            List<int> actual = new List<int> { 6, 7 };
            CollectionAssert.AreEqual(source.WhereIf(m => m > 5, true).ToList(), actual);
        }

        [TestMethod()]
        public void OrderByTest_IQueryable()
        {
            IQueryable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            }.AsQueryable();

            Assert.AreEqual(source.OrderBy("Id").ToArray()[1].Name, "hdg");
            Assert.AreEqual(source.OrderBy("Name", ListSortDirection.Descending).ToArray()[3].Id, 1);
            Assert.AreEqual(source.OrderBy(new SortCondition("Id")).ToArray()[1].Name, "hdg");
            Assert.AreEqual(source.OrderBy(new SortCondition<TestEntity>(m => m.Id)).ToArray()[1].Name, "hdg");
            Assert.AreEqual(source.OrderBy(new SortCondition<TestEntity>(m => m.Name.Length)).ToArray()[1].Name, "fda");
            Assert.AreEqual(source.OrderBy(new SortCondition("Name", ListSortDirection.Descending)).ToArray()[3].Id, 1);
        }

        [TestMethod()]
        public void ThenByTest_IQueryable()
        {
            IQueryable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            }.AsQueryable();
            Assert.AreEqual(source.OrderBy("IsDeleted").ThenBy("Id").ToArray()[2].Name, "fda");
            Assert.AreEqual(source.OrderBy("IsDeleted", ListSortDirection.Descending).ThenBy("Id", ListSortDirection.Descending).ToArray()[2].Name,
                "hdg");
        }

    }
}