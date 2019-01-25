using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 弹幕 : Form
    {
        public 弹幕()
        {
            InitializeComponent();
        }

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        private void 弹幕_Load(object sender, EventArgs e)
        {
            TransparencyKey = BackColor;//背景透明(鼠标穿透)
            label1.ForeColor = BackColor;//文字不显示
        }

        GraphicsPath GetStringPath(string s, float dpi, RectangleF rect, Font font, StringFormat format)
        {
            GraphicsPath path = new GraphicsPath();
            // Convert font size into appropriate coordinates
            float emSize = dpi * font.SizeInPoints / 72;
            path.AddString(s, font.FontFamily, (int)font.Style, emSize, rect, format);

            return path;
        }
        private void 弹幕_VisibleChanged(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                if (Visible == true)
            {
                Delay(2000);
                Hide();
            }
            }), null);
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            string text = label1.Text;
            if (text == null || text.Length < 1)
                return;

            Graphics g = e.Graphics;//相当于画笔
            Font f = Font;//设置的字体
            RectangleF rect = label1.ClientRectangle;//获取控件的工作区
            //计算垂直偏移量
            float dy = (label1.Height - g.MeasureString(text, Font).Height) / 2.0f;
            //计算水平偏移
            float dx = (label1.Width - g.MeasureString(text, Font).Width) / 2.0f;
            dx = 5;
            //将文字显示的工作区偏移dx,dy，实现文字居中、水平居中、垂直居中
            rect.Offset(dx, dy);
            //文本布局信息 详细功能请查看注释 这里可有可无
            StringFormat format = StringFormat.GenericTypographic;
            float dpi = g.DpiY;
            using (GraphicsPath path = GetStringPath(text, dpi, rect, f, format))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;//设置字体质量

                g.DrawPath(Pens.Black, path);//绘制轮廓（描边）

                g.FillPath(Brushes.White, path);//填充轮廓（填充）
            }
        }
    }
}
