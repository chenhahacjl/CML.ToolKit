using CML.CommonEx.EnumEx.ExFunction;
using System.ComponentModel;

namespace CML.CommonEx.FTPEx
{
    /// <summary>
    /// FTP请求方式
    /// </summary>
    public enum EFtpRequestMethod
    {
        /// <summary>
        /// 将文件追加到现有文件
        /// </summary>
        [Description("APPE")]
        AppendFile,
        /// <summary>
        /// 删除文件
        /// </summary>
        [Description("DELE")]
        DeleteFile,
        /// <summary>
        /// 下载文件
        /// </summary>
        [Description("RETR")]
        DownloadFile,
        /// <summary>
        /// 获取文件时间戳
        /// </summary>
        [Description("MDTM")]
        GetDateTimestamp,
        /// <summary>
        /// 获得文件大小
        /// </summary>
        [Description("SIZE")]
        GetFileSize,
        /// <summary>
        /// 枚举目录文件简要信息
        /// </summary>
        [Description("NLST")]
        ListDirectory,
        /// <summary>
        /// 枚举目录文件详细信息
        /// </summary>
        [Description("LIST")]
        ListDirectoryDetails,
        /// <summary>
        /// 创建文件夹
        /// </summary>
        [Description("MKD")]
        MakeDirectory,
        /// <summary>
        /// 打印当前工作目录名称
        /// </summary>
        [Description("PWD")]
        PrintWorkingDirectory,
        /// <summary>
        /// 删除文件夹
        /// </summary>
        [Description("RMD")]
        RemoveDirectory,
        /// <summary>
        /// 重命名目录
        /// </summary>
        [Description("RENAME")]
        Rename,
        /// <summary>
        /// 上传文件
        /// </summary>
        [Description("STOR")]
        UploadFile,
        /// <summary>
        /// 上传唯一名称的文件
        /// </summary>
        [Description("STOU")]
        UploadFileWithUniqueName,
    }

    /// <summary>
    /// FTP请求方式扩展方法
    /// </summary>
    internal static class EFtpRequestMethodExFunction
    {
        /// <summary>
        /// 取得协议字符串
        /// </summary>
        /// <param name="ftpRequestMethod"></param>
        /// <returns>协议字符串</returns>
        public static string GetProtocol(this EFtpRequestMethod ftpRequestMethod)
        {
            return ftpRequestMethod.CF_GetDescription();
        }
    }
}
