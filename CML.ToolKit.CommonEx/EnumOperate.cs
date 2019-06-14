using System;
using System.ComponentModel;
using System.Reflection;

namespace CML.ToolKit.CommonEx
{
    /// <summary>
    /// 枚举操作类
    /// </summary>
    public class EnumOperate
    {
        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string GetDescription(Enum en)
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
    }
}
