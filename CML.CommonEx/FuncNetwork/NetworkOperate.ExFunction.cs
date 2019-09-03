using System.IO;
using System.Net;
using System.Text;

namespace CML.CommonEx.NetworkEx.ExFunction
{
    /// <summary>
    /// 网络操作类(扩展方法)
    /// </summary>
    public static class NetworkOperateEF
    {
        /// <summary>
        /// 获取HTML代码（UTF-8 编码）
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModWebRequest webRequest, out string errMsg)
        {
            return NetworkOperate.CF_GetHtmlCode(webRequest, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModWebRequest webRequest, Encoding encoding, out string errMsg)
        {
            return NetworkOperate.CF_GetHtmlCode(webRequest, encoding, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModWebRequest webRequest, Encoding encoding, CookieContainer requestCookie, out string errMsg)
        {
            return NetworkOperate.CF_GetHtmlCode(webRequest, encoding, requestCookie, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(this ModWebRequest webRequest, Encoding encoding, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_GetHtmlCode(webRequest, encoding, out responseCookie, out errMsg);
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
        public static string CF_GetHtmlCode(this ModWebRequest webRequest, Encoding encoding, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_GetHtmlCode(webRequest, encoding, requestCookie, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(this ModWebRequest webRequest, string filePath, out string errMsg)
        {
            return NetworkOperate.CF_UploadFile(webRequest, filePath, null, out _, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(this ModWebRequest webRequest, string filePath, CookieContainer requestCookie, out string errMsg)
        {
            return NetworkOperate.CF_UploadFile(webRequest, filePath, requestCookie, out _, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(this ModWebRequest webRequest, string filePath, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_UploadFile(webRequest, filePath, null, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(this ModWebRequest webRequest, string filePath, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_UploadFile(webRequest, filePath, requestCookie, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this ModWebRequest webRequest, string savePath, out string errMsg)
        {
            return NetworkOperate.CF_DownloadFile(webRequest, savePath, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this ModWebRequest webRequest, string savePath, CookieContainer requestCookie, out string errMsg)
        {
            return NetworkOperate.CF_DownloadFile(webRequest, savePath, requestCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this ModWebRequest webRequest, string savePath, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_DownloadFile(webRequest, savePath, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(this ModWebRequest webRequest, string savePath, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_DownloadFile(webRequest, savePath, requestCookie, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModWebRequest webRequest, out string errMsg)
        {
            return NetworkOperate.CF_GetWebStream(webRequest, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModWebRequest webRequest, CookieContainer requestCookie, out string errMsg)
        {
            return NetworkOperate.CF_GetWebStream(webRequest, requestCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModWebRequest webRequest, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_GetWebStream(webRequest, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">[OUT]响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(this ModWebRequest webRequest, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            return NetworkOperate.CF_GetWebStream(webRequest, requestCookie, out responseCookie, out errMsg);
        }
    }
}
