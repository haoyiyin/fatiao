using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fatiao
{
    public partial class winapp界面 : Form
    {
        public winapp界面()
        {
            InitializeComponent();
        }


        private Point 拖动控件;
        private void 按下鼠标(object sender, MouseEventArgs e)
        {
            拖动控件 = new Point(-e.X, -e.Y);//
        }

        private void 移动鼠标(object sender, MouseEventArgs e)
        {
            ((Control)sender).Cursor = Cursors.Arrow;//设置拖动时鼠标箭头
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(拖动控件.X, 拖动控件.Y);//设置偏移
                ((Control)sender).Location = ((Control)sender).Parent.PointToClient(mousePos);
            }
        }

        private void winapp界面_FormClosing(object sender, FormClosingEventArgs e)
        {
            生成程序.程序窗口.按键一位置X = button1.Location.X;
            生成程序.程序窗口.按键二位置X = button2.Location.X;
            生成程序.程序窗口.按键三位置X = button3.Location.X;
            生成程序.程序窗口.按键四位置X = button4.Location.X;
            生成程序.程序窗口.文本位置X = label1.Location.X;
            生成程序.程序窗口.运行次数位置X = textBox1.Location.X;
            生成程序.程序窗口.窗口大小X = Size.Width;

            生成程序.程序窗口.按键一位置Y = button1.Location.Y;
            生成程序.程序窗口.按键二位置Y = button2.Location.Y;
            生成程序.程序窗口.按键三位置Y = button3.Location.Y;
            生成程序.程序窗口.按键四位置Y = button4.Location.Y;
            生成程序.程序窗口.文本位置Y = label1.Location.Y;
            生成程序.程序窗口.运行次数位置Y = textBox1.Location.Y;
            生成程序.程序窗口.窗口大小Y = Size.Height;
        }

        private void winapp界面_Load(object sender, EventArgs e)
        {
            if (生成程序.程序窗口.listView1.Items[2].SubItems[2].Text != "不适用")
            {
                button1.Text = 生成程序.程序窗口.listView1.Items[2].SubItems[2].Text;
            }
            else
            {
                button1.Visible = false;
            }
            if (生成程序.程序窗口.listView1.Items[3].SubItems[2].Text != "不适用")
            {
                button2.Text = 生成程序.程序窗口.listView1.Items[3].SubItems[2].Text;
            }
            else
            {
                button2.Visible = false;
            }
            if (生成程序.程序窗口.listView1.Items[4].SubItems[2].Text != "不适用")
            {
                button3.Text = 生成程序.程序窗口.listView1.Items[4].SubItems[2].Text;
            }
            else
            {
                button3.Visible = false;
            }
            if (生成程序.程序窗口.listView1.Items[5].SubItems[2].Text != "不适用")
            {
                button4.Text = 生成程序.程序窗口.listView1.Items[5].SubItems[2].Text;
            }
            else
            {
                button4.Visible = false;
            }

            if (生成程序.程序窗口.comboBox1.Text == "不启用")
            {
                label1.Visible = false;
                textBox1.Visible = false;
            }
        }
    }
}
