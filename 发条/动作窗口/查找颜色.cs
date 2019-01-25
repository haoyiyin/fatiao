using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 查找颜色 : Form
    {

        public static 动作类型 动作类型 = new 动作类型();


        public 查找颜色()
        {
            InitializeComponent();
        }

        string 内容一;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox9.Text != "" && textBox10.Text != "")
            {
                if (button6.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox3.Text;
                }

                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "查找颜色(" + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + "," + 内容一 + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox10.Text + "," + trackBar1.Value + ");" + textBox2.Text);

                    if (textBox6.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox6.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 3, "注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 4, "﹂如果末;");
                    }
                    else
                    {
                        if (textBox1.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                    }
                }
                else
                {

                        发条.发条窗口.listBox1.Items.Add("查找颜色(" + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + "," + 内容一 + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox10.Text + "," + trackBar1.Value + ");" + textBox2.Text);

                    if (textBox6.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("﹁如果始" + "(" + textBox6.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Add("注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Add("﹂如果末;");
                    }
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }

                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        int 判断编辑框 = 0;

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            判断编辑框 = 1;
            退出线程 = true;
            坐标与颜色操作();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 2;

            发条.发条窗口.参数窗口 = "查找颜色";
            选取范围 选取范围 = new 选取范围();
            选取范围.Show();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 3;

            发条.发条窗口.参数窗口 = "查找颜色";
            选取范围 选取范围 = new 选取范围();
            选取范围.Show();
        }


        #region 参数菜单

        工具字幕 字幕 = new 工具字幕();
        bool 退出线程 = false;

        public void 坐标与颜色操作()
        {
            退出线程 = false;
            int hwnd;//窗口句柄变量
            string 锁定窗口名 = "";
            object x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            int X, Y;
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
                    if (动作.判断按键(192) == true)//快捷键“~”
                    {
                        锁定窗口名 = 窗口名;
                        发条.发条窗口.fatiao.GetClientRect(hwnd, out x1, out y1, out object x2, out object y2);
                        
                    }

                    if (锁定窗口名 == "")
                    {
                        发条.发条窗口.fatiao.GetClientRect(hwnd, out x1, out y1, out x2, out y2);
                        X = 坐标.X - (int)x1;
                        Y = 坐标.Y - (int)y1;
                    }
                    else
                    {
                        X = 坐标.X - (int)x1;
                        Y = 坐标.Y - (int)y1;
                    }

                    string color = 发条.发条窗口.fatiao.GetColor(坐标.X, 坐标.Y);//获取坐标颜色
                    if (锁定窗口名 != "")
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 锁定窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标\r\n颜色值：" + color + "   “C”键获取颜色值";

                    }
                    else
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标\r\n颜色值：" + color + "   “C”键获取颜色值";
                    }

                    if (!动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        switch (判断编辑框)
                        {
                            case 2:
                                textBox4.Text = 坐标.X.ToString();//将窗口内坐标写入编辑框
                                textBox9.Text = 坐标.Y.ToString();//将窗口内坐标写入编辑框
                                break;
                            case 3:
                                textBox5.Text = 坐标.X.ToString();//将窗口内坐标写入编辑框
                                textBox10.Text = 坐标.Y.ToString();//将窗口内坐标写入编辑框
                                break;
                        }
                        字幕.label1.Text = "";

                        return;
                    }

                    if (动作.判断按键(17) && 动作.判断按键(32))//快捷键Ctrl+空格
                    {
                        switch (判断编辑框)
                        {
                            case 2:
                                textBox4.Text = X.ToString();//将窗口内坐标写入编辑框
                                textBox9.Text = Y.ToString();//将窗口内坐标写入编辑框
                                break;
                            case 3:
                                textBox5.Text = X.ToString();//将窗口内坐标写入编辑框
                                textBox10.Text = Y.ToString();//将窗口内坐标写入编辑框
                                break;
                        }
                        字幕.label1.Text = "";

                        return;
                    }

                    if (动作.判断按键(67))//快捷键“C”
                    {
                        switch (判断编辑框)
                        {
                            case 1:
                                textBox3.Text = color;//将窗口坐标与颜色写入剪贴板
                                break;
                        }
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
                label16.Text = "不存在历史变量名";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Items.Clear();
            int iCount = 动作.读取键("变量名", @".\Generate\variable.ini").Count - 1;
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip3.Items.Add(动作.读取键("变量名", @".\Generate\variable.ini")[i].ToString());
            }
            if (contextMenuStrip3.Items.Count > 0)
            {
                contextMenuStrip3.Show(MousePosition.X, MousePosition.Y);
            }
            else
            {
                label16.Text = "不存在历史变量名";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            contextMenuStrip4.Items.Clear();
            int iCount = 动作.读取键("变量名", @".\Generate\variable.ini").Count - 1;
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip4.Items.Add(动作.读取键("变量名", @".\Generate\variable.ini")[i].ToString());
            }
            if (contextMenuStrip4.Items.Count > 0)
            {
                contextMenuStrip4.Show(MousePosition.X, MousePosition.Y);
            }
            else
            {
                label16.Text = "不存在历史变量名";
            }
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    textBox6.Text = items.ToString();
            }
        }

        private void contextMenuStrip3_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip3.Items)
            {
                if (items.Selected == true)
                    textBox7.Text = items.ToString();
            }
        }

        private void contextMenuStrip4_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip4.Items)
            {
                if (items.Selected == true)
                    textBox8.Text = items.ToString();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox9.Text != "" && textBox10.Text != "")
            {
                if (button6.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox3.Text;
                }
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);

                    发条.发条窗口.listBox1.Items.Insert(选中行, "查找颜色(" + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + "," + 内容一 + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox10.Text + "," + trackBar1.Value + ");" + textBox2.Text);


                发条.发条窗口.listBox1.SelectedIndex = 选中行;
