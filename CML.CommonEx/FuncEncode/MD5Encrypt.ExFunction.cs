using System.IO;

namespace CML.CommonEx.EncodeEx.ExFunction
{
    /// <summary>
    /// MD5加密操作类(扩展方法)
    /// </summary>
    public static class MD5EncryptEF
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="isUpper">大写输出</param>
        /// <returns>16位MD5值</returns>
        public static string CF_MD5Encrypt16(this string input, bool isUpper = true)
        {
            return MD5Encrypt.CF_MD5Encrypt16(input, isUpper);
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="isUpper">大写输出</param>
        /// <returns>32位MD5值</returns>
        public static string CF_MD5Encrypt32(this string input, bool isUpper = true)
        {
            return MD5Encrypt.CF_MD5Encrypt32(input, isUpper);
        }

        /// <summary>
        /// 文件MD5加密
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="isUpper">大写输出</param>
        /// <returns>文件MD5值</returns>
        public static string CF_MD5EncryptFile(this FileInfo file, bool isUpper = true)
        {
            return MD5Encrypt.CF_MD5EncryptFile(file, isUpper);
        }

        /// <summary>
        /// 文件MD5加密
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isUpper">大写输出</param>
        /// <returns>文件MD5值</returns>
        public static string CF_MD5EncryptFile(this string filePath, bool isUpper = true)
        {
            return MD5Encrypt.CF_MD5EncryptFile(filePath, isUpper);
        }
    }
}
