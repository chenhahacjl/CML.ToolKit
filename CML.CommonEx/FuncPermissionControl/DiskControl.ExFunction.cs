using System.IO;

namespace CML.CommonEx.PermissionControlEx.ExFunction
{
    /// <summary>
    /// 磁盘权限控制(扩展方法)
    /// </summary>
    public static class DiskControlEF
    {
        /// <summary>
        /// 检查是否设置磁盘权限
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <returns>检查结果</returns>
        public static bool CF_IsExistPermission(this DriveInfo driveInfo)
        {
            return DiskControl.CF_IsExistPermission(driveInfo);
        }

        /// <summary>
        /// 设置磁盘权限
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <param name="diskPermission">磁盘权限</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>设置结果</returns>
        public static bool CF_SetPermission(this DriveInfo driveInfo, ModDiskPermission diskPermission, out string errMsg)
        {
            return DiskControl.CF_SetPermission(driveInfo, diskPermission, out errMsg);
        }

        /// <summary>
        /// 检查磁盘权限
        /// </summary>
        /// <param name="diskPermission">磁盘权限</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>检查结果</returns>
        public static bool CF_CheckPermission(this ModDiskPermission diskPermission, out string errMsg)
        {
            return DiskControl.CF_CheckPermission(diskPermission, out errMsg);
        }

        /// <summary>
        /// 清除磁盘权限
        /// </summary>
        /// <param name="driveInfo">磁盘信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>清除结果</returns>
        public static bool CF_ClearPermission(this DriveInfo driveInfo, out string errMsg)
        {
            return DiskControl.CF_ClearPermission(driveInfo, out errMsg);
        }
    }
}