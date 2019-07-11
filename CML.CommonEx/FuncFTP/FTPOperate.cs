using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace CML.CommonEx.FTPEx
{
    /// <summary>
    /// FTP操作类
    /// </summary>
    public class FTPOperate
    {
        /// <summary>
        /// 检测能否连接服务器
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（根目录）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static bool CF_CanConnect(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            bool result = false;

            try
            {
                ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.ListDirectory;
                FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);

                if (ftpWebResponse != null)
                {
                    ftpWebResponse.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（检测文件路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistFile(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            EExistence result;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                string filename = ftpInfomation.GetCurrentName();
                ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetParentPath();

                result = CF_IsExistFile(ftpInfomation, filename, out errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = EExistence.Error;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 检测文件夹是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（检测文件夹路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistDirectory(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            EExistence result;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                string dirname = ftpInfomation.GetCurrentName();
                ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetParentPath();

                result = CF_IsExistDirectory(ftpInfomation, dirname, out errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = EExistence.Error;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 检测文件（夹）是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="tarName">检测文件（夹）名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExist(ModelFtpInfomation ftpInfomation, string tarName, out string errMsg)
        {
            EExistence result = EExistence.Error;

            try
            {
                List<ModelFileInfo> files = CF_GetFileAndDirectory(ftpInfomation, out errMsg);

                if (files != null)
                {
                    if (files.Find(item => item.Name == tarName) != null)
                    {
                        result = EExistence.Exist;
                    }
                    else
                    {
                        result = EExistence.NotExist;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = EExistence.Error;
            }

            return result;
        }

        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="fileName">检测文件名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistFile(ModelFtpInfomation ftpInfomation, string fileName, out string errMsg)
        {
            EExistence result = EExistence.Error;

            try
            {
                List<ModelFileInfo> files = CF_GetFile(ftpInfomation, out errMsg);

                if (files != null)
                {
                    if (files.Find(item => item.Name == fileName) != null)
                    {
                        result = EExistence.Exist;
                    }
                    else
                    {
                        result = EExistence.NotExist;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = EExistence.Error;
            }

            return result;
        }

        /// <summary>
        /// 检测文件夹是否存在
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="dirName">检测文件夹名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检测情况</returns>
        public static EExistence CF_IsExistDirectory(ModelFtpInfomation ftpInfomation, string dirName, out string errMsg)
        {
            EExistence result = EExistence.Error;

            try
            {
                List<ModelFileInfo> files = CF_GetDirectory(ftpInfomation, out errMsg);

                if (files != null)
                {
                    if (files.Find(item => item.Name == dirName) != null)
                    {
                        result = EExistence.Exist;
                    }
                    else
                    {
                        result = EExistence.NotExist;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = EExistence.Error;
            }

            return result;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（目标文件路径）</param>
        /// <param name="fileName">FTP文件名</param>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="overwrite">是否覆盖文件</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        public static bool CF_UploadFile(ModelFtpInfomation ftpInfomation, string fileName, string filePath, bool overwrite, out string errMsg)
        {
            bool result = false;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                //判断原始文件是否存在
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)
                {
                    errMsg = "本地文件不存在！";
                    result = false;
                }
                else
                {
                    EExistence isExistDir = CF_IsExistDirectory(ftpInfomation, out errMsg);
                    if (isExistDir == EExistence.Exist || string.IsNullOrEmpty(ftpInfomation.GetCurrentName()))
                    {
                        //判断目标文件是否存在
                        EExistence isExistFile = CF_IsExistFile(ftpInfomation, fileName, out errMsg);
                        if (isExistFile == EExistence.Exist)
                        {
                            //是否覆盖
                            if (!overwrite)
                            {
                                errMsg = "目标文件已存在！";
                                result = false;
                            }
                            else
                            {
                                isExistFile = EExistence.NotExist;
                            }
                        }

                        if (isExistFile == EExistence.NotExist)
                        {
                            ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.UploadFile;
                            ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(fileName);
                            FtpWebRequest ftpWebRequest = CF_GetFtpRequest(ftpInfomation, out errMsg);

                            if (ftpWebRequest != null)
                            {
                                using (Stream stream = ftpWebRequest.GetRequestStream())
                                {
                                    using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                                    {
                                        fileStream.CopyTo(stream);
                                    }
                                }
                            }

                            result = true;
                        }
                    }
                    else
                    {
                        errMsg = "目标文件夹不存在！";
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="fileName">FTP文件名</param>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="overwrite">是否覆盖文件</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        public static bool CF_DownloadFile(ModelFtpInfomation ftpInfomation, string fileName, string filePath, bool overwrite, out string errMsg)
        {
            bool result = false;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                //判断FTP文件是否存在
                EExistence isExistFile = CF_IsExistFile(ftpInfomation, fileName, out errMsg);
                if (isExistFile == EExistence.NotExist)
                {
                    errMsg = "FTP文件不存在！";
                    result = false;
                }
                else if (isExistFile == EExistence.Exist)
                {
                    FileInfo file = new FileInfo(filePath);

                    //创建本地文件夹
                    if (!file.Directory.Exists) { file.Directory.Create(); }

                    bool isExist = file.Exists;

                    if (isExist)
                    {
                        //是否覆盖
                        if (!overwrite)
                        {
                            errMsg = "目标文件已存在！";
                            result = false;
                        }
                        else
                        {
                            isExist = false;
                        }
                    }

                    if (!isExist)
                    {
                        ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.DownloadFile;
                        ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(fileName);
                        FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);

                        if (ftpWebResponse != null)
                        {
                            using (Stream stream = ftpWebResponse.GetResponseStream())
                            {
                                using (FileStream fileStream = new FileStream(file.FullName, FileMode.Create, FileAccess.Write))
                                {
                                    stream.CopyTo(fileStream);
                                }
                            }
                        }

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>删除情况</returns>
        public static bool CF_DeleteFile(ModelFtpInfomation ftpInfomation, string fileName, out string errMsg)
        {
            bool result = false;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                EExistence isExist = CF_IsExistFile(ftpInfomation, fileName, out errMsg);

                if (isExist == EExistence.NotExist)
                {
                    errMsg = "目标文件不存在！";
                    result = false;
                }
                else if (isExist == EExistence.Exist)
                {
                    ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.DeleteFile;
                    ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(fileName);

                    FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);
                    if (ftpWebResponse != null)
                    {
                        ftpWebResponse.Close();

                        errMsg = "";
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="dirName">文件夹名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>删除情况</returns>
        public static bool CF_DeleteDirectory(ModelFtpInfomation ftpInfomation, string dirName, out string errMsg)
        {
            bool result = false;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                EExistence isExist = CF_IsExistDirectory(ftpInfomation, dirName, out errMsg);

                if (isExist == EExistence.NotExist)
                {
                    errMsg = "目标文件夹不存在！";
                    result = false;
                }
                else if (isExist == EExistence.Exist)
                {
                    ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.RemoveDirectory;
                    ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(dirName);

                    FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);
                    if (ftpWebResponse != null)
                    {
                        ftpWebResponse.Close();

                        errMsg = "";
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="orgFileName">原始文件名</param>
        /// <param name="tarFileName">目标文件名</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>重命名情况</returns>
        public static bool CF_RenameFile(ModelFtpInfomation ftpInfomation, string orgFileName, string tarFileName, out string errMsg)
        {
            bool result = false;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                EExistence isOrgExist = CF_IsExistFile(ftpInfomation, orgFileName, out errMsg);

                if (isOrgExist == EExistence.NotExist)
                {
                    errMsg = "待重命名文件不存在！";
                    result = false;
                }
                else if (isOrgExist == EExistence.Exist)
                {
                    EExistence isTarExist = CF_IsExist(ftpInfomation, tarFileName, out errMsg);
                    if (isTarExist == EExistence.Exist)
                    {
                        errMsg = "已存在同名文件（夹）！";
                        result = false;
                    }
                    else if (isTarExist == EExistence.NotExist)
                    {
                        ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.Rename;
                        ftpInfomation.FtpReqInfo.RenameTo = ftpInfomation.GetCombinePath(tarFileName);
                        ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(orgFileName);

                        FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);
                        if (ftpWebResponse != null)
                        {
                            FtpStatusCode statusCode;
                            using (ftpWebResponse.GetResponseStream())
                            {
                                statusCode = ftpWebResponse.StatusCode;
                            }

                            if (statusCode == FtpStatusCode.CommandOK || statusCode == FtpStatusCode.FileActionOK)
                            {
                                errMsg = "";
                                result = true;
                            }
                            else
                            {
                                errMsg = "文件重命名错误！";
                                result = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="orgDirName">原始文件夹名</param>
        /// <param name="tarDirName">目标文件夹名</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>重命名情况</returns>
        public static bool CF_RenameDirectory(ModelFtpInfomation ftpInfomation, string orgDirName, string tarDirName, out string errMsg)
        {
            bool result = false;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                EExistence isOrgExist = CF_IsExistDirectory(ftpInfomation, orgDirName, out errMsg);

                if (isOrgExist == EExistence.NotExist)
                {
                    errMsg = "待重命名文件夹不存在！";
                    result = false;
                }
                else if (isOrgExist == EExistence.Exist)
                {
                    EExistence isTarExist = CF_IsExist(ftpInfomation, tarDirName, out errMsg);
                    if (isTarExist == EExistence.Exist)
                    {
                        errMsg = "已存在同名文件（夹）！";
                        result = false;
                    }
                    else if (isTarExist == EExistence.NotExist)
                    {
                        ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.Rename;
                        ftpInfomation.FtpReqInfo.RenameTo = ftpInfomation.GetCombinePath(tarDirName);
                        ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(orgDirName);

                        FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);
                        if (ftpWebResponse != null)
                        {
                            FtpStatusCode statusCode;
                            using (ftpWebResponse.GetResponseStream())
                            {
                                statusCode = ftpWebResponse.StatusCode;
                            }

                            if (statusCode == FtpStatusCode.CommandOK || statusCode == FtpStatusCode.FileActionOK)
                            {
                                errMsg = "";
                                result = true;
                            }
                            else
                            {
                                errMsg = "文件夹重命名错误！";
                                result = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="dirName">文件夹名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns></returns>
        public static bool CF_MakeDirectory(ModelFtpInfomation ftpInfomation, string dirName, out string errMsg)
        {
            bool result = false;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                EExistence isTarExist = CF_IsExist(ftpInfomation, dirName, out errMsg);
                if (isTarExist == EExistence.Exist)
                {
                    errMsg = "已存在同名文件（夹）！";
                    result = false;
                }
                else if (isTarExist == EExistence.NotExist)
                {
                    ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.MakeDirectory;
                    ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(dirName);

                    FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);
                    if (ftpWebResponse != null)
                    {
                        ftpWebResponse.Close();

                        errMsg = "";
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（父文件夹路径）</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件大小</returns>
        public static long CF_GetFileSize(ModelFtpInfomation ftpInfomation, string fileName, out string errMsg)
        {
            long result = -1;
            ftpInfomation.FtpReqInfo.CF_PushRequestPath();

            try
            {
                EExistence isTarExist = CF_IsExistFile(ftpInfomation, fileName, out errMsg);
                if (isTarExist == EExistence.NotExist)
                {
                    errMsg = "目标文件不存在！";
                    result = -1;
                }
                else if (isTarExist == EExistence.Exist)
                {
                    ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.GetFileSize;
                    ftpInfomation.FtpReqInfo.RequestPath = ftpInfomation.GetCombinePath(fileName);

                    FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);
                    if (ftpWebResponse != null)
                    {
                        result = ftpWebResponse.ContentLength;
                        ftpWebResponse.Close();

                        errMsg = "";
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = -1;
            }

            ftpInfomation.FtpReqInfo.CF_PopRequestPath();
            return result;
        }

        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件夹列表</returns>
        public static List<ModelFileInfo> CF_GetDirectory(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            List<ModelFileInfo> result = null;
            try
            {
                List<ModelFileInfo> fileList = CF_GetFileAndDirectory(ftpInfomation, out errMsg);

                if (fileList != null)
                {
                    result = fileList.Where(item => item.IsDirectory).ToList();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（文件夹路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件列表</returns>
        public static List<ModelFileInfo> CF_GetFile(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            List<ModelFileInfo> result = null;
            try
            {
                List<ModelFileInfo> fileList = CF_GetFileAndDirectory(ftpInfomation, out errMsg);

                if (fileList != null)
                {
                    result = fileList.Where(item => !item.IsDirectory).ToList();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 获取文件和文件夹列表
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型（文件夹路径）</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>文件和文件夹列表</returns>
        public static List<ModelFileInfo> CF_GetFileAndDirectory(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            List<ModelFileInfo> result = null;

            try
            {
                ftpInfomation.FtpReqInfo.Method = EFtpRequestMethod.ListDirectoryDetails;
                FtpWebResponse ftpWebResponse = CF_GetFtpResponse(ftpInfomation, out errMsg);

                if (ftpWebResponse != null)
                {
                    List<string> fileLines = new List<string>();

                    using (Stream stream = ftpWebResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            while (!sr.EndOfStream)
                            {
                                fileLines.Add(sr.ReadLine());
                            }
                        }
                    }

                    ftpWebResponse.Close();

                    result = new List<ModelFileInfo>();
                    foreach (string fileLine in fileLines)
                    {
                        //分离项目
                        string[] arrItem = fileLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        //日期
                        string[] arrData = arrItem[0].Split('-');
                        string strDate = "20" + arrData[2] + "/" + arrData[0] + "/" + arrData[1];

                        //时间
                        string[] arrTime = arrItem[1].Split(':');
                        //转换为24小时制
                        int hour = arrTime[1].Contains("AM") ? Convert.ToInt32(arrTime[0]) : Convert.ToInt32(arrTime[0] == "12" ? "00" : arrTime[0]) + 12;
                        string strTime = $"{hour}:{ arrTime[1].Substring(0, 2)}:00";

                        //文件（夹）名称
                        string tmpStr = fileLine.Substring(fileLine.IndexOf(arrItem[1]) + arrItem[1].Length);
                        tmpStr = tmpStr.Substring(tmpStr.IndexOf(arrItem[2]) + arrItem[2].Length);
                        string name = tmpStr.Substring(tmpStr.IndexOf(arrItem[3]));

                        result.Add(new ModelFileInfo(
                            fileLine.IndexOf("<DIR>") > 0 ? true : false,
                            name,
                            fileLine.IndexOf("<DIR>") > 0 ? 0 : Convert.ToInt64(arrItem[2]),
                            Convert.ToDateTime(strDate + " " + strTime),
                            $"{ftpInfomation.FtpReqInfo.RequestPath.Replace("\\", "/").Trim('/')}/{name}"
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 获取FTP请求响应
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>FTP请求响应</returns>
        public static FtpWebResponse CF_GetFtpResponse(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            FtpWebResponse result = null;

            try
            {
                //基本信息
                FtpWebRequest ftpWebRequest = CF_GetFtpRequest(ftpInfomation, out errMsg);

                if (ftpWebRequest != null)
                {
                    FtpWebResponse response = ftpWebRequest.GetResponse() as FtpWebResponse;

                    errMsg = "";
                    result = response;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 获取FTP请求
        /// </summary>
        /// <param name="ftpInfomation">FTP基本信息模型</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>FTP请求</returns>
        public static FtpWebRequest CF_GetFtpRequest(ModelFtpInfomation ftpInfomation, out string errMsg)
        {
            FtpWebRequest result;
            try
            {
                //基本信息
                FtpWebRequest ftpWebRequest = WebRequest.Create(ftpInfomation.GetUrl()) as FtpWebRequest;
                ftpWebRequest.Credentials = new NetworkCredential(ftpInfomation.Username, ftpInfomation.Password);
                ftpWebRequest.Method = ftpInfomation.FtpReqInfo.Method.GetProtocol();

                //扩展信息
                ftpWebRequest.Timeout = ftpInfomation.FtpExtendInfo.Timeout;
                ftpWebRequest.ReadWriteTimeout = ftpInfomation.FtpExtendInfo.ReadWriteTimeout;

                ftpWebRequest.EnableSsl = ftpInfomation.FtpExtendInfo.EnableSsl;
                ftpWebRequest.UseBinary = ftpInfomation.FtpExtendInfo.UseBinary;
                ftpWebRequest.UsePassive = ftpInfomation.FtpExtendInfo.UsePassive;
                ftpWebRequest.KeepAlive = ftpInfomation.FtpExtendInfo.KeepAlive;

                if (ftpInfomation.FtpReqInfo.Method == EFtpRequestMethod.Rename)
                {
                    ftpWebRequest.RenameTo = ftpInfomation.FtpReqInfo.RenameTo;
                }

                if (ftpInfomation.FtpExtendInfo.Proxy.Enable)
                {
                    ftpWebRequest.Proxy = new WebProxy(ftpInfomation.FtpExtendInfo.Proxy.Host, ftpInfomation.FtpExtendInfo.Proxy.Port);
                }

                errMsg = "";
                result = ftpWebRequest;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = null;
            }

            return result;
        }
    }
}
