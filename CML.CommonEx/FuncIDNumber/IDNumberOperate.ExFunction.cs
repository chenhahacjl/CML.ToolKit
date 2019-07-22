using System;
using System.Linq;

namespace CML.CommonEx.IDNumberEx.ExFunction
{
    /// <summary>
    /// 身份证号操作类
    /// </summary>
    public static class IDNumberOperateEF
    {
        /// <summary>
        /// 获取身份证号类型
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>身份证号类型</returns>
        public static EIDNumberType CF_GetIDNumberType(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetIDNumberType(idNumber);
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>性别</returns>
        public static EGender CF_GetGender(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetGender(idNumber);
        }

        /// <summary>
        /// 获取出生年月日
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>出生年月日</returns>
        public static DateTime CF_GetBirthday(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetBirthday(idNumber);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>验证码（0-9|X|E-Error）</returns>
        public static char CF_GetSpecialCode(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetSpecialCode(idNumber);
        }

        /// <summary>
        /// 获取发卡机构
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>发卡单位</returns>
        public static string CF_GetCardIssuer(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetCardIssuer(idNumber);
        }

        /// <summary>
        /// 获取籍贯
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>籍贯</returns>
        public static string CF_GetDomicile(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetDomicile(idNumber);
        }

        /// <summary>
        /// 获取地址信息
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>发卡单位及籍贯</returns>
        public static ModAddress CF_GetAddressInfo(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetAddressInfo(idNumber);
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>年龄（-1为身份证号错误）</returns>
        public static int CF_GetAge(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetAge(idNumber);
        }

        /// <summary>
        /// 获取生肖
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>生肖</returns>
        public static string CF_GetZodiac(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetZodiac(idNumber);
        }

        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>星座</returns>
        public static string CF_GetConstellation(this ModIDNumber idNumber)
        {
            return IDNumberOperate.CF_GetConstellation(idNumber);
        }
    }
}
