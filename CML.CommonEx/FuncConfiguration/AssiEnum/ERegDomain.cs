namespace CML.CommonEx.ConfigurationEx
{
    /// <summary>
    /// 注册表基项静态域
    /// </summary>
    public enum ERegDomain
    {
        /// <summary>
        /// 对应于HKEY_CLASSES_ROOT主键
        /// </summary>
        ClassesRoot,
        /// <summary>
        /// 对应于HKEY_CURRENT_USER主键
        /// </summary>
        CurrentUser,
        /// <summary>
        /// 对应于 HKEY_LOCAL_MACHINE主键
        /// </summary>
        LocalMachine,
        /// <summary>
        /// 对应于 HKEY_USER主键
        /// </summary>
        User,
        /// <summary>
        /// 对应于HEKY_CURRENT_CONFIG主键
        /// </summary>
        CurrentConfig,
        /// <summary>
        /// 对应于HKEY_PERFORMANCE_DATA主键
        /// </summary>
        PerformanceData
    }
}
