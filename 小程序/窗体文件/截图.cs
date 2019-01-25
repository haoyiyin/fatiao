using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 截图 : Form
    {
        string path = winapp.小程序窗口.我的文档路径 + @"\Winapp" +@"\素材库\";//设置打开路径的目录
        Bitmap bit;//字段
        int x;//字段
        int y;//字段
        int xx;//字段
        int yy;//字段
        Pen pen = new Pen(Color.CornflowerBlue, 3);//定义用于绘制直线和曲线的对象。 用指定的 颜色 和 宽度 初始化
        Rectangle rect = new Rectangle();//存储一组整数，共四个，表示一个矩形的位置和大小
        string tpName = "fatiao";//图片保存名字

        public 截图()
        {
            InitializeComponent();
            TransparencyKey = BackColor;//背景透明(鼠标穿透)
        }

        弹幕 弹幕内容 = new 弹幕();


        private void 截图_Load(object sender, EventArgs e)
        {

            if (!System.IO.Directory.Exists(@".\素材库"))//不存在就创建
            {
                System.IO.Directory.CreateDirectory(@".\素材库");
            }
            Cursor = Cursors.Cross;//鼠标为 十字线光标.


            int Width = (int)(Screen.PrimaryScreen.Bounds.Width * 分辨率缩放.ScaleX);//获取主窗体 宽度
            int Height = (int)(Screen.PrimaryScreen.Bounds.Height * 分辨率缩放.ScaleX);//获取主窗体 高度

            bit = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);//获取桌面图

            Graphics g = Graphics.FromImage(bit);//把图画出来
            g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(Width, Height));//绘制图片，g.CopyFromScreen(起点的屏幕坐标X,起点的屏幕坐标Y,0,0,new Size(窗体的宽度,窗体的高度))

            pictureBox1.Image = bit;//将图片显示在 pictureBox1  控件上

            switch (winapp.小程序窗口.参数窗口)
            {
                case "用户输入窗":
                    弹幕内容.label1.Text = "左键可反复绘制,右键取消截图\r\n\r\n空格确定截图绘制";
                    break;
                case "询问输入":
                    弹幕内容.label1.Text = "左键可反复绘制,右键取消截图\r\n\r\n空格确定截图绘制";
                    break;
                default:
                    弹幕内容.label1.Text = "左键可反复绘制,右键取消截图\r\n\r\n空格确定截图绘制\r\n\r\nCtrl+空格确定连续截图";
                    break;
            }
            弹幕内容.Show();

            winapp.小程序窗口.fatiao.BindWindow(winapp.小程序窗口.fatiao.GetSpecialWindow(0), "normal", "normal", "normal", 0);//前台基于窗口
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//是否按下鼠标右键
            {
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
                if (rect.Width == 0 || rect.Height == 0) //判断 宽度 或高度 是否为0
                {
                    Close();//关闭窗体
                }
            }
        }

        private void 截图_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !e.Control)
            {
/*                x = (int)(x * (double)分辨率缩放.ScaleX);
                y = (int)(y * (double)分辨率缩放.ScaleY);
                xx = (int)(xx * (double)分辨率缩放.ScaleX);
                yy = (int)(yy * (double)分辨率缩放.ScaleY);*/
                if (xx > x || yy > y) //判断 宽度 或高度 是否为0
                {
                    tpName += DateTime.Now.ToString("yyyyMMddHHmmss");//获取当前时间为 文件名
                    winapp.小程序窗口.fatiao.Capture((int)((x + 3) * 分辨率缩放.ScaleX), (int)((y + 3) * 分辨率缩放.ScaleX), (int)((xx - 3) * 分辨率缩放.ScaleX), (int)((yy - 3) * 分辨率缩放.ScaleX), path + tpName + ".bmp");
                    switch (winapp.小程序窗口.参数窗口)
                    {
                        case "查找图片":
                            Image img = Image.FromFile(path + tpName + ".bmp");
                            Image bmp = new Bitmap(img);
                            img.Dispose();
                            break;
                        case "用户输入窗":
                            winapp.小程序窗口.参数窗口 =  tpName + ".bmp";
                            break;
                        case "询问输入":
                            winapp.小程序窗口.参数窗口 = tpName + ".bmp";
                            break;
                    }
                }
                Close();//关闭窗体
            }

            if (e.KeyCode == Keys.Space && e.Control)//连续截图
            {
                if (xx > x || yy > y) //判断 宽度 或高度 是否为0
                {
                    tpName += DateTime.Now.ToString("yyyyMMddHHmmss");//获取当前时间为 文件名
                    winapp.小程序窗口.fatiao.Capture((int)((x + 3) * 分辨率缩放.ScaleX), (int)((y + 3) * 分辨率缩放.ScaleX), (int)((xx - 3) * 分辨率缩放.ScaleX), (int)((yy - 3) * 分辨率缩放.ScaleX), path + tpName + ".bmp");
                    switch (winapp.小程序窗口.参数窗口)
                    {
                        case "查找图片":


                            弹幕内容.label1.Text = "保存截图成功\r\n\r\n右键退出连续截图";
                            弹幕内容.Show();
                            break;
                    }
                }
            }

        }

        private void 截图_FormClosing(object sender, FormClosingEventArgs e)
        {
            bit.Dispose();
            Dispose();//释放资源
            Close();//关闭窗体
        }
    }
}
