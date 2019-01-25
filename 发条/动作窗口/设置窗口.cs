using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 设置窗口 : Form
    {


        public static 动作类型 动作类型 = new 动作类型();


        public 设置窗口()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "窗口大小":
                    label8.Visible = true;
                    label12.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;

                    label13.Visible = false;
                    label14.Visible = false;
                    comboBox2.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    textBox6.Visible = false;
                    label17.Visible = false;
                    label18.Visible = false;
                    textBox7.Visible = false;
                    label23.Visible = false;
                    label19.Visible = false;
                    label20.Visible = false;
                    trackBar1.Visible = false;
                    break;
                case "窗口状态":
                    label13.Visible = true;
                    label14.Visible = true;
                    comboBox2.Visible = true;

                    label8.Visible = false;
                    label12.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    textBox6.Visible = false;
                    label17.Visible = false;
                    label18.Visible = false;
                    textBox7.Visible = false;
                    label19.Visible = false;
                    label20.Visible = false;
                    trackBar1.Visible = false;
                    label23.Visible = false;
                    break;
                case "窗口标题":
                    label15.Visible = true;
                    label16.Visible = true;
                    textBox6.Visible = true;

                    label8.Visible = false;
                    label12.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    comboBox2.Visible = false;
                    label17.Visible = false;
                    label18.Visible = false;
                    textBox7.Visible = false;
                    label23.Visible = false;
                    label19.Visible = false;
                    label20.Visible = false;
                    trackBar1.Visible = false;
                    break;
                case "窗口位置":
                    label17.Visible = true;
                    label18.Visible = true;
                    textBox7.Visible = true;
                    label23.Visible = true;
                    label8.Visible = false;
                    label12.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    comboBox2.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    textBox6.Visible = false;
                    label19.Visible = false;
                    label20.Visible = false;
                    trackBar1.Visible = false;
                    break;
                case "窗口透明度":
                    label19.Visible = true;
                    label20.Visible = true;
                    trackBar1.Visible = true;

                    label8.Visible = false;
                    label12.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    comboBox2.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    textBox6.Visible = false;
                    label17.Visible = false;
                    label18.Visible = false;
                    textBox7.Visible = false;
                    label23.Visible = false;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox7.Text != "" && textBox6.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox8.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    switch (comboBox1.Text)
                    {
                        case "窗口大小":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
                            break;
                        case "客户区大小":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
                            break;
                        case "窗口状态":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + comboBox2.Text + ");" + textBox2.Text);
                            break;
                        case "窗口标题":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + ",\"" + textBox6.Text + "\");" + textBox2.Text);
                            break;
                        case "窗口位置":
                            textBox7.Text = textBox7.Text.Replace("，", ",");
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox7.Text + ");" + textBox2.Text);
                            break;
                        case "窗口透明度":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + trackBar1.Value + ");" + textBox2.Text);
                            break;
                    }
                    if (textBox3.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox3.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 3, "注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 4, "﹂如果末;");
                    }
                    else
                    {
                        if (textBox2.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                    }
                }
                else
                {
                    switch (comboBox1.Text)
                    {
                        case "窗口大小":
                            发条.发条窗口.listBox1.Items.Add("设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
                            break;
                        case "客户区大小":
                            发条.发条窗口.listBox1.Items.Add("设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
                            break;
                        case "窗口状态":
                            发条.发条窗口.listBox1.Items.Add("设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + comboBox2.Text + ");" + textBox2.Text);
                            break;
                        case "窗口标题":
                            发条.发条窗口.listBox1.Items.Add("设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + ",\"" + textBox6.Text + "\");" + textBox2.Text);
                            break;
                        case "窗口位置":
                            textBox7.Text = textBox7.Text.Replace("，", ",");
                            发条.发条窗口.listBox1.Items.Add("设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox7.Text + ");" + textBox2.Text);
                            break;
                        case "窗口透明度":
                            发条.发条窗口.listBox1.Items.Add("设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + trackBar1.Value + ");" + textBox2.Text);
                            break;
                    }
                    if (textBox3.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("﹁如果始" + "(" + textBox3.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Add("注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Add("﹂如果末;");
                    }
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }
                //发条.发条窗口.添加变量(textBox3.Text);
                Close();
            }
            else
            {
                label21.Text = "参数不能为空";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && textBox6.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox8.Text != "")
            {
                int 选中行;

                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);


                switch (comboBox1.Text)
                {
                    case "窗口大小":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
                        break;
                    case "客户区大小":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
                        break;
                    case "窗口状态":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + comboBox2.Text + ");" + textBox2.Text);
                        break;
                    case "窗口标题":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + ",\"" + textBox6.Text + "\");" + textBox2.Text);
                        break;
                    case "窗口位置":
                        textBox7.Text = textBox7.Text.Replace("，", ",");
                        发条.发条窗口.listBox1.Items.Insert(选中行, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + textBox7.Text + ");" + textBox2.Text);
                        break;
                    case "窗口透明度":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "设置窗口(" + textBox3.Text + ",\"" + textBox8.Text + "\"," + comboBox1.Text + "," + trackBar1.Value + ");" + textBox2.Text);
                        break;
                }

                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox3.Text);
                Close();
            }
            else
            {
                label21.Text = "参数不能为空";
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            坐标与颜色操作();
        }

        #region 参数菜单

        工具字幕 字幕 = new 工具字幕();
        bool 退出线程 = false;


        public void 坐标与颜色操作()
        {
            退出线程 = false;
            int hwnd;//窗口句柄变量
            object x1 = 0, y1 = 0;
            字幕.Show();
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                发条.发条窗口.fatiao.BindWindow(发条.发条窗口.fatiao.GetSpecialWindow(0), "normal", "normal", "normal", 0);//前台基于窗口
                while (true)
                {
                    hwnd = 发条.发条窗口.fatiao.GetMousePointWindow();//获取鼠标指向窗口句柄
                    string 窗口名 = 发条.发条窗口.fatiao.GetWindowTitle(hwnd);//获得窗口标题
                    动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标
                    int X = 坐标.X - (int)x1;
                    int Y = 坐标.Y - (int)y1;

                    string color = 发条.发条窗口.fatiao.GetColor(坐标.X, 坐标.Y);//获取坐标颜色

                    字幕.label1.Text = "窗口名：" + 窗口名 + "\r\n屏幕坐标：" + X + "," + Y + "   空格键获取坐标";


                    if (!动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        textBox7.Text = X + "," + Y;//将窗口内坐标写入剪贴板
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




        private void 设置窗口_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 设置窗口_Load(object sender, EventArgs e)
        {
/*            label21.Text = "";*/
        }

        private void 设置窗口_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void 设置窗口_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip1.Items)
            {
                if (items.Selected == true)
                    textBox8.Text = items.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
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
    }
}
