using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fatiao.动作窗口
{
    public partial class 字符拼接 : Form
    {
        public 字符拼接()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 字符拼接_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 字符拼接_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label9.Text = "";

                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label5.Text = "请输入拼接内容";
                    button17.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    label5.Text = "请输入读取的变量名";
                    button17.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button4.BackgroundImage = Properties.Resources.关闭变量;
                    label10.Text = "请输入拼接内容";
                }
                else//红色
                {
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    button4.BackgroundImage = Properties.Resources.开启变量;
                    label10.Text = "请输入读取的变量名";
                }


            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
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
                textBox4.BackColor = Color.White;//白色
                textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button17.BackgroundImage = Properties.Resources.关闭变量;
                label5.Text = "请输入拼接内容";
            }
            else//红色
            {
                textBox4.BackColor = Color.Linen;//黄色
                textBox4.ForeColor = Color.Maroon;//红色
                button17.BackgroundImage = Properties.Resources.开启变量;
                label5.Text = "请输入读取的变量名";
            }
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
                label10.Text = "请输入拼接内容";
            }
            else//红色
            {
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                button4.BackgroundImage = Properties.Resources.开启变量;
                label10.Text = "请输入读取的变量名";
            }
        }

        string 内容一, 内容二;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != ""&& textBox3.Text != "")
            {
                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox4.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox4.Text;
                }

                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容二 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容二 = textBox3.Text;
                }

                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "字符拼接(" + 内容一 + "," + 内容二 + "," + textBox5.Text + ");" + textBox2.Text);

                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                    }
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Add("字符拼接(" + 内容一 + "," + 内容二 + "," + textBox5.Text + ");" + textBox2.Text);

                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox3.Text != "")
            {
                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox4.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox4.Text;
                }

                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容二 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容二 = textBox3.Text;
                }

                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);

                发条.发条窗口.listBox1.Items.Insert(选中行, "字符拼接(" + 内容一 +","+内容二 +","+textBox5.Text+");" + textBox2.Text);

                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                Close();
            }
        }
    }
}
