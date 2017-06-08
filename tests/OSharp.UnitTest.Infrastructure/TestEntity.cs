﻿// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014:07:04 17:46</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;


namespace OSharp.UnitTest.Infrastructure
{
    [Description("测试实体")]
    public class TestEntity
    {
        public TestEntity()
        {
            AddDate = DateTime.Now;
            IsDeleted = false;
        }

        [Description("编号")]
        public int Id { get; set; }

        [Description("名称")]
        public string Name { get; set; }

        [Description("添加时间")]
        public DateTime AddDate { get; set; }

        [Description("是否删除")]
        public bool IsDeleted { get; set; }
    }
}