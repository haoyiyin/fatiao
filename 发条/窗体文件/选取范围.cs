using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 选取范围 : Form
    {
        Bitmap bit;//字段
        Bitmap copy;//字段
        int x, y, x2, y2;
        Pen pen = new Pen(Color.CornflowerBlue, 3);//定义用于绘制直线和曲线的对象。 用指定的 颜色 和 宽度 初始化
        Rectangle rect = new Rectangle();//存储一组整数，共四个，表示一个矩形的位置和大小
        public 选取范围()
        {
            InitializeComponent();
            TransparencyKey = BackColor;//背景透明(鼠标穿透)
        }

        弹幕 弹幕内容 = new 弹幕();

        private void 选取范围_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.Cross;//鼠标为 十字线光标.

            int Width = (int)(Screen.PrimaryScreen.Bounds.Width * 分辨率缩放.ScaleX);//获取主窗体 宽度
            int Height = (int)(Screen.PrimaryScreen.Bounds.Height * 分辨率缩放.ScaleX);//获取主窗体 高度

            bit = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);//获取桌面图

            Graphics g = Graphics.FromImage(bit);//把图画出来
            g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(Width, Height));//绘制图片，g.CopyFromScreen(起点的屏幕坐标X,起点的屏幕坐标Y,0,0,new Size(窗体的宽度,窗体的高度))

            pictureBox1.Image = bit;//将图片显示在 pictureBox1  控件上
            弹幕内容.label1.Text = "左键可反复绘制，右键取消绘制\r\n\r\n空格确定桌面内坐标绘制\r\n\r\nCtrl+空格确定窗口内坐标绘制\r\n\r\n“z”键开启动态坐标";
            弹幕内容.Show();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//是否按下鼠标右键
            {
                Dispose();//是否资源
                Close();//关闭当前窗体
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = Math.Abs(e.X);// e.X 获取鼠标在产生鼠标事件时的 x 坐标。绝对值     区域截图 开始 鼠标按下的X坐标
            y = Math.Abs(e.Y);// e.Y 获取鼠标在产生鼠标事件时的 y 坐标。绝对值     区域截图 开始 鼠标按下的Y坐标
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//判断 鼠标 是否 按下左键  这是 拖拽出 区域截图的 时候最后松开鼠标的 坐标
            {
                x2 = Math.Abs(e.X);//最后的X坐标
                y2 = Math.Abs(e.Y);//最后的Y坐标
                Graphics g = pictureBox1.CreateGraphics();//为控件创建 System.Drawing.Graphics。
                rect = new Rectangle(x, y, x2 - x, y2 - y);//  Rectangle(起始x, 起始y, 截取宽度xx - x, 截取高度yy - y) 最后坐标xx 减去 起始x 得到 宽度。      用指定的位置和大小初始化 System.Drawing.Rectangle 类的新实例。
                Refresh();//强制控件使其工作区无效并立即重绘自己和任何子控件。
                g.DrawRectangle(pen, rect);//绘制由 System.Drawing.Rectangle 结构指定的矩形。
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//判断 鼠标 是否 按下左键
            {
                copy = (Bitmap)bit.Clone();//创建此 System.Drawing.Image 的一个精确副本。
                if (rect.Width == 0 || rect.Height == 0) //判断 宽度 或高度 是否为0
                {
                    Dispose();//释放资源
                    Close();//关闭窗体
                }
            }
        }

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        private void 选取范围_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !e.Control)
            {
                if (x2 > x || y2 > y) //判断 宽度 或高度 是否为0
                {
                    switch (发条.发条窗口.参数窗口)
                    {
                        case "查找图片":
                            发条.发条窗口.查找图片.textBox4.Text = x.ToString();
                            发条.发条窗口.查找图片.textBox9.Text = y.ToString();
                            发条.发条窗口.查找图片.textBox5.Text = x2.ToString();
                            发条.发条窗口.查找图片.textBox10.Text = y2.ToString();
                            break;
                        case "判断画面":
                            发条.发条窗口.判断画面.textBox4.Text = x.ToString();
                            发条.发条窗口.判断画面.textBox7.Text = y.ToString();
                            发条.发条窗口.判断画面.textBox5.Text = x2.ToString();
                            发条.发条窗口.判断画面.textBox8.Text = y2.ToString();
                            break;
                        case "查找颜色":
                            发条.发条窗口.查找颜色.textBox4.Text = x.ToString();
                            发条.发条窗口.查找颜色.textBox9.Text = y.ToString();
                            发条.发条窗口.查找颜色.textBox5.Text = x2.ToString();
                            发条.发条窗口.查找颜色.textBox10.Text = y2.ToString();
                            break;
                        case "鼠标操作":
                            发条.发条窗口.鼠标操作.textBox4.Text = x.ToString();
                            发条.发条窗口.鼠标操作.textBox9.Text = y.ToString();
                            发条.发条窗口.鼠标操作.textBox5.Text = x2.ToString();
                            发条.发条窗口.鼠标操作.textBox6.Text = y2.ToString();
                            break;
                    }
                }
                Dispose();//释放资源
                Close();//关闭窗体
            }
            if (e.KeyCode == Keys.Space && e.Control)
            {
                Dispose();//释放资源
                Close();//关闭窗体
                Delay(200);
                if (x2 > x || y2 > y) //判断 宽度 或高度 是否为0
                {
                    int x3, y3, x4, y4;
                    发条.发条窗口.fatiao.GetClientRect(发条.发条窗口.fatiao.GetPointWindow(x, y), out object x5, out object y5, out object x6, out object y6);
                    x3 = x - (int)x5;
                    y3 = y - (int)y5;
                    x4 = x2 - (int)x5;
                    y4 = y2 - (int)y5;

                    switch (发条.发条窗口.参数窗口)
                    {
                        case "查找图片":
                            发条.发条窗口.查找图片.textBox4.Text = x3.ToString();
                            发条.发条窗口.查找图片.textBox9.Text = y3.ToString();
                            发条.发条窗口.查找图片.textBox5.Text = x4.ToString();
                            发条.发条窗口.查找图片.textBox10.Text = y4.ToString();
                            break;
                        case "判断画面":
                            发条.发条窗口.fatiao.GetWindowRect(发条.发条窗口.fatiao.GetPointWindow(x, y), out  x5, out  y5, out  x6, out  y6);
                            x -= (int)x5;
                            y -= (int)y5;
                            x2 -= (int)x5;
                            y2 -= (int)y5;
                            发条.发条窗口.判断画面.textBox4.Text = x.ToString();
                            发条.发条窗口.判断画面.textBox7.Text = y.ToString();
                            发条.发条窗口.判断画面.textBox5.Text = x2.ToString();
                            发条.发条窗口.判断画面.textBox8.Text = y2.ToString();
                            break;
                        case "查找颜色":
                            发条.发条窗口.查找颜色.textBox4.Text = x3.ToString();
                            发条.发条窗口.查找颜色.textBox9.Text = y3.ToString();
                            发条.发条窗口.查找颜色.textBox5.Text = x4.ToString();
                            发条.发条窗口.查找颜色.textBox10.Text = y4.ToString();
                            break;
                        case "鼠标操作":
                            发条.发条窗口.鼠标操作.textBox4.Text = x3.ToString();
                            发条.发条窗口.鼠标操作.textBox9.Text = y3.ToString();
                            发条.发条窗口.鼠标操作.textBox5.Text = x4.ToString();
                            发条.发条窗口.鼠标操作.textBox6.Text = y4.ToString();
                            break;
                    }
                }
            }

            if (e.KeyCode == Keys.Z)
            {
                switch (发条.发条窗口.参数窗口)
                {
                    case "查找图片":
                        发条.发条窗口.查找图片.坐标与颜色操作();
                        break;
                    case "判断画面":
                        发条.发条窗口.判断画面.坐标与颜色操作();
                        break;
                    case "查找颜色":
                        发条.发条窗口.查找颜色.坐标与颜色操作();
                        break;
                    case "鼠标操作":
                        发条.发条窗口.鼠标操作.坐标与颜色操作();
                        break;
                }
                Dispose();//释放资源
                Close();//关闭窗体
            }

        }
    }
}
