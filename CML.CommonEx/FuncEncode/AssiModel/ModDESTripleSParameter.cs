using System.Text;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// 3DES参数模型
    /// </summary>
    public class ModDESTripleParameter
    {
        /// <summary>
        /// 密钥存储
        /// </summary>
        private string key = "Cmile_9669_elimCCmile_96";
        /// <summary>
        /// 向量存储
        /// </summary>
        private string iv = "Cmile_96";

        /// <summary>
        /// 密钥（24位）
        /// </summary>
        public string Key
        {
            get
            {
                if (key.Length < 24)
                {
                    return key.PadLeft(24, PaddingChar);
                }
                else if (key.Length > 24)
                {
                    return key.Substring(key.Length - 24);
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
        public ModDESTripleParameter() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（24位）</param>
        /// <param name="iv">向量（8位）</param>
        public ModDESTripleParameter(string key, string iv)
        {
            Key = key;
            IV = iv;
        }
    }
}
