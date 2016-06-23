// -----------------------------------------------------------------------
//  <copyright file="MathHelper.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-18 16:21</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace OSharp.Utility.Data
{
    /// <summary>
    /// 数据计算辅助操作类
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// 获取两个坐标的距离
        /// </summary>
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }

        /// <summary>
        /// 计算四则表达式的结果
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static double FourSpecies(string exp)
        {
            MathExpression expression = new MathExpression(exp);
            return expression.Compute();
        }
    }
}