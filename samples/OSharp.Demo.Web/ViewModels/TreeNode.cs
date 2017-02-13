// -----------------------------------------------------------------------
//  <copyright file="TreeNode.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-01-09 17:55</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;


namespace OSharp.Demo.Web.ViewModels
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode
    {
        public TreeNode()
        {
            Checked = false;
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public bool Checked { get; set; }

        public int Order { get; set; }

        public string IconCls { get; set; }

        public string Url { get; set; }

        public ICollection<TreeNode> Children { get; set; }
    }
}