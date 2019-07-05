namespace CML.CommonEx.IDNumberEx
{
    /// <summary>
    /// 身份证号模型
    /// </summary>
    public class ModelIDNumber
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 是否合法
        /// </summary>
        public bool IsLawful => RegexEx.RegexOperate.CF_IsIDCard(IDNumber);

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ModelIDNumber() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="idNumber">身份证号</param>
        public ModelIDNumber(string idNumber)
        {
            IDNumber = idNumber;
        }
    }
}
