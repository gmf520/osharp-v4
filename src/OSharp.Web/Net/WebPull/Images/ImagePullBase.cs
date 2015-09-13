// 源文件头信息：
// <copyright file="ImagePullBase.cs">
// Copyright(c)2013-2013 Netstr.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：柳柳工作室
// 组织网站：http://www.netstr.com
// 所属工程：Liuliu.Tools.WebPull
// 最后修改：郭明锋
// 最后修改：2013/03/06 16:46
// </copyright>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Web.Net.WebPull.Images
{
    public abstract class ImagePullBase
    {
        private readonly ICollection<Forum> _forums;

        protected ImagePullBase()
        {
            _forums = new List<Forum>();
        }

        /// <summary>
        /// 网站主页地址，不以 / 收尾
        /// </summary>
        protected abstract string BaseUrl { get; }

        /// <summary>
        /// 是否还有下一个列表页的匹配模式
        /// </summary>
        protected abstract string HasNextListPagePattern { get; }

        /// <summary>
        /// 下一个列表页的匹配模式
        /// </summary>
        protected abstract string NextListPagePattern { get; }

        /// <summary>
        /// 板块列表中的图组列表匹配模式
        /// </summary>
        protected abstract string GroupsPattern { get; }

        /// <summary>
        /// 是否还有下一个图片页的匹配模式
        /// </summary>
        protected abstract string HasNextImagePagePattern { get; }

        /// <summary>
        /// 下一个图片页的匹配模式
        /// </summary>
        protected abstract string NextImagePagePattern { get; }

        /// <summary>
        /// 图片地址匹配模式
        /// </summary>
        protected abstract string ImagePattern { get; }

        /// <summary>
        /// HTTP客户端访问通道
        /// </summary>
        protected abstract WebClient WebClient { get; }

        public event EventHandler<WebClientErrorEventArgs> WebClientError;
        public event EventHandler<GroupGetEventArgs> GroupGetCompleted;
        public event EventHandler<GroupDownloadEventArgs> GroupDownloading;
        public event EventHandler<GroupDownloadEventArgs> GroupDownloadCompleted;
        public event EventHandler<ImageUrlGetEventArgs> ImageUrlGetCompleted;
        public event EventHandler<ImageDownloadEventArgs> ImageDownloadCompleted;


        protected void OnWebClientError(WebClientErrorEventArgs e)
        {
            EventHandler<WebClientErrorEventArgs> handler = WebClientError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnGroupGetCompleted(GroupGetEventArgs e)
        {
            EventHandler<GroupGetEventArgs> handler = GroupGetCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnGroupDownloading(GroupDownloadEventArgs e)
        {
            EventHandler<GroupDownloadEventArgs> handler = GroupDownloading;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnGroupDownloadCompleted(GroupDownloadEventArgs e)
        {
            EventHandler<GroupDownloadEventArgs> handler = GroupDownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnImageUrlGetCompleted(ImageUrlGetEventArgs e)
        {
            EventHandler<ImageUrlGetEventArgs> handler = ImageUrlGetCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnImageDownloadCompleted(ImageDownloadEventArgs e)
        {
            EventHandler<ImageDownloadEventArgs> handler = ImageDownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 初始化板块数据，可以手动填写，也可由规则在首页HTML中获取
        /// </summary>
        /// <param name="forums"></param>
        protected abstract void InitializeForums(ICollection<Forum> forums);

        /// <summary>
        /// 获取图组信息的第一次匹配数据
        /// </summary>
        /// <param name="html">列表的Html</param>
        /// <param name="forumUrl">板块地址</param>
        /// <returns></returns>
        private IEnumerable<Group> GetGroups(string html, string forumUrl)
        {
            GroupsPattern.CheckNotNull("GroupsPattern");

            List<string> matches = html.Matches(GroupsPattern).ToList();
            return matches.Select(m => GetGroup(m, forumUrl)).ToList();
        }

        /// <summary>
        /// 由匹配数据生成取图组信息
        ///  </summary>
        /// <param name="match">第一次匹配数据</param>
        /// <param name="forumUrl">板块地址</param>
        /// <returns></returns>
        protected virtual Group GetGroup(string match, string forumUrl)
        {
            string url = match;
            if (url.StartsWith("/"))
            {
                url = BaseUrl + url;
            }
            else if (!url.StartsWith("http"))
            {
                url = forumUrl.Substring(0, forumUrl.LastIndexOf("/", StringComparison.Ordinal) + 1) + url;
            }
            return new Group { Url = url };
        }

        /// <summary>
        /// 获取图片地址的第一次匹配结果
        /// </summary>
        /// <param name="html">图片页的HTML</param>
        /// <param name="groupUrl">图组地址</param>
        /// <returns></returns>
        private IEnumerable<string> GetImageUrls(string html, string groupUrl)
        {
            ImagePattern.CheckNotNull("ImagePattern");

            List<string> matches = html.Matches(ImagePattern).ToList();
            return matches.Select(m => GetImageUrl(m, groupUrl)).ToList();
        }

        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <param name="match">第一次匹配获得的数据</param>
        /// <param name="groupUrl">图组地址</param>
        /// <returns></returns>
        protected virtual string GetImageUrl(string match, string groupUrl)
        {
            string url = match;
            if (url.StartsWith("/"))
            {
                url = BaseUrl + url;
            }
            else if (!url.StartsWith("http"))
            {
                url = groupUrl.Substring(0, groupUrl.LastIndexOf("/", StringComparison.Ordinal) + 1) + url;
            }
            return url;
        }

        /// <summary>
        /// 图片下载前对下载地址进行处理
        /// </summary>
        /// <param name="imageUrl">原始图片地址</param>
        /// <returns></returns>
        protected virtual string ImageDowning(string imageUrl)
        {
            return imageUrl;
        }

        /// <summary>
        /// 加载第一个列表页时完善板块信息，如列表页数，起始页码，下页格式等
        /// </summary>
        /// <param name="html">第一个列表页的HTML</param>
        /// <param name="forum">原始的板块信息</param>
        /// <returns></returns>
        protected virtual Forum PerfectForum(string html, Forum forum)
        {
            return forum;
        }

        /// <summary>
        ///   加载第一张图片页时完善图组内容，比如标题，图片数量等信息
        /// </summary>
        /// <param name="html"> 第一张图片页的Html </param>
        /// <param name="group"> 原始的图组信息 </param>
        /// <returns> </returns>
        protected virtual Group PerfectGroup(string html, Group group)
        {
            return group;
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="imageUrl">图片的URL地址</param>
        /// <param name="filename">图片的保存文件名</param>
        private void ImageDownload(string imageUrl, string filename)
        {
            if (File.Exists(filename))
            {
                return;
            }
            byte[] data = WebClient.DownloadData(imageUrl);
            MemoryStream ms = new MemoryStream(data);
            Bitmap image = new Bitmap(ms);
            ImageFormat format;
            switch (Path.GetExtension(filename))
            {
                case ".jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                default:
                    format = ImageFormat.Jpeg;
                    break;
            }
            image.Save(filename, format);
        }

        /// <summary>
        /// 开始下载工作
        /// </summary>
        /// <param name="savePath">图片保存路径</param>
        public void StartDown(string savePath)
        {
            savePath.CheckNotNullOrEmpty("savePath");
            BaseUrl.CheckNotNullOrEmpty("BaseUrl" );
            WebClient.CheckNotNull("WebClient" );

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            InitializeForums(_forums);
            int total = 0;
            foreach (Forum tmpForum in _forums)
            {
                List<Group> groups = new List<Group>();
                string html;
                try
                {
                    html = WebClient.DownloadString(tmpForum.Url);
                }
                catch (Exception e)
                {
                    WebClientErrorEventArgs arg = new WebClientErrorEventArgs { Url = tmpForum.Url, Message = e.Message };
                    OnWebClientError(arg);
                    continue;
                }
                Forum forum = PerfectForum(html, tmpForum);
                string forumPath = Path.Combine(savePath, forum.Name);
                if (!Directory.Exists(forumPath))
                {
                    Directory.CreateDirectory(forumPath);
                }
                foreach (Group @group in GetGroups(html, forum.Url))
                {
                    if (groups.All(m => m.Url != @group.Url))
                    {
                        groups.Add(@group);
                    }
                }
                if (forum.NextSearchMode == NextSearchMode.Cycle)
                {
                    forum.NextUrlFormat.CheckNotNullOrEmpty("forum.NextUrlFormat" );
                    for (int i = forum.SecondListNum; i <= forum.ListsCount - (2 - forum.SecondListNum); i++)
                    {
                        string forumUrl = string.Format(forum.NextUrlFormat, i);
                        try
                        {
                            html = WebClient.DownloadString(forumUrl);
                        }
                        catch (Exception e)
                        {
                            WebClientErrorEventArgs arg = new WebClientErrorEventArgs { Url = forumUrl, Message = e.Message };
                            OnWebClientError(arg);
                            continue;
                        }
                        foreach (Group @group in GetGroups(html, forumUrl))
                        {
                            if (groups.All(m => m.Url != @group.Url))
                            {
                                groups.Add(@group);
                            }
                        }
                    }
                }
                else
                {
                    HasNextListPagePattern.CheckNotNullOrEmpty("HasNextListPagePattern" );
                    NextListPagePattern.CheckNotNullOrEmpty("NextListPagePattern" );
                    while (html.IsMatch(HasNextListPagePattern))
                    {
                        string forumUrl = html.Match(NextListPagePattern);
                        if (forumUrl.StartsWith("/"))
                        {
                            forumUrl = BaseUrl + forumUrl;
                        }
                        else if (!forumUrl.StartsWith("http"))
                        {
                            forumUrl = forum.Url.Substring(0, forum.Url.LastIndexOf("/", StringComparison.Ordinal) + 1) + forumUrl;
                        }
                        try
                        {
                            html = WebClient.DownloadString(forumUrl);
                        }
                        catch (Exception e)
                        {
                            WebClientErrorEventArgs arg = new WebClientErrorEventArgs { Url = forumUrl, Message = e.Message };
                            OnWebClientError(arg);
                            break;
                        }
                        foreach (Group @group in GetGroups(html, forumUrl))
                        {
                            if (groups.All(m => m.Url != @group.Url))
                            {
                                groups.Add(@group);
                            }
                        }
                    }
                }
                forum.Groups.AddRange(groups);
                OnGroupGetCompleted(new GroupGetEventArgs { Forum = forum });
                int groupCount = 0;
                foreach (Group tmpGroup in groups)
                {
                    groupCount++;
                    try
                    {
                        html = WebClient.DownloadString(tmpGroup.Url);
                    }
                    catch (Exception e)
                    {
                        WebClientErrorEventArgs arg = new WebClientErrorEventArgs { Url = tmpForum.Url, Message = e.Message };
                        OnWebClientError(arg);
                        continue;
                    }
                    Group group = PerfectGroup(html, tmpGroup);

                    OnGroupDownloading(new GroupDownloadEventArgs { Group = group });
                    List<string> images = GetImageUrls(html, group.Url).ToList();
                    foreach (string image in images)
                    {
                        if (!group.Images.Contains(image))
                        {
                            group.Images.Add(image);
                            OnImageUrlGetCompleted(new ImageUrlGetEventArgs { ForumName = forum.Name, GroupName = group.Name, ImageUrl = image });
                        }
                    }
                    if (group.NextSearchMode == NextSearchMode.Cycle)
                    {
                        group.NextUrlFormat.CheckNotNullOrEmpty("group.NextUrlFormat" );
                        for (int i = group.SecondImageNum; i < group.ImagesCount - (2 - group.SecondImageNum); i++)
                        {
                            string groupUrl = string.Format(group.NextUrlFormat, i);
                            try
                            {
                                html = WebClient.DownloadString(groupUrl);
                            }
                            catch (Exception e)
                            {
                                WebClientErrorEventArgs arg = new WebClientErrorEventArgs { Url = groupUrl, Message = e.Message };
                                OnWebClientError(arg);
                                continue;
                            }
                            foreach (string image in GetImageUrls(html, group.Url))
                            {
                                if (!group.Images.Contains(image))
                                {
                                    group.Images.Add(image);
                                    OnImageUrlGetCompleted(new ImageUrlGetEventArgs { ForumName = forum.Name, GroupName = group.Name, ImageUrl = image });
                                }
                            }
                        }
                    }
                    else
                    {
                        HasNextImagePagePattern.CheckNotNullOrEmpty("HasNextImagePagePattern" );
                        NextImagePagePattern.CheckNotNullOrEmpty("NextImagePagePattern" );

                        while (html.IsMatch(HasNextImagePagePattern))
                        {
                            string groupUrl = html.Match(NextImagePagePattern);
                            if (groupUrl.StartsWith("/"))
                            {
                                groupUrl = BaseUrl + groupUrl;
                            }
                            else if (!groupUrl.StartsWith("http"))
                            {
                                groupUrl = group.Url.Substring(0, group.Url.LastIndexOf("/", StringComparison.Ordinal) + 1) + groupUrl;
                            }
                            try
                            {
                                html = WebClient.DownloadString(groupUrl);
                            }
                            catch (Exception e)
                            {
                                WebClientErrorEventArgs arg = new WebClientErrorEventArgs { Url = groupUrl, Message = e.Message };
                                OnWebClientError(arg);
                                break;
                            }
                            foreach (string image in GetImageUrls(html, groupUrl))
                            {
                                if (!group.Images.Contains(image))
                                {
                                    group.Images.Add(image);
                                    OnImageUrlGetCompleted(new ImageUrlGetEventArgs { ForumName = forum.Name, GroupName = group.Name, ImageUrl = image });
                                }
                            }
                        }
                    }
                    int imageCount = 0;
                    foreach (string image in group.Images)
                    {
                        imageCount++;
                        string imageUrl = ImageDowning(image);
                        string ext = imageUrl.Substring(imageUrl.LastIndexOf('.')).ToLower();
                        string filename = string.Format("{0}.{1}-{2}{3}", groupCount, group.Name, imageCount.ToString("d3"),
                            (ext == ".jpg" || ext == ".png" || ext == ".bmp" || ext == ".gif") ? ext : ".jpg");
                        filename = Path.Combine(forumPath, filename);

                        try
                        {
                            ImageDownload(imageUrl, filename);
                            total++;
                            ImageDownloadEventArgs arg = new ImageDownloadEventArgs { Url = imageUrl, FileName = filename, Count = total };
                            OnImageDownloadCompleted(arg);
                        }
                        catch (Exception e)
                        {
                            WebClientErrorEventArgs arg = new WebClientErrorEventArgs { Url = imageUrl, Message = e.Message };
                            OnWebClientError(arg);
                        }
                    }
                    OnGroupDownloadCompleted(new GroupDownloadEventArgs { Group = group, Count = groupCount });
                }
                string forumFile = Path.Combine(savePath, forum.Name + ".xml");
                SerializeHelper.ToXmlFile(forum, forumFile);
                //if (!Directory.Exists(savePath))
                //{
                //    Directory.CreateDirectory(savePath);
                //}
                //TextWriter writer = File.CreateText(forumFile);
                //writer.WriteLine(forum.ToJsonString());
                //writer.Close();
            }
        }
    }
}