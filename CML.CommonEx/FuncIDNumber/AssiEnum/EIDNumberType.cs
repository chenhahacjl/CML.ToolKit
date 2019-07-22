using System.ComponentModel;

namespace CML.CommonEx.IDNumberEx
{
    /// <summary>
    /// 身份证号类型
    /// </summary>
    public enum EIDNumberType
    {
        /// <summary>
        /// 身份证号错误
        /// </summary>
        [Description("身份证号错误")]
        Error = -1,
        /// <summary>
        /// 15位身份证号
        /// </summary>
        [Description("15位身份证号")]
        Digit15 = 15,
        /// <summary>
        /// 18位身份证号
        /// </summary>
        [Description("18位身份证号")]
        Digit18 = 18
    }

    /// <summary>
    /// 身份证号类型枚举扩展方法
    /// </summary>
    internal static class EIDNumberTypeExFunction
    {
        /// <summary>
        /// 获取身份证号类型
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <param name="isVerifyCheckCode">是否验证校检码（18位身份证号特有）</param>
        /// <returns>身份证号类型</returns>
        public static EIDNumberType CF_GetIDNumberType(this ModIDNumber idNumber, bool isVerifyCheckCode = true)
        {
            return !RegexEx.RegexOperate.CF_IsIDCard(idNumber.IDNumber, isVerifyCheckCode) ?
                EIDNumberType.Error : idNumber.IDNumber.Length == 15 ?
                EIDNumberType.Digit15 : EIDNumberType.Digit18;
        }
    }
}
