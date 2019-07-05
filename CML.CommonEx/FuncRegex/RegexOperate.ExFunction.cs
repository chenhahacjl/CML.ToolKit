namespace CML.CommonEx.RegexEx.ExFunction
{
    /// <summary>
    /// 正则表达式帮助类(扩展方法)
    /// </summary>
    public static class RegexOperateEF
    {
        #region 匹配方法
        /// <summary>
        /// 验证字符串是否匹配正则表达式描述的规则
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsMatch(this string input, string pattern)
        {
            return RegexOperate.CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证字符串是否匹配正则表达式描述的规则
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsMatch(this string input, string pattern, bool ignoreCase)
        {
            return RegexOperate.CF_IsMatch(input, pattern, ignoreCase);
        }
        #endregion

        #region 类型验证
        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsInterger(this string input)
        {
            return RegexOperate.CF_IsInterger(input);
        }

        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="type">数值验证类型</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsInterger(this string input, ENumberVerifyType type)
        {
            return RegexOperate.CF_IsInterger(input, type);
        }

        /// <summary>
        /// 验证单精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsFloat(this string input)
        {
            return RegexOperate.CF_IsFloat(input);
        }

        /// <summary>
        /// 验证单精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="type">数值验证类型</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsFloat(this string input, ENumberVerifyType type)
        {
            return RegexOperate.CF_IsFloat(input, type);
        }

        /// <summary>
        /// 验证双精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsDouble(this string input)
        {
            return RegexOperate.CF_IsDouble(input);
        }

        /// <summary>
        /// 验证双精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="type">数值验证类型</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsDouble(this string input, ENumberVerifyType type)
        {
            return RegexOperate.CF_IsDouble(input, type);
        }

        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsDateTime(this string input)
        {
            return RegexOperate.CF_IsDateTime(input);
        }
        #endregion

        #region 常用验证
        /// <summary>
        /// 验证Email字符串
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsEmail(this string input)
        {
            return RegexOperate.CF_IsEmail(input);
        }

        /// <summary>
        /// 验证固定电话号码
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsTelePhoneNumber(this string input)
        {
            return RegexOperate.CF_IsTelePhoneNumber(input);
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsMobilePhoneNumber(this string input)
        {
            return RegexOperate.CF_IsMobilePhoneNumber(input);
        }

        /// <summary>
        /// 验证电话号码 [可以是固定电话号码或手机号码]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsPhoneNumber(this string input)
        {
            return RegexOperate.CF_IsPhoneNumber(input);
        }

        /// <summary>
        /// 验证邮政编码
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsZipCode(this string input)
        {
            return RegexOperate.CF_IsZipCode(input);
        }

        /// <summary>
        /// 验证IPv4地址 [第一位和最后一位数字不能是0或255，允许用0补位]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIPv4(this string input)
        {
            return RegexOperate.CF_IsIPv4(input);
        }

        /// <summary>
        /// 验证IPv6地址
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIPv6(this string input)
        {
            return RegexOperate.CF_IsIPv6(input);
        }

        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIDCard(this string input)
        {
            return RegexOperate.CF_IsIDCard(input);
        }

        /// <summary>
        /// 验证一代身份证号 [15位数]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIDCard15(this string input)
        {
            return RegexOperate.CF_IsIDCard15(input);
        }

        /// <summary>
        /// 验证二代身份证号 [18位数，GB11643-1999标准]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIDCard18(this string input)
        {
            return RegexOperate.CF_IsIDCard18(input);
        }

        /// <summary>
        /// 验证经度 [范围为-180～180，小数位数1-5位]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsLongitude(this string input)
        {
            return RegexOperate.CF_IsLongitude(input);
        }

        /// <summary>
        /// 验证纬度 [范围为-90～90，小数位数1-5位]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsLatitude(this string input)
        {
            return RegexOperate.CF_IsLatitude(input);
        }
        #endregion
    }
}
