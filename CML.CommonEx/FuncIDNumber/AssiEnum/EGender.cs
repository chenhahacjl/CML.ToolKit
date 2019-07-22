using System;
using System.ComponentModel;

namespace CML.CommonEx.IDNumberEx
{
    /// <summary>
    /// 性别（正常情况）
    /// </summary>
    public enum EGender
    {
        /// <summary>
        /// 身份证号错误
        /// </summary>
        [Description("身份证号错误")]
        Error = -1,
        /// <summary>
        /// 男性
        /// </summary>
        [Description("男性")]
        Male,
        /// <summary>
        /// 女性
        /// </summary>
        [Description("女性")]
        Female
    }

    /// <summary>
    /// 性别枚举扩展方法
    /// </summary>
    internal static class EGenderExFunction
    {
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>性别</returns>
        public static EGender CF_GetGender(this ModIDNumber idNumber)
        {
            EGender gender = EGender.Error;

            if (idNumber.CF_GetIDNumberType() == EIDNumberType.Digit15)
            {
                gender = Convert.ToInt32(idNumber.IDNumber.Substring(14, 1)) % 2 == 1 ? EGender.Male : EGender.Female;
            }
            else if (idNumber.CF_GetIDNumberType() == EIDNumberType.Digit18)
            {
                gender = Convert.ToInt32(idNumber.IDNumber.Substring(16, 1)) % 2 == 1 ? EGender.Male : EGender.Female;
            }

            return gender;
        }
    }
}
