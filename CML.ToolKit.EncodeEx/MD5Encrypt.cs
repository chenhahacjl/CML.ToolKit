﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace CML.ToolKit.EncodeEx
{
    /// <summary>
    /// MD5加密操作类
    /// </summary>
    public class MD5Encrypt
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="isUpper">大写输出</param>
        /// <returns>16位MD5值</returns>
        public static string MD5Encrypt16(string input, bool isUpper = true)
        {
            string strMD5;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                strMD5 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(input)), 4, 8);
            }

            if (isUpper)
            {
                return strMD5.Replace("-", "").ToUpper();
            }
            else
            {
                return strMD5.Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="isUpper">大写输出</param>
        /// <returns>32位MD5值</returns>
        public static string MD5Encrypt32(string input, bool isUpper = true)
        {
            string strMD5 = "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] byteMD5 = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                for (int i = 0; i < byteMD5.Length; i++)
                {
                    strMD5 += byteMD5[i].ToString("X");
                }
            }

            if (isUpper)
            {
                return strMD5.Replace("-", "").ToUpper();
            }
            else
            {
                return strMD5.Replace("-", "").ToLower();
            }
        }
    }
}