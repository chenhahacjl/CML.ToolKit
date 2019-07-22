namespace CML.CommonEx.EncodeEx.ExFunction
{
    /// <summary>
    /// 3DES加密解密操作类(扩展方法)
    /// </summary>
    public static class DESTripleEncryptEF
    {
        /// <summary>
        /// 3DES加密文件
        /// </summary>
        /// <param name="desPara">3DES加密参数</param>
        /// <param name="inFilePath">待加密文件路径</param>
        /// <param name="outFilePath">[OUT]已加密文件存储路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptFile(this ModDESTripleParameter desPara, string inFilePath, string outFilePath, out string errMsg)
        {
            return DESTripleEncrypt.CF_EncryptFile(desPara, inFilePath, outFilePath, out errMsg);
        }

        /// <summary>
        /// 3DES解密文件
        /// </summary>
        /// <param name="desPara">3DES解密参数</param>
        /// <param name="inFilePath">待解密文件路径</param>
        /// <param name="outFilePath">[OUT]已解密文件存储路径</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptFile(this ModDESTripleParameter desPara, string inFilePath, string outFilePath, out string errMsg)
        {
            return DESTripleEncrypt.CF_DecryptFile(desPara, inFilePath, outFilePath, out errMsg);
        }

        /// <summary>
        /// 3DES加密字符串
        /// </summary>
        /// <param name="desPara">3DES加密参数</param>
        /// <param name="inString">待加密字符串</param>
        /// <param name="outString">[OUT]已加密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptString(this ModDESTripleParameter desPara, string inString, out string outString, out string errMsg)
        {
            return DESTripleEncrypt.CF_EncryptString(desPara, inString, out outString, out errMsg);
        }

        /// <summary>
        /// 3DES解密字符串
        /// </summary>
        /// <param name="desPara">3DES解密参数</param>
        /// <param name="inString">待解密字符串</param>
        /// <param name="outString">[OUT]已解密字符串</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptString(this ModDESTripleParameter desPara, string inString, out string outString, out string errMsg)
        {
            return DESTripleEncrypt.CF_DecryptString(desPara, inString, out outString, out errMsg);
        }

        /// <summary>
        /// 3DES加密字节数组
        /// </summary>
        /// <param name="desPara">3DES加密参数</param>
        /// <param name="inBytes">待加密字节数组</param>
        /// <param name="outBytes">[OUT]已加密字节数组</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_EncryptBytes(this ModDESTripleParameter desPara, byte[] inBytes, out byte[] outBytes, out string errMsg)
        {
            return DESTripleEncrypt.CF_EncryptBytes(desPara, inBytes, out outBytes, out errMsg);
        }

        /// <summary>
        /// 3DES解密字节数组
        /// </summary>
        /// <param name="desPara">3DES解密参数</param>
        /// <param name="inBytes">待解密字节数组</param>
        /// <param name="outBytes">[OUT]已解密字节数组</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_DecryptBytes(this ModDESTripleParameter desPara, byte[] inBytes, out byte[] outBytes, out string errMsg)
        {
            return DESTripleEncrypt.CF_DecryptBytes(desPara, inBytes, out outBytes, out errMsg);
        }
    }
}
