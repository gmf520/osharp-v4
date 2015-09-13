// -----------------------------------------------------------------------
//  <copyright file="Forum.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-02 12:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;


namespace OSharp.Web.Net.WebPull.Images
{
    /// <summary>
    /// 板块信息类
    /// </summary>
    [Serializable]
    public class Forum
    {
        /// <summary>
        /// 板块的图组集合
        /// </summary>
        public Forum()
        {
            Groups = new List<Group>();
        }

        /// <summary>
        /// 板块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 板块第一页地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 板块列表页数
        /// </summary>
        public int ListsCount { get; set; }

        /// <summary>
        /// 第二个列表页的数值标识
        /// </summary>
        public int SecondListNum { get; set; }

        /// <summary>
        /// 下一个列表页的地址格式
        /// </summary>
        public string NextUrlFormat { get; set; }

        /// <summary>
        /// 下一个列表页的搜索模式
        /// </summary>
        public NextSearchMode NextSearchMode { get; set; }

        /// <summary>
        /// 板块的图组集合
        /// </summary>
        public List<Group> Groups { get; set; }
    }
}