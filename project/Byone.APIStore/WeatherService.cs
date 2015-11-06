/********************************************************************************
** 命名空间:  Byone.APIStore
** 文 件 名:  WeatherService
** 作    者： Hxjhaker
** 生成日期： 2015-11-05 17:14:35
** 版 本 号:  V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Byone.APIStore
{
    public static class WeatherService
    {
        private const string apiUrl = "http://apis.baidu.com/apistore/weatherservice/citylist";
        private const string apiKey = "de1270ef1ca4b685a19624939e64df11";
        public static string Get(Dictionary<string, string> dic = null)
        {
            string url = apiUrl + "?r=" + Math.Abs(DateTime.Now.ToBinary()).ToString();
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    url += "&" + item.Key + "=" + item.Value;
                }
            }
            WebClient wc = new WebClient();
            wc.RequestHeaders.Add("apikey", apiKey);
            return wc.Get(apiUrl);
        }
    }
}
