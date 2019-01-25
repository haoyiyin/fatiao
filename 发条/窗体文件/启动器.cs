using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace fatiao.窗体文件
{
    public partial class 启动器 : Form
    {
        public 启动器()
        {
            InitializeComponent();
        }

        private void 启动器_Load(object sender, EventArgs e)
        {
            TransparencyKey = BackColor;//背景透明(鼠标穿透)
        }

        private void 启动器_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Point 鼠标坐标 = MousePosition;
                鼠标坐标.X -= (Size.Width / 2);
                鼠标坐标.Y -= (Size.Height / 2);
                Location = new Point(鼠标坐标.X, 鼠标坐标.Y);

                label2.Text= 发条.发条窗口.label12.Text;
                label3.Text = 发条.发条窗口.label13.Text;
                label4.Text = 发条.发条窗口.label9.Text;
                label5.Text = 发条.发条窗口.label14.Text;

                BackgroundImage = Properties.Resources.高亮关闭;

            }
        }


    }
}
