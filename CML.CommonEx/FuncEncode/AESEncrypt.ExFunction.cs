namespace CML.CommonEx.EncodeEx.ExFunction
{
    /// <summary>
    /// AES加密解密操作类(扩展方法)
    /// </summary>
    public static class AESEncryptEF
    {
        /// <summary>
        /// AES加密文件
        /// </summary>
        /// <param name="aesPara">AES加密参数</param>
        /// <param name="inFilePath">待加密文件路径</param>
        /// <param name="outFilePath">[OUT]已加密文件存储路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptFile(this ModAESParameter aesPara, string inFilePath, string outFilePath, out string errMsg)
        {
            return AESEncrypt.CF_EncryptFile(aesPara, inFilePath, outFilePath, out errMsg);
        }

        /// <summary>
        /// AES解密文件
        /// </summary>
        /// <param name="aesPara">AES解密参数</param>
        /// <param name="inFilePath">待解密文件路径</param>
        /// <param name="outFilePath">[OUT]已解密文件存储路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptFile(this ModAESParameter aesPara, string inFilePath, string outFilePath, out string errMsg)
        {
            return AESEncrypt.CF_DecryptFile(aesPara, inFilePath, outFilePath, out errMsg);
        }

        /// <summary>
        /// AES加密字符串
        /// </summary>
        /// <param name="aesPara">AES加密参数</param>
        /// <param name="inString">待加密字符串</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptString(this ModAESParameter aesPara, string inString, out string outString, out string errMsg)
        {
            return AESEncrypt.CF_EncryptString(aesPara, inString, out outString, out errMsg);
        }

        /// <summary>
        /// AES解密字符串
        /// </summary>
        /// <param name="aesPara">AES解密参数</param>
        /// <param name="inString">待解密字符串</param>
        /// <param name="outString">[OUT]已解密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptString(this ModAESParameter aesPara, string inString, out string outString, out string errMsg)
        {
            return AESEncrypt.CF_DecryptString(aesPara, inString, out outString, out errMsg);
        }

        /// <summary>
        /// AES加密字节数组
        /// </summary>
        /// <param name="aesPara">AES加密参数</param>
        /// <param name="inBytes">待加密字节数组</param>
        /// <param name="outBytes">[OUT]已加密字节数组</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptBytes(this ModAESParameter aesPara, byte[] inBytes, out byte[] outBytes, out string errMsg)
        {
            return AESEncrypt.CF_EncryptBytes(aesPara, inBytes, out outBytes, out errMsg);
        }

        /// <summary>
        /// AES解密字节数组
        /// </summary>
        /// <param name="aesPara">AES解密参数</param>
        /// <param name="inBytes">待解密字节数组</param>
        /// <param name="outBytes">[OUT]已解密字节数组</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptBytes(this ModAESParameter aesPara, byte[] inBytes, out byte[] outBytes, out string errMsg)
        {
            return AESEncrypt.CF_DecryptBytes(aesPara, inBytes, out outBytes, out errMsg);
        }
    }
}
