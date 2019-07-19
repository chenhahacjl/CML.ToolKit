namespace CML.CommonEx.EverythingEx
{
    /// <summary>
    /// 执行错误枚举
    /// </summary>
    public enum EExecuteError : uint
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        OK = 0,
        /// <summary>
        /// 内存分配错误
        /// </summary>
        MEMORY = 1,
        /// <summary>
        /// IPC不可用或Everything未启动
        /// </summary>
        IPC = 2,
        /// <summary>
        /// 注册搜索查询窗口类失败
        /// </summary>
        REGISTERCLASSEX = 3,
        /// <summary>
        /// 创建搜索查询窗口失败
        /// </summary>
        CREATEWINDOW = 4,
        /// <summary>
        /// 创建搜索查询线程失败
        /// </summary>
        CREATETHREAD = 5,
        /// <summary>
        /// 索引无效
        /// </summary>
        INVALIDINDEX = 6,
        /// <summary>
        /// 调用无效
        /// </summary>
        INVALIDCALL = 7,
    }
}
