﻿namespace CML.CommonEx.NetworkEx
{
    /// <summary>
    /// 传输速率模型
    /// </summary>
    public class ModHttpTransSpeed
    {
        /// <summary>
        /// 传输速率值（数值大于0时启用）
        /// </summary>
        public int Speed { get; set; } = 0;

        /// <summary>
        /// 传输速率单位
        /// </summary>
        public EHttpSpeedUnit Unit { get; set; } = EHttpSpeedUnit.MB;

        /// <summary>
        /// 传输间隔毫秒数（默认为100MS）
        /// </summary>
        public int Delay { get; set; } = 100;

        /// <summary>
        /// 是否启用限速
        /// </summary>
        public bool EnableLimit => Speed > 0;

        /// <summary>
        /// 默认构造函数（不限速）
        /// </summary>
        public ModHttpTransSpeed() { }

        /// <summary>
        /// 构造函数（默认延时100MS）
        /// </summary>
        /// <param name="speed">传输速率值（数值大于0时启用）</param>
        /// <param name="unit">传输速率单位</param>
        public ModHttpTransSpeed(int speed, EHttpSpeedUnit unit)
        {
            Speed = speed;
            Unit = unit;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="speed">传输速率值（数值大于0时启用）</param>
        /// <param name="unit">传输速率单位</param>
        /// <param name="delay">传输间隔毫秒数</param>
        public ModHttpTransSpeed(int speed, EHttpSpeedUnit unit, int delay)
        {
            Speed = speed;
            Unit = unit;
            Delay = delay;
        }
    }
}
