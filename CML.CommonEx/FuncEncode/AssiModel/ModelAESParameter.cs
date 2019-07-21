using System.Text;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// AES参数模型
    /// </summary>
    public class ModelAESParameter
    {
        /// <summary>
        /// 密钥存储
        /// </summary>
        private string key = "Cmile_9669_elimC";
        /// <summary>
        /// 向量存储
        /// </summary>
        private string iv = "Cmile_9669_elimC";

        /// <summary>
        /// 密钥（16位）
        /// </summary>
        public string Key
        {
            get
            {
                if (key.Length < 16)
                {
                    return key.PadLeft(16, PaddingChar);
                }
                else if (key.Length > 16)
                {
                    return key.Substring(key.Length - 16);
                }
                else
                {
                    return key;
                }
            }

            set => key = value ?? "";
        }

        /// <summary>
        /// 向量（16位）
        /// </summary>
        public string IV
        {
            get
            {
                if (iv.Length < 16)
                {
                    return iv.PadLeft(16, PaddingChar);
                }
                else if (iv.Length > 16)
                {
                    return iv.Substring(iv.Length - 16);
                }
                else
                {
                    return iv;
                }
            }

            set => iv = value ?? "";
        }

        /// <summary>
        /// 填充字符
        /// </summary>
        public char PaddingChar { get; set; } = ' ';

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
        /// 默认构造函数
        /// </summary>
        public ModelAESParameter() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（16位）</param>
        /// <param name="iv">向量（16位）</param>
        public ModelAESParameter(string key, string iv)
        {
            Key = key;
            IV = iv;
        }
    }
}
