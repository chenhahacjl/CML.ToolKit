using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CML.CommonEx.EnumEx
{
    /// <summary>
    /// 枚举操作类
    /// </summary>
    public static class EnumOperate
    {
        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string CF_GetDescription(this Enum en)
        {
            //获取成员
            MemberInfo[] memberInfos = en.GetType().GetMember(en.ToString());

            if (memberInfos != null && memberInfos.Length > 0)
            {
                //获取描述特性
                if (memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attribute && attribute.Length > 0)
                {
                    //返回当前描述
                    return attribute[0].Description;
                }
            }

            return en.ToString();
        }

        /// <summary>
        /// 枚举转数值
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static int CF_ToNumber(this Enum en)
        {
            return Convert.ToInt32(en);
        }

        /// <summary>
        /// 枚举转字符串
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string CF_ToString(this Enum en)
        {
            return Convert.ToString(en);
        }
    }
}
