// -----------------------------------------------------------------------
//  <copyright file="DecToOctConverter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-01-26 22:19</last-date>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;

using OSharp.Utility.Extensions;


namespace OSharp.Utility.Wpf.Converters
{
    /// <summary>
    /// 十进制-八进制转换器
    /// </summary>
    public class DecToOctConverter : IValueConverter
    {
        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定源生成的值。</param><param name="targetType">绑定目标属性的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "0";
            }
            if (!(value is int))
            {
                return "0";
            }
            int dec = (int)value;
            return System.Convert.ToString(dec, 8);
        }

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        /// <param name="value">绑定目标生成的值。</param><param name="targetType">要转换到的类型。</param><param name="parameter">要使用的转换器参数。</param><param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            string binPattern = "^[0-7]+$";
            string bin = value.ToString();
            if (!bin.IsMatch(binPattern, false))
            {
                return 0;
            }
            return System.Convert.ToInt32(bin, 8);
        }
    }
}