using System;
using System.Collections.Generic;
using System.Net;

namespace CML.CommonEx.NetworkEx
{
    /// <summary>
    /// WEB请求模型
    /// </summary>
    public class ModelWebRequest
    {
        /// <summary>
        /// 请求网址
        /// </summary>
        public string RequestUrl { get; set; } = "";

        /// <summary>
        ///  保持存活（默认false）
        /// </summary>
        public bool KeepAlive { get; set; } = false;
        /// <summary>
        ///  允许自动重定向（默认false）
        /// </summary>
        public bool AllowAutoRedirect { get; set; } = false;

        /// <summary>
        /// 代理（默认不开启）
        /// </summary>
        public ModelProxy Proxy { get; set; } = new ModelProxy();
        /// <summary>
        /// 超时时间（毫秒，默认1000毫秒）
        /// </summary>
        public int TimeOut { get; set; } = 1000;
        /// <summary>
        /// 请求方式（默认GET）
        /// </summary>
        public ERequestMethod Method { get; set; } = ERequestMethod.GET;
        /// <summary>
        /// HTTP版本（默认Version10）
        /// </summary>
        public Version ProtocolVersion { get; set; } = HttpVersion.Version10;

        /// <summary>
        /// HTTP的Host请求头
        /// </summary>
        public string Host { get; set; } = "";
        /// <summary>
        /// HTTP的Accept请求头
        /// </summary>
        public string Accept { get; set; } = "";
        /// <summary>
        /// HTTP的Referer请求头
        /// </summary>
        public string Referer { get; set; } = "";
        /// <summary>
        /// HTTP的UserAgent请求头
        /// </summary>
        public string UserAgent { get; set; } = "";
        /// <summary>
        /// HTTP的ContentType请求头
        /// </summary>
        public string ContentType { get; set; } = "";
        /// <summary>
        /// HTTP的请求头
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Cookie字符串
        /// </summary>
        public string Cookie { get; set; } = "";
    }
}
