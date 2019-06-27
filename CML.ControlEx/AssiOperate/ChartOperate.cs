using System.Drawing;

namespace CML.ControlEx
{
    /// <summary>
    /// 图标控件帮助类
    /// </summary>
    internal class ChartOperate
    {
        /// <summary>
        /// 计算绘图时的相对偏移值
        /// </summary>
        /// <param name="max">0-100分的最大值，就是指准备绘制的最大值</param>
        /// <param name="min">0-100分的最小值，就是指准备绘制的最小值</param>
        /// <param name="height">实际绘图区域的高度</param>
        /// <param name="value">需要绘制数据的当前值</param>
        /// <returns>相对于0的位置，还需要增加上面的偏值</returns>
        public static float ComputePaintLocationY(float max, float min, float height, float value)
        {
            return height - (value - min) / (max - min) * height;
        }

        /// <summary>
        /// 根据指定的方向绘制一个箭头
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <param name="brush">笔刷</param>
        /// <param name="point">起始点</param>
        /// <param name="size">长度</param>
        /// <param name="direction">方向</param>
        public static void PaintTriangle(Graphics g, Brush brush, PointF point, int size, EGraphDirection direction)
        {
            PointF[] array = new PointF[4];

            if (direction == EGraphDirection.Leftward)
            {
                array[0] = new PointF(point.X, point.Y - size);
                array[1] = new PointF(point.X, point.Y + size);
                array[2] = new PointF(point.X - (2 * size), point.Y);
            }
            else if (direction == EGraphDirection.Rightward)
            {
                array[0] = new PointF(point.X, point.Y - size);
                array[1] = new PointF(point.X, point.Y + size);
                array[2] = new PointF(point.X + (2 * size), point.Y);
            }
            else if (direction == EGraphDirection.Upward)
            {
                array[0] = new PointF(point.X - size, point.Y);
                array[1] = new PointF(point.X + size, point.Y);
                array[2] = new PointF(point.X, point.Y - (2 * size));
            }
            else
            {
                array[0] = new PointF(point.X - size, point.Y);
                array[1] = new PointF(point.X + size, point.Y);
                array[2] = new PointF(point.X, point.Y + (2 * size));
            }

            array[3] = array[0];
            g.FillPolygon(brush, array);
        }
    }
}
