using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 悬浮窗 : Form
    {
        public static 快捷命令 快捷命令 = new 快捷命令();

        public 悬浮窗()
        {
            InitializeComponent();
            int y = Screen.PrimaryScreen.WorkingArea.Width;
            int x = Size.Width;
            if (x > y)
            {
                x = 0;
            }
            else
            {
               x = y - x-x;
            }
            Point p = new Point(x, Size.Height);
            Location = p;
        }
        private Point formPoint = new Point();

        private void 悬浮窗_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-formPoint.X, -formPoint.Y);
                Location = myPosittion;
            }
        }

        public static void Delay(int milliSecond)//异步延时子进程函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//退出
        {
            winapp.小程序窗口.悬浮窗开关 = false;//悬浮窗未显示
            winapp.悬浮窗.Hide();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)//退出
        {
             winapp.小程序窗口.退出发条应用(sender, e);
        }

        private void 悬浮窗_DoubleClick(object sender, EventArgs e)//双击显示/隐藏
        {
            if (winapp.小程序窗口.WindowState == FormWindowState.Minimized)//判断当前 窗体是否为 最小化
            {
                winapp.小程序窗口.Show();
                winapp.小程序窗口.WindowState = FormWindowState.Normal;//恢复窗体默认大小
                winapp.小程序窗口.Activate();//激活窗体，并给予焦点。
            }
            else
            {
                winapp.小程序窗口.WindowState = FormWindowState.Minimized;//最小化
                winapp.小程序窗口.Hide();
            }
        }
        private void contextMenuStrip4_Click(object sender, EventArgs e)
        {
            StreamReader _rstream = null;
            string 快捷命令名称="";
            foreach (ToolStripItem items in contextMenuStrip4.Items)
            {
                if (items.Selected == true)
                    快捷命令名称 = items.ToString();
            }

            快捷命令.停止作用 = false;
            快捷命令.暂停作用 = false;
            winapp.小程序窗口.listBox6.Items.Clear();
            _rstream = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Command\" + 快捷命令名称, Encoding.UTF8);//读取流程
            string line;
            while ((line = _rstream.ReadLine()) != null)
            {
                winapp.小程序窗口.listBox6.Items.Add(line);
            }
            _rstream.Close();//读取快捷命令流程
            快捷命令.快捷命令运行();
        }

    }
}
