namespace CML.ControlEx
{
    /// <summary>
    /// 标记点的样式信息
    /// </summary>
    public enum EMarkTextPositionStyle
    {
        /// <summary>
        /// 在标记点的上方
        /// </summary>
        Up = 1,
        /// <summary>
        /// 在标记点的右侧
        /// </summary>
        Right,
        /// <summary>
        /// 在标记点的下方
        /// </summary>
        Down,
        /// <summary>
        /// 在标记点的左侧
        /// </summary>
        Left,
        /// <summary>
        /// 自动选择位置
        /// </summary>
        Auto = 10
    }

    /// <summary>
    /// 图形的方向
    /// </summary>
    public enum EGraphDirection
    {
        /// <summary>
        /// 向上
        /// </summary>
        Upward = 1,
        /// <summary>
        /// 向下
        /// </summary>
        Downward,
        /// <summary>
        /// 向左
        /// </summary>
        Leftward,
        /// <summary>
        /// 向右
        /// </summary>
        Rightward
    }
}
