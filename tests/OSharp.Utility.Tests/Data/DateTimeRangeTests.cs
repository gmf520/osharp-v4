// -----------------------------------------------------------------------
//  <copyright file="DateTimeRangeTests.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-08-07 11:13</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Fakes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.QualityTools.Testing.Fakes;

using OSharp.Utility.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OSharp.UnitTest.Infrastructure;


namespace OSharp.Utility.Data.Tests
{
    [TestClass()]
    public class DateTimeRangeTests : UnitTestBase
    {
        [TestMethod()]
        public void DateTimeRangeTest_Ctor()
        {
            DateTimeRange range = new DateTimeRange();
            Assert.AreEqual(range.StartTime, DateTime.MinValue);
            Assert.AreEqual(range.EndTime, DateTime.MaxValue);

            DateTime now = new DateTime(2015, 8, 7, 11, 15, 22);
            range = new DateTimeRange(now.Date.AddDays(-1), now.Date.AddDays(2));
            Assert.AreEqual(range.StartTime.Day, 6);
            Assert.AreEqual(range.EndTime.Day, 9);
        }

        [TestMethod()]
        public void DateTimeRangeTest_Properties()
        {
            DateTime now = new DateTime(2015, 8, 7, 11, 15, 22);
            ShimDateTime.NowGet = () => now;

            Assert.AreEqual(DateTimeRange.Yesterday.StartTime, new DateTime(2015, 8, 6));
            Assert.AreEqual(DateTimeRange.Yesterday.EndTime, new DateTime(2015, 8, 7).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.Today.StartTime, new DateTime(2015, 8, 7));
            Assert.AreEqual(DateTimeRange.Today.EndTime, new DateTime(2015, 8, 8).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.Tomorrow.StartTime, new DateTime(2015, 8, 8));
            Assert.AreEqual(DateTimeRange.Tomorrow.EndTime, new DateTime(2015, 8, 9).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.LastWeek.StartTime, new DateTime(2015, 7, 26));
            Assert.AreEqual(DateTimeRange.LastWeek.EndTime, new DateTime(2015, 8, 2).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.ThisWeek.StartTime, new DateTime(2015, 8, 2));
            Assert.AreEqual(DateTimeRange.ThisWeek.EndTime, new DateTime(2015, 8, 9).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.NextWeek.StartTime, new DateTime(2015, 8, 9));
            Assert.AreEqual(DateTimeRange.NextWeek.EndTime, new DateTime(2015, 8, 16).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.LastMonth.StartTime, new DateTime(2015, 7, 1));
            Assert.AreEqual(DateTimeRange.LastMonth.EndTime, new DateTime(2015, 8, 1).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.ThisMonth.StartTime, new DateTime(2015, 8, 1));
            Assert.AreEqual(DateTimeRange.ThisMonth.EndTime, new DateTime(2015, 9, 1).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.NextMonth.StartTime, new DateTime(2015, 9, 1));
            Assert.AreEqual(DateTimeRange.NextMonth.EndTime, new DateTime(2015, 10, 1).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.LastYear.StartTime, new DateTime(2014, 1, 1));
            Assert.AreEqual(DateTimeRange.LastYear.EndTime, new DateTime(2015, 1, 1).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.ThisYear.StartTime, new DateTime(2015, 1, 1));
            Assert.AreEqual(DateTimeRange.ThisYear.EndTime, new DateTime(2016, 1, 1).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.NextYear.StartTime, new DateTime(2016, 1, 1));
            Assert.AreEqual(DateTimeRange.NextYear.EndTime, new DateTime(2017, 1, 1).AddMilliseconds(-1));

            Assert.AreEqual(DateTimeRange.Last30Days.StartTime, new DateTime(2015, 7, 8, 11, 15, 22));
            Assert.AreEqual(DateTimeRange.Last30Days.EndTime, now);

            Assert.AreEqual(DateTimeRange.Last7Days.StartTime, new DateTime(2015, 7, 31, 11, 15, 22));
            Assert.AreEqual(DateTimeRange.Last7Days.EndTime, now);
        }
    }
}