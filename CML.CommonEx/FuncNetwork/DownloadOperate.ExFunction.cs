using System.IO;
using System.Net;
using System.Text;

namespace CML.CommonEx.NetworkEx.ExFunction
{
    /// <summary>
    /// 下载操作类(扩展方法)
    /// </summary>
    public static class DownloadOperateEF
    {
        /// <summary>
        /// 获取HTML代码（UTF-8 编码）
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModelWebRequest webRequest, out string errMsg)
        {
            return DownloadOperate.CF_GetHtmlCode(webRequest, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModelWebRequest webRequest, Encoding encoding, out string errMsg)
        {
            return DownloadOperate.CF_GetHtmlCode(webRequest, encoding, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModelWebRequest webRequest, Encoding encoding, CookieContainer requestCookie, out string errMsg)
        {
            return DownloadOperate.CF_GetHtmlCode(webRequest, encoding, requestCookie, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModelWebRequest webRequest, Encoding encoding, out CookieContainer responseCookie, out string errMsg)
        {
            return DownloadOperate.CF_GetHtmlCode(webRequest, encoding, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModelWebRequest webRequest, Encoding encoding, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            return DownloadOperate.CF_GetHtmlCode(webRequest, encoding, requestCookie, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this string savePath, ModelWebRequest webRequest, out string errMsg)
        {
            return DownloadOperate.CF_DownloadFile(savePath, webRequest, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this string savePath, ModelWebRequest webRequest, CookieContainer requestCookie, out string errMsg)
        {
            return DownloadOperate.CF_DownloadFile(savePath, webRequest, requestCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this string savePath, ModelWebRequest webRequest, out CookieContainer responseCookie, out string errMsg)
        {
            return DownloadOperate.CF_DownloadFile(savePath, webRequest, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this string savePath, ModelWebRequest webRequest, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            return DownloadOperate.CF_DownloadFile(savePath, webRequest, requestCookie, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModelWebRequest webRequest, out string errMsg)
        {
            return DownloadOperate.CF_GetWebStream(webRequest, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModelWebRequest webRequest, CookieContainer requestCookie, out string errMsg)
        {
            return DownloadOperate.CF_GetWebStream(webRequest, requestCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModelWebRequest webRequest, out CookieContainer responseCookie, out string errMsg)
        {
            return DownloadOperate.CF_GetWebStream(webRequest, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModelWebRequest webRequest, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            return DownloadOperate.CF_GetWebStream(webRequest, requestCookie, out responseCookie, out errMsg);
        }
    }
}
