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
    public partial class 随机数字 : Form
    {
        public 随机数字()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                int 选中行;

                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);


                发条.发条窗口.listBox1.Items.Insert(选中行, "随机数字(" + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);

                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox3.Text);
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "随机数字(" + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                    }
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Add("随机数字(" + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + ");" + textBox2.Text);
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
                label16.Text = "参数不能为空";
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
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
                textBox4.BackColor = Color.White;//白色
                textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button4.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox4.BackColor = Color.Linen;//黄色
                textBox4.ForeColor = Color.Maroon;//红色
                button4.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            if (button7.ForeColor != Color.Maroon)//等于蓝色
            {
                button7.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button7.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button7.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox5.BackColor = Color.White;//白色
                textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button7.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox5.BackColor = Color.Linen;//黄色
                textBox5.ForeColor = Color.Maroon;//红色
                button7.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void 随机数字_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void 随机数字_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 随机数字_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button4.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    button4.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button7.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox5.BackColor = Color.White;//白色
                    textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button7.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox5.BackColor = Color.Linen;//黄色
                    textBox5.ForeColor = Color.Maroon;//红色
                    button7.BackgroundImage = Properties.Resources.开启变量;
                }


            }
        }
    }
}
