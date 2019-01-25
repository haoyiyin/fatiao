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
        int x;//字段
        int y;//字段
        int xx;//字段
        int yy;//字段
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
                xx = Math.Abs(e.X);//最后的X坐标
                yy = Math.Abs(e.Y);//最后的Y坐标
                Graphics g = pictureBox1.CreateGraphics();//为控件创建 System.Drawing.Graphics。
                rect = new Rectangle(x, y, xx - x, yy - y);//  Rectangle(起始x, 起始y, 截取宽度xx - x, 截取高度yy - y) 最后坐标xx 减去 起始x 得到 宽度。      用指定的位置和大小初始化 System.Drawing.Rectangle 类的新实例。
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
                if (xx > x || yy > y) //判断 宽度 或高度 是否为0
                {

                }
                Dispose();//释放资源
                Close();//关闭窗体
            }
            if (e.KeyCode == Keys.Space && e.Control)
            {
                Dispose();//释放资源
                Close();//关闭窗体
                Delay(200);
                if (xx > x || yy > y) //判断 宽度 或高度 是否为0
                {
                    winapp.小程序窗口.fatiao.GetClientRect(winapp.小程序窗口.fatiao.GetPointWindow(x, y), out object x1, out object y1, out object x2, out object y2);
                    x = x - (int)x1;
                    y = y - (int)y1;
                    xx = xx - (int)x1;
                    yy = yy - (int)y1;


                }
            }

            if (e.KeyCode == Keys.Z)
            {

                Dispose();//释放资源
                Close();//关闭窗体
            }

        }
    }
}
