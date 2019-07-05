﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace CML.CommonEx.NetworkEx
{
    /// <summary>
    /// 下载操作类
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
                if (!(WebRequest.Create(webRequest.RequestUrl) is HttpWebRequest httpWebRequest))
                {
                    responseCookie = null;
                    errMsg = "URL不规范！";
                }
                else
                {
                    httpWebRequest.KeepAlive = webRequest.KeepAlive;
                    httpWebRequest.AllowAutoRedirect = webRequest.AllowAutoRedirect;

                    if (webRequest.Proxy.Enable)
                    {
                        httpWebRequest.Proxy = new WebProxy(webRequest.Proxy.Host, webRequest.Proxy.Port);
                    }
                    httpWebRequest.Timeout = webRequest.TimeOut;
                    httpWebRequest.Method = webRequest.Method.ToString();
                    httpWebRequest.ProtocolVersion = webRequest.ProtocolVersion;

                    if (!string.IsNullOrEmpty(webRequest.Host))
                    {
                        httpWebRequest.Host = webRequest.Host;
                    }
                    if (!string.IsNullOrEmpty(webRequest.Accept))
                    {
                        httpWebRequest.Accept = webRequest.Accept;
                    }
                    if (!string.IsNullOrEmpty(webRequest.Referer))
                    {
                        httpWebRequest.Referer = webRequest.Referer;
                    }
                    if (!string.IsNullOrEmpty(webRequest.UserAgent))
                    {
                        httpWebRequest.UserAgent = webRequest.UserAgent;
                    }
                    if (!string.IsNullOrEmpty(webRequest.ContentType))
                    {
                        httpWebRequest.ContentType = webRequest.ContentType;
                    }
                    if (!string.IsNullOrEmpty(webRequest.ContentType))
                    {
                        httpWebRequest.ContentType = webRequest.ContentType;
                    }
                    foreach (string key in webRequest.Headers.Keys)
                    {
                        httpWebRequest.Headers[key] = webRequest.Headers[key];
                    }
                    if (!string.IsNullOrEmpty(webRequest.Cookie))
                    {
                        httpWebRequest.Headers["Cookie"] = webRequest.Cookie;
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
    }
}
