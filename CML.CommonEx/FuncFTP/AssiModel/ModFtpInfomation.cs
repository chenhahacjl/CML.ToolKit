using System;

namespace CML.CommonEx.FTPEx
{
    /// <summary>
    /// FTP信息模型
    /// </summary>
    public class ModFtpInfomation
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string IP { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = "";

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port { get; set; } = 21;

        /// <summary>
        /// FTP请求信息
        /// </summary>
        public ModFtpReqInfo FtpReqInfo { get; set; } = new ModFtpReqInfo();

        /// <summary>
        /// FTP扩展信息
        /// </summary>
        public ModFtpExtendInfo FtpExtendInfo { get; set; } = new ModFtpExtendInfo();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">服务器IP</param>
        public ModFtpInfomation(string ip)
        {
            IP = ip;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">服务器IP</param>
        /// <param name="port">服务端口</param>
        public ModFtpInfomation(string ip, int port)
        {
            IP = ip;
            Port = port;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">服务器IP</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public ModFtpInfomation(string ip, string username, string password)
        {
            IP = ip;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">服务器IP</param>
        /// <param name="port">服务端口</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public ModFtpInfomation(string ip, int port, string username, string password)
        {
            IP = ip;
            Port = port;
            Username = username;
            Password = password;
        }
    }

    /// <summary>
    /// FTP基本信息扩展方法
    /// </summary>
    internal static class ModelFtpBaseInfoExFunction
    {
        /// <summary>
        /// 获取当前文件（夹）名称
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息</param>
        /// <returns>URL地址</returns>
        public static string GetCurrentName(this ModFtpInfomation ftpInfomation)
        {
            //目录结构格式化
            string directory = ftpInfomation.FtpReqInfo.RequestPath.Replace("\\", "/").Trim('/');

            if (directory.Contains("/"))
            {
                return directory.Substring(directory.LastIndexOf("/") + 1);
            }
            else
            {
                return directory;
            }
        }

        /// <summary>
        /// 获取父路径（/{RequestPath}/../）
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息</param>
        /// <returns>URL地址</returns>
        public static string GetParentPath(this ModFtpInfomation ftpInfomation)
        {
            //目录结构格式化
            string directory = ftpInfomation.FtpReqInfo.RequestPath.Replace("\\", "/").Trim('/');

            if (directory.Contains("/"))
            {
                return "/" + directory.Substring(0, directory.LastIndexOf("/"));
            }
            else
            {
                return "/";
            }
        }

        /// <summary>
        /// 获取合并地址（/{RequestPath}/{path}）
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息</param>
        /// <param name="path">子路径</param>
        /// <returns>URL地址</returns>
        public static string GetCombinePath(this ModFtpInfomation ftpInfomation, string path = "")
        {
            //目录结构格式化
            string directory = ftpInfomation.FtpReqInfo.RequestPath.Replace("\\", "/").Trim('/');

            return $"/{directory}/{path}";
        }

        /// <summary>
        /// 获取URL地址（ftp://{IP}:{Port}/{RequestPath}）
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息</param>
        /// <returns>URL地址</returns>
        public static Uri GetUrl(this ModFtpInfomation ftpInfomation)
        {
            //目录结构格式化
            string directory = ftpInfomation.FtpReqInfo.RequestPath.Replace("\\", "/").Trim('/');

            return new Uri($"ftp://{ftpInfomation.IP}:{ftpInfomation.Port}/{directory}");
        }
    }
}
