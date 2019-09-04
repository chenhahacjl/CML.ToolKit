namespace CML.CommonEx.ResultEx
{
    /// <summary>
    /// 操作结果基类
    /// </summary>
    public class TResultBase
    {
        /// <summary>
        /// 是否调用成功
        /// </summary>
        public bool IsSuccess => ErrorCode == 0;

        /// <summary>
        /// 错误代码(0代表调用成功)
        /// </summary>
        public int ErrorCode { get; } = 0;

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMessage { get; } = string.Empty;

        /// <summary>
        /// 构造操作成功结果
        /// </summary>
        public TResultBase()
        {

        }

        /// <summary>
        /// 构造操作结果
        /// </summary>
        /// <param name="errCode">错误代码</param>
        /// <param name="errMsg">错误描述</param>
        public TResultBase(int errCode, string errMsg)
        {
            ErrorCode = errCode;
            ErrorMessage = errMsg;
        }
    }
}
