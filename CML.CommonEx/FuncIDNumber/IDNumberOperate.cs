using System;
using System.Linq;

namespace CML.CommonEx.IDNumberEx
{
    /// <summary>
    /// 身份证号操作类
    /// </summary>
    public class IDNumberOperate
    {
        /// <summary>
        /// 获取身份证号类型
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>身份证号类型</returns>
        public static EIDNumberType CF_GetIDNumberType(ModelIDNumber idNumber)
        {
            return idNumber.CF_GetIDNumberType();
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>性别</returns>
        public static EGender CF_GetGender(ModelIDNumber idNumber)
        {
            return idNumber.CF_GetGender();
        }

        /// <summary>
        /// 获取出生年月日
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>出生年月日</returns>
        public static DateTime CF_GetBirthday(ModelIDNumber idNumber)
        {
            DateTime birthday = new DateTime(0001, 1, 1);

            if (idNumber.CF_GetIDNumberType() == EIDNumberType.Digit15)
            {
                int year = Convert.ToInt32(idNumber.IDNumber.Substring(6, 2));
                int month = Convert.ToInt32(idNumber.IDNumber.Substring(8, 2));
                int day = Convert.ToInt32(idNumber.IDNumber.Substring(10, 2));

                year = Convert.ToInt32(year < 20 ? "20" + year : "19" + year);

                birthday = new DateTime(year, month, day);
            }
            else if (idNumber.CF_GetIDNumberType() == EIDNumberType.Digit18)
            {
                int year = Convert.ToInt32(idNumber.IDNumber.Substring(6, 4));
                int month = Convert.ToInt32(idNumber.IDNumber.Substring(10, 2));
                int day = Convert.ToInt32(idNumber.IDNumber.Substring(12, 2));

                birthday = new DateTime(year, month, day);
            }

            return birthday;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>验证码（0-9|X|E-Error）</returns>
        public static char CF_GetSpecialCode(ModelIDNumber idNumber)
        {
            string strIDNumber = "";

            if (idNumber.CF_GetIDNumberType(false) == EIDNumberType.Digit15)
            {
                int year = Convert.ToInt32(idNumber.IDNumber.Substring(6, 2));
                year = Convert.ToInt32(year < 20 ? "20" + year : "19" + year);

                strIDNumber = idNumber.IDNumber.Substring(0, 6) + year + idNumber.IDNumber.Substring(8, 7);
            }
            else if (idNumber.CF_GetIDNumberType(false) == EIDNumberType.Digit18)
            {
                strIDNumber = idNumber.IDNumber.Substring(0, 17);
            }

            char number = 'E';
            if (!string.IsNullOrEmpty(strIDNumber))
            {
                string[] arrVarifyCode = ("1,0,X,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = strIDNumber.ToCharArray();

                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }

                number = arrVarifyCode[sum % 11][0];
            }

            return number;
        }

        /// <summary>
        /// 获取发卡机构
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>发卡单位</returns>
        public static string CF_GetCardIssuer(ModelIDNumber idNumber)
        {
            string cardIssuer = "该区行政区编号未收录！";

            ModelAddress address = CF_GetAddressInfo(idNumber);
            if (address != null)
            {
                cardIssuer = CF_GetAddressInfo(idNumber).CardIssuer;
            }

            return cardIssuer;
        }

        /// <summary>
        /// 获取籍贯
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>籍贯</returns>
        public static string CF_GetDomicile(ModelIDNumber idNumber)
        {
            string domicile = "该区行政区编号未收录！";

            ModelAddress address = CF_GetAddressInfo(idNumber);
            if (address != null)
            {
                domicile = CF_GetAddressInfo(idNumber).Domicile;
            }

            return domicile;
        }

        /// <summary>
        /// 获取地址信息
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>发卡单位及籍贯</returns>
        public static ModelAddress CF_GetAddressInfo(ModelIDNumber idNumber)
        {
            ModelAddress address = new ModelAddress();

            if (idNumber.IsLawful)
            {
                address = AddressOperate.GetAllAddressModel().Where(item => item.AddressNumber == idNumber.IDNumber.Substring(0, 6)).FirstOrDefault();
            }

            return address;
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>年龄（-1为身份证号错误）</returns>
        public static int CF_GetAge(ModelIDNumber idNumber)
        {
            int age = -1;

            if (idNumber.IsLawful)
            {
                DateTime birthday = CF_GetBirthday(idNumber);

                if (birthday <= DateTime.Now)
                {
                    int start = Convert.ToInt32(birthday.ToString("yyyyMMdd"));
                    int end = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));

                    age = Convert.ToInt32((end - start).ToString("00000000").Substring(0, 4));
                }
            }

            return age;
        }

        /// <summary>
        /// 获取生肖
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>生肖</returns>
        public static string CF_GetZodiac(ModelIDNumber idNumber)
        {
            string zodiac = "身份证号错误";

            if (idNumber.IsLawful)
            {
                string[] zodiacs = new string[] { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

                zodiac = zodiacs[(CF_GetBirthday(idNumber).Year - 1900) % 12];
            }

            return zodiac;
        }

        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="idNumber">身份证号模型</param>
        /// <returns>星座</returns>
        public static string CF_GetConstellation(ModelIDNumber idNumber)
        {
            string constellation = "身份证号错误";

            if (idNumber.IsLawful)
            {
                if (CF_GetBirthday(idNumber).Month == 1)
                {
                    constellation = CF_GetBirthday(idNumber).Day <= 21 ? "摩羯座" : "水瓶座";
                }
                else if (CF_GetBirthday(idNumber).Month == 2)
                {
                    return CF_GetBirthday(idNumber).Day <= 19 ? "水瓶座" : "双鱼座";
                }
                else if (CF_GetBirthday(idNumber).Month == 3)
                {
                    return CF_GetBirthday(idNumber).Day <= 20 ? "双鱼座" : "白羊座";
                }
                else if (CF_GetBirthday(idNumber).Month == 4)
                {
                    return CF_GetBirthday(idNumber).Day <= 20 ? "白羊座" : "金牛座";
                }
                else if (CF_GetBirthday(idNumber).Month == 5)
                {
                    return CF_GetBirthday(idNumber).Day <= 21 ? "金牛座" : "双子座";
                }
                else if (CF_GetBirthday(idNumber).Month == 6)
                {
                    return CF_GetBirthday(idNumber).Day <= 21 ? "双子座" : "巨蟹座";
                }
                else if (CF_GetBirthday(idNumber).Month == 7)
                {
                    return CF_GetBirthday(idNumber).Day <= 22 ? "巨蟹座" : "狮子座";
                }
                else if (CF_GetBirthday(idNumber).Month == 8)
                {
                    return CF_GetBirthday(idNumber).Day <= 22 ? "狮子座" : "处女座";
                }
                else if (CF_GetBirthday(idNumber).Month == 9)
                {
                    return CF_GetBirthday(idNumber).Day <= 23 ? "处女座" : "天平座";
                }
                else if (CF_GetBirthday(idNumber).Month == 10)
                {
                    return CF_GetBirthday(idNumber).Day <= 22 ? "天平座" : "天蝎座";
                }
                else if (CF_GetBirthday(idNumber).Month == 11)
                {
                    return CF_GetBirthday(idNumber).Day <= 21 ? "天蝎座" : "射手座";
                }
                else
                {
                    return CF_GetBirthday(idNumber).Day <= 20 ? "射手座" : "魔羯座";
                }
            }

            return constellation;
        }
    }
}
