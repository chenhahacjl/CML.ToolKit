using System.Text;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// AES参数模型
    /// </summary>
    public class ModAESParameter
    {
        /// <summary>
        /// 密钥存储
        /// </summary>
        private string key = "Cmile_9669_elimCCmile_9669_elimC";
        /// <summary>
        /// 向量存储
        /// </summary>
        private string iv = "Cmile_9669_elimC";

        /// <summary>
        /// 密钥
        /// </summary>
        public string Key
        {
            get => key;
            set => key = value ?? "";
        }

        /// <summary>
        /// 向量
        /// </summary>
        public string IV
        {
            get => iv;
            set => iv = value ?? "";
        }

        /// <summary>
        /// 加密模式
        /// </summary>
        public ECipherMode CipherMode { get; set; } = ECipherMode.CBC;

        /// <summary>
        /// 填充模式
        /// </summary>
        public EPaddingMode PaddingMode { get; set; } = EPaddingMode.PKCS7;

        /// <summary>
        /// 字符编码
        /// </summary>
        public Encoding Encode { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 获取或设置对称算法所用密钥的大小（以位为单位）
        /// </summary>
        public int KeySize { get; set; } = 0x80;

        /// <summary>
        /// 获取或设置加密操作的块大小（以位为单位）。
        /// </summary>
        public int BlockSize { get; set; } = 0x80;

        /// <summary>
        /// 获取或设置加密操作的反馈大小（以位为单位）
        /// </summary>
        public int FeedbackSize { get; set; } = 0x80;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ModAESParameter() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（16位）</param>
        /// <param name="iv">向量（16位）</param>
        public ModAESParameter(string key, string iv)
        {
            Key = key;
            IV = iv;
        }
    }
}
