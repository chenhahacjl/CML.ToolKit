using System;
using System.Text.RegularExpressions;

namespace CML.CommonEx.RegexEx.ExFunction
{
    /// <summary>
    /// 正则表达式帮助类(扩展方法)
    /// </summary>
    public static class RegexOperate
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
            return CF_IsMatch(input, pattern, false);
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
            return new Regex(pattern, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None).IsMatch(input);
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
            return CF_IsInterger(input, ENumberVerifyType.Normal);
        }

        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="type">数值验证类型</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsInterger(this string input, ENumberVerifyType type)
        {
            bool result = false;

            switch (type)
            {
                case ENumberVerifyType.Normal:
                {
                    result = int.TryParse(input, out int _);
                    break;
                }
                case ENumberVerifyType.Nagtive:
                {
                    result = int.TryParse(input, out int num) && num < 0;
                    break;
                }
                case ENumberVerifyType.Positive:
                {
                    result = int.TryParse(input, out int num) && num > 0;
                    break;
                }
                case ENumberVerifyType.NotNagtive:
                {
                    result = int.TryParse(input, out int num) && num >= 0;
                    break;
                }
                case ENumberVerifyType.NotPositive:
                {
                    result = int.TryParse(input, out int num) && num <= 0;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 验证单精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsFloat(this string input)
        {
            return CF_IsFloat(input, ENumberVerifyType.Normal);
        }

        /// <summary>
        /// 验证单精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="type">数值验证类型</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsFloat(this string input, ENumberVerifyType type)
        {
            bool result = false;

            switch (type)
            {
                case ENumberVerifyType.Normal:
                {
                    result = float.TryParse(input, out float _);
                    break;
                }
                case ENumberVerifyType.Nagtive:
                {
                    result = float.TryParse(input, out float num) && num < 0;
                    break;
                }
                case ENumberVerifyType.Positive:
                {
                    result = float.TryParse(input, out float num) && num > 0;
                    break;
                }
                case ENumberVerifyType.NotNagtive:
                {
                    result = float.TryParse(input, out float num) && num >= 0;
                    break;
                }
                case ENumberVerifyType.NotPositive:
                {
                    result = float.TryParse(input, out float num) && num <= 0;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 验证双精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsDouble(this string input)
        {
            return CF_IsDouble(input, ENumberVerifyType.Normal);
        }

        /// <summary>
        /// 验证双精度浮点数
        /// </summary>
        /// <param name="input">待验证字符串</param>
        /// <param name="type">数值验证类型</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsDouble(this string input, ENumberVerifyType type)
        {
            bool result = false;

            switch (type)
            {
                case ENumberVerifyType.Normal:
                {
                    result = double.TryParse(input, out double _);
                    break;
                }
                case ENumberVerifyType.Nagtive:
                {
                    result = double.TryParse(input, out double num) && num < 0;
                    break;
                }
                case ENumberVerifyType.Positive:
                {
                    result = double.TryParse(input, out double num) && num > 0;
                    break;
                }
                case ENumberVerifyType.NotNagtive:
                {
                    result = double.TryParse(input, out double num) && num >= 0;
                    break;
                }
                case ENumberVerifyType.NotPositive:
                {
                    result = double.TryParse(input, out double num) && num <= 0;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsDateTime(this string input)
        {
            return DateTime.TryParse(input, out _);
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
            string pattern = @"^([\w-\.]+)@([\w-\.]+)(\.[a-zA-Z0-9]+)$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证固定电话号码
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsTelePhoneNumber(this string input)
        {
            string pattern = @"^(((0\d2|0\d{2})[- ]?)?\d{8}|((0\d3|0\d{3})[- ]?)?\d{7})(-\d{3})?$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsMobilePhoneNumber(this string input)
        {
            string pattern = @"^((\+)?86|((\+)?86)?)0?1[3458]\d{9}$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证电话号码 [可以是固定电话号码或手机号码]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsPhoneNumber(this string input)
        {
            string pattern = @"^((\+)?86|((\+)?86)?)0?1[3458]\d{9}$|^(((0\d2|0\d{2})[- ]?)?\d{8}|((0\d3|0\d{3})[- ]?)?\d{7})(-\d{3})?$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证邮政编码
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsZipCode(this string input)
        {
            string pattern = @"^\d{6}$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证IPv4地址 [第一位和最后一位数字不能是0或255，允许用0补位]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIPv4(this string input)
        {
            string pattern = @"^(25[0-4]|2[0-4]\d]|[01]?\d{2}|[1-9])\.(25[0-5]|2[0-4]\d]|[01]?\d?\d)\.(25[0-5]|2[0-4]\d]|[01]?\d?\d)\.(25[0-4]|2[0-4]\d]|[01]?\d{2}|[1-9])$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证IPv6地址
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIPv6(this string input)
        {
            string pattern = @"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIDCard(this string input)
        {
            if (input.Length == 18)
            {
                return CF_IsIDCard18(input);
            }
            else if (input.Length == 15)
            {
                return CF_IsIDCard15(input);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证一代身份证号 [15位数]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIDCard15(this string input)
        {
            //验证是否可以转换为15位整数
            if (!long.TryParse(input, out long num) || num.ToString().Length != 15)
            {
                return false;
            }

            //验证省份是否匹配
            string address = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91,";
            if (!address.Contains(input.Remove(2) + ","))
            {
                return false;
            }

            //验证生日是否匹配
            string birthdate = input.Substring(6, 6).Insert(4, "/").Insert(2, "/");
            return DateTime.TryParse(birthdate, out _);
        }

        /// <summary>
        /// 验证二代身份证号 [18位数，GB11643-1999标准]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsIDCard18(this string input)
        {
            //验证是否可以转换为正确的整数
            if (!long.TryParse(input.Remove(17), out long num) || num.ToString().Length != 17 || !long.TryParse(input.Replace('x', '0').Replace('X', '0'), out _))
            {
                return false;
            }

            //验证省份是否匹配
            string address = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91,";
            if (!address.Contains(input.Remove(2) + ","))
            {
                return false;
            }

            //验证生日是否匹配
            string birthdate = input.Substring(6, 8).Insert(6, "/").Insert(4, "/");
            if (!DateTime.TryParse(birthdate, out DateTime dt))
            {
                return false;
            }

            //校验码验证
            char[] Ai = input.Remove(17).ToCharArray();
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');

            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }

            Math.DivRem(sum, 11, out int y);
            return arrVarifyCode[y] == input.Substring(17, 1).ToLower();
        }

        /// <summary>
        /// 验证经度 [范围为-180～180，小数位数1-5位]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsLongitude(this string input)
        {
            string pattern = @"^[-\+]?((1[0-7]\d{1}|0?\d{1,2})\.\d{1,5}|180\.0{1,5})$";
            return CF_IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证纬度 [范围为-90～90，小数位数1-5位]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool CF_IsLatitude(this string input)
        {
            string pattern = @"^[-\+]?([0-8]?\d{1}\.\d{1,5}|90\.0{1,5})$";
            return CF_IsMatch(input, pattern);
        }
        #endregion
    }
}
