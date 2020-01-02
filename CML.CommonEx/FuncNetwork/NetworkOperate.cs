using CML.CommonEx.EnumEx.ExFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace CML.CommonEx.NetworkEx
{
    /// <summary>
    /// 网络操作类
    /// </summary>
    public class NetworkOperate
    {
        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModWebRequest webRequest, out string errMsg)
        {
            return CF_GetHtmlCode(webRequest, null, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModWebRequest webRequest, CookieContainer requestCookie, out string errMsg)
        {
            return CF_GetHtmlCode(webRequest, requestCookie, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModWebRequest webRequest, out CookieContainer responseCookie, out string errMsg)
        {
            return CF_GetHtmlCode(webRequest, null, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取HTML代码
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>HTML代码</returns>
        public static string CF_GetHtmlCode(ModWebRequest webRequest, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            errMsg = "";
            string result = string.Empty;

            try
            {
                Stream stream = CF_GetWebStream(webRequest, requestCookie, out responseCookie, out errMsg);

                if (string.IsNullOrEmpty(errMsg))
                {
                    //判断是否限速
                    ModHttpTransSpeed transmissionSpeed = webRequest.DownloadSpeed;
                    if (transmissionSpeed.EnableLimit)
                    {
                        //数据列表
                        List<byte> lstBytes = new List<byte>();

                        //缓存字节数
                        int bufferSize = transmissionSpeed.Speed * (int)Math.Pow(2, transmissionSpeed.Unit.CF_ToNumber());
                        //缓存
                        byte[] btBuffer = new byte[bufferSize];

                        //读取的字节数
                        int readSize = stream.Read(btBuffer, 0, bufferSize);
                        while (readSize > 0)
                        {
                            //从服务器读取
                            stream.Read(btBuffer, 0, readSize);
                            //记入列表
                            lstBytes.AddRange(btBuffer);

                            readSize = stream.Read(btBuffer, 0, bufferSize);

                            //延时
                            if (readSize > 0)
                            {
                                Thread.Sleep(transmissionSpeed.Delay);
                            }
                        }

                        //转化为字符串
                        result = webRequest.Encoding.GetString(lstBytes.ToArray());
                    }
                    else
                    {
                        using (StreamReader streamReader = new StreamReader(stream, webRequest.Encoding))
                        {
                            result = streamReader.ReadToEnd();
                        }
                    }
                }

                stream.Close();
            }
            catch (Exception ex)
            {
                responseCookie = null;
                errMsg = string.IsNullOrEmpty(errMsg) ? ex.Message : errMsg;
            }

            return result;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(ModWebRequest webRequest, string filePath, out string errMsg)
        {
            return CF_UploadFile(webRequest, filePath, null, out _, out _, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="returnMsg">[OUT]返回消息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(ModWebRequest webRequest, string filePath, out string returnMsg, out string errMsg)
        {
            return CF_UploadFile(webRequest, filePath, null, out _, out returnMsg, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(ModWebRequest webRequest, string filePath, CookieContainer requestCookie, out string errMsg)
        {
            return CF_UploadFile(webRequest, filePath, requestCookie, out _, out _, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="returnMsg">[OUT]返回消息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(ModWebRequest webRequest, string filePath, CookieContainer requestCookie, out string returnMsg, out string errMsg)
        {
            return CF_UploadFile(webRequest, filePath, requestCookie, out _, out returnMsg, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(ModWebRequest webRequest, string filePath, out CookieContainer responseCookie, out string errMsg)
        {
            return CF_UploadFile(webRequest, filePath, null, out responseCookie, out _, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="returnMsg">[OUT]返回消息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(ModWebRequest webRequest, string filePath, out CookieContainer responseCookie, out string returnMsg, out string errMsg)
        {
            return CF_UploadFile(webRequest, filePath, null, out responseCookie, out returnMsg, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="returnMsg">[OUT]返回消息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_UploadFile(ModWebRequest webRequest, string filePath, CookieContainer requestCookie, out CookieContainer responseCookie, out string returnMsg, out string errMsg)
        {
            errMsg = "";
            bool result = false;

            //本地文件
            FileInfo file = new FileInfo(filePath);

            if (!file.Exists)
            {
                result = false;
                responseCookie = null;
                returnMsg = "";
                errMsg = "本地文件不存在！";
            }
            else
            {
                try
                {
                    // 随机分隔线
                    string boundary = "CML.ToolKit.NetworkEx." + DateTime.Now.Ticks.ToString("X");

                    //请求头
                    string strPostHeader =
                         $"--{boundary}\r\n" +
                         $"Content-Disposition: form-data; name=\"file\"; filename=\"{file.Name}\"\r\n" +
                         $"Content-Type: application/octet-stream\r\n\r\n";

                    //请求尾
                    string strPostEnder = $"\r\n--{boundary}--\r\n";

                    //请求数据
                    List<byte> bytePostData = new List<byte>();
                    bytePostData.AddRange(Encoding.ASCII.GetBytes(strPostHeader));
                    bytePostData.AddRange(File.ReadAllBytes(file.FullName));
                    bytePostData.AddRange(Encoding.ASCII.GetBytes(strPostEnder));

                    //构造请求信息模型
                    webRequest.ContentType = $"multipart/form-data;boundary={boundary}";
                    webRequest.PostBytes = bytePostData.ToArray();
                    webRequest.Method = ERequestMethod.POST;

                    using (Stream stream = CF_GetWebStream(webRequest, requestCookie, out responseCookie, out errMsg))
                    {
                        result = stream != null;

                        if (result)
                        {
                            using (StreamReader streamReader = new StreamReader(stream, webRequest.Encoding))
                            {
                                returnMsg = streamReader.ReadToEnd();
                            }
                        }
                        else
                        {
                            returnMsg = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseCookie = null;
                    returnMsg = "";
                    errMsg = string.IsNullOrEmpty(errMsg) ? ex.Message : errMsg;
                }
            }

            return result;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(ModWebRequest webRequest, string savePath, out string errMsg)
        {
            return CF_DownloadFile(webRequest, savePath, null, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(ModWebRequest webRequest, string savePath, CookieContainer requestCookie, out string errMsg)
        {
            return CF_DownloadFile(webRequest, savePath, requestCookie, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(ModWebRequest webRequest, string savePath, out CookieContainer responseCookie, out string errMsg)
        {
            return CF_DownloadFile(webRequest, savePath, null, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DownloadFile(ModWebRequest webRequest, string savePath, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            errMsg = "";
            bool result = false;

            try
            {
                Stream stream = CF_GetWebStream(webRequest, requestCookie, out responseCookie, out errMsg);

                if (string.IsNullOrEmpty(errMsg))
                {
                    using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        ModHttpTransSpeed transmissionSpeed = webRequest.DownloadSpeed;
                        if (transmissionSpeed.EnableLimit)
                        {
                            //缓存字节数
                            int bufferSize = transmissionSpeed.Speed * (int)Math.Pow(2, transmissionSpeed.Unit.CF_ToNumber());
                            //缓存
                            byte[] btBuffer = new byte[bufferSize];

                            //读取的字节数
                            int readSize = stream.Read(btBuffer, 0, bufferSize);
                            while (readSize > 0)
                            {
                                //写入服务器
                                fileStream.Write(btBuffer, 0, readSize);

                                readSize = stream.Read(btBuffer, 0, bufferSize);

                                //延时
                                if (readSize > 0)
                                {
                                    Thread.Sleep(transmissionSpeed.Delay);
                                }
                            }
                        }
                        else
                        {
                            stream.CopyTo(fileStream);
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                responseCookie = null;
                errMsg = string.IsNullOrEmpty(errMsg) ? ex.Message : errMsg;
            }

            return result;
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModWebRequest webRequest, out string errMsg)
        {
            return CF_GetWebStream(webRequest, null, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModWebRequest webRequest, CookieContainer requestCookie, out string errMsg)
        {
            return CF_GetWebStream(webRequest, requestCookie, out CookieContainer _, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModWebRequest webRequest, out CookieContainer responseCookie, out string errMsg)
        {
            return CF_GetWebStream(webRequest, null, out responseCookie, out errMsg);
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="webRequest">WEB请求信息</param>
        /// <param name="requestCookie">请求Cookie</param>
        /// <param name="responseCookie">响应Cookie</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>数据流</returns>
        public static Stream CF_GetWebStream(ModWebRequest webRequest, CookieContainer requestCookie, out CookieContainer responseCookie, out string errMsg)
        {
            errMsg = "";
            Stream result = null;

            try
            {
                //检查输入URL
                if (!(WebRequest.Create(webRequest.RequestUrl) is HttpWebRequest httpWebRequest))
                {
                    responseCookie = null;
                    errMsg = "URL不规范！";
                }
                else
                {
                    //初始化请求模型
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

                    //判断请求方式
                    if (webRequest.Method == ERequestMethod.POST)
                    {
                        byte[] postData;
                        //优先处理 PostBytes
                        if (webRequest.PostBytes == null)
                        {
                            StringBuilder postString = new StringBuilder();

                            //再处理 PostString
                            if (string.IsNullOrEmpty(webRequest.PostString))
                            {
                                foreach (string key in webRequest.PostDictionary.Keys)
                                {
                                    if (!string.IsNullOrEmpty(key))
                                    {
                                        postString.Append($"{key}={ webRequest.PostDictionary[key]}&");
                                    }
                                }
                            }
                            else
                            {
                                postString.Append(webRequest.PostString);
                            }

                            postData = Encoding.UTF8.GetBytes(postString.ToString().TrimEnd('&'));
                        }
                        else
                        {
                            postData = webRequest.PostBytes;
                        }

                        httpWebRequest.ContentLength = postData.Length;
                        using (Stream requestStream = httpWebRequest.GetRequestStream())
                        {
                            ModHttpTransSpeed transmissionSpeed = webRequest.UploadSpeed;
                            if (transmissionSpeed.EnableLimit)
                            {
                                //单次发送字节数
                                int bufferSize = transmissionSpeed.Speed * (int)Math.Pow(2, transmissionSpeed.Unit.CF_ToNumber());

                                //发送数据字节数小于限速单次发送字节数
                                if (postData.Length <= bufferSize)
                                {
                                    requestStream.Write(postData, 0, postData.Length);
                                }
                                else
                                {
                                    //计算传输次数
                                    int count = postData.Length / bufferSize;
                                    //发送数据
                                    for (int i = 0; i < count; i++)
                                    {
                                        requestStream.Write(postData, i * bufferSize, bufferSize);

                                        //延时（非最后一次）
                                        if (i != count - 1)
                                        {
                                            Thread.Sleep(transmissionSpeed.Delay);
                                        }
                                    }

                                    //补偿剩余字节
                                    if (bufferSize * count != postData.Length)
                                    {
                                        Thread.Sleep(transmissionSpeed.Delay);
                                        requestStream.Write(postData, bufferSize * count, postData.Length - bufferSize * count);
                                    }
                                }
                            }
                            else
                            {
                                requestStream.Write(postData, 0, postData.Length);
                            }

                            requestStream.Close();
                        }
                    }

                    //获取远程响应
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
                errMsg = string.IsNullOrEmpty(errMsg) ? ex.Message : errMsg;
            }

            return result;
        }
    }
}
