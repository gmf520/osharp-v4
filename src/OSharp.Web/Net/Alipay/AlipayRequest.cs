using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

using OSharp.Utility.Secutiry;


namespace OSharp.Web.Net.Alipay
{
    /// <summary>
    /// 支付宝各接口请求提交类，构造支付宝接口表单HTML文本，获取远程HTTP数据
    /// </summary>
    public static class AlipayRequest
    {
        #region 字段

        //支付宝网关地址
        private const string GateWay = "https://mapi.alipay.com/gateway.do?";
        //商户的私钥
        private static readonly string Key;
        //编码格式
        private static readonly string InputCharset;
        //签名方式
        private static readonly string SignType;

        #endregion

        #region 构造函数

        static AlipayRequest()
        {
            Key = AlipayConfig.Key.Trim();
            InputCharset = AlipayConfig.InputCharset.Trim().ToLower();
            SignType = AlipayConfig.SignType.Trim().ToUpper();
        }

        #endregion

        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        /// <param name="sPara">请求给支付宝的参数数组</param>
        /// <returns>签名结果</returns>
        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string prestr = AlipayCore.CreateLinkString(sPara);

            //把最终的字符串签名，获得签名结果
            string mysign = string.Empty;
            switch (SignType)
            {
                case "MD5":
                    mysign = HashHelper.GetMd5(prestr + Key, Encoding.GetEncoding(InputCharset));
                    break;
            }

            return mysign;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //过滤签名参数数组
            Dictionary<string, string> sPara = AlipayCore.FilterPara(sParaTemp);

            //获得签名结果
            string mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", SignType);

            return sPara;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>要请求的参数数组字符串</returns>
        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = BuildRequestPara(sParaTemp);

            //把参数组中所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
            string strRequestData = AlipayCore.CreateLinkStringUrlencode(sPara, code);

            return strRequestData;
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = BuildRequestPara(sParaTemp);

            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + GateWay + "_input_charset=" + InputCharset + "' method='" +
                strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// 建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <returns>支付宝处理结果</returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp)
        {
            Encoding code = Encoding.GetEncoding(InputCharset);

            //待请求参数数组字符串
            string strRequestData = BuildRequestParaToString(sParaTemp, code);

            //把数组转换成流中所需字节数组类型
            byte[] bytesRequestData = code.GetBytes(strRequestData);

            //构造请求地址
            string strUrl = GateWay + "_input_charset=" + InputCharset;

            //请求远程HTTP
            string strResult = string.Empty;
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(strUrl);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                //填充POST数据
                myReq.ContentLength = bytesRequestData.Length;
                Stream requestStream = myReq.GetRequestStream();
                requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                requestStream.Close();

                //发送POST数据请求服务器
                HttpWebResponse httpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = httpWResp.GetResponseStream();

                //获取服务器返回信息
                if (myStream != null)
                {
                    StreamReader reader = new StreamReader(myStream, code);
                    StringBuilder responseData = new StringBuilder();
                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        responseData.Append(line);
                    }

                    //释放
                    myStream.Close();

                    strResult = responseData.ToString();
                }
            }
            catch (Exception exp)
            {
                strResult = "报错：" + exp.Message;
            }

            return strResult;
        }

        /// <summary>
        /// 建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果，带文件上传功能
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="fileName">文件绝对路径</param>
        /// <param name="data">文件数据</param>
        /// <param name="contentType">文件内容类型</param>
        /// <param name="lengthFile">文件长度</param>
        /// <returns>支付宝处理结果</returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string fileName, byte[] data,
            string contentType, int lengthFile)
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = BuildRequestPara(sParaTemp);

            //构造请求地址
            string strUrl = GateWay + "_input_charset=" + InputCharset;

            //设置HttpWebRequest基本信息
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
            //设置请求方式：get、post
            request.Method = strMethod;
            //设置boundaryValue
            string boundaryValue = DateTime.Now.Ticks.ToString("x");
            string boundary = "--" + boundaryValue;
            request.ContentType = "\r\nmultipart/form-data; boundary=" + boundaryValue;
            //设置KeepAlive
            request.KeepAlive = true;
            //设置请求数据，拼接成字符串
            StringBuilder sbHtml = new StringBuilder();
            foreach (KeyValuePair<string, string> key in dicPara)
            {
                sbHtml.Append(boundary + "\r\nContent-Disposition: form-data; name=\"" + key.Key + "\"\r\n\r\n" + key.Value + "\r\n");
            }
            sbHtml.Append(boundary + "\r\nContent-Disposition: form-data; name=\"withhold_file\"; filename=\"");
            sbHtml.Append(fileName);
            sbHtml.Append("\"\r\nContent-Type: " + contentType + "\r\n\r\n");
            string postHeader = sbHtml.ToString();
            //将请求数据字符串类型根据编码格式转换成字节流
            Encoding code = Encoding.GetEncoding(InputCharset);
            byte[] postHeaderBytes = code.GetBytes(postHeader);
            byte[] boundayBytes = Encoding.ASCII.GetBytes("\r\n" + boundary + "--\r\n");
            //设置长度
            long length = postHeaderBytes.Length + lengthFile + boundayBytes.Length;
            request.ContentLength = length;

            //请求远程HTTP
            Stream requestStream = request.GetRequestStream();
            Stream myStream;
            try
            {
                //发送数据请求服务器
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                requestStream.Write(data, 0, lengthFile);
                requestStream.Write(boundayBytes, 0, boundayBytes.Length);
                HttpWebResponse httpWResp = (HttpWebResponse)request.GetResponse();
                myStream = httpWResp.GetResponseStream();
            }
            catch (WebException e)
            {
                return e.ToString();
            }
            finally
            {
                requestStream.Close();
            }

            //读取支付宝返回处理结果
            if (myStream != null)
            {
                StreamReader reader = new StreamReader(myStream, code);
                StringBuilder responseData = new StringBuilder();

                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }
                myStream.Close();
                return responseData.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
        /// 注意：远程解析XML出错，与IIS服务器配置有关
        /// </summary>
        /// <returns>时间戳字符串</returns>
        public static string QueryTimestamp()
        {
            string url = GateWay + "service=query_timestamp&partner=" + AlipayConfig.Partner;

            XmlTextReader reader = new XmlTextReader(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);

            XmlNode node = xmlDoc.SelectSingleNode("/alipay/response/timestamp/encrypt_key");
            if (node != null)
            {
                string encryptKey = node.InnerText;

                return encryptKey;
            }
            return string.Empty;
        }
    }
}
