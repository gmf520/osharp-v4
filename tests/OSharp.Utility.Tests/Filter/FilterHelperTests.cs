// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:05 3:24</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using OSharp.UnitTest.Infrastructure;
using OSharp.Utility.Exceptions;
using OSharp.Utility.Filter;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OSharp.Utility.Filter.Tests
{
    [TestClass()]
    public class FilterHelperTests : UnitTestBase
    {
        [TestMethod()]
        public void GetExpressionTest()
        {
            IQueryable<TestEntity> source = Entities.AsQueryable();

            //空条件
            Expression<Func<TestEntity, bool>> predicate = FilterHelper.GetExpression<TestEntity>();
            Assert.IsTrue(source.Where(predicate).SequenceEqual(source));

            //单条件，属性不存在
            FilterRule rule = new FilterRule("Name1", "5", FilterOperate.EndsWith);
            FilterRule rule1 = rule;
            ExceptionAssert.IsException<OSharpException>(() => FilterHelper.GetExpression<TestEntity>(rule1));

            //单条件
            rule = new FilterRule("Name", "5", FilterOperate.EndsWith);
            predicate = FilterHelper.GetExpression<TestEntity>(rule);
            Assert.IsTrue(source.Where(predicate).SequenceEqual(source.Where(m => m.Name.EndsWith("5"))));

            //二级条件
            rule = new FilterRule("Name.Length", 5, FilterOperate.Greater);
            predicate = FilterHelper.GetExpression<TestEntity>(rule);
            Assert.IsTrue(source.Where(predicate).SequenceEqual(source.Where(m => m.Name.Length > 5)));

            //多条件，异常
            ExceptionAssert.IsException<OSharpException>(() => new FilterGroup
            {
                Rules = new List<FilterRule> { rule, new FilterRule("IsDeleted", true) },
                Operate = FilterOperate.Equal
            });

            //多条件
            FilterGroup group = new FilterGroup
            {
                Rules = new List<FilterRule> { rule, new FilterRule("IsDeleted", true) },
                Operate = FilterOperate.And
            };
            predicate = FilterHelper.GetExpression<TestEntity>(group);
            Assert.IsTrue(source.Where(predicate).SequenceEqual(source.Where(m => m.Name.Length > 5 && m.IsDeleted)));

            //条件组嵌套
            DateTime dt = DateTime.Now;
            group = new FilterGroup
            {
                Rules = new List<FilterRule>
                {
                    new FilterRule("AddDate", dt, FilterOperate.Greater)
                },
                Groups = new List<FilterGroup> { group },
                Operate = FilterOperate.Or
            };
            predicate = FilterHelper.GetExpression<TestEntity>(group);
            Assert.IsTrue(source.Where(predicate).SequenceEqual(source.Where(m => m.AddDate > dt || m.Name.Length > 5 && m.IsDeleted)));

        }
    }
}