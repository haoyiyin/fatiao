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
    public partial class 写到剪贴板 : Form
    {
        public 写到剪贴板()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        string 内容一;
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
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "写到剪贴板(" + 内容一 + ");" + textBox2.Text);
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                    }
                    Close();
                }
                else
                {
                            发条.发条窗口.listBox1.Items.Add("写到剪贴板(" + 内容一 + ");" + textBox2.Text);
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                    Close();
                }
            }
            else
            {
                label21.Text="内容不能为空";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
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

                int 选中行;
                
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);

                        发条.发条窗口.listBox1.Items.Insert(选中行, "写到剪贴板(" + 内容一 + ");" + textBox2.Text);
                
                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                Close();
            }
            else
            {
                label21.Text = "内容不能为空";
            }
        }

        private void 写到剪贴板_Load(object sender, EventArgs e)
        {
/*            label21.Text = "";*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
                    label8.Text = "设置剪贴板文本内容";
            
        }

        private void 写到剪贴板_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 写到剪贴板_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";
                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button17.BackgroundImage = Properties.Resources.关闭变量;
                    label8.Text = "设置剪贴板文本内容";
                }
                else//红色
                {
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    button17.BackgroundImage = Properties.Resources.开启变量;
                    label8.Text = "请输入读取的变量名";
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
                label8.Text = "设置剪贴板文本内容";
            }
            else//红色
            {
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                button17.BackgroundImage = Properties.Resources.开启变量;
                label8.Text = "请输入读取的变量名";
            }
        }
    }
}
