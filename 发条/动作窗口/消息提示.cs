using System;
using System.Drawing;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 消息提示 : Form
    {
        public 消息提示()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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
                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox3.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox3.Text;
                }
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "消息提示(" + textBox6.Text + "," + comboBox1.Text + "," + 内容一 + "," + comboBox2.Text + ");" + textBox2.Text);
                    if (textBox6.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox6.Text + "等于" + "\"" + "真" + "\"" + ");");
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
                        发条.发条窗口.listBox1.Items.Add("消息提示(" + textBox6.Text + "," + comboBox1.Text + "," + 内容一 + "," + comboBox2.Text + ");" + textBox2.Text);

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
                label21.Text = "自定义内容不能为空";
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                if (button5.ForeColor != Color.Maroon)//等于蓝色
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

                    发条.发条窗口.listBox1.Items.Insert(选中行, "消息提示(" + textBox6.Text + "," + comboBox1.Text + "," + 内容一 + "," + comboBox2.Text + ");" + textBox2.Text);


                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox6.Text);
                Close();
            }
            else
            {
                label21.Text = "自定义内容不能为空";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "弹幕消息")
            {
                comboBox2.Visible = false;
                label5.Visible = false;
                label11.Visible = false;
                textBox6.Enabled = false;
                textBox6.Text = "";
            }
            else
            {
                comboBox2.Visible = true;
                label5.Visible = true;
                label11.Visible = true;
                textBox6.Enabled = true;
            }
        }


        private void 消息提示_Load(object sender, EventArgs e)
        {
/*            label21.Text = "";*/
        }


        private void 消息提示_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 消息提示_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";

                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label2.Text = "自定义提示的内容";
                    button5.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    label2.Text = "请输入读取的变量名";
                    button5.BackgroundImage = Properties.Resources.开启变量;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
                label2.Text = "自定义提示的内容";
                button5.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                label2.Text = "请输入读取的变量名";
                button5.BackgroundImage = Properties.Resources.开启变量;
            }
        }


    }
}
