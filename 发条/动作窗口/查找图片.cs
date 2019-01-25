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
    public partial class 查找图片 : Form
    {

        public static 动作类型 动作类型 = new 动作类型();


        public 查找图片()
        {
            InitializeComponent();
        }

        string 内容一;
        private void button1_Click(object sender, EventArgs e)//确定
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox9.Text != "" && textBox10.Text != "")
            {
                if (textBox3.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox3.Text;
                }
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "查找图片(" + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + "," + 内容一 + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox10.Text + "," + trackBar1.Value + ");" + textBox2.Text);


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

                        发条.发条窗口.listBox1.Items.Add("查找图片(" + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + "," + 内容一 + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox10.Text + "," + trackBar1.Value + ");" + textBox2.Text);

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

        private void label23_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 2;
            Delay(200);
            发条.发条窗口.参数窗口 = "查找图片";
            选取范围 选取范围 = new 选取范围();
            选取范围.Show();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 3;
            Delay(200);
            发条.发条窗口.参数窗口 = "查找图片";
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
            object x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            int X, Y;
            string 锁定窗口名 = "";
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
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 锁定窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标";
                    }
                    else
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标";
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
                    if (退出线程 == true || 动作.判断按键(27))//退出线程
                    {
                        字幕.label1.Text = "";
                        return;
                    }
                }
            }), null);
        }
        #endregion



        private void button13_Click(object sender, EventArgs e)//编辑
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox9.Text != "" && textBox10.Text != "")
            {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                if (textBox3.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox3.Text;
                }
                    发条.发条窗口.listBox1.Items.Insert(选中行, "查找图片(" + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + "," + 内容一 + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox10.Text + "," + trackBar1.Value + ");" + textBox2.Text);


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
                label23.Visible = true;
                button15.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label23.Visible = false;
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
                label24.Visible = true;
                button17.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label24.Visible = false;
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

        private void 查找图片_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 查找图片_Load(object sender, EventArgs e)
        {
            /*            label16.Text = "";
                        if (pictureBox1.Image != null)
                        {
                            label19.Visible = false;
                        }
                        else
                        {
                            label19.Visible = true;
                        }*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            if (pictureBox1.Image == null)
            {
                label19.Visible = true;
            }
            label8.Text = "“…”选取待找图片，左侧图片框可截取图片";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label19_Click(sender, e);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            label8.Text = "“…”选取待找图片，左侧图片框可截取图片";
            button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            textBox3.Clear();
            发条.发条窗口.参数窗口 = "查找图片";
            截图 截图 = new 截图();
            截图.Show();
        }

        private void 查找图片_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void 查找图片_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
                if (pictureBox1.Image != null)
                {
                    label19.Visible = false;
                }
                else
                {
                    label19.Visible = true;
                }

                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label22.Visible = true;
                    label8.Text = "“…”选取待找图片，左侧图片框可截取图片";
                    button5.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label22.Visible = false;
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    label8.Text = "请输入读取的变量名";
                    button5.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button15.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label23.Visible = true;
                    button15.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label23.Visible = false;
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    button15.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox5.BackColor = Color.White;//白色
                    textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label24.Visible = true;
                    button17.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label24.Visible = false;
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

        private void button5_Click(object sender, EventArgs e)//变量切换
        {
            textBox3.Text = "";
            if (button5.ForeColor != Color.Maroon)//等于蓝色
            {
                button5.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button5.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox3.BackColor = Color.White;//白色
                textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label22.Visible = true;
                label8.Text = "“…”选取待找图片，左侧图片框可截取图片";
                button5.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label22.Visible = false;
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                label8.Text = "请输入读取的变量名";
                button5.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(@".\素材库"))//不存在就创建
            {
                System.IO.Directory.CreateDirectory(@".\素材库");
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory + @".\素材库";//设置打开路径的目录
            ofd.Filter = "bmp图片|*.bmp*";
            ofd.Multiselect = true;
            ofd.Title = "请选择待找图片，按住Ctrl可多选";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox3.Clear();//清空文本框
                    string[] str = ofd.FileNames;//所选图片目录和名字
                    for (int i = 0; i < str.Length; i++)//查找数组所有数据
                    {
                        string[] 图片 = str[i].Split(new string[] { @"素材库\" }, StringSplitOptions.None);//分割
                        string 多图片 = 图片[1] + "|";

                        textBox3.Text += 多图片;//自动填入所选图片目录和名字
                    }
                    textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 1);//删除最后一个多余的"|"
                }
                catch//如果报错
                {
                    发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "发条错误", "请选择素材库下的图片", ToolTipIcon.Error);
                }
            }
        }
    }
}
