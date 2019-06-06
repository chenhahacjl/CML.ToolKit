using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 曲线图表控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("OnCurveDoubleClick")]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public partial class ChartCurve : UserControl
    {
        #region 私有变量
        //运行变量
        private int m_nDataCount = 0;
        private int m_nRowBetweenStart = -1;
        private int m_nRowBetweenEnd = -1;
        private int m_nRowBetweenStartHeight = -1;
        private int m_nRowBetweenHeight = -1;
        private float m_fDataScaleRender = 1;
        private bool m_IsMouseLeftDown = false;
        private bool m_isMouseOnPicture = false;
        private bool m_isShowTextInfomation = true;
        private Point m_ptMouseLocation = new Point(-1, -1);
        private readonly StringFormat m_sfTop = new StringFormat();
        private readonly StringFormat m_sfMain = new StringFormat();
        private readonly StringFormat m_sfLeft = new StringFormat();
        private readonly StringFormat m_sfRight = new StringFormat();
        private readonly StringFormat m_sfDefault = new StringFormat();
        private readonly List<ModCurveItem> m_lstCurveItem = new List<ModCurveItem>();
        private List<DateTime> m_lstDateTime = new List<DateTime>();
        private readonly List<ModMarkLine> m_lstMarkLine = new List<ModMarkLine>();
        private readonly List<ModMarkText> m_lstMarkText = new List<ModMarkText>();
        private readonly List<ModAuxiliaryLine> m_lstAuxiliaryLine = new List<ModAuxiliaryLine>();
        private readonly List<ModMarkBackSection> m_lstMarkBackSection = new List<ModMarkBackSection>();
        private readonly List<ModMarkForeSection> m_lstMarkForeSection = new List<ModMarkForeSection>();
        private readonly List<ModMarkForeSection> m_lstMarkForeSectionTemp = new List<ModMarkForeSection>();
        private readonly List<ModMarkForeSection> m_lstMarkForeActiveSection = new List<ModMarkForeSection>();
        private readonly Dictionary<string, ModCurveItem> m_dicCurveItem = new Dictionary<string, ModCurveItem>();

        //属性变量
        private bool m_isRederRightCoordinate = true;
        private int m_nSegmentCount = 5;
        private int m_nDataTipWidth = 150;
        private float m_fLeftLimitMax = 100f;
        private float m_fLeftLimitMin = 0f;
        private float m_fRightLimitMax = 100f;
        private float m_fRightLimitMin = 0f;
        private string m_strLeftUnit = string.Empty;
        private string m_strRightUnit = string.Empty;
        private Color m_colorCoordinate = Color.LightGray;
        private Brush m_brushCoordinate = new SolidBrush(Color.LightGray);
        private Pen m_penCoordinate = new Pen(Color.LightGray);
        private Color m_colorCoordinateDash = Color.FromArgb(72, 72, 72);
        private Pen m_penCoordinateDash = new Pen(Color.FromArgb(72, 72, 72));
        private Color m_colorMarkLine = Color.Cyan;
        private Pen m_penMarkLine = new Pen(Color.Cyan);
        private Color m_colorMarkText = Color.Yellow;
        private Brush m_brushMarkText = new SolidBrush(Color.Yellow);
        private Color m_colorMoveLine = Color.White;
        private Pen m_PenMoveLine = new Pen(Color.White);
        #endregion

        #region 委托方法
        /// <summary>
        /// 曲线双击的委托方法
        /// </summary>
        /// <param name="evtCurveChart">曲线控件信息</param>
        /// <param name="index">数据的索引</param>
        /// <param name="dateTime">时间信息</param>
        public delegate void CurveDoubleClick(ChartCurve evtCurveChart, int index, DateTime dateTime);

        /// <summary>
        /// 当鼠标在曲线上移动的时候触发的委托方法
        /// </summary>
        /// <param name="evtCurveChart">曲线控件信息</param>
        /// <param name="x">横坐标x</param>
        /// <param name="y">横坐标y</param>
        public delegate void CurveMouseMove(ChartCurve evtCurveChart, int x, int y);
        #endregion

        #region 重写属性
        /// <summary>
        /// 获取或设置当前控件的文本
        /// </summary>
        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance"), Description("获取或设置当前控件的文本")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                m_isShowTextInfomation = true;

                Image image = picChart.Image;
                if (image != null)
                {
                    image.Dispose();
                }
                picChart.Image = GetBitmapFromString(Text);
            }
        }

        /// <summary>
        /// 获取或设置控件的背景色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true), DefaultValue(typeof(Color), "[46, 46, 46]")]
        [Category("Appearance"), Description("获取或设置控件的背景色")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;

                if (m_isShowTextInfomation)
                {
                    Image image = picChart.Image;
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    picChart.Image = GetBitmapFromString(Text);
                }
            }
        }

        /// <summary>
        /// 获取或设置控件的背景色
        /// </summary>
        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true), DefaultValue(typeof(Color), "Yellow")]
        [Category("Appearance"), Description("获取或设置控件的背景色")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;

                if (m_isShowTextInfomation)
                {
                    Image image = picChart.Image;
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    picChart.Image = GetBitmapFromString(Text);
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置图例显示状态
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Category("CMLAttribute"), Description("获取或设置图例显示状态")]
        public bool CP_ShowLegend { get; set; } = true;

        /// <summary>
        /// 获取或设置数据提示框显示状态
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Category("CMLAttribute"), Description("获取或设置数据提示框显示状态")]
        public bool CP_ShowDataTip { get; set; } = true;

        /// <summary>
        /// 获取或设置曲线控件是否显示右侧的坐标轴
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Category("CMLAttribute"), Description("获取或设置曲线控件是否显示右侧的坐标轴")]
        public bool CP_RenderRightCoordinate
        {
            get => m_isRederRightCoordinate;
            set
            {
                m_isRederRightCoordinate = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置图形的纵轴分段数
        /// </summary>
        [Browsable(true), DefaultValue(5)]
        [Category("CMLAttribute"), Description("获取或设置图形的纵轴分段数")]
        public int CP_ValueSegment
        {
            get => m_nSegmentCount;
            set
            {
                m_nSegmentCount = value;
                base.Invalidate();

                if (m_isShowTextInfomation)
                {
                    Image image = picChart.Image;
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    picChart.Image = GetBitmapFromString(Text);
                }
            }
        }

        /// <summary>
        /// 获取或设置鼠标移动过程中提示信息的宽度
        /// </summary>
        [Browsable(true), DefaultValue(150)]
        [Category("CMLAttribute"), Description("获取或设置鼠标移动过程中提示信息的宽度")]
        public int CP_DataTipWidth
        {
            get => m_nDataTipWidth;
            set
            {
                if (value > 20 && value < 500)
                {
                    m_nDataTipWidth = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的左纵坐标的最大值，该值必须大于最小值
        /// </summary>
        [Browsable(true), DefaultValue(100)]
        [Category("CMLAttribute"), Description("获取或设置图形的左纵坐标的最大值，该值必须大于最小值")]
        public float CP_ValueMaxLeft
        {
            get => m_fLeftLimitMax;
            set
            {
                if (value > m_fLeftLimitMin)
                {
                    m_fLeftLimitMax = value;
                    base.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的左纵坐标的最小值，该值必须小于最大值
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("CMLAttribute"), Description("获取或设置图形的左纵坐标的最小值，该值必须小于最大值")]
        public float CP_ValueMinLeft
        {
            get => m_fLeftLimitMin;
            set
            {
                if (value < m_fLeftLimitMax)
                {
                    m_fLeftLimitMin = value;
                    base.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的右纵坐标的最大值，该值必须大于最小值
        /// </summary>
        [Browsable(true), DefaultValue(100)]
        [Category("CMLAttribute"), Description("获取或设置图形的右纵坐标的最大值，该值必须大于最小值")]
        public float CP_ValueMaxRight
        {
            get => m_fRightLimitMax;
            set
            {
                if (value > m_fRightLimitMin)
                {
                    m_fRightLimitMax = value;
                    base.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的右纵坐标的最小值，该值必须小于最大值
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("CMLAttribute"), Description("获取或设置图形的右纵坐标的最小值，该值必须小于最大值")]
        public float CP_ValueMinRight
        {
            get => m_fRightLimitMin;
            set
            {
                if (value < m_fRightLimitMax)
                {
                    m_fRightLimitMin = value;
                    base.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置左侧的单位显示的文本信息
        /// </summary>
        [Browsable(true), DefaultValue("")]
        [Category("CMLAttribute"), Description("获取或设置左侧的单位显示的文本信息")]
        public string CP_UnitLeft
        {
            get => m_strLeftUnit;
            set
            {
                m_strLeftUnit = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置右侧的单位显示的文本信息
        /// </summary>
        [Browsable(true), DefaultValue("")]
        [Category("CMLAttribute"), Description("获取或设置右侧的单位显示的文本信息")]
        public string CP_UnitRight
        {
            get => m_strRightUnit;
            set
            {
                m_strRightUnit = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置左侧坐标轴数值的格式
        /// </summary>
        [Browsable(true), DefaultValue("0")]
        [Category("CMLAttribute"), Description("获取或设置左侧坐标轴数值的格式")]
        public string CP_AxisLeftFormat { get; set; } = "0";

        /// <summary>
        /// 获取或设置右侧坐标轴数值的格式
        /// </summary>
        [Browsable(true), DefaultValue("0")]
        [Category("CMLAttribute"), Description("获取或设置右侧坐标轴数值的格式")]
        public string CP_AxisRightFormat { get; set; } = "0";

        /// <summary>
        /// 获取或设置时间显示的格式
        /// </summary>
        [Browsable(true), DefaultValue("yyyy-MM-dd HH:mm:ss")]
        [Category("CMLAttribute"), Description("获取或设置时间显示的格式")]
        public string CP_DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 获取或设置所有的实线坐标轴的颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "LightGray")]
        [Category("CMLAttribute"), Description("获取或设置所有的实线坐标轴的颜色")]
        public Color CP_CoordinateColor
        {
            get => m_colorCoordinate;
            set
            {
                m_colorCoordinate = value;
                m_penCoordinate.Dispose();
                m_penCoordinate = new Pen(m_colorCoordinate);
                m_brushCoordinate.Dispose();
                m_brushCoordinate = new SolidBrush(m_colorCoordinate);
                base.Invalidate();

                if (m_isShowTextInfomation)
                {
                    Image image = picChart.Image;
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    picChart.Image = GetBitmapFromString(Text);
                }
            }
        }

        /// <summary>
        /// 获取或设置所有的虚线坐标轴的颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "[72, 72, 72]")]
        [Category("CMLAttribute"), Description("获取或设置所有的虚线坐标轴的颜色")]
        public Color CP_DashCoordinateColor
        {
            get => m_colorCoordinateDash;
            set
            {
                m_colorCoordinateDash = value;
                m_penCoordinateDash.Dispose();
                m_penCoordinateDash = new Pen(m_colorCoordinateDash)
                {
                    DashPattern = new float[] { 5f, 5f },
                    DashStyle = DashStyle.Custom
                };
                base.Invalidate();

                if (m_isShowTextInfomation)
                {
                    Image image = picChart.Image;
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    picChart.Image = GetBitmapFromString(Text);
                }
            }
        }

        /// <summary>
        /// 获取或设置所有的区间标记的线条颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "Cyan")]
        [Category("CMLAttribute"), Description("获取或设置所有的区间标记的线条颜色")]
        public Color CP_MarkLineColor
        {
            get => m_colorMarkLine;
            set
            {
                m_colorMarkLine = value;
                m_penMarkLine.Dispose();
                m_penMarkLine = new Pen(m_colorMarkLine);
            }
        }

        /// <summary>
        /// 获取或设置所有的区间标记的文本颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "Cyan")]
        [Category("CMLAttribute"), Description("获取或设置所有的区间标记的文本颜色")]
        public Color CP_MarkTextColor
        {
            get => m_colorMarkText;
            set
            {
                m_colorMarkText = value;
                m_brushMarkText.Dispose();
                m_brushMarkText = new SolidBrush(m_colorMarkText);
            }
        }

        /// <summary>
        /// 获取或设置鼠标移动过程中的提示线的颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "White")]
        [Category("CMLAttribute"), Description("获取或设置鼠标移动过程中的提示线的颜色")]
        public Color CP_MoveLineColor
        {
            get => m_colorMoveLine;
            set
            {
                m_colorMoveLine = value;
                m_PenMoveLine.Dispose();
                m_PenMoveLine = new Pen(m_colorMoveLine);
            }
        }

        /// <summary>
        /// 获取已添加曲线列表
        /// </summary>
        [Browsable(false)]
        [Category("CMLAttribute"), Description("获取已添加曲线列表")]
        public string[] CP_CurveList => m_dicCurveItem.Keys.ToArray();
        #endregion

        #region 注册事件
        /// <summary>
        /// 当鼠标在曲线上双击时触发，由此获取到点击的数据的索引位置，时间坐标
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("EvtEvent"), Description("当鼠标在曲线上双击时触发，由此获取到点击的数据的索引位置，时间坐标")]
        public event CurveDoubleClick CE_OnCurveDoubleClick;

        /// <summary>
        /// 当鼠标在曲线上移动时触发，由此获取到鼠标的移动位置
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("EvtEvent"), Description("当鼠标在曲线上移动时触发，由此获取到鼠标的移动位置")]
        public event CurveMouseMove CE_OnCurveMouseMove;
        #endregion

        #region 私有方法
        /// <summary>
        /// 添加辅助线
        /// </summary>
        /// <param name="fValue">辅助线位置数值</param>
        /// <param name="colorLine">辅助线颜色</param>
        /// <param name="dLineThickness">辅助线宽度</param>
        /// <param name="isLeft">是否为左侧辅助线</param>
        private void AddAuxiliary(float fValue, Color colorLine, double dLineThickness, bool isLeft)
        {
            m_lstAuxiliaryLine.Add(new ModAuxiliaryLine
            {
                Value = fValue,
                LineColor = colorLine,
                PenDash = new Pen(colorLine)
                {
                    DashStyle = DashStyle.Custom,
                    DashPattern = new float[] { 5f, 5f }
                },
                IsLeftFrame = isLeft,
                LineThickness = dLineThickness,
                LineTextBrush = new SolidBrush(colorLine)
            });

            CalculateAuxiliaryPaintY();

            if (m_isShowTextInfomation)
            {
                Image image = picChart.Image;
                if (image != null)
                {
                    image.Dispose();
                }
                picChart.Image = GetBitmapFromString(Text);
            }
        }

        /// <summary>
        /// 文字转图片
        /// </summary>
        /// <param name="text">转换文字</param>
        /// <returns></returns>
        private Bitmap GetBitmapFromString(string text)
        {
            int width = (pnlAxis.Width > 10) ? pnlAxis.Width : 1;
            int height = (pnlAxis.Height > 10) ? pnlAxis.Height : 10;

            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(BackColor);

                PaintFromString(graphics, text, width, height);
            }
            return bitmap;
        }

        /// <summary>
        /// 文字转图片
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="text">转换文字</param>
        /// <param name="nWidth">宽度</param>
        /// <param name="nHeight">高度</param>
        private void PaintFromString(Graphics graphics, string text, int nWidth, int nHeight)
        {
            Font font = new Font(Font.FontFamily, 18f);
            if (text != null && text.Length > 400)
            {
                font = new Font(Font.FontFamily, 12f);
            }

            graphics.DrawLine(m_penCoordinate, 0, nHeight - 40, nWidth - 1, nHeight - 40);

            for (int i = 1; i <= m_nSegmentCount; i++)
            {
                float value = i * (m_fLeftLimitMax - m_fLeftLimitMin) / m_nSegmentCount + m_fLeftLimitMin;
                float num = ChartOperate.ComputePaintLocationY(m_fLeftLimitMax, m_fLeftLimitMin, nHeight - 60, value) + 20;

                if (IsNeedPaintDash(num))
                {
                    graphics.DrawLine(m_penCoordinateDash, 0f, num, (nWidth - 1), num);
                }
            }

            CalculateAuxiliaryPaintY();

            for (int j = 0; j < m_lstAuxiliaryLine.Count; j++)
            {
                graphics.DrawLine(m_lstAuxiliaryLine[j].PenDash, 0f, m_lstAuxiliaryLine[j].PaintValue, (nWidth - 1), m_lstAuxiliaryLine[j].PaintValue);
            }

            for (int k = 200; k < nWidth; k += 200)
            {
                graphics.DrawLine(m_penCoordinateDash, k, nHeight - 40, k, 0);
            }

            Rectangle rect = new Rectangle(0, 0, nWidth, nHeight);
            using (Brush brush = new SolidBrush(ForeColor))
            {
                graphics.DrawString(text, font, brush, rect, m_sfMain);
            }

            font.Dispose();
        }

        /// <summary>
        /// 是否需要绘制虚线
        /// </summary>
        /// <param name="fValue">数值</param>
        /// <returns>是否需要绘制虚线</returns>
        private bool IsNeedPaintDash(float fValue)
        {
            for (int i = 0; i < m_lstAuxiliaryLine.Count; i++)
            {
                if (Math.Abs(m_lstAuxiliaryLine[i].PaintValue - fValue) < Font.Height)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 计算Y轴辅助线
        /// </summary>
        private void CalculateAuxiliaryPaintY()
        {
            for (int i = 0; i < m_lstAuxiliaryLine.Count; i++)
            {
                if (m_lstAuxiliaryLine[i].IsLeftFrame)
                {
                    m_lstAuxiliaryLine[i].PaintValue =
                        ChartOperate.ComputePaintLocationY(m_fLeftLimitMax, m_fLeftLimitMin, (pnlAxis.Height - 60), m_lstAuxiliaryLine[i].Value) + 20f;
                }
                else
                {
                    m_lstAuxiliaryLine[i].PaintValue =
                        ChartOperate.ComputePaintLocationY(m_fRightLimitMax, m_fRightLimitMin, (pnlAxis.Height - 60), m_lstAuxiliaryLine[i].Value) + 20f;
                }
            }
        }

        /// <summary>
        /// 计算数据最大值
        /// </summary>
        private void CalculateCurveDataMax()
        {
            m_nDataCount = 0;
            for (int i = 0; i < m_lstCurveItem.Count; i++)
            {
                if (m_nDataCount < m_lstCurveItem[i].Data.Length)
                {
                    m_nDataCount = m_lstCurveItem[i].Data.Length;
                }
            }
        }

        /// <summary>
        /// 绘制坐标轴
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="isLeft">是否为左侧坐标轴</param>
        /// <param name="fX">X</param>
        /// <param name="fY">Y</param>
        /// <param name="fMax">最大值</param>
        /// <param name="fMin">最小值</param>
        /// <param name="stringFormat">文本显示信息</param>
        /// <param name="strUnit">单位</param>
        private void DrawCoordinate(Graphics graphics, bool isLeft, float fX, float fY, float fMax, float fMin, StringFormat stringFormat, string strUnit)
        {
            graphics.TranslateTransform(fX, fY);

            float fTop = (base.Height - 31f);
            float fPadding = 40f;

            if (isLeft)
            {
                graphics.DrawLine(m_penCoordinate, fPadding - 1f, 5f, fPadding - 1f, fTop - 35f);
                ChartOperate.PaintTriangle(graphics, m_brushCoordinate, new PointF(fPadding - 1f, 10f), 5, EGraphDirection.Upward);
            }
            else
            {
                graphics.DrawLine(m_penCoordinate, 0f, 5f, 0f, fTop - 35f);
                ChartOperate.PaintTriangle(graphics, m_brushCoordinate, new PointF(0f, 10f), 5, EGraphDirection.Upward);
            }
            graphics.DrawString(strUnit, Font, m_brushCoordinate, new RectangleF(0f, -15f, fPadding, 30f), m_sfMain);

            for (int i = 0; i <= m_nSegmentCount; i++)
            {
                float value = (i * (fMax - fMin) / m_nSegmentCount + fMin);
                float fDash = ChartOperate.ComputePaintLocationY(fMax, fMin, fTop - 60f, value) + 20f;

                if (IsNeedPaintDash(fDash))
                {
                    if (isLeft)
                    {
                        graphics.DrawLine(m_penCoordinate, fPadding - 5f, fDash, fPadding - 2f, fDash);
                        RectangleF layoutRectangle = new RectangleF(0f, fDash - 9f, fPadding - 4f, 20f);
                        graphics.DrawString(value.ToString(CP_AxisLeftFormat), Font, m_brushCoordinate, layoutRectangle, stringFormat);
                    }
                    else
                    {
                        graphics.DrawLine(m_penCoordinate, 1f, fDash, 5f, fDash);
                        RectangleF layoutRectangle2 = new RectangleF(5f, fDash - 9f, fPadding - 3f, 20f);
                        graphics.DrawString(value.ToString(CP_AxisRightFormat), Font, m_brushCoordinate, layoutRectangle2, stringFormat);
                    }
                }
            }

            for (int j = 0; j < m_lstAuxiliaryLine.Count; j++)
            {
                if (m_lstAuxiliaryLine[j].IsLeftFrame)
                {
                    float num4 = ChartOperate.ComputePaintLocationY(fMax, fMin, fTop - 60f, m_lstAuxiliaryLine[j].Value) + 20f;
                    if (isLeft)
                    {
                        graphics.DrawLine(m_penCoordinate, fPadding - 5f, num4, fPadding - 2f, num4);
                        RectangleF layoutRectangle3 = new RectangleF(0f, num4 - 9f, fPadding - 4f, 20f);
                        graphics.DrawString(m_lstAuxiliaryLine[j].Value.ToString(), Font, m_brushCoordinate, layoutRectangle3, stringFormat);
                    }
                    else
                    {
                        graphics.DrawLine(m_penCoordinate, 1f, num4, 5f, num4);
                        RectangleF layoutRectangle4 = new RectangleF(6f, num4 - 9f, fPadding - 3f, 20f);
                        graphics.DrawString(m_lstAuxiliaryLine[j].Value.ToString(), Font, m_brushCoordinate, layoutRectangle4, stringFormat);
                    }
                }
            }
            graphics.TranslateTransform(-fX, -fY);
        }

        /// <summary>
        /// 绘制前景选区
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <param name="markForeSection">前景选区</param>
        /// <param name="font">字体</param>
        private void DrawMarkForeSection(Graphics g, ModMarkForeSection markForeSection, Font font)
        {
            if (markForeSection != null)
            {
                float num = (markForeSection.Height > 1f) ? markForeSection.Height : ((pnlAxis.Height - 60) * markForeSection.Height + 20f);
                float num2 = (markForeSection.StartHeight > 1f) ? markForeSection.StartHeight : ((pnlAxis.Height - 60) * markForeSection.StartHeight + 20f);

                if (markForeSection.StartIndex != -1 &&
                    markForeSection.EndIndex != -1 &&
                    markForeSection.EndIndex > markForeSection.StartIndex &&
                    num2 < num)
                {
                    int num3 = Convert.ToInt32(markForeSection.StartIndex * m_fDataScaleRender);
                    int num4 = Convert.ToInt32(markForeSection.EndIndex * m_fDataScaleRender);

                    if (markForeSection.StartIndex < m_nDataCount &&
                        markForeSection.EndIndex < m_nDataCount &&
                        markForeSection.StartIndex < m_lstDateTime.Count &&
                        markForeSection.EndIndex < m_lstDateTime.Count)
                    {
                        g.DrawLine(markForeSection.LinePen, new PointF(num3, num2), new PointF(num3, num + 30f));
                        g.DrawLine(markForeSection.LinePen, new PointF(num4, num2), new PointF(num4, num + 30f));
                        int num5 = markForeSection.IsRenderTimeText ? 20 : 0;
                        int num6 = (num4 - num3 > 100) ? num5 : 110;

                        g.DrawLine(markForeSection.LinePen, new PointF((num3 - num6), num), new PointF((num4 + num5), num));
                        g.DrawLines(markForeSection.LinePen, new PointF[]
                        {
                            new PointF(num3 + 20, num + 10f),
                            new PointF(num3, num),
                            new PointF(num3 + 20, num - 10f)
                        });
                        g.DrawLines(markForeSection.LinePen, new PointF[]
                        {
                            new PointF((num4 - 20), num - 10f),
                            new PointF(num4, num),
                            new PointF((num4 - 20), num + 10f)
                        });
                        TimeSpan timeSpan = m_lstDateTime[markForeSection.EndIndex] - m_lstDateTime[markForeSection.StartIndex];

                        if (num4 - num3 <= 100)
                        {
                            g.DrawString(timeSpan.TotalMinutes.ToString("F1") + " 分钟", font, markForeSection.FontBrush, new PointF((num3 - 100), num - 17f));
                        }
                        else
                        {
                            g.DrawString(timeSpan.TotalMinutes.ToString("F1") + " 分钟", font, markForeSection.FontBrush, new RectangleF(num3, num - font.Height - 2f, (num4 - num3), font.Height), m_sfMain);
                        }

                        if (!string.IsNullOrEmpty(markForeSection.MarkText))
                        {
                            g.DrawString(markForeSection.MarkText, font, markForeSection.FontBrush, new RectangleF(num3, num + 3f, (num4 - num3), font.Height), m_sfMain);
                        }

                        if (markForeSection.IsRenderTimeText)
                        {
                            g.DrawString("开始:" + m_lstDateTime[markForeSection.StartIndex].ToString(CP_DateTimeFormat), font, markForeSection.FontBrush, new PointF((num4 + 5), num - 27f));
                            g.DrawString("结束:" + m_lstDateTime[markForeSection.EndIndex].ToString(CP_DateTimeFormat), font, markForeSection.FontBrush, new PointF((num4 + 5), num - 8f));

                            string strTimsSpain = "";
                            TimeSpan ts = m_lstDateTime[markForeSection.EndIndex] - m_lstDateTime[markForeSection.StartIndex];
                            if (ts.Days != 0)
                            {
                                strTimsSpain += $"{ts.Days}日";
                            }

                            if (ts.Hours != 0 || !string.IsNullOrEmpty(strTimsSpain))
                            {
                                strTimsSpain += $"{ts.Hours}小时";
                            }

                            if (ts.Minutes != 0 || !string.IsNullOrEmpty(strTimsSpain))
                            {
                                strTimsSpain += $"{ts.Minutes}分钟";
                            }

                            if (ts.Seconds != 0 || !string.IsNullOrEmpty(strTimsSpain))
                            {
                                strTimsSpain += $"{ts.Seconds}秒";
                            }

                            g.DrawString($"持续:{strTimsSpain}", font, markForeSection.FontBrush, new PointF((num4 + 5), num + 11f));

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绘制图例
        /// </summary>
        /// <param name="g">绘图对象</param>
        private void PaintLegend(Graphics g)
        {
            float fLeft = 50f;
            foreach (KeyValuePair<string, ModCurveItem> keyValuePair in m_dicCurveItem)
            {
                if (keyValuePair.Value.Visible)
                {
                    Pen pen = keyValuePair.Value.LineRenderVisiable ?
                        new Pen(keyValuePair.Value.LineColor) :
                        new Pen(Color.FromArgb(80, keyValuePair.Value.LineColor));

                    g.DrawLine(pen, fLeft, 16f, fLeft + 30f, 16f);
                    g.DrawEllipse(pen, fLeft + 8f, 9f, 14f, 14f);
                    pen.Dispose();

                    SolidBrush solidBrush = keyValuePair.Value.LineRenderVisiable ?
                        new SolidBrush(keyValuePair.Value.LineColor) :
                        new SolidBrush(Color.FromArgb(80, keyValuePair.Value.LineColor));

                    g.DrawString(keyValuePair.Key, Font, solidBrush, new RectangleF(fLeft + 35f, 2f, 120f, 30F), m_sfLeft);
                    keyValuePair.Value.TitleRegion = new RectangleF(fLeft, 2f, 120f, 30f);
                    solidBrush.Dispose();

                    fLeft += 150f;
                }
            }
        }

        /// <summary>
        /// 绘制图表坐标轴
        /// </summary>
        /// <param name="graphics">Graphics</param>
        private void PaintMain(Graphics graphics)
        {
            //绘制坐标轴
            DrawCoordinate(graphics, true, 3f, 29f, m_fLeftLimitMax, m_fLeftLimitMin, m_sfRight, m_strLeftUnit);

            //是否为双坐标轴
            if (m_isRederRightCoordinate)
            {
                DrawCoordinate(graphics, false, (base.Width - 43), 29f, m_fRightLimitMax, m_fRightLimitMin, m_sfLeft, m_strRightUnit);
            }

            //绘制图例
            if (CP_ShowLegend)
            {
                PaintLegend(graphics);
            }
        }

        /// <summary>
        /// 绘制图表数据
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <param name="nWidth">宽度</param>
        /// <param name="nHeight">高度</param>
        private void GetRenderCurveMain(Graphics g, int nWidth, int nHeight)
        {
            //背景选区
            for (int i = 0; i < m_lstMarkBackSection.Count; i++)
            {
                //绘制选区
                RectangleF rect = new RectangleF(m_lstMarkBackSection[i].StartIndex * m_fDataScaleRender, 0f, (m_lstMarkBackSection[i].EndIndex - m_lstMarkBackSection[i].StartIndex) * m_fDataScaleRender, pnlAxis.Height - 60 + 20);
                using (Brush brush = new SolidBrush(m_lstMarkBackSection[i].BackColor))
                {
                    g.FillRectangle(brush, rect);
                }
                g.DrawRectangle(Pens.DimGray, rect.X, rect.Y, rect.Width, rect.Height);

                //绘制说明文字
                string text = m_lstMarkBackSection[i].MarkText ?? string.Empty;
                if (m_lstMarkBackSection[i].StartIndex < m_lstDateTime.Count &&
                    m_lstMarkBackSection[i].EndIndex < m_lstDateTime.Count)
                {
                    text = text + " (" + (m_lstDateTime[m_lstMarkBackSection[i].EndIndex] - m_lstDateTime[m_lstMarkBackSection[i].StartIndex]).TotalMinutes.ToString("F1") + " 分钟)";
                }
                g.DrawString(text, Font, Brushes.DimGray, new RectangleF(rect.X, 3f, rect.Width, (pnlAxis.Height - 60)), m_sfTop);
            }

            g.DrawLine(m_penCoordinate, 0, pnlAxis.Height - 40, nWidth - 1, pnlAxis.Height - 40);
            CalculateAuxiliaryPaintY();

            //绘制Y轴分段数值
            for (int j = 1; j <= m_nSegmentCount; j++)
            {
                float value = (j * (m_fLeftLimitMax - m_fLeftLimitMin) / m_nSegmentCount + m_fLeftLimitMin);
                float num = ChartOperate.ComputePaintLocationY(m_fLeftLimitMax, m_fLeftLimitMin, (pnlAxis.Height - 60), value) + 20f;

                if (IsNeedPaintDash(num))
                {
                    g.DrawLine(m_penCoordinateDash, 0f, num, (nWidth - 1), num);
                }
            }

            //辅助线
            for (int k = 0; k < m_lstAuxiliaryLine.Count; k++)
            {
                g.DrawLine(m_lstAuxiliaryLine[k].PenDash, 0f, m_lstAuxiliaryLine[k].PaintValue, (nWidth - 1), m_lstAuxiliaryLine[k].PaintValue);
            }

            //绘制X轴时间
            for (int l = 200; l < nWidth; l += 200)
            {
                g.DrawLine(m_penCoordinateDash, l, pnlAxis.Height - 38, l, 0);
                if (m_lstDateTime != null)
                {
                    int num2 = Convert.ToInt32(l / m_fDataScaleRender);
                    if (num2 < m_lstDateTime.Count)
                    {
                        g.DrawString(m_lstDateTime[num2].ToString(CP_DateTimeFormat), Font, m_brushCoordinate, new Rectangle(l - 100, pnlAxis.Height - 40, 200, 22), m_sfMain);
                    }
                }
            }

            //绘制标记点文字
            foreach (ModMarkText markText in m_lstMarkText)
            {
                foreach (KeyValuePair<string, ModCurveItem> keyValuePair in m_dicCurveItem)
                {
                    if (keyValuePair.Value.Visible &&
                        keyValuePair.Value.LineRenderVisiable &&
                        keyValuePair.Key == markText.CurveKey)
                    {
                        float[] data = keyValuePair.Value.Data;

                        if (data != null && data.Length > 1 &&
                            markText.Index >= 0 && markText.Index < keyValuePair.Value.Data.Length)
                        {
                            PointF pointF = new PointF(markText.Index * m_fDataScaleRender, ChartOperate.ComputePaintLocationY(keyValuePair.Value.IsLeftFrame ? m_fLeftLimitMax : m_fRightLimitMax, keyValuePair.Value.IsLeftFrame ? m_fLeftLimitMin : m_fRightLimitMin, (pnlAxis.Height - 60), keyValuePair.Value.Data[markText.Index]) + 20f);
                            g.FillEllipse(markText.CircleBrush, new RectangleF(pointF.X - 3f, pointF.Y - 3f, 6f, 6f));

                            switch ((markText.PositionStyle == EMarkTextPositionStyle.Auto) ? ModMarkText.CalculateDirectionFromDataIndex(keyValuePair.Value.Data, markText.Index) : markText.PositionStyle)
                            {
                                case EMarkTextPositionStyle.Up:
                                    g.DrawString(markText.MarkedText, Font, markText.TextBrush, new RectangleF(pointF.X - 100f, pointF.Y - Font.Height - ModMarkText.MarkTextOffect, 200f, (Font.Height + 2)), m_sfMain);
                                    break;
                                case EMarkTextPositionStyle.Right:
                                    g.DrawString(markText.MarkedText, Font, markText.TextBrush, new RectangleF(pointF.X + ModMarkText.MarkTextOffect, pointF.Y - Font.Height, 100f, (Font.Height * 2)), m_sfLeft);
                                    break;
                                case EMarkTextPositionStyle.Down:
                                    g.DrawString(markText.MarkedText, Font, markText.TextBrush, new RectangleF(pointF.X - 100f, pointF.Y + ModMarkText.MarkTextOffect, 200f, (Font.Height + 2)), m_sfMain);
                                    break;
                                case EMarkTextPositionStyle.Left:
                                    g.DrawString(markText.MarkedText, Font, markText.TextBrush, new RectangleF(pointF.X - 100f, pointF.Y - Font.Height, (100 - ModMarkText.MarkTextOffect), (Font.Height * 2)), m_sfRight);
                                    break;
                            }
                        }
                    }
                }
            }

            //绘制直线标记
            foreach (ModMarkLine markLine in m_lstMarkLine)
            {
                PointF[] points = markLine.Points;

                if (points != null && points.Length > 1)
                {
                    PointF[] array = new PointF[markLine.Points.Length];
                    for (int markCount = 0; markCount < markLine.Points.Length; markCount++)
                    {
                        array[markCount].X = markLine.Points[markCount].X * m_fDataScaleRender;
                        array[markCount].Y = ChartOperate.ComputePaintLocationY(markLine.IsLeftFrame ? m_fLeftLimitMax : m_fRightLimitMax, markLine.IsLeftFrame ? m_fLeftLimitMin : m_fRightLimitMin, (pnlAxis.Height - 60), markLine.Points[markCount].Y) + 20f;

                        g.FillEllipse(markLine.CircleBrush, new RectangleF(array[markCount].X - 3f, array[markCount].Y - 3f, 6f, 6f));
                        EMarkTextPositionStyle markTextPositionStyle =
                            ModMarkText.CalculateDirectionFromDataIndex((
                                from m in markLine.Points
                                select m.Y).ToArray<float>(),
                                markCount);

                        if (markLine.Marks != null)
                        {
                            switch (markTextPositionStyle)
                            {
                                case EMarkTextPositionStyle.Up:
                                    g.DrawString(markLine.Marks[markCount], Font, markLine.TextBrush, new RectangleF(array[markCount].X - 100f, array[markCount].Y - Font.Height - ModMarkText.MarkTextOffect, 200f, (Font.Height + 2)), m_sfMain);
                                    break;
                                case EMarkTextPositionStyle.Right:
                                    g.DrawString(markLine.Marks[markCount], Font, markLine.TextBrush, new RectangleF(array[markCount].X + ModMarkText.MarkTextOffect, array[markCount].Y - Font.Height, 100f, (Font.Height * 2)), m_sfLeft);
                                    break;
                                case EMarkTextPositionStyle.Down:
                                    g.DrawString(markLine.Marks[markCount], Font, markLine.TextBrush, new RectangleF(array[markCount].X - 100f, array[markCount].Y + ModMarkText.MarkTextOffect, 200f, (Font.Height + 2)), m_sfMain);
                                    break;
                                case EMarkTextPositionStyle.Left:
                                    g.DrawString(markLine.Marks[markCount], Font, markLine.TextBrush, new RectangleF(array[markCount].X - 100f, array[markCount].Y - Font.Height, (100 - ModMarkText.MarkTextOffect), (Font.Height * 2)), m_sfRight);
                                    break;
                            }
                        }
                    }

                    if (markLine.IsLineClosed)
                    {
                        g.DrawLines(markLine.LinePen, array);
                        g.DrawLine(markLine.LinePen, array[0], array[array.Length - 1]);
                    }
                    else
                    {
                        g.DrawLines(markLine.LinePen, array);
                    }
                }
            }

            //绘制曲线
            foreach (ModCurveItem CurveItem in m_dicCurveItem.Values)
            {
                if (CurveItem.Visible && CurveItem.LineRenderVisiable)
                {
                    float[] curveItemData = CurveItem.Data;

                    if (curveItemData != null && curveItemData.Length > 1)
                    {
                        PointF[] array2 = new PointF[CurveItem.Data.Length];
                        for (int n = 0; n < CurveItem.Data.Length; n++)
                        {
                            array2[n].X = n * m_fDataScaleRender;
                            array2[n].Y = ChartOperate.ComputePaintLocationY(
                                CurveItem.IsLeftFrame ? m_fLeftLimitMax : m_fRightLimitMax,
                                CurveItem.IsLeftFrame ? m_fLeftLimitMin : m_fRightLimitMin,
                                (pnlAxis.Height - 60),
                                CurveItem.Data[n]) + 20f;
                        }

                        using (Pen pen = new Pen(CurveItem.LineColor, CurveItem.LineThickness))
                        {
                            if (CurveItem.IsSmoothCurve)
                            {
                                g.DrawCurve(pen, array2);
                            }
                            else
                            {
                                g.DrawLines(pen, array2);
                            }
                        }
                    }
                }
            }

            //前景选区
            for (int markCount = 0; markCount < m_lstMarkForeSection.Count; markCount++)
            {
                DrawMarkForeSection(g, m_lstMarkForeSection[markCount], Font);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 设置横向滚动条的位置信息
        /// </summary>
        /// <param name="e">滚动事件参数</param>
        public void CF_SetScrollPosition(ScrollEventArgs e)
        {
            pnlAxis.HorizontalScroll.Value = e.NewValue;
        }

        /// <summary>
        /// 添加左侧辅助线
        /// </summary>
        /// <param name="value">辅助线所在位置数值</param>
        public void CF_AddLeftAuxiliary(float value)
        {
            CF_AddLeftAuxiliary(value, m_colorCoordinate);
        }

        /// <summary>
        /// 添加左侧辅助线
        /// </summary>
        /// <param name="value">辅助线所在位置数值</param>
        /// <param name="lineColor">辅助线颜色</param>
        public void CF_AddLeftAuxiliary(float value, Color lineColor)
        {
            CF_AddLeftAuxiliary(value, lineColor, 1f);
        }

        /// <summary>
        /// 添加左侧辅助线
        /// </summary>
        /// <param name="value">辅助线所在位置数值</param>
        /// <param name="lineColor">辅助线颜色</param>
        /// <param name="lineThickness">辅助线宽度</param>
        public void CF_AddLeftAuxiliary(float value, Color lineColor, double lineThickness)
        {
            AddAuxiliary(value, lineColor, lineThickness, true);
        }

        /// <summary>
        /// 添加右侧辅助线
        /// </summary>
        /// <param name="value">辅助线所在位置数值</param>
        public void CF_AddRightAuxiliary(float value)
        {
            CF_AddRightAuxiliary(value, m_colorCoordinate);
        }

        /// <summary>
        /// 添加右侧辅助线
        /// </summary>
        /// <param name="value">辅助线所在位置数值</param>
        /// <param name="lineColor">辅助线颜色</param>
        public void CF_AddRightAuxiliary(float value, Color lineColor)
        {
            CF_AddRightAuxiliary(value, lineColor, 1f);
        }

        /// <summary>
        /// 添加右侧辅助线
        /// </summary>
        /// <param name="value">辅助线所在位置数值</param>
        /// <param name="lineColor">辅助线颜色</param>
        /// <param name="lineThickness">辅助线宽度</param>
        public void CF_AddRightAuxiliary(float value, Color lineColor, double lineThickness)
        {
            AddAuxiliary(value, lineColor, lineThickness, false);
        }

        /// <summary>
        /// 清除辅助线
        /// </summary>
        /// <param name="value">辅助线所在位置数值</param>
        public void CF_RemoveAuxiliary(double value)
        {
            bool isRemoved = false;
            for (int i = m_lstAuxiliaryLine.Count - 1; i >= 0; i--)
            {
                if (m_lstAuxiliaryLine[i].Value == value)
                {
                    m_lstAuxiliaryLine[i].Dispose();
                    m_lstAuxiliaryLine.RemoveAt(i);

                    isRemoved = true;
                }
            }

            if (isRemoved)
            {
                base.Invalidate();
            }
        }

        /// <summary>
        /// 清除所有辅助线
        /// </summary>
        public void CF_RemoveAllAuxiliary()
        {
            bool isRemoved = m_lstAuxiliaryLine.Count > 0;
            m_lstAuxiliaryLine.Clear();

            if (isRemoved)
            {
                base.Invalidate();
            }
        }

        /// <summary>
        /// 添加标记直线
        /// </summary>
        /// <param name="markLine">标记直线信息</param>
        public void CF_AddMarkLine(ModMarkLine markLine)
        {
            m_lstMarkLine.Add(markLine);
        }

        /// <summary>
        /// 清除标记直线
        /// </summary>
        /// <param name="markLine">标记直线信息</param>
        public void CF_RemoveMarkLine(ModMarkLine markLine)
        {
            m_lstMarkLine.Remove(markLine);
        }

        /// <summary>
        /// 清除所有标记直线
        /// </summary>
        public void CF_RemoveAllMarkLine()
        {
            m_lstMarkLine.Clear();
        }

        /// <summary>
        /// 添加标记文本
        /// </summary>
        /// <param name="markText">标记文本信息</param>
        public void CF_AddMarkText(ModMarkText markText)
        {
            m_lstMarkText.Add(markText);
        }

        /// <summary>
        /// 清除标记文本
        /// </summary>
        /// <param name="markText">标记文本信息</param>
        public void CF_RemoveMarkText(ModMarkText markText)
        {
            m_lstMarkText.Remove(markText);
        }

        /// <summary>
        /// 清除所有标记文本
        /// </summary>
        public void CF_RemoveAllMarkText()
        {
            m_lstMarkText.Clear();
        }

        /// <summary>
        /// 设置X轴时间数组
        /// </summary>
        /// <param name="times">时间数组</param>
        public void CF_SetDateTimes(DateTime[] times)
        {
            m_lstDateTime = new List<DateTime>(times);
        }

        /// <summary>
        /// 设置X轴缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        public void CF_SetScaleByXAxis(float scale)
        {
            m_fDataScaleRender = scale;
        }

        /// <summary>
        /// 添加左侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        public void CF_SetLeftCurve(string key, float[] data)
        {
            CF_SetLeftCurve(key, data, Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256)));
        }

        /// <summary>
        /// 添加左侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        /// <param name="lineColor">曲线颜色</param>
        public void CF_SetLeftCurve(string key, float[] data, Color lineColor)
        {
            CF_SetCurve(key, true, data, lineColor, 1f, false);
        }

        /// <summary>
        /// 添加左侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        /// <param name="lineColor">曲线颜色</param>
        /// <param name="isSmooth">是否平滑曲线</param>
        public void CF_SetLeftCurve(string key, float[] data, Color lineColor, bool isSmooth)
        {
            CF_SetCurve(key, true, data, lineColor, 1f, isSmooth);
        }

        /// <summary>
        /// 添加左侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        /// <param name="lineColor">曲线颜色</param>
        /// <param name="isSmooth">是否平滑曲线</param>
        /// <param name="renderFormat">显示格式</param>
        public void CF_SetLeftCurve(string key, float[] data, Color lineColor, bool isSmooth, string renderFormat)
        {
            CF_SetCurve(key, true, data, lineColor, 1f, isSmooth, renderFormat);
        }

        /// <summary>
        /// 添加右侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        public void CF_SetRightCurve(string key, float[] data)
        {
            CF_SetRightCurve(key, data, Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256)));
        }

        /// <summary>
        /// 添加右侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        /// <param name="lineColor">曲线颜色</param>
        public void CF_SetRightCurve(string key, float[] data, Color lineColor)
        {
            CF_SetCurve(key, false, data, lineColor, 1f, false);
        }

        /// <summary>
        /// 添加右侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        /// <param name="lineColor">曲线颜色</param>
        /// <param name="isSmooth">是否平滑曲线</param>
        public void CF_SetRightCurve(string key, float[] data, Color lineColor, bool isSmooth)
        {
            CF_SetCurve(key, false, data, lineColor, 1f, isSmooth);
        }

        /// <summary>
        /// 添加右侧坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="data">数据数组</param>
        /// <param name="lineColor">曲线颜色</param>
        /// <param name="isSmooth">是否平滑曲线</param>
        /// <param name="renderFormat">显示格式</param>
        public void CF_SetRightCurve(string key, float[] data, Color lineColor, bool isSmooth, string renderFormat)
        {
            CF_SetCurve(key, false, data, lineColor, 1f, isSmooth, renderFormat);
        }

        /// <summary>
        /// 添加坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="isLeft">是否为左侧坐标轴</param>
        /// <param name="data">数据数组</param>
        /// <param name="lineColor">曲线颜色</param>
        /// <param name="thickness">曲线宽度</param>
        /// <param name="isSmooth">是否平滑曲线</param>
        /// <param name="renderFormat">显示格式</param>
        public void CF_SetCurve(string key, bool isLeft, float[] data, Color lineColor, float thickness, bool isSmooth, string renderFormat = "{0}")
        {
            if (m_dicCurveItem.ContainsKey(key))
            {
                if (data == null)
                {
                    data = new float[0];
                }

                m_dicCurveItem[key].Data = data;
            }
            else
            {
                if (data == null)
                {
                    data = new float[0];
                }

                m_dicCurveItem.Add(key, new ModCurveItem
                {
                    Data = data,
                    LineThickness = thickness,
                    LineColor = lineColor,
                    IsLeftFrame = isLeft,
                    IsSmoothCurve = isSmooth,
                    RenderFormat = renderFormat
                });

                m_lstCurveItem.Add(m_dicCurveItem[key]);
            }
            CalculateCurveDataMax();
        }

        /// <summary>
        /// 删除坐标轴曲线
        /// </summary>
        /// <param name="key">曲线标识</param>
        public void CF_RemoveCurve(string key)
        {
            if (m_dicCurveItem.ContainsKey(key))
            {
                m_lstCurveItem.Remove(m_dicCurveItem[key]);
                m_dicCurveItem.Remove(key);
            }

            if (m_dicCurveItem.Count == 0)
            {
                m_lstDateTime = new List<DateTime>(0);
            }

            CalculateCurveDataMax();
        }

        /// <summary>
        /// 删除所有坐标轴曲线
        /// </summary>
        public void CF_RemoveAllCurve()
        {
            m_dicCurveItem.Clear();
            m_lstCurveItem.Clear();
            m_lstMarkBackSection.Clear();
            m_lstMarkForeSection.Clear();
            m_lstMarkForeSectionTemp.Clear();
            m_lstMarkForeActiveSection.Clear();
            m_lstMarkText.Clear();
            m_lstMarkLine.Clear();

            if (m_dicCurveItem.Count == 0)
            {
                m_lstDateTime = new List<DateTime>(0);
            }

            CalculateCurveDataMax();
        }

        /// <summary>
        /// 设置曲线可见性
        /// </summary>
        /// <param name="key">曲线标识</param>
        /// <param name="visible">可见性</param>
        public void CF_SetCurveVisible(string key, bool visible)
        {
            if (m_dicCurveItem.ContainsKey(key))
            {
                ModCurveItem CurveItem = m_dicCurveItem[key];
                CurveItem.Visible = visible;
            }
        }

        /// <summary>
        /// 设置曲线可见性
        /// </summary>
        /// <param name="keys">曲线标识集</param>
        /// <param name="visible">可见性</param>
        public void CF_SetCurveVisible(string[] keys, bool visible)
        {
            foreach (string key in keys)
            {
                if (m_dicCurveItem.ContainsKey(key))
                {
                    ModCurveItem CurveItem = m_dicCurveItem[key];
                    CurveItem.Visible = visible;
                }
            }
        }

        /// <summary>
        /// 刷新图表
        /// </summary>
        public void CF_RenderCurveUI()
        {
            int width = (int)(m_nDataCount * m_fDataScaleRender) + 200;
            if (width < pnlAxis.Width)
            {
                width = pnlAxis.Width;
            }

            int height = pnlAxis.Height - 18;
            if (height < 10)
            {
                height = 10;
            }

            Bitmap image = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.Clear(BackColor);
                GetRenderCurveMain(graphics, width, height);
            }

            Image image2 = picChart.Image;
            if (image2 != null)
            {
                image2.Dispose();
            }
            picChart.Image = image;
            base.Invalidate();
            m_isShowTextInfomation = false;
        }

        /// <summary>
        /// 设置曲线鼠标位置
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void CF_SetCurveMousePosition(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                m_isMouseOnPicture = false;
            }
            else
            {
                m_isMouseOnPicture = true;
            }

            m_ptMouseLocation.X = x;
            m_ptMouseLocation.Y = y;
            picChart.Invalidate();
        }

        /// <summary>
        /// 添加背景标记区间
        /// </summary>
        /// <param name="markBackSection">背景标记区间信息</param>
        public void CF_AddMarkBackSection(ModMarkBackSection markBackSection)
        {
            m_lstMarkBackSection.Add(markBackSection);
        }

        /// <summary>
        /// 添加前景标记区间
        /// </summary>
        /// <param name="markForeSection">前景标记区间信息</param>
        public void CF_AddMarkForeSection(ModMarkForeSection markForeSection)
        {
            m_lstMarkForeSection.Add(markForeSection);
        }

        /// <summary>
        /// 添加触发标记区间
        /// </summary>
        /// <param name="markActiveSection">前景标记区间信息</param>
        public void CF_AddMarkActiveSection(ModMarkForeSection markActiveSection)
        {
            m_lstMarkForeActiveSection.Add(markActiveSection);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <returns>位图数据</returns>
        public Bitmap CF_SaveToBitmap()
        {
            Bitmap result;
            if (m_isShowTextInfomation)
            {
                Bitmap bitmap = new Bitmap(base.Width, base.Height);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    graphics.Clear(BackColor);
                    PaintMain(graphics);
                    graphics.TranslateTransform(43f, 29f);
                    int width = (pnlAxis.Width > 10) ? pnlAxis.Width : 1;
                    int height = (pnlAxis.Height > 10) ? pnlAxis.Height : 10;
                    PaintFromString(graphics, Text, width, height);
                    graphics.TranslateTransform(-43f, -29f);
                }
                result = bitmap;
            }
            else
            {
                Bitmap bitmap = new Bitmap(picChart.Width + 86, base.Height);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    graphics.Clear(BackColor);
                    DrawCoordinate(graphics, true, 3f, 29f, m_fLeftLimitMax, m_fLeftLimitMin, m_sfRight, m_strLeftUnit);

                    if (m_isRederRightCoordinate)
                    {
                        DrawCoordinate(graphics, false, (bitmap.Width - 43), 29f, m_fRightLimitMax, m_fRightLimitMin, m_sfLeft, m_strRightUnit);
                    }
                    PaintLegend(graphics);

                    int width = (int)(m_nDataCount * m_fDataScaleRender) + 200;
                    if (width < pnlAxis.Width)
                    {
                        width = pnlAxis.Width;
                    }

                    int height = pnlAxis.Height - 18;
                    if (height < 10)
                    {
                        height = 10;
                    }

                    graphics.TranslateTransform(43f, 29f);
                    GetRenderCurveMain(graphics, width, height);
                    graphics.TranslateTransform(-43f, -29f);
                }
                result = bitmap;
            }
            return result;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 实例化一个默认的曲线控件
        /// </summary>
        public ChartCurve()
        {
            InitializeComponent();

            base.SetStyle(ControlStyles.UserPaint |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint,
                true);

            m_sfMain.Alignment = StringAlignment.Center;
            m_sfMain.LineAlignment = StringAlignment.Center;
            m_sfTop.Alignment = StringAlignment.Center;
            m_sfTop.LineAlignment = StringAlignment.Near;
            m_sfLeft.LineAlignment = StringAlignment.Center;
            m_sfLeft.Alignment = StringAlignment.Near;
            m_sfRight.LineAlignment = StringAlignment.Center;
            m_sfRight.Alignment = StringAlignment.Far;

            m_penCoordinateDash.DashPattern = new float[] { 5f, 5f };
            m_penCoordinateDash.DashStyle = DashStyle.Custom;

            base.BackColor = Color.FromArgb(46, 46, 46);
            base.ForeColor = Color.Yellow;
        }
        #endregion

        #region 重写事件
        /// <summary>
        /// 引发 System.Windows.Forms.Control.DockChanged 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            EvtCurveChart_SizeChanged(this, e);
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.Paint 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            PaintMain(graphics);
            base.OnPaint(e);
        }
        #endregion

        #region 窗体事件
        private void EvtCurveChart_Load(object sender, EventArgs e)
        {
            if (m_isShowTextInfomation)
            {
                Image image = picChart.Image;
                if (image != null)
                {
                    image.Dispose();
                }

                picChart.Image = GetBitmapFromString(Text);
            }
        }

        private void EvtCurveChart_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (KeyValuePair<string, ModCurveItem> keyValuePair in m_dicCurveItem)
            {
                if (keyValuePair.Value.TitleRegion.Contains(e.Location))
                {
                    keyValuePair.Value.LineRenderVisiable = !keyValuePair.Value.LineRenderVisiable;
                    CF_RenderCurveUI();
                    break;
                }
            }
        }

        private void EvtCurveChart_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (KeyValuePair<string, ModCurveItem> keyValuePair in m_dicCurveItem)
            {
                if (keyValuePair.Value.TitleRegion.Contains(e.Location))
                {
                    Cursor = Cursors.Hand;
                    return;
                }
            }

            Cursor = Cursors.Arrow;
        }

        private void EvtCurveChart_SizeChanged(object sender, EventArgs e)
        {
            if (m_isShowTextInfomation)
            {
                Image image = picChart.Image;
                if (image != null)
                {
                    image.Dispose();
                }
                picChart.Image = GetBitmapFromString(Text);
                base.Invalidate();
            }
            else
            {
                CF_RenderCurveUI();
            }
        }
        #endregion

        #region 坐标轴事件
        private void PnlAxis_Scroll(object sender, ScrollEventArgs e)
        {
            OnScroll(e);
        }
        #endregion

        #region 图标事件
        private void PicChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_lstMarkForeSectionTemp.Clear();
                m_nRowBetweenStart = -1;
                m_nRowBetweenEnd = -1;
                m_nRowBetweenHeight = -1;
                m_nRowBetweenStartHeight = -1;
                picChart.Invalidate();
            }
            else
            {
                m_IsMouseLeftDown = true;
                m_nRowBetweenStart = Convert.ToInt32(((e.X >= 0) ? e.X : (65536 + e.X)) / m_fDataScaleRender);
                m_nRowBetweenStartHeight = e.Y;
                m_nRowBetweenHeight = e.Y;
            }
        }

        private void PicChart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ModMarkForeSection item = new ModMarkForeSection
                {
                    StartIndex = m_nRowBetweenStart,
                    EndIndex = m_nRowBetweenEnd,
                    Height = m_nRowBetweenHeight,
                    StartHeight = m_nRowBetweenStartHeight,
                    LinePen = m_penMarkLine,
                    FontBrush = m_brushMarkText
                };

                m_lstMarkForeSectionTemp.Add(item);
                m_IsMouseLeftDown = false;
                m_nRowBetweenStart = -1;
                m_nRowBetweenEnd = -1;
                m_nRowBetweenHeight = -1;
                m_nRowBetweenStartHeight = -1;
                picChart.Invalidate();
            }
        }

        private void PicChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_isMouseOnPicture && !m_isShowTextInfomation)
            {
                m_ptMouseLocation = e.Location;

                if (m_ptMouseLocation.X < 0)
                {
                    m_ptMouseLocation.X = 65536 + m_ptMouseLocation.X;
                }

                CE_OnCurveMouseMove?.Invoke(this, m_ptMouseLocation.X, m_ptMouseLocation.Y);
                picChart.Invalidate();
            }
        }

        private void PicChart_MouseEnter(object sender, EventArgs e)
        {
            m_isMouseOnPicture = true;
        }

        private void PicChart_MouseLeave(object sender, EventArgs e)
        {
            m_isMouseOnPicture = false;
            CE_OnCurveMouseMove?.Invoke(this, -1, -1);
            picChart.Invalidate();
        }

        private void PicChart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!m_isShowTextInfomation)
            {
                int num = Convert.ToInt32(((e.X >= 0) ? e.X : (65536 + e.X)) / m_fDataScaleRender);

                if (num >= 0 && num < m_lstDateTime.Count)
                {
                    CE_OnCurveDoubleClick?.Invoke(this, num, m_lstDateTime[num]);
                }
            }
        }

        private void PicChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (Font font = new Font(Font.FontFamily, 12f))
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(220, Color.FromArgb(52, 52, 52))))
                {
                    int num = Convert.ToInt32(m_ptMouseLocation.X / m_fDataScaleRender);
                    for (int i = 0; i < m_lstMarkForeSectionTemp.Count; i++)
                    {
                        DrawMarkForeSection(graphics, m_lstMarkForeSectionTemp[i], font);
                    }

                    if (m_IsMouseLeftDown &&
                        m_nRowBetweenStart != -1 &
                        num < m_nDataCount)
                    {
                        m_nRowBetweenEnd = Convert.ToInt32(m_ptMouseLocation.X / m_fDataScaleRender);
                        m_nRowBetweenHeight = m_ptMouseLocation.Y;
                    }

                    ModMarkForeSection markForeSection = new ModMarkForeSection
                    {
                        StartIndex = m_nRowBetweenStart,
                        EndIndex = m_nRowBetweenEnd,
                        Height = m_nRowBetweenHeight,
                        StartHeight = m_nRowBetweenStartHeight,
                        LinePen = m_penMarkLine,
                        FontBrush = m_brushMarkText
                    };
                    DrawMarkForeSection(graphics, markForeSection, font);

                    for (int j = 0; j < m_lstMarkForeActiveSection.Count; j++)
                    {
                        if (num >= m_lstMarkForeActiveSection[j].StartIndex &&
                            num <= m_lstMarkForeActiveSection[j].EndIndex)
                        {
                            DrawMarkForeSection(graphics, m_lstMarkForeActiveSection[j], font);
                        }
                    }

                    if (m_isMouseOnPicture && !m_isShowTextInfomation)
                    {
                        graphics.DrawLine(m_PenMoveLine, m_ptMouseLocation.X, 15, m_ptMouseLocation.X, pnlAxis.Height - 38);

                        if (num >= m_nDataCount || num < 0)
                        {
                            return;
                        }

                        //查询可见曲线
                        Dictionary<string, ModCurveItem> dicVisiable = new Dictionary<string, ModCurveItem>();
                        foreach (string key in m_dicCurveItem.Keys)
                        {
                            if (m_dicCurveItem[key].Visible)
                            {
                                dicVisiable.Add(key, m_dicCurveItem[key]);
                            }
                        }

                        //绘制数值提示
                        if (dicVisiable.Count != 0 && CP_ShowDataTip)
                        {
                            bool isleft = false;
                            int xBase = m_ptMouseLocation.X;
                            int yBase = m_ptMouseLocation.Y - dicVisiable.Count * 20 - 20;

                            if (xBase + 5 + m_nDataTipWidth > pnlAxis.Width)
                            {
                                isleft = true;
                                xBase = m_ptMouseLocation.X - m_nDataTipWidth;
                            }
                            if (yBase < 5)
                            {
                                yBase = 5;
                            }

                            Rectangle rect1 = new Rectangle(isleft ? xBase - 5 : xBase + 5, yBase - 5, m_nDataTipWidth, dicVisiable.Count * 20 + 10);

                            graphics.FillRectangle(brush, rect1);
                            graphics.DrawRectangle(Pens.HotPink, rect1);

                            foreach (KeyValuePair<string, ModCurveItem> keyValuePair in dicVisiable)
                            {
                                Rectangle r = new Rectangle(isleft ? xBase - 2 : xBase + 8, yBase, m_nDataTipWidth - 6, 20);
                                graphics.DrawString(keyValuePair.Key, font, Brushes.Cyan, r, m_sfLeft);

                                if (num < keyValuePair.Value.Data.Length)
                                {
                                    graphics.DrawString(string.Format(keyValuePair.Value.RenderFormat, keyValuePair.Value.Data[num]), font, Brushes.Cyan, r, m_sfRight);
                                }
                                yBase += 20;
                            }
                        }

                        //绘制前景选取
                        int height2 = m_ptMouseLocation.Y + 25;
                        for (int k = 0; k < m_lstMarkForeActiveSection.Count; k++)
                        {
                            if (num >= m_lstMarkForeActiveSection[k].StartIndex &&
                                num <= m_lstMarkForeActiveSection[k].EndIndex)
                            {
                                double baseHeight = 0f;
                                foreach (KeyValuePair<string, string> keyValuePair2 in m_lstMarkForeActiveSection[k].CursorTexts)
                                {
                                    baseHeight += graphics.MeasureString(keyValuePair2.Key + " : " + keyValuePair2.Value, Font, 244).Height + 8f;
                                }
                                Rectangle rect2 = new Rectangle(m_ptMouseLocation.X + 5, height2 - 5, 250, (int)baseHeight + 10);
                                graphics.FillRectangle(brush, rect2);
                                graphics.DrawRectangle(Pens.HotPink, rect2);
                                foreach (KeyValuePair<string, string> keyValuePair3 in m_lstMarkForeActiveSection[k].CursorTexts)
                                {
                                    baseHeight = graphics.MeasureString(keyValuePair3.Key + " : " + keyValuePair3.Value, Font, 244).Height;
                                    Rectangle r2 = new Rectangle(m_ptMouseLocation.X + 8, height2, 244, 200);
                                    graphics.DrawString(keyValuePair3.Key + " : " + keyValuePair3.Value, font, Brushes.Yellow, r2, m_sfDefault);
                                    height2 += (int)baseHeight + 8;
                                }
                                break;
                            }
                        }

                        //下方的时间提示
                        Rectangle rectangle = new Rectangle(m_ptMouseLocation.X - 50, pnlAxis.Height - 38, 100, 18);
                        if (num < m_lstDateTime.Count)
                        {
                            graphics.FillRectangle(brush, rectangle);
                            graphics.DrawRectangle(Pens.HotPink, rectangle);
                            graphics.DrawString(m_lstDateTime[num].ToString("HH:mm:ss"), Font, Brushes.Cyan, rectangle, m_sfMain);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
