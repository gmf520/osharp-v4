// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:04 17:45</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.Extensions.Tests
{
    [TestClass()]
    public class ExpressionExtensionsTests : UnitTestBase
    {
        [TestMethod()]
        public void ComposeTest()
        {
            Expression<Func<TestEntity, bool>> predicate = m => m.IsDeleted;
            Expression<Func<TestEntity, bool>> p1 = predicate.Compose(m => m.Id > 500, Expression.AndAlso);
            Expression<Func<TestEntity, bool>> p2 = predicate.Compose(m => m.Id > 500, Expression.OrElse);
            List<TestEntity> list1 = Entities.Where(m => m.IsDeleted && m.Id > 500).ToList();
            List<TestEntity> list2 = Entities.Where(p1.Compile()).ToList();
            Assert.IsTrue(list1.SequenceEqual(list2));
            list1 = Entities.Where(m => m.IsDeleted || m.Id > 500).ToList();
            list2 = Entities.Where(p2.Compile()).ToList();
            Assert.IsTrue(list1.SequenceEqual(list2));
        }

        [TestMethod()]
        public void AndTest()
        {
            Expression<Func<TestEntity, bool>> predicate = m => m.IsDeleted;
            Expression<Func<TestEntity, bool>> actual = m => m.IsDeleted && m.Id > 500;
            predicate = predicate.And(m => m.Id > 500);
            List<TestEntity> list1 = Entities.Where(predicate.Compile()).ToList();
            List<TestEntity> list2 = Entities.Where(actual.Compile()).ToList();
            Assert.IsTrue(list1.SequenceEqual(list2));
        }

        [TestMethod()]
        public void OrTest()
        {
            Expression<Func<TestEntity, bool>> predicate = m => m.IsDeleted;
            Expression<Func<TestEntity, bool>> actual = m => m.IsDeleted || m.Id > 500;
            predicate = predicate.Or(m => m.Id > 500);
            List<TestEntity> list1 = Entities.Where(predicate.Compile()).ToList();
            List<TestEntity> list2 = Entities.Where(actual.Compile()).ToList();
            Assert.IsTrue(list1.SequenceEqual(list2));
        }
    }
}