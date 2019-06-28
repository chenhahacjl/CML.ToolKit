namespace CML.CommonEx.UIAutomationEx
{
    /// <summary>
    /// 复选框状态
    /// </summary>
    public enum ECheckBoxStatus
    {
        /// <summary>
        /// 错误
        /// </summary>
        Error = -1,
        /// <summary>
        /// 已选中
        /// </summary>
        On = 0,
        /// <summary>
        /// 未选中
        /// </summary>
        Off = 1,
        /// <summary>
        /// 不确定
        /// </summary>
        Indeterminate = 2
    }

    /// <summary>
    /// 复选框状态枚举扩展方法
    /// </summary>
    public static class ECheckBoxStatusExFunction
    {
        /// <summary>
        /// 复选框状态是否获取错误
        /// </summary>
        /// <param name="checkBoxStatus">复选框状态</param>
        /// <returns>获取结果</returns>
        public static bool CF_IsError(this ECheckBoxStatus checkBoxStatus)
        {
            return checkBoxStatus == ECheckBoxStatus.Error;
        }
    }
}
