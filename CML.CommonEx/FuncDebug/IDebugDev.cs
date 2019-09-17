namespace CML.CommonEx.DebugEx
{
    /// <summary>
    /// 调试模块开发接口
    /// </summary>
    public interface IDebugDev
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        string ModelName { get; }

        /// <summary>
        /// 模块描述
        /// </summary>
        string ModelDesc { get; }

        /// <summary>
        /// 模块识别码
        /// </summary>
        string ModelID { get; }

        /// <summary>
        /// 项目实例
        /// </summary>
        object ProjectObject { get; set; }

        /// <summary>
        /// 执行调试
        /// </summary>
        void ExecuteDebug();
    }
}
