// -----------------------------------------------------------------------
//  <copyright file="ViewModelExBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-12-27 1:26</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using GalaSoft.MvvmLight;

using Newtonsoft.Json;


namespace OSharp.Utility.Wpf
{
    /// <summary>
    /// 实现了属性设置值(SetProperty)功能的ViewModelBase，可以使用表达式触发RaisePropertyChanged事件，免去硬编码属性名的编写
    /// </summary>
    public abstract class ViewModelExBase : ViewModelBase, IDataErrorInfo
    {
        protected void SetProperty<T>(ref T field, T value, Expression<Func<T>> expression)
        {
            if ((object)field != null && field.Equals(value))
            {
                return;
            }
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException(@"表达式类型必须为 MemberExpression", "expression");
            }
            PropertyInfo property = body.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(@"表达式类型必须为 PropertyExpression", "expression");
            }
            string propertyName = property.Name;
            field = value;
            RaisePropertyChanged(propertyName);
        }

        #region Implementation of IDataErrorInfo

        /// <summary>
        /// 获取具有给定名称的属性的错误信息。
        /// </summary>
        /// <returns>
        /// 该属性的错误信息。默认值为空字符串 ("")。
        /// </returns>
        /// <param name="columnName">要获取其错误信息的属性的名称。</param>
        [JsonIgnore]
        public virtual string this[string columnName]
        {
            get
            {
                ValidationContext context = new ValidationContext(this, null, null) { MemberName = columnName };
                List<ValidationResult> results = new List<ValidationResult>();
                Validator.TryValidateProperty(GetType().GetProperty(columnName).GetValue(this, null), context, results);
                if (results.Count > 0)
                {
                    return string.Join(Environment.NewLine, results.Select(m => m.ErrorMessage).ToArray());
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取指示对象何处出错的错误信息。
        /// </summary>
        /// <returns>
        /// 指示对象何处出错的错误信息。默认值为空字符串 ("")。
        /// </returns>
        [JsonIgnore]
        public virtual string Error
        {
            get { return string.Empty; }
        }

        #endregion
    }
}