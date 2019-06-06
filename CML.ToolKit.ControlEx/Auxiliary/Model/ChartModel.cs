using System;
using System.Collections.Generic;
using System.Drawing;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 所有标记信息的基类，支持区间的表示
    /// </summary>
    public class ModMarkSectionBase
    {
        /// <summary>
        /// 开始的数据位置
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// 结束的数据位置
        /// </summary>
        public int EndIndex { get; set; }
    }

    /// <summary>
    /// 用于背景色标记的区段信息
    /// </summary>
    public class ModMarkBackSection : ModMarkSectionBase
    {
        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public ModMarkBackSection()
        {
            this.BackColor = Color.FromArgb(52, 52, 52);
        }

        /// <summary>
        /// 特殊标记的区间的背景色
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// 标记文本
        /// </summary>
        public string MarkText { get; set; }
    }

    /// <summary>
    /// 用于前景色标记的区段信息
    /// </summary>
    public class ModMarkForeSection : ModMarkSectionBase
    {
        /// <summary>
        /// 绘制的起始高度
        /// </summary>
        public float StartHeight { get; set; }

        /// <summary>
        /// 绘制是高度信息
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// 是否显示开始和结束的信息
        /// </summary>
        public bool IsRenderTimeText { get; set; }

        /// <summary>
        /// 特殊标记的区间的背景色
        /// </summary>
        public Pen LinePen { get; set; }

        /// <summary>
        /// 字体的颜色
        /// </summary>
        public Brush FontBrush { get; set; }

        /// <summary>
        /// 特殊标记的文本，如果不为空，则显示出来
        /// </summary>
        public string MarkText { get; set; }

        /// <summary>
        /// 光标处显示的信息
        /// </summary>
        public Dictionary<string, string> CursorTexts { get; set; }

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public ModMarkForeSection()
        {
            this.LinePen = Pens.Cyan;
            this.FontBrush = Brushes.Yellow;
            this.IsRenderTimeText = true;
            this.CursorTexts = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// 曲线图里的文本标记信息
    /// </summary>
    public class ModMarkText
    {
        /// <summary>
        /// 文本的便宜标记信息
        /// </summary>
        public static readonly int MarkTextOffect = 5;

        /// <summary>
        /// 针对的曲线的关键字
        /// </summary>
        public string CurveKey { get; set; }

        /// <summary>
        /// 位置的索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 标记的文本
        /// </summary>
        public string MarkedText { get; set; }

        /// <summary>
        /// 文本的颜色
        /// </summary>
        public Brush TextBrush { get; set; }

        /// <summary>
        /// 圆圈的画刷
        /// </summary>
        public Brush CircleBrush { get; set; }

        /// <summary>
        /// 点的标记状态
        /// </summary>
        public EMarkTextPositionStyle PositionStyle { get; set; } = EMarkTextPositionStyle.Auto;

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public ModMarkText()
        {
            this.TextBrush = Brushes.Black;
            this.CircleBrush = Brushes.DodgerBlue;
        }

        /// <summary>
        /// 根据数据和索引计算文本的绘制的位置
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <param name="index">索引</param>
        /// <returns>绘制的位置信息</returns>
        public static EMarkTextPositionStyle CalculateDirectionFromDataIndex(float[] data, int index)
        {
            double numLeft = (index == 0) ? data[index] : data[index - 1];
            double numRight = (index == data.Length - 1) ? data[index] : data[index + 1];

            EMarkTextPositionStyle result;
            if (numLeft < data[index] && data[index] < numRight)
            {
                result = EMarkTextPositionStyle.Left;
            }
            else
            {
                if (numLeft > data[index] && data[index] > numRight)
                {
                    result = EMarkTextPositionStyle.Right;
                }
                else
                {
                    if (numLeft <= data[index] && data[index] >= numRight)
                    {
                        result = EMarkTextPositionStyle.Up;
                    }
                    else
                    {
                        if (numLeft >= data[index] && data[index] <= numRight)
                        {
                            result = EMarkTextPositionStyle.Down;
                        }
                        else
                        {
                            result = EMarkTextPositionStyle.Up;
                        }
                    }
                }
            }

            return result;
        }
    }

    /// <summary>
    /// 曲线图里的直线标记信息
    /// </summary>
    public class ModMarkLine
    {
        /// <summary>
        /// 文本的颜色
        /// </summary>
        public Brush TextBrush { get; set; }

        /// <summary>
        /// 圆圈的画刷
        /// </summary>
        public Brush CircleBrush { get; set; }

        /// <summary>
        /// 线段颜色的标记
        /// </summary>
        public Pen LinePen { get; set; }

        /// <summary>
        /// 所有的点的集合
        /// </summary>
        public PointF[] Points { get; set; }

        /// <summary>
        /// 所有的标记点的集合
        /// </summary>
        public string[] Marks { get; set; }

        /// <summary>
        /// 线段是否闭合
        /// </summary>
        public bool IsLineClosed { get; set; }

        /// <summary>
        /// 是否参照左侧的坐标轴
        /// </summary>
        public bool IsLeftFrame { get; set; }

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public ModMarkLine()
        {
            this.TextBrush = Brushes.DodgerBlue;
            this.CircleBrush = Brushes.DodgerBlue;
            this.LinePen = Pens.DodgerBlue;
            this.IsLeftFrame = true;
        }
    }

    /// <summary>
    /// 辅助线对象
    /// </summary>
    internal class ModAuxiliaryLine : IDisposable
    {
        /// <summary>
        /// 是否已经释放资源
        /// </summary>
        private bool isDisposedValue = false;

        /// <summary>
        /// 实际的数据值
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// 实际的数据绘制的Y轴位置
        /// </summary>
        public float PaintValue { get; set; }

        /// <summary>
        /// 实际的数据绘制的Y轴位置，备份使用的
        /// </summary>
        public float PaintValueBackUp { get; set; }

        /// <summary>
        /// 辅助线的颜色
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// 辅助线的画笔资源
        /// </summary>
        public Pen PenDash { get; set; }

        /// <summary>
        /// 辅助线的宽度
        /// </summary>
        public double LineThickness { get; set; }

        /// <summary>
        /// 辅助线文本的画刷
        /// </summary>
        public Brush LineTextBrush { get; set; }

        /// <summary>
        /// 是否左侧参考系，True为左侧，False为右侧
        /// </summary>
        public bool IsLeftFrame { get; set; }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposedValue)
            {
                if (disposing)
                {
                    Pen penDash = this.PenDash;
                    if (penDash != null)
                    {
                        penDash.Dispose();
                    }
                    this.LineTextBrush.Dispose();
                }

                this.isDisposedValue = true;
            }
        }

        /// <summary>
        /// 释放内存信息
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
    }

    /// <summary>
    /// 曲线数据对象
    /// </summary>
    internal class ModCurveItem
    {
        /// <summary>
        /// 实例化一个默认对象
        /// </summary>
        public ModCurveItem()
        {
            this.LineThickness = 1;
            this.IsLeftFrame = true;
            this.Visible = true;
            this.LineRenderVisiable = true;
            this.TitleRegion = new RectangleF(0f, 0f, 0f, 0f);
        }

        /// <summary>
        /// 线条的宽度
        /// </summary>
        public float LineThickness { get; set; }

        /// <summary>
        /// 是否平滑的曲线显示，默认为False
        /// </summary>
        public bool IsSmoothCurve { get; set; }

        /// <summary>
        /// 曲线颜色
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// 是否左侧参考系，True为左侧，False为右侧
        /// </summary>
        public bool IsLeftFrame { get; set; }

        /// <summary>
        /// 本曲线是否显示出来，默认为显示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 线体可见性
        /// </summary>
        public bool LineRenderVisiable { get; set; }

        /// <summary>
        /// 标题区域
        /// </summary>
        public RectangleF TitleRegion { get; set; }

        /// <summary>
        /// 本曲线在图形上显示的格式化信息，对历史数据有效
        /// </summary>
        public string RenderFormat { get; set; } = "{0}";

        /// <summary>
        /// 数据
        /// </summary>
        public float[] Data = null;

        /// <summary>
        /// 标记文本
        /// </summary>
        public string[] MarkText = null;
    }
}
