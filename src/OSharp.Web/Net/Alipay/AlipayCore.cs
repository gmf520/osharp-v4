using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using OSharp.Utility.Logging;


namespace OSharp.Web.Net.Alipay
{
    /// <summary>
    /// 支付宝接口公用函数类，该类是请求、通知返回两个文件所调用的公用函数核心处理文件，不需要修改
    /// </summary>
    public static class AlipayCore
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(AlipayCore));

        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            return dicArrayPre.Where(temp => temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && !string.IsNullOrEmpty(temp.Value))
                .ToDictionary(temp => temp.Key, temp => temp.Value);
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }
            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value, code) + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 写日志，方便测试（看网站需求，也可以改成把记录存入数据库）
        /// </summary>
        /// <param name="sWord">要写入日志里的文本内容</param>
        public static void LogResult(string sWord)
        {
            Logger.Debug(sWord);
        }

        ///// <summary>
        ///// 获取文件的md5摘要
        ///// </summary>
        ///// <param name="sFile">文件流</param>
        ///// <returns>MD5摘要结果</returns>
        //public static string GetAbstractToMd5(Stream sFile)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] result = md5.ComputeHash(sFile);
        //    StringBuilder sb = new StringBuilder(32);
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        sb.Append(result[i].ToString("x").PadLeft(2, '0'));
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        ///// 获取文件的md5摘要
        ///// </summary>
        ///// <param name="dataFile">文件流</param>
        ///// <returns>MD5摘要结果</returns>
        //public static string GetAbstractToMd5(byte[] dataFile)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] result = md5.ComputeHash(dataFile);
        //    StringBuilder sb = new StringBuilder(32);
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        sb.Append(result[i].ToString("x").PadLeft(2, '0'));
        //    }
        //    return sb.ToString();
        //}
    }
}
