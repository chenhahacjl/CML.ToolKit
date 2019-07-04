using System;
using System.IO;
using System.Net;
using System.Text;

namespace CML.CommonEx.NetworkEx
{
    /// <summary>
    /// 下载帮助类
    /// </summary>
    public class DownloadOperate
    {
        /// <summary>
        /// 获取HTML代码（UTF-8 编码）
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModelWebRequest webRequest, out string errMsg)
        {
            return CF_GetHtmlCode(webRequest, Encoding.UTF8, null, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModelWebRequest webRequest, Encoding encoding, out string errMsg)
        {
            return CF_GetHtmlCode(webRequest, encoding, null, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModelWebRequest webRequest, Encoding encoding, CookieContainer requestCookie, out string errMsg)
        {
            return CF_GetHtmlCode(webRequest, encoding, requestCookie, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModelWebRequest webRequest, Encoding encoding, out CookieContainer responseCookie, out string errMsg)
        {
            return CF_GetHtmlCode(webRequest, encoding, null, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModelWebRequest webRequest, Encoding encoding, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            string result = string.Empty;

            try
            {
                Stream stream = CF_GetWebStream(webRequest, requestCookie, out responseCookie, out errMsg);

                if (string.IsNullOrEmpty(errMsg))
                {
                    using (StreamReader streamReader = new StreamReader(stream, encoding))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                responseCookie = null;
                errMsg = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(string savePath, ModelWebRequest webRequest, out string errMsg)
        {
            return CF_DownloadFile(savePath, webRequest, null, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(string savePath, ModelWebRequest webRequest, CookieContainer requestCookie, out string errMsg)
        {
            return CF_DownloadFile(savePath, webRequest, requestCookie, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(string savePath, ModelWebRequest webRequest, out CookieContainer responseCookie, out string errMsg)
        {
            return CF_DownloadFile(savePath, webRequest, null, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(string savePath, ModelWebRequest webRequest, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            bool result = false;

            try
            {
                Stream stream = CF_GetWebStream(webRequest, requestCookie, out responseCookie, out errMsg);

                if (string.IsNullOrEmpty(errMsg))
                {
                    using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        stream.CopyTo(fileStream);
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                responseCookie = null;
                errMsg = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModelWebRequest webRequest, out string errMsg)
        {
            return CF_GetWebStream(webRequest, null, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModelWebRequest webRequest, CookieContainer requestCookie, out string errMsg)
        {
            return CF_GetWebStream(webRequest, requestCookie, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModelWebRequest webRequest, out CookieContainer responseCookie, out string errMsg)
        {
            return CF_GetWebStream(webRequest, null, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModelWebRequest webRequest, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            Stream result = null;

            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(webRequest.RequestUrl) as HttpWebRequest;

                if (httpWebRequest == null)
                {
                    responseCookie = null;
                    errMsg = "URL不规范！";
                }
                else
                {
                    httpWebRequest.KeepAlive = webRequest.KeepAlive;
                    httpWebRequest.AllowAutoRedirect = webRequest.AllowAutoRedirect;

                    httpWebRequest.Proxy = webRequest.Proxy;
                    httpWebRequest.Timeout = webRequest.TimeOut;
                    httpWebRequest.Method = webRequest.Method.ToString();
                    httpWebRequest.ProtocolVersion = webRequest.ProtocolVersion;

                    if (!string.IsNullOrEmpty(webRequest.Host))
                    {
                        httpWebRequest.Host = webRequest.Host;
                    }
                    httpWebRequest.Accept = webRequest.Accept;
                    httpWebRequest.UserAgent = webRequest.UserAgent;
                    httpWebRequest.ContentType = webRequest.ContentType;
                    foreach (string key in webRequest.Headers.Keys)
                    {
                        httpWebRequest.Headers.Add(key, webRequest.Headers[key]);
                    }

                    httpWebRequest.CookieContainer = requestCookie;

                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        responseCookie = httpWebRequest.CookieContainer;
                        result = httpWebResponse.GetResponseStream();
                        errMsg = "";
                    }
                    else
                    {
                        responseCookie = null;
                        result = null;
                        errMsg = $"[{(int)httpWebResponse.StatusCode}:{httpWebResponse.StatusCode}]数据流请求错误！";
                    }
                }

            }
            catch (Exception ex)
            {
                responseCookie = null;
                errMsg = ex.Message;
            }

            return result;
        }



        /// <summary>
        /// 获取随机UserAgent
        /// </summary>
        /// <returns>UserAgent</returns>
        public static string CF_GetUserAgents()
        {
            string[] userAgents = {
                "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0) Gecko/20100101 Firefox/5.0",
                "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.9.1) Gecko/20090624 Firefox/3.5",
                "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.6) Gecko/20091201 Firefox/3.5.6",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.11 (KHTML, like Gecko) Chrome/20.0.1132.11 TaoBrowser/2.0 Safari/536.11",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.71 Safari/537.1 LBBROWSER",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; LBBROWSER)",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; QQDownload 732; .NET4.0C; .NET4.0E; LBBROWSER)",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.84 Safari/535.11 LBBROWSER",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; QQBrowser/7.0.3698.400)",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; QQDownload 732; .NET4.0C; .NET4.0E)",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; SV1; QQDownload 732; .NET4.0C; .NET4.0E; 360SE)",
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1",
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.84 Safari/535.11 SE 2.X MetaSr 1.0",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; SV1; QQDownload 732; .NET4.0C; .NET4.0E; SE 2.X MetaSr 1.0)",
                "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:16.0) Gecko/20121026 Firefox/16.0",
                "Mozilla/5.0 (iPad; U; CPU OS 4_2_1 like Mac OS X; zh-cn) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8C148 Safari/6533.18.5",
                "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:2.0b13pre) Gecko/20110307 Firefox/4.0b13pre",
                "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:16.0) Gecko/20100101 Firefox/16.0",
                "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.15) Gecko/20110303 Firefox/3.6.15",
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11",
                "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Chrome/10.0.648.133",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Win64; x64; Trident/5.0)",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)",
                "Mozilla/5.0 (X11; U; Linux x86_64; zh-CN; rv:1.9.2.10) Gecko/20100922 Ubuntu/10.10 (maverick) Firefox/3.6.10",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
                "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:2.0.1) Gecko/20100101",
                "Mozilla/5.0 (Windows NT 6.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0;  SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E) QQBrowser/6.9.11079.201",
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.41 Safari/535.1 QQBrowser/6.9.11079.201",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0;  SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E)",
                "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.3 (KHTML, like Gecko) Chrome/6.0.472.33 Safari/534.3 SE 2.X MetaSr 1.0",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0;  SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E; SE 2.X MetaSr 1.0)",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0;  SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E)",
                "Mozilla/5.0 (Windows; U; Windows NT 6.1; ) AppleWebKit/534.12 (KHTML, like Gecko) Maxthon/3.0 Safari/534.12",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)",
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)",
                "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0;  SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; InfoPath.3)",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Win64; x64; Trident/5.0; .NET CLR 2.0.50727; SLCC2; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; Tablet PC 2.0; .NET4.0E)",
                "Opera/9.80 (Windows NT 6.1; U; zh-cn) Presto/2.9.168 Version/11.50",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/534.50 (KHTML, like Gecko) Version/5.1 Safari/534.50",
                "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:6.0) Gecko/20100101 Firefox/6.0",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.163 Safari/535.1",
                "MQQBrowser/26 Mozilla/5.0 (Linux; U; Android 2.3.7; zh-cn; MB200 Build/GRJ22; CyanogenMod-7) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1",
                "Mozilla/5.0 (Linux; U; Android 2.3.7; en-us; Nexus One Build/FRF91) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1",
                "MQQBrowser/26 Mozilla/5.0 (Linux; U; Android 2.3.7; zh-cn; MB200 Build/GRJ22; CyanogenMod-7) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1",
                "JUC (Linux; U; 2.3.7; zh-cn; MB200; 320*480) UCWEB7.9.3.103/139/999",
                "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:7.0a1) Gecko/20110623 Firefox/7.0a1 Fennec/7.0a1",
                "Opera/9.80 (Android 2.3.4; Linux; Opera Mobi/build-1107180945; U; en-GB) Presto/2.8.149 Version/11.10",
                "Mozilla/5.0 (Linux; U; Android 3.0; en-us; Xoom Build/HRI39) AppleWebKit/534.13 (KHTML, like Gecko) Version/4.0 Safari/534.13",
                "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/420.1 (KHTML, like Gecko) Version/3.0 Mobile/1A542a Safari/419.3",
                "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_0 like Mac OS X; en-us) AppleWebKit/532.9 (KHTML, like Gecko) Version/4.0.5 Mobile/8A293 Safari/6531.22.7",
                "Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B334b Safari/531.21.10",
                "Mozilla/5.0 (BlackBerry; U; BlackBerry 9800; en) AppleWebKit/534.1+ (KHTML, like Gecko) Version/6.0.0.337 Mobile Safari/534.1+",
                "Mozilla/5.0 (hp-tablet; Linux; hpwOS/3.0.0; U; en-US) AppleWebKit/534.6 (KHTML, like Gecko) wOSBrowser/233.70 Safari/534.6 TouchPad/1.0",
                "Mozilla/5.0 (SymbianOS/9.4; Series60/5.0 NokiaN97-1/20.0.019; Profile/MIDP-2.1 Configuration/CLDC-1.1) AppleWebKit/525 (KHTML, like Gecko) BrowserNG/7.1.18124",
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; HTC; Titan)"
            };

            return userAgents[new Random().Next(0, userAgents.Length)];
        }
    }
}
