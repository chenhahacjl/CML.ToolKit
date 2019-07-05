namespace CML.CommonEx.IDNumberEx
{
    /// <summary>
    /// 地址信息模型
    /// </summary>
    public class ModelAddress
    {
        /// <summary>
        /// 地址编号
        /// </summary>
        public string AddressNumber { get; }
        /// <summary>
        /// 发卡机构
        /// </summary>
        public string CardIssuer { get; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Domicile { get; }

        /// <summary>
        /// 构造函数（错误）
        /// </summary>
        public ModelAddress()
        {
            AddressNumber = "";
            CardIssuer = "";
            Domicile = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="addressNumber">地址编号</param>
        /// <param name="address">发卡地</param>
        /// <param name="domicile">籍贯</param>
        public ModelAddress(string addressNumber, string address, string domicile)
        {
            AddressNumber = addressNumber;
            CardIssuer = address;
            Domicile = domicile;
        }
    }
}
