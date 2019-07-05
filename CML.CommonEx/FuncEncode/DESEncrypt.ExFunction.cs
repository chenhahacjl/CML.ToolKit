namespace CML.CommonEx.EncodeEx.ExFunction
{
    /// <summary>
    /// DESEncrypt加密解密操作类(扩展方法)
    /// </summary>
    public static class DESEncryptEF
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
            return DESEncrypt.CF_Encrypt(inputValue, key, iv);
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
            return DESEncrypt.CF_Decrypt(inputValue, key, iv);
        }
        #endregion
    }
}
