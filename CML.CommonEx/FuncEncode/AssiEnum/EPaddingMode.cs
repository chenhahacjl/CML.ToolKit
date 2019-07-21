using System.Security.Cryptography;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// 填充模式枚举
    /// </summary>
    public enum EPaddingMode
    {
        /// <summary>
        /// 不填充
        /// </summary>
        None = 1,
        /// <summary>
        /// PKCS #7 填充字符串由一个字节序列组成，每个字节填充该字节序列的长度。
        /// </summary>
        PKCS7 = 2,
        /// <summary>
        /// 填充字符串由设置为零的字节组成。
        /// </summary>
        Zeros = 3,
        /// <summary>
        /// ANSIX923 填充字符串由一个字节序列组成，此字节序列的最后一个字节填充字节序列的长度，其余字节均填充数字零。
        /// </summary>
        ANSIX923 = 4,
        /// <summary>
        /// ISO10126 填充字符串由一个字节序列组成，此字节序列的最后一个字节填充字节序列的长度，其余字节填充随机数据。
        /// </summary>
        ISO10126 = 5,
    }

    /// <summary>
    /// 填充模式枚举扩展方法
    /// </summary>
    internal static class EPaddingModeExFunction
    {
        /// <summary>
        /// 转换类型
        /// </summary>
        /// <returns></returns>
        public static PaddingMode Convert(this EPaddingMode paddingMode)
        {
            return (PaddingMode)paddingMode;
        }
    }
}
