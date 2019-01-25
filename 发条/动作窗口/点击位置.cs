using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 动作库;

namespace fatiao.动作库
{
    public partial class 点击位置 : Form
    {
        public 点击位置()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 点击比例_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            退出线程 = true;
            Hide();
        }

        private void 点击比例_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label9.Text = "";
            }
        }

        工具字幕 字幕 = new 工具字幕();
        动作类型 动作类型 = new 动作类型();
        int hwnd;
        bool 退出线程 = false;
        public void 百分比位置操作()
        {
            退出线程 = false;
            object x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            int X, Y;
            字幕.Show();
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                //CheckForIllegalCrossThreadCalls = false;
                发条.发条窗口.fatiao.BindWindow(hwnd, "normal", "normal", "normal", 0);//前台基于窗口
                while (true)
                {

                    动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标

                    发条.发条窗口.fatiao.GetClientRect(hwnd, out x1, out y1, out x2, out y2);
                    X = 坐标.X - (int)x1;
                    Y = 坐标.Y - (int)y1;

                    动作.坐标转百分比(hwnd, X, Y,out double 百分比X, out double 百分比Y);

                    字幕.label1.Text = " 百分比位置：" + 百分比X.ToString("0%") + "," + 百分比Y.ToString("0%") + "   空格键确定";


                    if (!动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        textBox4.Text = 百分比X.ToString("0%").Substring(0, 百分比X.ToString("0%").Length - 1);
                        textBox5.Text = 百分比Y.ToString("0%").Substring(0, 百分比Y.ToString("0%").Length - 1);
                        字幕.label1.Text = "";

                        return;
                    }

                    if (退出线程 == true || 动作.判断按键(27))//退出线程
                    {
                        字幕.label1.Text = "";

                        return;
                    }

                }
            }), null);
        }

        private void 点击位置_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;

            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);

            发条.发条窗口.listBox1.Items.Insert(选中行, "点击位置(\"" + textBox3.Text + "\"," + textBox4.Text+","+ textBox5.Text + ");" + textBox2.Text);

            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != ""&& textBox4.Text != ""&& textBox5.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "点击位置(\"" + textBox3.Text + "\"," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);

                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                    }
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Add("点击位置(\"" + textBox3.Text + "\"," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);

                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }
                Close();
            }
            else
            {
                label9.Text = "参数不能为空";
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip1.Items)
            {
                if (items.Selected == true)
                    textBox3.Text = items.ToString();
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            var windows = 枚举窗口.FindAll();
            for (int i = 0; i < windows.Count; i++)
            {
                var window = windows[i];
                contextMenuStrip1.Items.Add(window.Title.ToString());
            }
            if (contextMenuStrip1.Items.Count > 0)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            if (textBox3.Text != "")
            {
                hwnd = 发条.发条窗口.fatiao.FindWindow("", textBox3.Text);
                if (hwnd != 0)
                {
                    百分比位置操作();
                }

                else
                {
                    label9.Text = "未找到窗口，请重新输入窗口名";
                }
            }
            else
            {
                label9.Text = "请先输入窗口名";
            }
        }
    }
}
