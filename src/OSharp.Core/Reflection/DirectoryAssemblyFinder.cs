﻿// -----------------------------------------------------------------------
//  <copyright file="DirectoryAssemblyFinder.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 10:15</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace OSharp.Core.Reflection
{
    /// <summary>
    /// 目录程序集查找器
    /// </summary>
    public class DirectoryAssemblyFinder : IAllAssemblyFinder
    {
        private static readonly IDictionary<string, Assembly[]> AssembliesesDict = new Dictionary<string, Assembly[]>();
        private readonly string _path;

        /// <summary>
        /// 初始化一个<see cref="DirectoryAssemblyFinder"/>类型的新实例
        /// </summary>
        public DirectoryAssemblyFinder()
            : this(GetBinPath())
        { }

        /// <summary>
        /// 初始化一个<see cref="DirectoryAssemblyFinder"/>类型的新实例
        /// </summary>
        public DirectoryAssemblyFinder(string path)
        {
            _path = path;
        }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            if (AssembliesesDict.ContainsKey(_path))
            {
                return AssembliesesDict[_path];
            }
            string[] files = Directory.GetFiles(_path, "*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(_path, "*.exe", SearchOption.TopDirectoryOnly))
                .ToArray();
            Assembly[] assemblies = files.Select(Assembly.LoadFrom).Distinct().ToArray();
            AssembliesesDict.Add(_path, assemblies);
            return assemblies;
        }

        private static string GetBinPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(path, "bin");
        }
    }
}