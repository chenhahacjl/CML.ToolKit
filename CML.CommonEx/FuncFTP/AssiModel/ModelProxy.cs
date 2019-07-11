namespace CML.CommonEx.FTPEx
{
    /// <summary>
    /// 代理模型
    /// </summary>
    public class ModelProxy
    {
        /// <summary>
        /// 是否启用代理
        /// </summary>
        public bool Enable { get; set; } = false;

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 默认构造函数（不启用代理）
        /// </summary>
        public ModelProxy()
        {
            Enable = false;
        }

        /// <summary>
        /// 构造函数（启用代理）
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public ModelProxy(string host, int port)
        {
            Host = host;
            Port = port;
            Enable = true;
        }
    }
}
