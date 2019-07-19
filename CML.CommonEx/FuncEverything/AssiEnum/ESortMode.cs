namespace CML.CommonEx.EverythingEx
{
    /// <summary>
    /// 排序方式枚举
    /// </summary>
    public enum ESortMode : uint
    {
        /// <summary>
        /// 名称升序排列
        /// </summary>
        NAME_ASCENDING = 1,
        /// <summary>
        /// 名称降序排列
        /// </summary>
        NAME_DESCENDING = 2,
        /// <summary>
        /// 路径升序排列
        /// </summary>
        PATH_ASCENDING = 3,
        /// <summary>
        /// 路径降序排列
        /// </summary>
        PATH_DESCENDING = 4,
        /// <summary>
        /// 文件大小升序排列
        /// </summary>
        SIZE_ASCENDING = 5,
        /// <summary>
        /// 文件大小降序排列
        /// </summary>
        SIZE_DESCENDING = 6,
        /// <summary>
        /// 后缀名升序排列
        /// </summary>
        EXTENSION_ASCENDING = 7,
        /// <summary>
        /// 后缀名降序排列
        /// </summary>
        EXTENSION_DESCENDING = 8,
        /// <summary>
        /// 类型名称升序排列
        /// </summary>
        TYPE_NAME_ASCENDING = 9,
        /// <summary>
        /// 类型名称降序排列
        /// </summary>
        TYPE_NAME_DESCENDING = 10,
        /// <summary>
        /// 创建日期升序排列
        /// </summary>
        DATE_CREATED_ASCENDING = 11,
        /// <summary>
        /// 创建日期降序排列
        /// </summary>
        DATE_CREATED_DESCENDING = 12,
        /// <summary>
        /// 修改日期升序排列
        /// </summary>
        DATE_MODIFIED_ASCENDING = 13,
        /// <summary>
        /// 修改日期降序排列
        /// </summary>
        DATE_MODIFIED_DESCENDING = 14,
        /// <summary>
        /// 属性升序排列
        /// </summary>
        ATTRIBUTES_ASCENDING = 15,
        /// <summary>
        /// 属性降序排列
        /// </summary>
        ATTRIBUTES_DESCENDING = 16,
        /// <summary>
        /// 文件列表和文件名升序排列
        /// </summary>
        FILE_LIST_FILENAME_ASCENDING = 17,
        /// <summary>
        /// 文件列表和文件名降序排列
        /// </summary>
        FILE_LIST_FILENAME_DESCENDING = 18,
        /// <summary>
        /// 运行次数升序排列
        /// </summary>
        RUN_COUNT_ASCENDING = 19,
        /// <summary>
        /// 运行次数降序排列
        /// </summary>
        RUN_COUNT_DESCENDING = 20,
        /// <summary>
        /// 最近修改日期升序排列
        /// </summary>
        DATE_RECENTLY_CHANGED_ASCENDING = 21,
        /// <summary>
        /// 最近修改日期降序排列
        /// </summary>
        DATE_RECENTLY_CHANGED_DESCENDING = 22,
        /// <summary>
        /// 访问日期升序排列
        /// </summary>
        DATE_ACCESSED_ASCENDING = 23,
        /// <summary>
        /// 访问日期降序排列
        /// </summary>
        DATE_ACCESSED_DESCENDING = 24,
        /// <summary>
        /// 运行日期升序排列
        /// </summary>
        DATE_RUN_ASCENDING = 25,
        /// <summary>
        /// 运行日期降序排列
        /// </summary>
        DATE_RUN_DESCENDING = 26,
    }
}
