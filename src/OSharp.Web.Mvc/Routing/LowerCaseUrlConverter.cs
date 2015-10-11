// -----------------------------------------------------------------------
//  <copyright file="LowerCaseUrlConverter.cs" company="OSharp��Դ�Ŷ�">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>������</last-editor>
//  <last-date>2014-08-29 15:12</last-date>
// -----------------------------------------------------------------------

using System;
using System.Text;


namespace OSharp.Web.Mvc.Routing
{
    /// <summary>
    /// СдURLת����
    /// </summary>
    public static class LowerCaseUrlConverter
    {
        private const char Minus = '-';

        /// <summary>
        /// ������ĵ��ʲ�ֳ��� - ���ӵ�Сд��ʽ
        /// </summary>
        /// <param name="input"> </param>
        /// <returns> </returns>
        public static string Spliter(string input)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            foreach (char str in input)
            {
                if (str >= 'A' && str <= 'Z')
                {
                    if (index > 0)
                    {
                        builder.Append(Minus);
                    }
                    builder.Append(Char.ToLower(str));
                }
                else if (str == Minus)
                {
                    builder.Append(Minus);
                    builder.Append(Minus);
                }
                else
                {
                    builder.Append(str);
                }
                index++;
            }

            return builder.ToString();
        }

        /// <summary>
        /// ���� - ���ӵ�Сд���ʻ�ԭΪ��д��ʽ
        /// </summary>
        /// <param name="input"> </param>
        /// <returns> </returns>
        public static string Restore(string input)
        {
            StringBuilder builder = new StringBuilder();
            CharEnumerator iterator = input.GetEnumerator();
            int index = 0;
            while (iterator.MoveNext())
            {
                if (iterator.Current != Minus)
                {
                    char c = iterator.Current;
                    if (index == 0)
                    {
                        c = char.ToUpper(c);
                    }
                    builder.Append(c);
                    index++;
                    continue;
                }
                if (!iterator.MoveNext())
                {
                    builder.Append(Minus);
                    break;
                }
                if (iterator.Current == Minus)
                {
                    builder.Append(Minus);
                }
                else
                {
                    char c = iterator.Current;
                    c = char.ToUpper(c);
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}