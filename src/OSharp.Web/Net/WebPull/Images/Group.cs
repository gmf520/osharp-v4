// -----------------------------------------------------------------------
//  <copyright file="Group.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-02 12:28</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OSharp.Web.Net.WebPull.Images
{
    /// <summary>
    /// 图组信息类
    /// </summary>
    [Serializable]
    public class Group
    {
        public Group()
        {
            Images = new List<string>();
        }

        /// <summary>
        /// 图组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图组第一个图片页的地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图片个数，如不设置将使用逐个查找下一页的方式来获取下一页地址，设置则直接循环获取下一页地址。
        /// </summary>
        public int ImagesCount { get; set; }

        /// <summary>
        /// 循环获取时的第二页地址页数标识
        /// </summary>
        public int SecondImageNum { get; set; }

        /// <summary>
        /// 下一页的地址格式
        /// </summary>
        public string NextUrlFormat { get; set; }

        /// <summary>
        /// 下一个图片页的获取方式
        /// </summary>
        public NextSearchMode NextSearchMode { get; set; }

        /// <summary>
        /// 图组的所有图片地址
        /// </summary>
        public List<string> Images { get; set; }
    }
}