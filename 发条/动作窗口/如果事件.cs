using System;
using System.Drawing;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 如果事件 : Form
    {
        public 如果事件()
        {
            InitializeComponent();
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    if (button4.ForeColor != Color.Maroon)//等于蓝色
                    {
                        内容一 = "\"" + textBox3.Text + "\"";
                    }
                    else//红色
                    {
                        内容一 = textBox3.Text;
                    }

                    if (button5.ForeColor != Color.Maroon)//等于蓝色
                    {
                        内容二 = "\"" + textBox4.Text + "\"";
                    }
                    else//红色
                    {
                        内容二 = textBox4.Text;
                    }

                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "﹁如果始" + "(" + 内容一 + comboBox3.Text + 内容二 + ");" + textBox2.Text);

                   发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "注释(添加内容到事件里);");
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 3, "﹂如果末;");
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 4, "等待时间(" + textBox1.Text + ");");
                    }
                }
                else
                {
                    if (button4.ForeColor != Color.Maroon)//等于蓝色
                    {
                        内容一 = "\"" + textBox3.Text + "\"";
                    }
                    else//红色
                    {
                        内容一 = textBox3.Text;
                    }

                    if (button5.ForeColor != Color.Maroon)//等于蓝色
                    {
                        内容二 = "\"" + textBox4.Text + "\"";
                    }
                    else//红色
                    {
                        内容二 = textBox4.Text;
                    }

                    发条.发条窗口.listBox1.Items.Add("﹁如果始" + "(" + 内容一 + comboBox3.Text + 内容二 + ");" + textBox2.Text);

                    发条.发条窗口.listBox1.Items.Add("注释(添加内容到事件里);");
                    发条.发条窗口.listBox1.Items.Add("﹂如果末;");
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }

                Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        string 内容一, 内容二;
        private void button3_Click(object sender, EventArgs e)
        {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);

                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\""+textBox3.Text+ "\"";
                }
                else//红色
                {
                    内容一 = textBox3.Text;
                }

                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容二 = "\""+textBox4.Text+ "\"";
                }
                else//红色
                {
                    内容二 = textBox4.Text;
                }

                    发条.发条窗口.listBox1.Items.Insert(选中行, "﹁如果始" + "(" + 内容一 + comboBox3.Text + 内容二 + ");" + textBox2.Text);



                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            if (button4.ForeColor != Color.Maroon)//等于蓝色
            {
                button4.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button4.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox3.BackColor = Color.White;//白色
                textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button4.BackgroundImage = Properties.Resources.关闭变量;
                label8.Text = "请输入内容一";
                ;
            }
            else//红色
            {
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                button4.BackgroundImage = Properties.Resources.开启变量;
                label8.Text = "请输入读取的变量名";
            }
        }

        private void 如果事件_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 如果事件_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";

                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button4.BackgroundImage = Properties.Resources.关闭变量;
                    label8.Text = "请输入内容一";
                    ;
                }
                else//红色
                {
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    button4.BackgroundImage = Properties.Resources.开启变量;
                    label8.Text = "请输入读取的变量名";
                }
                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button5.BackgroundImage = Properties.Resources.关闭变量;
                    label10.Text = "请输入内容二";
                    ;
                }
                else//红色
                {
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    button5.BackgroundImage = Properties.Resources.开启变量;
                    label10.Text = "请输入读取的变量名";
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
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
                textBox4.BackColor = Color.White;//白色
                textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button5.BackgroundImage = Properties.Resources.关闭变量;
                label10.Text = "请输入内容二";
                ;
            }
            else//红色
            {
                textBox4.BackColor = Color.Linen;//黄色
                textBox4.ForeColor = Color.Maroon;//红色
                button5.BackgroundImage = Properties.Resources.开启变量;
                label10.Text = "请输入读取的变量名";
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

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    textBox4.Text = items.ToString();
            }
        }
    }


    }

