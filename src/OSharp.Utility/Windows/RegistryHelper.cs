// -----------------------------------------------------------------------
//  <copyright file="RegistryHelper.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-18 18:53</last-date>
// -----------------------------------------------------------------------

using System.Linq;

using Microsoft.Win32;


namespace OSharp.Utility.Windows
{
    /// <summary>
    /// 注册表辅助类
    /// </summary>
    public class RegistryHelper
    {
        #region 构造函数

        /// <summary>
        /// 使用默认参数实例化一个注册表操作实例
        /// </summary>
        public RegistryHelper()
            : this(RegistryBaseKey.LocalMachine, "Software\\")
        { }

        /// <summary>
        /// 使用参数实例化一个注册表操作实例
        /// </summary>
        /// <param name="subKeyName">注册表项名称</param>
        public RegistryHelper(string subKeyName)
            : this(RegistryBaseKey.LocalMachine, subKeyName)
        { }

        /// <summary>
        /// 使用参数实例化一个注册表操作实例
        /// </summary>
        /// <param name="baseKey">注册表基项域</param>
        /// <param name="subKeyName">注册表项名称</param>
        public RegistryHelper(RegistryBaseKey baseKey, string subKeyName)
        {
            BaseKey = baseKey;
            SubKeyName = subKeyName ?? "Software\\";
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置注册表基项域
        /// </summary>
        public RegistryBaseKey BaseKey { get; set; }

        /// <summary>
        /// 注册表项名称
        /// </summary>
        public string SubKeyName { get; set; }

        /// <summary>
        /// 键值名称
        /// </summary>
        public string ValueName { get; set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取注册表基项域对应顶级节点
        /// </summary>
        /// <returns>顶级节点</returns>
        private RegistryKey GetTopKey()
        {
            RegistryKey key;
            switch (BaseKey)
            {
                case RegistryBaseKey.ClassesRoot:
                    key = Registry.ClassesRoot;
                    break;
                case RegistryBaseKey.CurrentUser:
                    key = Registry.CurrentUser;
                    break;
                case RegistryBaseKey.LocalMachine:
                    key = Registry.LocalMachine;
                    break;
                case RegistryBaseKey.Users:
                    key = Registry.Users;
                    break;
                case RegistryBaseKey.CurrentConfig:
                    key = Registry.CurrentConfig;
                    break;
                default:
                    key = Registry.LocalMachine;
                    break;
            }
            return key;
        }

        /// <summary>
        /// 获取指定注册表基项域对应顶级节点
        /// </summary>
        /// <returns>顶级节点</returns>
        private static RegistryKey GetTopKey(RegistryBaseKey baseKey)
        {
            RegistryKey key;
            switch (baseKey)
            {
                case RegistryBaseKey.ClassesRoot:
                    key = Registry.ClassesRoot;
                    break;
                case RegistryBaseKey.CurrentUser:
                    key = Registry.CurrentUser;
                    break;
                case RegistryBaseKey.LocalMachine:
                    key = Registry.LocalMachine;
                    break;
                case RegistryBaseKey.Users:
                    key = Registry.Users;
                    break;
                case RegistryBaseKey.CurrentConfig:
                    key = Registry.CurrentConfig;
                    break;
                default:
                    key = Registry.LocalMachine;
                    break;
            }
            return key;
        }

        /// <summary>
        /// 打开注册表项节点
        /// </summary>
        /// <param name="writable">true为只读访问，false为写访问</param>
        /// <returns></returns>
        private RegistryKey OpenSubKey(bool writable = true)
        {
            RegistryKey topKey = GetTopKey();
            RegistryKey subKey = topKey.OpenSubKey(SubKeyName, writable);
            topKey.Close();
            return subKey;
        }

        /// <summary>
        /// 打开注册表项节点，以只读的方式检查子项
        /// </summary>
        /// <param name="baseKey">基项域</param>
        /// <param name="subKeyName">注册表项名称</param>
        /// <param name="writable">true为只读访问，false为写访问</param>
        /// <returns></returns>
        private static RegistryKey OpenSubKey(RegistryBaseKey baseKey, string subKeyName, bool writable = true)
        {
            RegistryKey topKey = GetTopKey(baseKey);
            RegistryKey subKey = topKey.OpenSubKey(subKeyName, writable);
            topKey.Close();
            return subKey;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 是否存在注册表项
        /// </summary>
        /// <returns>是否存在</returns>
        public bool IsExistSubKey()
        {
            bool flag = false;
            RegistryKey topKey = GetTopKey();
            if (topKey != null)
            {
                RegistryKey subKey = topKey.OpenSubKey(SubKeyName);
                if (subKey != null)
                {
                    flag = true;
                }
                topKey.Close();
            }
            return flag;
        }

        /// <summary>
        /// 是否存在指定的注册表项
        /// </summary>
        /// <param name="baseKey">要检查的基项域</param>
        /// <param name="subKeyName">要检查的注册表项名称</param>
        /// <returns>是否存在</returns>
        public static bool IsExistSubKey(RegistryBaseKey baseKey, string subKeyName)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");

            bool flag = false;
            RegistryKey topKey = GetTopKey(baseKey);
            RegistryKey subKey = topKey.OpenSubKey(subKeyName);
            if (subKey != null)
            {
                flag = true;
            }
            topKey.Close();
            return flag;
        }

        /// <summary>
        /// 检查是否存在键值
        /// </summary>
        /// <returns>是否存在</returns>
        public bool IsExistValueName()
        {
            if (string.IsNullOrEmpty(ValueName))
            {
                return false;
            }

            bool flag = false;
            if (IsExistSubKey())
            {
                RegistryKey subKey = OpenSubKey();
                if (subKey != null)
                {
                    string[] valueNames = subKey.GetValueNames();
                    if (valueNames.Any(name => string.CompareOrdinal(name, ValueName) == 0))
                    {
                        flag = true;
                    }
                    subKey.Close();
                }
            }
            return flag;
        }

        /// <summary>
        /// 检查指定注册表项中是否存在指定键值
        /// </summary>
        /// <param name="baseKey">基项域</param>
        /// <param name="subKeyName">注册表项</param>
        /// <param name="valueName">要检查的键值名称</param>
        /// <returns>是否存在</returns>
        public static bool IsExistValueName(RegistryBaseKey baseKey, string subKeyName, string valueName)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");
            valueName.CheckNotNullOrEmpty("valueName");

            bool flag = false;
            if (IsExistSubKey(baseKey, subKeyName))
            {
                RegistryKey subKey = OpenSubKey(baseKey, subKeyName);
                if (subKey != null)
                {
                    string[] valueNames = subKey.GetValueNames();
                    if (valueNames.Any(name => string.CompareOrdinal(valueName, name) == 0))
                    {
                        flag = true;
                    }
                    subKey.Close();
                }
            }
            return flag;
        }

        /// <summary>
        /// 创建注册表项
        /// </summary>
        public void CreateSubKey()
        {
            RegistryKey topKey = GetTopKey();
            if (!IsExistSubKey())
            {
                topKey.CreateSubKey(SubKeyName);
            }
            topKey.Close();
        }

        /// <summary>
        /// 在指定基项域中创建注册表项
        /// </summary>
        /// <param name="baseKey">定基项域</param>
        /// <param name="subKeyName">注册表项名称</param>
        public static void CreateSubKey(RegistryBaseKey baseKey, string subKeyName)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");

            RegistryKey topKey = GetTopKey(baseKey);
            if (!IsExistSubKey(baseKey, subKeyName))
            {
                topKey.CreateSubKey(subKeyName);
            }
            topKey.Close();
        }

