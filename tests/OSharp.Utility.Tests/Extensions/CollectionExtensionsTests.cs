// -----------------------------------------------------------------------
//  <copyright file="CollectionExtensionsTests.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-10-16 2:30</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Data;

using Xunit;


namespace OSharp.Utility.Extensions.Tests
{
    public class CollectionExtensionsTests
    {
        [Fact()]
        public void ExpandAndToStringTest_IEnumerable()
        {
            List<int> source = new List<int>();
            //当为空集合时，返回null
            Assert.Equal(source.ExpandAndToString(), null);

            source.AddRange(new List<int>() { 1, 2, 3, 4, 5, 6 });
            //没有分隔符时，默认为逗号
            Assert.Equal(source.ExpandAndToString(), "1,2,3,4,5,6");
            Assert.Equal(source.ExpandAndToString(null), "123456");
            Assert.Equal(source.ExpandAndToString(""), "123456");
            Assert.Equal(source.ExpandAndToString("|"), "1|2|3|4|5|6");
        }

        [Fact()]
        public void ExpandAndToStringTest_IEnumerable2()
        {
            List<int> source = new List<int> { 1, 2, 3, 4, 5, 6 };

            //转换委托不能为空
            Assert.Throws<ArgumentNullException>(() => source.ExpandAndToString(itemFormatFunc: null));
            //没有分隔符时，默认为逗号
            Assert.Equal(source.ExpandAndToString(item => (item + 1).ToString()), "2,3,4,5,6,7");
            Assert.Equal(source.ExpandAndToString(item => (item + 1).ToString(), "|"), "2|3|4|5|6|7");
        }

        [Fact()]
        public void IsEmptyTest_IEnumerable()
        {
            List<int> source = new List<int>();
            Assert.True(source.IsEmpty());

            source.Add(1);
            Assert.False(source.IsEmpty());
        }

        [Fact()]
        public void WhereIfTest_IEnumerable()
        {
            List<int> source = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            Assert.Equal(source.WhereIf(m => m > 5, false).ToList(), source);
            List<int> actual = new List<int> { 6, 7 };
            Assert.Equal(source.WhereIf(m => m > 5, true).ToList(), actual);
        }

        [Fact()]
        public void DistinctByTest_IEnumerable()
        {
            List<int> source = new List<int> { 1, 2, 3, 3, 4, 4, 5, 6, 7, 7 };
            List<int> actual = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            Assert.Equal(source.DistinctBy(m => m).ToList(), actual);
        }

        [Fact()]
        public void OrderByTest_IEnumerable()
        {
            IEnumerable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            };

            Assert.Equal(source.OrderBy("Id").ToArray()[1].Name, "hdg");
            Assert.Equal(source.OrderBy("Name", ListSortDirection.Descending).ToArray()[3].Id, 1);
            Assert.Equal(source.OrderBy(new SortCondition("Id")).ToArray()[1].Name, "hdg");
            Assert.Equal(source.OrderBy(new SortCondition<TestEntity>(m => m.Id)).ToArray()[1].Name, "hdg");
            Assert.Equal(source.OrderBy(new SortCondition<TestEntity>(m => m.Name.Length)).ToArray()[1].Name, "fda");
            Assert.Equal(source.OrderBy(new SortCondition("Name", ListSortDirection.Descending)).ToArray()[3].Id, 1);
        }

        [Fact()]
        public void ThenByTest_IEnumerable()
        {
            IEnumerable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            };
            Assert.Equal(source.OrderBy("IsDeleted").ThenBy("Id").ToArray()[2].Name, "fda");
            Assert.Equal(source.OrderBy("IsDeleted", ListSortDirection.Descending).ThenBy("Id", ListSortDirection.Descending).ToArray()[2].Name,
                "hdg");
            Assert.Equal(source.OrderBy(new SortCondition("IsDeleted")).ThenBy(new SortCondition("Name")).ToArray()[2].Name, "fda");
        }

        [Fact()]
        public void WhereIfTest_IQueryable()
        {
            IQueryable<int> source = new List<int> { 1, 2, 3, 4, 5, 6, 7 }.AsQueryable();
            Assert.Equal(source.WhereIf(m => m > 5, false).ToList(), source.ToList());

            List<int> actual = new List<int> { 6, 7 };
            Assert.Equal(source.WhereIf(m => m > 5, true).ToList(), actual);
        }

        [Fact()]
        public void OrderByTest_IQueryable()
        {
            IQueryable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            }.AsQueryable();

            Assert.Equal(source.OrderBy("Id").ToArray()[1].Name, "hdg");
            Assert.Equal(source.OrderBy("Name", ListSortDirection.Descending).ToArray()[3].Id, 1);
            Assert.Equal(source.OrderBy(new SortCondition("Id")).ToArray()[1].Name, "hdg");
            Assert.Equal(source.OrderBy(new SortCondition<TestEntity>(m => m.Id)).ToArray()[1].Name, "hdg");
            Assert.Equal(source.OrderBy(new SortCondition<TestEntity>(m => m.Name.Length, ListSortDirection.Ascending)).ToArray()[1].Name, "fda");
            Assert.Equal(source.OrderBy(new SortCondition("Name", ListSortDirection.Descending)).ToArray()[3].Id, 1);
        }

        [Fact()]
        public void ThenByTest_IQueryable()
        {
            IQueryable<TestEntity> source = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "abc" },
                new TestEntity { Id = 4, Name = "fda", IsDeleted = true },
                new TestEntity { Id = 6, Name = "rwg", IsDeleted = true },
                new TestEntity { Id = 3, Name = "hdg" },
            }.AsQueryable();
            Assert.Equal(source.OrderBy("IsDeleted").ThenBy("Id").ToArray()[2].Name, "fda");
            Assert.Equal(source.OrderBy("IsDeleted", ListSortDirection.Descending).ThenBy("Id", ListSortDirection.Descending).ToArray()[2].Name,
                "hdg");
            Assert.Equal(source.OrderBy(new SortCondition("IsDeleted")).ThenBy(new SortCondition("Name")).ToArray()[2].Name, "fda");
        }
    }
}