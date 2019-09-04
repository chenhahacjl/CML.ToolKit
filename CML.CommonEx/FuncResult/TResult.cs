using CML.CommonEx.EnumEx.ExFunction;
using System;

namespace CML.CommonEx.ResultEx
{
    /// <summary>
    /// 泛型操作结果类
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    public class TResult<T> : TResultBase
    {
        /// <summary>
        /// 泛型结果数据
        /// </summary>
        public T Result { get; } = default;

        /// <summary>
        /// 获得异常信息字符串
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <returns></returns>
        private static string GetExceptionString(Exception exception)
        {
            return
                $"*************************异常详细信息*************************\r\n" +
                $"【发生时间】 {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\r\n" +
                $"【异常类型】 {exception.GetType().Name}\r\n" +
                $"【异常方法】 {exception.TargetSite}\r\n" +
                $"【异常信息】 {exception.Message}\r\n" +
                $"【堆栈调用】 {exception.StackTrace}\r\n" +
                $"**************************************************************";
        }

        /// <summary>
        /// 构造成功操作结果
        /// </summary>
        /// <param name="result">结果数据</param>
        public TResult(T result) : base()
        {
            Result = result;
        }

        /// <summary>
        /// 构造失败操作结果（错误代码为-1）
        /// </summary>
        /// <param name="result">结果数据</param>
        /// <param name="errMsg">错误描述</param>
        public TResult(T result, string errMsg) : base(-1, errMsg.Trim())
        {
            Result = result;
        }

        /// <summary>
        /// 构造失败操作结果（错误代码为-1）
        /// </summary>
        /// <param name="result">结果数据</param>
        /// <param name="exception">异常信息对象</param>
        public TResult(T result, Exception exception) : base(-1, GetExceptionString(exception))
        {
            Result = result;
        }

        /// <summary>
        /// 构造操作结果
        /// </summary>
        /// <param name="result">结果数据</param>
        /// <param name="errCode">错误代码(0代表调用成功)</param>
        /// <param name="errMsg">错误描述</param>
        public TResult(T result, int errCode, string errMsg) : base(errCode, errMsg?.Trim())
        {
            Result = result;
        }

        /// <summary>
        /// 构造操作结果
        /// </summary>
        /// <param name="result">结果数据</param>
        /// <param name="errEnum">描述枚举（取数值与描述）</param>
        public TResult(T result, Enum errEnum) : base(errEnum.CF_ToNumber(), errEnum.CF_GetDescription())
        {
            Result = result;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[{ErrorCode}]{ErrorMessage}";

        /// <summary>
        /// 重载!操作符
        /// </summary>
        /// <param name="result">操作结果</param>
        /// <returns></returns>
        public static bool operator !(TResult<T> result)
        {
            return !result.IsSuccess;
        }
    }
}
