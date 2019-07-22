using System.IO;

namespace CML.CommonEx.EncodeEx.ExFunction
{
    /// <summary>
    /// MD5加密操作类(扩展方法)
    /// </summary>
    public static class MD5EncryptEF
    {
        /// <summary>
        /// DES加密文件
        /// </summary>
        /// <param name="md5Para">MD5加密参数</param>
        /// <param name="inFilePath">待加密文件路径</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptFile(this ModMD5Parameter md5Para, string inFilePath, out string outString, out string errMsg)
        {
            return MD5Encrypt.CF_EncryptFile(md5Para, inFilePath, out outString, out errMsg);
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="md5Para">MD5加密参数</param>
        /// <param name="inString">待加密字符串</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptString(this ModMD5Parameter md5Para, string inString, out string outString, out string errMsg)
        {
            return MD5Encrypt.CF_EncryptString(md5Para, inString, out outString, out errMsg);
        }

        /// <summary>
        /// MD5加密字节数组
        /// </summary>
        /// <param name="md5Para">MD5加密参数</param>
        /// <param name="inBytes">待加密字节数组</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptBytes(this ModMD5Parameter md5Para, byte[] inBytes, out string outString, out string errMsg)
        {
            return MD5Encrypt.CF_EncryptBytes(md5Para, inBytes, out outString, out errMsg);
        }
    }
}
