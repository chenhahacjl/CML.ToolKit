using CML.CommonEx.EncodeEx;
using System;
using System.IO;
using System.Management;

namespace CML.CommonEx.PermissionControlEx
{
    /// <summary>
    /// 磁盘权限控制
    /// </summary>
    public class DiskControl
    {
        /// <summary>
        /// 控制文件夹路径
        /// </summary>
        private readonly static string m_ControlFolderPath = "DiskControl";
        /// <summary>
        /// 控制文件路径
        /// </summary>
        private readonly static string m_ControlFilePath = "DiskControl\\VerificationContent";
        /// <summary>
        /// 控制文件路径
        /// </summary>
        private readonly static string m_VolumeLabelFilePath = "DiskControl\\VolumeLabelName";

        /// <summary>
        /// 检查是否设置磁盘权限
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <returns>检查结果</returns>
        public static bool CF_IsExistPermission(DriveInfo driveInfo)
        {
            //磁盘属性
            if (!driveInfo.IsReady || driveInfo.DriveType != DriveType.Removable)
            {
                return false;
            }

            //判断是否存在权限控制文件
            return File.Exists(Path.Combine(driveInfo.Name, m_ControlFilePath));
        }

        /// <summary>
        /// 设置磁盘权限
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <param name="diskPermission">磁盘权限</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>设置结果</returns>
        public static bool CF_SetPermission(DriveInfo driveInfo, ModDiskPermission diskPermission, out string errMsg)
        {
            if (!driveInfo.IsReady)
            {
                errMsg = "驱动器未准备好";
                return false;
            }

            if (driveInfo.DriveType != DriveType.Removable)
            {
                errMsg = $"不支持的驱动器类型: {driveInfo.DriveType.ToString()}";
                return false;
            }

            if (CF_IsExistPermission(driveInfo))
            {
                errMsg = "磁盘已设置权限";
                return false;
            }

            if (string.IsNullOrEmpty(diskPermission.VerificationContent))
            {
                errMsg = "验证内容为空";
                return false;
            }

            try
            {
                string content = diskPermission.VerificationContent;

                //序列号控制
                if (diskPermission.EnableSerialNumberControl)
                {
                    string serialNumber = GetSerialNumber(driveInfo);

                    if (string.IsNullOrEmpty(serialNumber))
                    {
                        errMsg = "磁盘序列号获取失败";
                        return false;
                    }

                    ModAESParameter modAESParameter = new ModAESParameter() { Key = serialNumber };
                    if (!AESEncrypt.CF_EncryptString(modAESParameter, content, out content, out errMsg))
                    {
                        errMsg = $"验证内容加密失败: {errMsg}";
                        return false;
                    }
                }

                //型号控制
                if (diskPermission.EnableModelControl)
                {
                    string model = GetModel(driveInfo);

                    if (string.IsNullOrEmpty(model))
                    {
                        errMsg = "磁盘型号获取失败";
                        return false;
                    }

                    ModAESParameter modAESParameter = new ModAESParameter() { Key = model };
                    if (!AESEncrypt.CF_EncryptString(modAESParameter, content, out content, out errMsg))
                    {
                        errMsg = $"验证内容加密失败: {errMsg}";
                        return false;
                    }
                }

                //创建文件夹
                Directory.CreateDirectory(Path.Combine(driveInfo.Name, m_ControlFolderPath));

                //卷标名控制
                if (diskPermission.EnableVolumeLabelControl)
                {
                    string volumeLabel = driveInfo.VolumeLabel;

                    if (!AESEncrypt.CF_EncryptString(new ModAESParameter(), volumeLabel, out volumeLabel, out errMsg))
                    {
                        errMsg = $"卷标名加密失败: {errMsg}";
                        return false;
                    }

                    FileInfo vlFile = new FileInfo(Path.Combine(driveInfo.Name, m_VolumeLabelFilePath));
                    if (vlFile.Exists)
                    {
                        vlFile.Attributes = FileAttributes.Normal;
                    }

                    File.WriteAllText(vlFile.FullName, volumeLabel);
                    vlFile.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System;

                    driveInfo.VolumeLabel = diskPermission.VolumeLabel;

                    if (driveInfo.VolumeLabel != diskPermission.VolumeLabel)
                    {
                        errMsg = "卷标名太长，或包含无效字符";
                        return false;
                    }
                }

                FileInfo cFile = new FileInfo(Path.Combine(driveInfo.Name, m_ControlFilePath));
                if (cFile.Exists)
                {
                    cFile.Attributes = FileAttributes.Normal;
                }

                File.WriteAllText(cFile.FullName, content);
                cFile.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System;

                DirectoryInfo cFolder = new DirectoryInfo(Path.Combine(driveInfo.Name, m_ControlFolderPath));
                cFolder.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            errMsg = "";
            return true;
        }

        /// <summary>
        /// 检查磁盘权限
        /// </summary>
        /// <param name="diskPermission">磁盘权限</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检查结果</returns>
        public static bool CF_CheckPermission(ModDiskPermission diskPermission, out string errMsg)
        {
            errMsg = "未授权";

            foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
            {
                //磁盘状态判断
                if (!driveInfo.IsReady || driveInfo.DriveType != DriveType.Removable) { continue; }

                //权限设置判断
                if (!CF_IsExistPermission(driveInfo)) { continue; }

                //卷标名判断
                if (diskPermission.EnableVolumeLabelControl && driveInfo.VolumeLabel != diskPermission.VolumeLabel) { continue; }

                //获取验证内容
                string content = File.ReadAllText(Path.Combine(driveInfo.Name, m_ControlFilePath));

                //型号控制
                if (diskPermission.EnableModelControl)
                {
                    string model = GetModel(driveInfo);

                    if (string.IsNullOrEmpty(model))
                    {
                        errMsg = "磁盘型号获取失败";
                        continue;
                    }

                    ModAESParameter modAESParameter = new ModAESParameter() { Key = model };
                    if (!AESEncrypt.CF_DecryptString(modAESParameter, content, out content, out errMsg))
                    {
                        errMsg = $"验证内容解密失败: {errMsg}";
                        continue;
                    }
                }

                //序列号控制
                if (diskPermission.EnableSerialNumberControl)
                {
                    string serialNumber = GetSerialNumber(driveInfo);

                    if (string.IsNullOrEmpty(serialNumber))
                    {
                        errMsg = "磁盘序列号获取失败";
                        continue;
                    }

                    ModAESParameter modAESParameter = new ModAESParameter() { Key = serialNumber };
                    if (!AESEncrypt.CF_DecryptString(modAESParameter, content, out content, out errMsg))
                    {
                        errMsg = $"验证内容加密失败: {errMsg}";
                        continue;
                    }
                }

                if (content == diskPermission.VerificationContent)
                {
                    errMsg = "";
                    break;
                }
            }

            return string.IsNullOrEmpty(errMsg);
        }

        /// <summary>
        /// 清除磁盘权限
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>清除结果</returns>
        public static bool CF_ClearPermission(DriveInfo driveInfo, out string errMsg)
        {
            if (!driveInfo.IsReady)
            {
                errMsg = "驱动器未准备好";
                return false;
            }

            if (driveInfo.DriveType != DriveType.Removable)
            {
                errMsg = $"不支持的驱动器类型: {driveInfo.DriveType.ToString()}";
                return false;
            }

            if (!CF_IsExistPermission(driveInfo))
            {
                errMsg = "磁盘未设置权限";
                return false;
            }

            try
            {
                FileInfo vlFile = new FileInfo(Path.Combine(driveInfo.Name, m_VolumeLabelFilePath));
                if (vlFile.Exists)
                {
                    string volumeLabel = File.ReadAllText(vlFile.FullName);

                    if (!AESEncrypt.CF_DecryptString(new ModAESParameter(), volumeLabel, out volumeLabel, out errMsg))
                    {
                        errMsg = $"卷标名加密失败: {errMsg}";
                        return false;
                    }

                    driveInfo.VolumeLabel = volumeLabel;
                    if (driveInfo.VolumeLabel != volumeLabel)
                    {
                        errMsg = "卷标名恢复失败";
                        return false;
                    }

                    vlFile.Attributes = FileAttributes.Normal;
                }

                FileInfo cFile = new FileInfo(Path.Combine(driveInfo.Name, m_ControlFilePath));
                if (cFile.Exists) { cFile.Attributes = FileAttributes.Normal; }

                DirectoryInfo cFolder = new DirectoryInfo(Path.Combine(driveInfo.Name, m_ControlFolderPath))
                {
                    Attributes = FileAttributes.Normal
                };
                cFolder.Delete(true);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            errMsg = "";
            return true;
        }

        /// <summary>
        /// 获取磁盘序列号
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <returns>磁盘序列号</returns>
        private static string GetSerialNumber(DriveInfo driveInfo)
        {
            using (ManagementClass DiskClass = new ManagementClass("Win32_Diskdrive"))
            {
                foreach (ManagementObject moDisk in DiskClass.GetInstances())
                {
                    foreach (ManagementObject moPartition in moDisk.GetRelated("Win32_DiskPartition"))
                    {
                        foreach (ManagementBaseObject mboLogical in moPartition.GetRelated("Win32_LogicalDisk"))
                        {
                            if (driveInfo.Name.TrimEnd('\\') == mboLogical["Name"] as string)
                            {
                                return moDisk.Properties["SerialNumber"].Value as string;
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取磁盘型号
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <returns>磁盘型号</returns>
        private static string GetModel(DriveInfo driveInfo)
        {
            using (ManagementClass DiskClass = new ManagementClass("Win32_Diskdrive"))
            {
                foreach (ManagementObject moDisk in DiskClass.GetInstances())
                {
                    foreach (ManagementObject moPartition in moDisk.GetRelated("Win32_DiskPartition"))
                    {
                        foreach (ManagementBaseObject mboLogical in moPartition.GetRelated("Win32_LogicalDisk"))
                        {
                            if (driveInfo.Name.TrimEnd('\\') == mboLogical["Name"] as string)
                            {
                                return moDisk.Properties["Model"].Value as string;
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}