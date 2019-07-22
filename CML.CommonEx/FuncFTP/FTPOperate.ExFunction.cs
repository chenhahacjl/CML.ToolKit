using System.Collections.Generic;
using System.Net;

namespace CML.CommonEx.FTPEx.ExFunction
{
    /// <summary>
    /// FTP操作类
    /// </summary>
    public static class FTPOperateEF
    {
        /// <summary>
        /// 检测能否连接服务器
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（根目录）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static bool CF_CanConnect(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_CanConnect(ftpInfomation, out errMsg);
        }

        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（检测文件路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistFile(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_IsExistFile(ftpInfomation, out errMsg);
        }

        /// <summary>
        /// 检测文件夹是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（检测文件夹路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistDirectory(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_IsExistDirectory(ftpInfomation, out errMsg);
        }

        /// <summary>
        /// 检测文件（夹）是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="tarName">检测文件（夹）名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExist(this ModFtpInfomation ftpInfomation, string tarName, out string errMsg)
        {
            return FTPOperate.CF_IsExist(ftpInfomation, tarName, out errMsg);
        }

        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="fileName">检测文件名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistFile(this ModFtpInfomation ftpInfomation, string fileName, out string errMsg)
        {
            return FTPOperate.CF_IsExistFile(ftpInfomation, fileName, out errMsg);
        }

        /// <summary>
        /// 检测文件夹是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="dirName">检测文件夹名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistDirectory(this ModFtpInfomation ftpInfomation, string dirName, out string errMsg)
        {
            return FTPOperate.CF_IsExistDirectory(ftpInfomation, dirName, out errMsg);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（目标文件路径）</param>
        /// <param name="fileName">FTP文件名</param>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="overwrite">是否覆盖文件</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        public static bool CF_UploadFile(this ModFtpInfomation ftpInfomation, string fileName, string filePath, bool overwrite, out string errMsg)
        {
            return FTPOperate.CF_UploadFile(ftpInfomation, fileName, filePath, overwrite, out errMsg);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="fileName">FTP文件名</param>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="overwrite">是否覆盖文件</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        public static bool CF_DownloadFile(this ModFtpInfomation ftpInfomation, string fileName, string filePath, bool overwrite, out string errMsg)
        {
            return FTPOperate.CF_DownloadFile(ftpInfomation, fileName, filePath, overwrite, out errMsg);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>删除情况</returns>
        public static bool CF_DeleteFile(this ModFtpInfomation ftpInfomation, string fileName, out string errMsg)
        {
            return FTPOperate.CF_DeleteFile(ftpInfomation, fileName, out errMsg);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="dirName">文件夹名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>删除情况</returns>
        public static bool CF_DeleteDirectory(this ModFtpInfomation ftpInfomation, string dirName, out string errMsg)
        {
            return FTPOperate.CF_DeleteDirectory(ftpInfomation, dirName, out errMsg);
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="orgFileName">原始文件名</param>
        /// <param name="tarFileName">目标文件名</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>重命名情况</returns>
        public static bool CF_RenameFile(this ModFtpInfomation ftpInfomation, string orgFileName, string tarFileName, out string errMsg)
        {
            return FTPOperate.CF_RenameFile(ftpInfomation, orgFileName, tarFileName, out errMsg);
        }

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="orgDirName">原始文件夹名</param>
        /// <param name="tarDirName">目标文件夹名</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>重命名情况</returns>
        public static bool CF_RenameDirectory(this ModFtpInfomation ftpInfomation, string orgDirName, string tarDirName, out string errMsg)
        {
            return FTPOperate.CF_RenameDirectory(ftpInfomation, orgDirName, tarDirName, out errMsg);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="dirName">文件夹名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns></returns>
        public static bool CF_MakeDirectory(this ModFtpInfomation ftpInfomation, string dirName, out string errMsg)
        {
            return FTPOperate.CF_MakeDirectory(ftpInfomation, dirName, out errMsg);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件大小</returns>
        public static long CF_GetFileSize(this ModFtpInfomation ftpInfomation, string fileName, out string errMsg)
        {
            return FTPOperate.CF_GetFileSize(ftpInfomation, fileName, out errMsg);
        }

        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件夹列表</returns>
        public static List<ModFileInfo> CF_GetDirectory(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_GetDirectory(ftpInfomation, out errMsg);
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（文件夹路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件列表</returns>
        public static List<ModFileInfo> CF_GetFile(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_GetFile(ftpInfomation, out errMsg);
        }

        /// <summary>
        /// 获取文件和文件夹列表
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（文件夹路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件和文件夹列表</returns>
        public static List<ModFileInfo> CF_GetFileAndDirectory(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_GetFileAndDirectory(ftpInfomation, out errMsg);
        }

        /// <summary>
        /// 获取FTP请求响应
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>FTP请求响应</returns>
        public static FtpWebResponse CF_GetFtpResponse(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_GetFtpResponse(ftpInfomation, out errMsg);
        }

        /// <summary>
        /// 获取FTP请求
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>FTP请求</returns>
        public static FtpWebRequest CF_GetFtpRequest(this ModFtpInfomation ftpInfomation, out string errMsg)
        {
            return FTPOperate.CF_GetFtpRequest(ftpInfomation, out errMsg);
        }
    }
}
