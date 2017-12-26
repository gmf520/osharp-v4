// -----------------------------------------------------------------------
//  <copyright file="FontWeightConverter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-12-27 1:05</last-date>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace OSharp.Utility.Wpf.Converters
{
    /// <summary>
    /// bool - 字重转化
    /// </summary>
    public class BooleanToFontWeightConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool flag = (bool)value;
                return flag ? FontWeights.Normal : FontWeights.Bold;
            }
            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}