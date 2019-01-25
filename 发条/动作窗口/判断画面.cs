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
    public partial class 判断画面 : Form
    {

        public static 动作类型 动作类型 = new 动作类型();


        public 判断画面()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "判断画面(" + textBox6.Text + "," + textBox3.Text + "," + textBox4.Text + "," + textBox7.Text + "," + textBox5.Text + "," + textBox8.Text +","+ trackBar1.Value + ");" + textBox2.Text);
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
                    发条.发条窗口.listBox1.Items.Add("判断画面(" + textBox6.Text + "," + textBox3.Text + "," + textBox4.Text + "," + textBox7.Text + "," + textBox5.Text + "," + textBox8.Text +"," + trackBar1.Value + ");" + textBox2.Text);
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
                //发条.发条窗口.添加变量(textBox6.Text);
                Close();
            }
            else
            {
                label16.Text="参数不能为空";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
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
        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        int 判断编辑框 = 0;

        private void label17_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 1;
            Delay(200);
            发条.发条窗口.参数窗口 = "判断画面";
            选取范围 选取范围 = new 选取范围();
            选取范围.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 2;
            Delay(200);
            发条.发条窗口.参数窗口 = "判断画面";
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
            object x1 = 0, y1 = 0,x2=0,y2=0;
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
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y+ "   “空格”键获取桌面内坐标\r\n" + 锁定窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标" ;
                    }
                    else
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标";
                    }

                    if (!动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        switch (判断编辑框)
                        {
                            case 1:
                                textBox4.Text = 坐标.X.ToString();//将窗口内坐标写入编辑框
                                textBox7.Text = 坐标.Y.ToString();
                                break;
                            case 2:
                                textBox5.Text = 坐标.X.ToString();//将窗口内坐标写入编辑框
                                textBox8.Text = 坐标.Y.ToString();
                                break;
                        }
                        字幕.label1.Text = "";
                        
                        return;
                    }

                    if (动作.判断按键(17) && 动作.判断按键(32))//快捷键Ctrl+空格
                    {
                        switch (判断编辑框)
                        {
                            case 1:
                                textBox4.Text = X.ToString();//将窗口内坐标写入编辑框
                                textBox7.Text = Y.ToString();
                                break;
                            case 2:
                                textBox5.Text = X.ToString();//将窗口内坐标写入编辑框
                                textBox8.Text = Y.ToString();
                                break;
                        }
                        字幕.label1.Text = "";
                        
                        return;
                    }

                    if (退出线程 == true|| 动作.判断按键(27))//退出线程
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
            if (textBox3.Text != ""&& textBox4.Text != "" && textBox5.Text != "")
            {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                发条.发条窗口.listBox1.Items.Insert(选中行, "判断画面(" + textBox6.Text + "," + textBox3.Text + "," + textBox4.Text + ","+ textBox7.Text + "," + textBox5.Text + ","+ textBox8.Text + "," + trackBar1.Value + ");" + textBox2.Text);
                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox6.Text);
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            if (button8.ForeColor != Color.Maroon)//等于蓝色
            {
                button8.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button8.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button8.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox4.BackColor = Color.White;//白色
                textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label17.Visible = true;
                button8.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label17.Visible = false;
                textBox4.BackColor = Color.Linen;//黄色
                textBox4.ForeColor = Color.Maroon;//红色
                button8.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            if (button9.ForeColor != Color.Maroon)//等于蓝色
            {
                button9.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button9.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button9.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox7.BackColor = Color.White;//白色
                textBox7.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button9.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox7.BackColor = Color.Linen;//黄色
                textBox7.ForeColor = Color.Maroon;//红色
                button9.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            if (button11.ForeColor != Color.Maroon)//等于蓝色
            {
                button11.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button11.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button11.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox5.BackColor = Color.White;//白色
                textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label18.Visible = true;
                button11.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label18.Visible = false;
                textBox5.BackColor = Color.Linen;//黄色
                textBox5.ForeColor = Color.Maroon;//红色
                button11.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            if (button10.ForeColor != Color.Maroon)//等于蓝色
            {
                button10.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button10.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button10.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox8.BackColor = Color.White;//白色
                textBox8.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button10.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox8.BackColor = Color.Linen;//黄色
                textBox8.ForeColor = Color.Maroon;//红色
                button10.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void 判断画面_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 判断画面_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void 判断画面_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void 判断画面_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
                if (button8.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label17.Visible = true;
                    button8.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label17.Visible = false;
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    button8.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button11.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox5.BackColor = Color.White;//白色
                    textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label18.Visible = true;
                    button11.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label18.Visible = false;
                    textBox5.BackColor = Color.Linen;//黄色
                    textBox5.ForeColor = Color.Maroon;//红色
                    button11.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button9.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox7.BackColor = Color.White;//白色
                    textBox7.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button9.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox7.BackColor = Color.Linen;//黄色
                    textBox7.ForeColor = Color.Maroon;//红色
                    button9.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button10.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox8.BackColor = Color.White;//白色
                    textBox8.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button10.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox8.BackColor = Color.Linen;//黄色
                    textBox8.ForeColor = Color.Maroon;//红色
                    button10.BackgroundImage = Properties.Resources.开启变量;
                }

            }
        }
    }
}
