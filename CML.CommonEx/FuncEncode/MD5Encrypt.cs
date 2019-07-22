using System;
using System.IO;
using System.Security.Cryptography;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// MD5加密操作类
    /// </summary>
    public class MD5Encrypt
    {
        /// <summary>
        /// DES加密文件
        /// </summary>
        /// <param name="md5Para">MD5加密参数</param>
        /// <param name="inFilePath">待加密文件路径</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptFile(ModMD5Parameter md5Para, string inFilePath, out string outString, out string errMsg)
        {
            bool result;

            try
            {
                byte[] bts = File.ReadAllBytes(inFilePath);

                result = CF_EncryptBytes(md5Para, bts, out outString, out errMsg);
            }
            catch (Exception ex)
            {
                outString = "";
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="md5Para">MD5加密参数</param>
        /// <param name="inString">待加密字符串</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptString(ModMD5Parameter md5Para, string inString, out string outString, out string errMsg)
        {
            bool result;

            try
            {
                byte[] bts = md5Para.Encode.GetBytes(inString);

                result = CF_EncryptBytes(md5Para, bts, out outString, out errMsg);
            }
            catch (Exception ex)
            {
                outString = "";
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// MD5加密字节数组
        /// </summary>
        /// <param name="md5Para">MD5加密参数</param>
        /// <param name="inBytes">待加密字节数组</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptBytes(ModMD5Parameter md5Para, byte[] inBytes, out string outString, out string errMsg)
        {
            bool result;

            try
            {
                byte[] outBytes;
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    outBytes = md5.ComputeHash(inBytes);
                }

                string strMD5 = "";
                if (md5Para.MD5Length == EMD5Length.L16)
                {
                    strMD5 = BitConverter.ToString(outBytes, 4, 8);
                }
                else
                {
                    for (int i = 0; i < outBytes.Length; i++)
                    {
                        strMD5 += outBytes[i].ToString("X2");
                    }
                }

                if (md5Para.IsUppercase)
                {
                    outString = strMD5.Replace("-", "").ToUpper();
                }
                else
                {
                    outString = strMD5.Replace("-", "").ToLower();
                }

                result = true;

                errMsg = "";
                result = true;
            }
            catch (Exception ex)
            {
                outString = "";
                errMsg = ex.Message;
                result = true;
            }

            return result;
        }
    }
}
