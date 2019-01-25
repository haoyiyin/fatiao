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
    public partial class 打开网址 : Form
    {
        public 打开网址()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox3.Text;
                }

                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "打开网址(" + textBox4.Text +","+ 内容一 + ");" + textBox2.Text);
                    if (textBox4.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox4.Text + "等于" + "\"" + "真" + "\"" + ");");
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

                    发条.发条窗口.listBox1.Items.Add("打开网址(" + textBox4.Text + "," + 内容一 + ");" + textBox2.Text);
                    if (textBox4.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("﹁如果始" + "(" + textBox4.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Add("注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Add("﹂如果末;");
                    }
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }
                //发条.发条窗口.添加变量(textBox4.Text);
                Close();
            }
            else
            {
                label16.Text = "网址不能为空";
            }
        }

        string 内容一;
        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;

            if (button17.ForeColor != Color.Maroon)//等于蓝色
            {
                内容一 = "\"" + textBox3.Text + "\"";
            }
            else//红色
            {
                内容一 = textBox3.Text;
            }

            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);


            发条.发条窗口.listBox1.Items.Insert(选中行, "打开网址(" + textBox4.Text + "," + 内容一 + ");" + textBox2.Text);


            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            //发条.发条窗口.添加变量(textBox4.Text);
            Close();
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    textBox4.Text = items.ToString();
            }
        }



        private void 打开网址_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 打开网址_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button17.BackgroundImage = Properties.Resources.关闭变量;
                    label2.Text = "待打开的网页链接地址";
                }
                else//红色
                {
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    button17.BackgroundImage = Properties.Resources.开启变量;
                    label2.Text = "请输入读取的变量名";
                }
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
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
                textBox3.BackColor = Color.White;//白色
                textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button17.BackgroundImage = Properties.Resources.关闭变量;
                label2.Text = "待打开的网页链接地址";
            }
            else//红色
            {
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                button17.BackgroundImage = Properties.Resources.开启变量;
                label2.Text = "请输入读取的变量名";
            }
        }
    }
}
