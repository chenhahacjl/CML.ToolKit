using System.Security.Cryptography;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// 加密模式枚举
    /// </summary>
    public enum ECipherMode
    {
        /// <summary>
        /// 密码块链模式
        /// </summary>
        CBC = 1,
        /// <summary>
        /// 电子密码本模式
        /// </summary>
        ECB = 2,
        /// <summary>
        /// 输出反馈模式
        /// </summary>
        OFB = 3,
        /// <summary>
        /// 密码反馈模式
        /// </summary>
        CFB = 4,
        /// <summary>
        /// 密码文本窃用模式
        /// </summary>
        CTS = 5
    }

    /// <summary>
    /// 加密模式枚举扩展方法
    /// </summary>
    internal static class ECipherModeExFunction
    {
        /// <summary>
        /// 转换类型
        /// </summary>
        /// <returns></returns>
        public static CipherMode Convert(this ECipherMode cipherMode)
        {
            return (CipherMode)cipherMode;
        }
    }
}
