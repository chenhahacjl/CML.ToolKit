namespace CML.CommonEx.PermissionControlEx
{
    /// <summary>
    /// 磁盘权限模型
    /// </summary>
    public class ModDiskPermission
    {
        /// <summary>
        /// 验证内容
        /// </summary>
        public string VerificationContent { get; }

        /// <summary>
        /// 启用磁盘序列号控制
        /// </summary>
        public bool EnableSerialNumberControl { get; set; } = false;

        /// <summary>
        /// 启用磁盘型号控制
        /// </summary>
        public bool EnableModelControl { get; set; } = false;

        /// <summary>
        /// 启用卷标名控制
        /// </summary>
        public bool EnableVolumeLabelControl => !string.IsNullOrEmpty(VolumeLabel);

        /// <summary>
        /// 卷标名（留空为不控制）
        /// </summary>
        public string VolumeLabel { get; set; } = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="verificationContent">验证内容</param>
        public ModDiskPermission(string verificationContent)
        {
            VerificationContent = verificationContent;
        }
    }
}
