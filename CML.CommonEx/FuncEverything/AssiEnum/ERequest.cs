namespace CML.CommonEx.EverythingEx
{
    /// <summary>
    /// 请求内容枚举
    /// </summary>
    public enum ERequest
    {
        /// <summary>
        /// 文件名
        /// </summary>
        FILE_NAME = 0x00000001,
        /// <summary>
        /// 路径
        /// </summary>
        PATH = 0x00000002,
        /// <summary>
        /// 完整路径与文件名
        /// </summary>
        FULL_PATH_AND_FILE_NAME = 0x00000004,
        /// <summary>
        /// 扩展名
        /// </summary>
        EXTENSION = 0x00000008,
        /// <summary>
        /// 文件大小（文件夹为-1）
        /// </summary>
        SIZE = 0x00000010,
        /// <summary>
        /// 创建日期
        /// </summary>
        DATE_CREATED = 0x00000020,
        /// <summary>
        /// 修改日期
        /// </summary>
        DATE_MODIFIED = 0x00000040,
        /// <summary>
        /// 访问日期
        /// </summary>
        DATE_ACCESSED = 0x00000080,
        /// <summary>
        /// 属性
        /// </summary>
        ATTRIBUTES = 0x00000100,
        /// <summary>
        /// 完整路径和文件名
        /// </summary>
        FILE_LIST_FILE_NAME = 0x00000200,
        /// <summary>
        /// 运行次数
        /// </summary>
        RUN_COUNT = 0x00000400,
        /// <summary>
        /// 运行日期
        /// </summary>
        DATE_RUN = 0x00000800,
        /// <summary>
        /// 最近修改日期
        /// </summary>
        DATE_RECENTLY_CHANGED = 0x00001000,
        /// <summary>
        /// 高亮的文件名
        /// </summary>
        HIGHLIGHTED_FILE_NAME = 0x00002000,
        /// <summary>
        /// 高亮的路径
        /// </summary>
        HIGHLIGHTED_PATH = 0x00004000,
        /// <summary>
        /// 高亮的完整路径与文件名
        /// </summary>
        HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 0x00008000,
    }
}
