namespace CML.CommonEx.RegexEx
{
    /// <summary>
    /// 数值验证类型
    /// </summary>
    public enum ENumberVerifyType
    {
        /// <summary>
        /// 所有数(-∞,+∞)
        /// </summary>
        Normal,
        /// <summary>
        /// 负数(-∞,0)
        /// </summary>
        Nagtive,
        /// <summary>
        /// 正数(0,+∞)
        /// </summary>
        Positive,
        /// <summary>
        /// 非负数[0,+∞)
        /// </summary>
        NotNagtive,
        /// <summary>
        /// 非正数(-∞,0]
        /// </summary>
        NotPositive
    }
}
