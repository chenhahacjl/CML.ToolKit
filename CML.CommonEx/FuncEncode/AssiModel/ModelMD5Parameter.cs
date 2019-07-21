using System.Text;

namespace CML.CommonEx.EncodeEx
{
    /// <summary>
    /// MD5参数模型
    /// </summary>
    public class ModelMD5Parameter
    {
        /// <summary>
        /// 输出长度（默认32位）
        /// </summary>
        public EMD5Length MD5Length { get; set; } = EMD5Length.L32;

        /// <summary>
        /// 大写输出
        /// </summary>
        public bool IsUppercase { get; set; } = true;

        /// <summary>
        /// 字符编码
        /// </summary>
        public Encoding Encode { get; set; } = Encoding.UTF8;
    }
}
