// -----------------------------------------------------------------------
//  <copyright file="ImagePullEventArgs.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-02 12:29</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSharp.Web.Net.WebPull.Images
{
    /// <summary>
    /// 模块下载事件数据类
    /// </summary>
    public class GroupDownloadEventArgs : EventArgs
    {
        /// <summary>
        /// 图组信息类
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// 图组信息个数
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 图片下载事件数据类
    /// </summary>
    public class ImageDownloadEventArgs : EventArgs
    {
        /// <summary>
        /// 图片Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 图片个数
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 获取模块信息事件数据类
    /// </summary>
    public class ImageUrlGetEventArgs : EventArgs
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ForumName { get; set; }

        /// <summary>
        /// 图组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }
    }

    /// <summary>
    /// 获取图组信息事件数据类
    /// </summary>
    public class GroupGetEventArgs : EventArgs
    {
        /// <summary>
        /// 板块信息
        /// </summary>
        public Forum Forum { get; set; }
    }


    /// <summary>
    /// 客户端错误事件信息类
    /// </summary>
    public class WebClientErrorEventArgs : EventArgs
    {
        /// <summary>
        /// 报错页面Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }
}