using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CML.CommonEx.EncodeEx.ExFunction
{
    /// <summary>
    /// DESEncrypt加密解密操作类(扩展方法)
    /// </summary>
    public static class DESEncrypt
    {
        #region 公共方法
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="inputValue">待加密字符串</param>
        /// <param name="key">密钥（8位）</param>
        /// <param name="iv">向量（8位）</param>
        /// <returns>已加密字符串（错误时返回 ERROR:{ERROR MESSAGE}）</returns>
        public static string CF_Encrypt(this string inputValue, string key, string iv)
        {
            if (string.IsNullOrEmpty(key) || key.Length != 8 ||
                string.IsNullOrEmpty(iv) || iv.Length != 8)
            {
                return "ERROR:输入参数错误！";
            }

            try
            {
                using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
                {
                    using (ICryptoTransform ct = sa.CreateEncryptor())
                    {
                        byte[] bt = Encoding.UTF8.GetBytes(inputValue);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                            {
                                cs.Write(bt, 0, bt.Length);
                                cs.FlushFinalBlock();
                            }

                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"ERROR:{ex.Message}";
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="inputValue">待解密字符串</param>
        /// <param name="key">密钥（8位）</param>
        /// <param name="iv">向量（8位）</param>
        /// <returns>已解密字符串（错误时为空）</returns>
        public static string CF_Decrypt(this string inputValue, string key, string iv)
        {
            if (string.IsNullOrEmpty(key) || key.Length != 8 ||
                string.IsNullOrEmpty(iv) || iv.Length != 8)
            {
                return "ERROR:输入参数错误！";
            }

            try
            {
                using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
                {
                    using (ICryptoTransform ct = sa.CreateDecryptor())
                    {
                        byte[] bt = Convert.FromBase64String(inputValue);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                            {
                                cs.Write(bt, 0, bt.Length);
                                cs.FlushFinalBlock();
                            }

                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"ERROR:{ex.Message}";
            }
        }
        #endregion
    }
}
