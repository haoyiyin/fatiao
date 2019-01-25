using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using 动作库;

namespace fatiao
{
    public partial class 基于平面 : Form
    {

        public static 动作类型 动作类型 = new 动作类型();



        public 基于平面()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (发条.发条窗口.listBox1.SelectedIndex != -1)
            {
                switch (comboBox4.Text)
                {
                    case "窗口内":
                        if (comboBox1.Text == "坐标位置")
                        {
                            textBox3.Text = textBox3.Text.Replace("，", ",");
                        }
                        if (textBox3.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "基于平面(" + textBox4.Text + "," + comboBox4.Text + "," + comboBox1.Text + ",\"" + textBox3.Text + "\"," + comboBox2.Text + ");" + textBox2.Text);

                            if (textBox1.Text != "")
                            {
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                            }
                            Close();
                        }


                        break;
                    default:
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "基于平面(" + textBox4.Text + "," + comboBox4.Text + "," + comboBox2.Text + ");" + textBox2.Text);
                        if (textBox1.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                        Close();
                        break;
                }
                label12.Text = "平面信息不能为空";
            }
            else
            {
                switch (comboBox4.Text)
                {
                    case "窗口内":
                        if (comboBox1.Text == "坐标位置")
                        {
                            textBox3.Text = textBox3.Text.Replace("，", ",");
                        }
                       if (textBox3.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Add("基于平面(" + textBox4.Text + "," + comboBox4.Text + "," + comboBox1.Text + ",\"" + textBox3.Text + "\"," + comboBox2.Text + ");" + textBox2.Text);

                            if (textBox1.Text != "")
                            {
                                发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                            }
                            Close();
                        }
                        break;
                    default:
                        发条.发条窗口.listBox1.Items.Add("基于平面(" + textBox4.Text + "," + comboBox4.Text + "," + comboBox2.Text + ");" + textBox2.Text);
                        Close();
                        break;
                }
                label12.Text = "平面信息不能为空";

            }

        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;

            选中行 = 发条.发条窗口.listBox1.SelectedIndex;

            switch (comboBox4.Text)
            {
                case "窗口内":

                    if (comboBox1.Text == "坐标位置")
                    {
                        textBox3.Text = textBox3.Text.Replace("，", ",");
                    }
                    if (textBox3.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                        发条.发条窗口.listBox1.Items.Insert(选中行, "基于平面(" + textBox4.Text + "," + comboBox4.Text + "," + comboBox1.Text + ",\"" + textBox3.Text + "\"," + comboBox2.Text + ");" + textBox2.Text);
                        发条.发条窗口.listBox1.SelectedIndex = 选中行;
                        Close();
                    }
                    break;
                default:
                    发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                    发条.发条窗口.listBox1.Items.Insert(选中行, "基于平面(" + textBox4.Text + "," + comboBox4.Text + "," + comboBox2.Text + ");" + textBox2.Text);
                    发条.发条窗口.listBox1.SelectedIndex = 选中行;
                    Close();
                    break;
            }

            label12.Text = "平面信息不能为空";


        }


        #region 参数菜单

        工具字幕 字幕 = new 工具字幕();
        bool 退出线程 = false;

        public void 窗口信息操作()
        {
            退出线程 = false;
            int hwnd;

            字幕.Show();
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                while (true)
                {
                    hwnd = 发条.发条窗口.fatiao.GetMousePointWindow();//获取鼠标指向窗口句柄
                    string 窗口名 = 发条.发条窗口.fatiao.GetWindowTitle(hwnd);//获得窗口标题
                    动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标
                    发条.发条窗口.fatiao.GetClientSize(hwnd, out object w, out object h);
                    动作.获取窗口大小(hwnd, out int 宽, out int 高);


                    字幕.label1.Text = "窗口大小：" + 宽 + "," + 高 + "\r\n客户区大小：" + (int)w + "," + (int)h + "\r\n标题：" + 窗口名 + "   空格键获取标题" + "\r\n句柄：" + hwnd + "   Ctrl+空格键获取句柄" + "\r\n坐标：" + 坐标.X.ToString() + "," + 坐标.Y.ToString() + "   Shift+空格键获取坐标";

                    if (!动作.判断按键(16) && !动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        textBox3.Text = 窗口名;//将窗口标题写入编辑框
                        字幕.label1.Text = "";
                        return;
                    }

                    if (动作.判断按键(17) && 动作.判断按键(32))//快捷键Ctrl+空格
                    {
                        textBox3.Text = hwnd.ToString();//将窗口句柄写入编辑框
                        字幕.label1.Text = "";
                        return;
                    }

                    if (动作.判断按键(16) && 动作.判断按键(32))//快捷键Shift+空格
                    {
                        textBox3.Text = 坐标.X.ToString() + "," + 坐标.Y.ToString();//将窗口内坐标写入编辑框
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

        #endregion

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    textBox4.Text = items.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Items.Clear();
            int iCount = 动作.读取键("变量名", @".\Generate\variable.ini").Count - 1;
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip2.Items.Add(动作.读取键("变量名", @".\Generate\variable.ini")[i].ToString());
            }
            if (contextMenuStrip2.Items.Count > 0)
            {
                contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
            }
            else
            {
                label12.Text = "不存在历史变量名";
            }
        }

        private void 基于平面_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 基于平面_Load(object sender, EventArgs e)
        {
/*            label12.Text = "";*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label15.Visible = true;
            label9.Text = "输入参数或右侧“…”获取参数";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.Text)
            {
                case "窗口内":
                    comboBox1.Visible = true;
                    label11.Visible = true;
                    label9.Visible = true;
                    label15.Visible = true;
                    textBox3.Visible = true;
                    button7.Visible = true;
                    break;
                default:
                    comboBox1.Visible = false;
                    label11.Visible = false;
                    label9.Visible = false;
                    label15.Visible = false;
                    textBox3.Visible = false;
                    button7.Visible = false;
                    break;
            }
        }

        private void 基于平面_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void 基于平面_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label12.Text = "";
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            窗口信息操作();
        }
    }
}


