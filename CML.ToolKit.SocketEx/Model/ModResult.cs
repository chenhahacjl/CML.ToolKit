using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 结果模型
    /// </summary>
    /// <typeparam name="T">结果内容数据类型</typeparam>
    internal class ModResult<T>
    {
        /// <summary>
        /// 结果状态
        /// </summary>
        public bool IsSuccess { get; } = true;

        /// <summary>
        /// 结果内容
        /// </summary>
        public T Result { get; } = default;

        /// <summary>
        /// 构造成功结果对象（默认结果内容）
        /// </summary>
        public ModResult()
        {
            IsSuccess = true;
            Result = default;
        }

        /// <summary>
        /// 构造结果对象
        /// </summary>
        /// <param name="isSuccess">结果状态</param>
        /// <param name="result">结果内容</param>
        public ModResult(bool isSuccess, T result)
        {
            IsSuccess = isSuccess;
            Result = result;
        }
    }
}