/*                //发条.发条窗口.添加变量(textBox6.Text);
                //发条.发条窗口.添加变量(textBox7.Text);
                //发条.发条窗口.添加变量(textBox8.Text);*/
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            if (button15.ForeColor != Color.Maroon)//等于蓝色
            {
                button15.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button15.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button15.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox4.BackColor = Color.White;//白色
                textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label22.Visible = true;
                button15.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label22.Visible = false;
                textBox4.BackColor = Color.Linen;//黄色
                textBox4.ForeColor = Color.Maroon;//红色
                button15.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            if (button14.ForeColor != Color.Maroon)//等于蓝色
            {
                button14.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button14.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button14.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox9.BackColor = Color.White;//白色
                textBox9.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button14.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox9.BackColor = Color.Linen;//黄色
                textBox9.ForeColor = Color.Maroon;//红色
                button14.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            if (button17.ForeColor != Color.Maroon)//等于蓝色
            {
                button17.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button17.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox5.BackColor = Color.White;//白色
                textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button17.BackgroundImage = Properties.Resources.关闭变量;
                label23.Visible = true;
            }
            else//红色
            {
                label23.Visible = false;
                textBox5.BackColor = Color.Linen;//黄色
                textBox5.ForeColor = Color.Maroon;//红色
                button17.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
            if (button16.ForeColor != Color.Maroon)//等于蓝色
            {
                button16.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button16.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button16.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox10.BackColor = Color.White;//白色
                textBox10.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button16.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox10.BackColor = Color.Linen;//黄色
                textBox10.ForeColor = Color.Maroon;//红色
                button16.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void contextMenuStrip5_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip5.Items)
            {
                if (items.Selected == true)
                    textBox4.Text = items.ToString();
            }
        }

        private void contextMenuStrip6_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip6.Items)
            {
                if (items.Selected == true)
                    textBox9.Text = items.ToString();
            }
        }

        private void contextMenuStrip7_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip7.Items)
            {
                if (items.Selected == true)
                    textBox5.Text = items.ToString();
            }
        }

        private void contextMenuStrip8_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip8.Items)
            {
                if (items.Selected == true)
                    textBox10.Text = items.ToString();
            }
        }

        private void 查找颜色_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            label19.Visible = true;
            label8.Text = "右侧“…”获取参数，多个颜色用“|”符号隔开";

        }

        private void 查找颜色_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void 查找颜色_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible==true)
            {
                label16.Text = "";

                if (button6.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label19.Visible = true;
                    label8.Text = "“…”获取参数，多个颜色用“|”符号隔开";
                    button6.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label19.Visible = false;
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    label8.Text = "请输入读取的变量名";
                    button6.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button15.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label22.Visible = true;
                    button15.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label22.Visible = false;
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    button15.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox5.BackColor = Color.White;//白色
                    textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button17.BackgroundImage = Properties.Resources.关闭变量;
                    label23.Visible = true;
                }
                else//红色
                {
                    label23.Visible = false;
                    textBox5.BackColor = Color.Linen;//黄色
                    textBox5.ForeColor = Color.Maroon;//红色
                    button17.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button14.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox9.BackColor = Color.White;//白色
                    textBox9.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button14.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox9.BackColor = Color.Linen;//黄色
                    textBox9.ForeColor = Color.Maroon;//红色
                    button14.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button16.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox10.BackColor = Color.White;//白色
                    textBox10.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button16.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox10.BackColor = Color.Linen;//黄色
                    textBox10.ForeColor = Color.Maroon;//红色
                    button16.BackgroundImage = Properties.Resources.开启变量;
                }


            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            if (button6.ForeColor != Color.Maroon)//等于蓝色
            {
                button6.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button6.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button6.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox3.BackColor = Color.White;//白色
                textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label19.Visible = true;
                label8.Text = "“…”获取参数，多个颜色用“|”符号隔开";
                button6.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label19.Visible = false;
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                label8.Text = "请输入读取的变量名";
                button6.BackgroundImage = Properties.Resources.开启变量;
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
    }
}
