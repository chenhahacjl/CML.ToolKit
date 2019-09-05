using System;
using System.IO;
using System.Security.Cryptography;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// AES加密解密操作类
    /// </summary>
    public class AESEncrypt
    {
        /// <summary>
        /// AES加密文件
        /// </summary>
        /// <param name="aesPara">AES加密参数</param>
        /// <param name="inFilePath">待加密文件路径</param>
        /// <param name="outFilePath">[OUT]已加密文件存储路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptFile(ModAESParameter aesPara, string inFilePath, string outFilePath, out string errMsg)
        {
            bool result;

            try
            {
                byte[] bts = File.ReadAllBytes(inFilePath);
                if (CF_EncryptBytes(aesPara, bts, out byte[] outBytes, out errMsg))
                {
                    File.WriteAllBytes(outFilePath, outBytes);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// AES解密文件
        /// </summary>
        /// <param name="aesPara">AES解密参数</param>
        /// <param name="inFilePath">待解密文件路径</param>
        /// <param name="outFilePath">[OUT]已解密文件存储路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptFile(ModAESParameter aesPara, string inFilePath, string outFilePath, out string errMsg)
        {
            bool result;

            try
            {
                byte[] bts = File.ReadAllBytes(inFilePath);
                if (CF_DecryptBytes(aesPara, bts, out byte[] outBytes, out errMsg))
                {
                    File.WriteAllBytes(outFilePath, outBytes);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// AES加密字符串
        /// </summary>
        /// <param name="aesPara">AES加密参数</param>
        /// <param name="inString">待加密字符串</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptString(ModAESParameter aesPara, string inString, out string outString, out string errMsg)
        {
            bool result;

            try
            {
                byte[] bts = aesPara.Encode.GetBytes(inString);
                if (CF_EncryptBytes(aesPara, bts, out byte[] outBytes, out errMsg))
                {
                    outString = Convert.ToBase64String(outBytes);
                    result = true;
                }
                else
                {
                    outString = "";
                    result = false;
                }
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
        /// AES解密字符串
        /// </summary>
        /// <param name="aesPara">AES解密参数</param>
        /// <param name="inString">待解密字符串</param>
        /// <param name="outString">[OUT]已解密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptString(ModAESParameter aesPara, string inString, out string outString, out string errMsg)
        {
            bool result;

            try
            {
                byte[] bts = Convert.FromBase64String(inString);
                if (CF_DecryptBytes(aesPara, bts, out byte[] outBytes, out errMsg))
                {
                    outString = aesPara.Encode.GetString(outBytes);
                    result = true;
                }
                else
                {
                    outString = "";
                    result = false;
                }
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
        /// AES加密字节数组
        /// </summary>
        /// <param name="aesPara">AES加密参数</param>
        /// <param name="inBytes">待加密字节数组</param>
        /// <param name="outBytes">[OUT]已加密字节数组</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptBytes(ModAESParameter aesPara, byte[] inBytes, out byte[] outBytes, out string errMsg)
        {
            bool result;

            try
            {
                using (RijndaelManaged aes = new RijndaelManaged
                {
                    Mode = aesPara.CipherMode.Convert(),
                    Padding = aesPara.PaddingMode.Convert(),
                    KeySize = aesPara.KeySize,
                    BlockSize = aesPara.BlockSize,
                    FeedbackSize = aesPara.FeedbackSize
                })
                {
                    aes.Key = aesPara.Encode.GetBytes(aesPara.Key);
                    aes.IV = aesPara.Encode.GetBytes(aesPara.IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(inBytes, 0, inBytes.Length);
                            cryptoStream.FlushFinalBlock();
                        }

                        outBytes = ms.ToArray();
                    }
                }

                errMsg = "";
                result = true;
            }
            catch (Exception ex)
            {
                outBytes = null;
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// AES解密字节数组
        /// </summary>
        /// <param name="aesPara">AES解密参数</param>
        /// <param name="inBytes">待解密字节数组</param>
        /// <param name="outBytes">[OUT]已解密字节数组</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptBytes(ModAESParameter aesPara, byte[] inBytes, out byte[] outBytes, out string errMsg)
        {
            bool result;

            try
            {
                using (RijndaelManaged aes = new RijndaelManaged
                {
                    Mode = aesPara.CipherMode.Convert(),
                    Padding = aesPara.PaddingMode.Convert(),
                    KeySize = aesPara.KeySize,
                    BlockSize = aesPara.BlockSize,
                    FeedbackSize = aesPara.FeedbackSize,
                })
                {
                    aes.Key = aesPara.Encode.GetBytes(aesPara.Key);
                    aes.IV = aesPara.Encode.GetBytes(aesPara.IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(inBytes, 0, inBytes.Length);
                            cryptoStream.FlushFinalBlock();
                        }

                        outBytes = ms.ToArray();
                    }
                }

                errMsg = "";
                result = true;
            }
            catch (Exception ex)
            {
                outBytes = null;
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }
    }
}