        /// <summary>
        /// 删除注册表项
        /// </summary>
        public void DeleteSubKey()
        {
            RegistryKey topKey = GetTopKey(BaseKey);
            if (IsExistSubKey())
            {
                topKey.DeleteSubKey(SubKeyName);
            }
        }

        /// <summary>
        /// 删除指定基项域中的注册表项
        /// </summary>
        /// <param name="baseKey">指定基项域</param>
        /// <param name="subKeyName">注册表项名称</param>
        public static void DeleteSubKey(RegistryBaseKey baseKey, string subKeyName)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");

            RegistryKey topKey = GetTopKey(baseKey);
            if (IsExistSubKey(baseKey, subKeyName))
            {
                topKey.DeleteSubKey(subKeyName);
            }
        }

        /// <summary>
        /// 获取键值内容
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            object value = null;
            if (IsExistValueName())
            {
                RegistryKey subKey = OpenSubKey(writable: false);
                value = subKey.GetValue(ValueName);
                subKey.Close();
            }
            return value;
        }

        /// <summary>
        /// 获取指定键值的内容
        /// </summary>
        /// <param name="baseKey">基项域</param>
        /// <param name="subKeyName">注册表项</param>
        /// <param name="valueName">要检查的键值名称</param>
        /// <returns></returns>
        public static object GetValue(RegistryBaseKey baseKey, string subKeyName, string valueName)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");
            valueName.CheckNotNullOrEmpty("valueName");

            object value = null;
            if (IsExistValueName(baseKey, subKeyName, valueName))
            {
                RegistryKey subKey = OpenSubKey(baseKey, subKeyName, writable: false);
                value = subKey.GetValue(valueName);
                subKey.Close();
            }
            return value;
        }

        /// <summary>
        /// 设置键值的内容
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            value.CheckNotNull("value");
            //不存在注册表项，则创建之
            if (!IsExistSubKey())
            {
                CreateSubKey();
            }

            RegistryKey subKey = OpenSubKey();
            try
            {
                subKey.SetValue(ValueName, value);
            }
            finally
            {
                subKey.Close();
            }
        }

        /// <summary>
        /// 设置键值的内容，并指定数据类型
        /// </summary>
        /// <param name="value">要设置的值</param>
        /// <param name="valueKind">设置值的数据类型</param>
        public void SetValue(object value, RegistryValueKind valueKind)
        {
            value.CheckNotNull("value");

            //不存在注册表项，则创建之
            if (!IsExistSubKey())
            {
                CreateSubKey();
            }
            RegistryKey subKey = OpenSubKey();
            try
            {
                subKey.SetValue(ValueName, value, valueKind);
            }
            finally
            {
                subKey.Close();
            }
        }

        /// <summary>
        /// 设置指定键值的内容
        /// </summary>
        /// <param name="baseKey">基项域</param>
        /// <param name="subKeyName">注册表项</param>
        /// <param name="valueName">要检查的键值名称</param>
        /// <param name="value"></param>
        public static void SetValue(RegistryBaseKey baseKey, string subKeyName, string valueName, object value)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");
            valueName.CheckNotNullOrEmpty("valueName");
            value.CheckNotNull("value");

            //不存在注册表项，则创建之
            if (!IsExistSubKey(baseKey, subKeyName))
            {
                CreateSubKey(baseKey, subKeyName);
            }

            RegistryKey subKey = OpenSubKey(baseKey, subKeyName);
            try
            {
                subKey.SetValue(valueName, value);
            }
            finally
            {
                subKey.Close();
            }
        }

        /// <summary>
        /// 设置指定键值的内容，并指定数据类型
        /// </summary>
        /// <param name="baseKey">基项域</param>
        /// <param name="subKeyName">注册表项</param>
        /// <param name="valueName">要检查的键值名称</param>
        /// <param name="value">要设置的值</param>
        /// <param name="valueKind">要设置的值的数据类型</param>
        public static void SetValue(RegistryBaseKey baseKey, string subKeyName, string valueName, object value, RegistryValueKind valueKind)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");
            valueName.CheckNotNullOrEmpty("valueName");

            //不存在注册表项，则创建之
            if (!IsExistSubKey(baseKey, subKeyName))
            {
                CreateSubKey(baseKey, subKeyName);
            }

            RegistryKey subKey = OpenSubKey(baseKey, subKeyName);
            try
            {
                subKey.SetValue(valueName, value, valueKind);
            }
            finally
            {
                subKey.Close();
            }
        }

        /// <summary>
        /// 删除键值
        /// </summary>
        public void DeleteValue()
        {
            if (IsExistValueName())
            {
                RegistryKey subKey = OpenSubKey();
                subKey.DeleteValue(ValueName);
            }
        }

        /// <summary>
        /// 删除指定的键值
        /// </summary>
        /// <param name="baseKey">基项域</param>
        /// <param name="subKeyName">注册表项</param>
        /// <param name="valueName">要检查的键值名称</param>
        public static void DeleteValue(RegistryBaseKey baseKey, string subKeyName, string valueName)
        {
            subKeyName.CheckNotNullOrEmpty("subKeyName");
            valueName.CheckNotNullOrEmpty("valueName");

            if (IsExistValueName(baseKey, subKeyName, valueName))
            {
                RegistryKey subKey = OpenSubKey(baseKey, subKeyName);
                subKey.DeleteValue(valueName);
            }
        }

        #endregion
    }
}