using System.Text;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// DES参数模型
    /// </summary>
    public class ModelDESParameter
    {
        /// <summary>
        /// 密钥存储
        /// </summary>
        private string key = "Cmile_96";
        /// <summary>
        /// 向量存储
        /// </summary>
        private string iv = "Cmile_96";

        /// <summary>
        /// 密钥（8位）
        /// </summary>
        public string Key
        {
            get
            {
                if (key.Length < 8)
                {
                    return key.PadLeft(8, PaddingChar);
                }
                else if (key.Length > 8)
                {
                    return key.Substring(key.Length - 8);
                }
                else
                {
                    return key;
                }
            }

            set => key = value ?? "";
        }

        /// <summary>
        /// 向量（8位）
        /// </summary>
        public string IV
        {
            get
            {
                if (iv.Length < 8)
                {
                    return iv.PadLeft(8, PaddingChar);
                }
                else if (iv.Length > 8)
                {
                    return iv.Substring(iv.Length - 8);
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
        public ModelDESParameter() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（8位）</param>
        /// <param name="iv">向量（8位）</param>
        public ModelDESParameter(string key, string iv)
        {
            Key = key;
            IV = iv;
        }
    }
}
