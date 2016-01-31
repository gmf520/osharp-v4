// -----------------------------------------------------------------------
//  <copyright file="RegistryBaseKey.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-18 18:52</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Utility.Windows
{
    /// <summary>
    /// 注册表基项域
    /// </summary>
    public enum RegistryBaseKey
    {
        /// <summary>
        /// 对应于HKEY_CLASSES_ROOT主键
        /// </summary>
        ClassesRoot,

        /// <summary>
        /// 对应于HKEY_CURRENT_USER主键
        /// </summary>
        CurrentUser,

        /// <summary>
        /// 对应于 HKEY_LOCAL_MACHINE主键
        /// </summary>
        LocalMachine,

        /// <summary>
        /// 对应于 HKEY_USER主键
        /// </summary>
        Users,

        /// <summary>
        /// 对应于HEKY_CURRENT_CONFIG主键
        /// </summary>
        CurrentConfig
    }
}