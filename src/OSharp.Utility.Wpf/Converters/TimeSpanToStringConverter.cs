// -----------------------------------------------------------------------
//  <copyright file="TimeSpanToStringConverter.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-12-27 1:08</last-date>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;


namespace OSharp.Utility.Wpf.Converters
{
    /// <summary>
    /// TimeSpan转字符串
    /// </summary>
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                TimeSpan ts = (TimeSpan)value;
                return ts.ToString(@"hh\:mm\:ss");
            }
            return "Not Start";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}