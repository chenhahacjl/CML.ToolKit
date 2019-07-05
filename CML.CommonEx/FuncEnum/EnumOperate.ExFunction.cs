using System;

namespace CML.CommonEx.EnumEx.ExFunction
{
    /// <summary>
    /// 枚举操作类(扩展方法)
    /// </summary>
    public static class EnumOperateEF
    {
        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string CF_GetDescription(this Enum en)
        {
            return EnumOperate.CF_GetDescription(en);
        }

        /// <summary>
        /// 枚举转数值
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static int CF_ToNumber(this Enum en)
        {
            return EnumOperate.CF_ToNumber(en);
        }

        /// <summary>
        /// 枚举转字符串
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string CF_ToString(this Enum en)
        {
            return EnumOperate.CF_ToString(en);
        }
    }
}
