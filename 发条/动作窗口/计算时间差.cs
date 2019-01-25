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
    public partial class 计算时间差 : Form
    {
        public 计算时间差()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "年月日时分秒":
                    dateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
                    dateTimePicker2.CustomFormat = "yyyy/MM/dd HH:mm:ss";
                    break;
                case "年月日":
                    dateTimePicker1.CustomFormat = "yyyy/MM/dd";
                    dateTimePicker2.CustomFormat = "yyyy/MM/dd";
                    break;
                case "时分秒":
                    dateTimePicker1.CustomFormat = "HH:mm:ss";
                    dateTimePicker2.CustomFormat = "HH:mm:ss";
                    break;
                case "仅时钟":
                    dateTimePicker1.CustomFormat = "HH";
                    dateTimePicker2.CustomFormat = "HH";
                    break;
                case "仅分钟":
                    dateTimePicker1.CustomFormat = "mm";
                    dateTimePicker2.CustomFormat = "mm";
                    break;
                case "仅秒钟":
                    dateTimePicker1.CustomFormat = "ss";
                    dateTimePicker2.CustomFormat = "ss";
                    break;
            }
        }

        string 内容一, 内容二;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + dateTimePicker2.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox1.Text;
                }
                if (button3.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容二 = "\"" + dateTimePicker1.Text + "\"";
                }
                else//红色
                {
                    内容二 = textBox11.Text;
                }

                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "计算时间差(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一+","+ 内容二 + ");" + textBox3.Text);
                        if (textBox2.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox2.Text + ");");
                        }

                }
                else
                {
                        发条.发条窗口.listBox1.Items.Add("计算时间差(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + ");" + textBox3.Text);

                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox2.Text + ");");
                    }
                }
                //发条.发条窗口.添加变量(textBox4.Text);
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + dateTimePicker2.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox1.Text;
                }
                if (button3.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容二 = "\"" + dateTimePicker1.Text + "\"";
                }
                else//红色
                {
                    内容二 = textBox11.Text;
                }

                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                    发条.发条窗口.listBox1.Items.Insert(选中行, "计算时间差(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + ");" + textBox3.Text);
                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox4.Text);
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void 判断时间_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 判断时间_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
                label8.Text = "";

                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    dateTimePicker2.Visible = true;
                    textBox1.Visible = false;
                    label12.Text = "请设置时间一";
                    button4.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    dateTimePicker2.Visible = false;
                    textBox1.Visible = true;
                    label12.Text = "请输入读取的变量名";
                    button4.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button3.ForeColor != Color.Maroon)//等于蓝色
                {
                    dateTimePicker1.Visible = true;
                    textBox11.Visible = false;
                    label2.Text = "请设置时间一";
                    button3.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    dateTimePicker1.Visible = false;
                    textBox11.Visible = true;
                    label2.Text = "请输入读取的变量名";
                    button3.BackgroundImage = Properties.Resources.开启变量;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.ForeColor != Color.Maroon)//等于蓝色
            {
                button3.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button3.ForeColor != Color.Maroon)//等于蓝色
            {
                dateTimePicker1.Visible = true;
                textBox11.Visible = false;
                label2.Text = "请设置时间一";
                button3.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                dateTimePicker1.Visible = false;
                textBox11.Visible = true;
                label2.Text = "请输入读取的变量名";
                button3.BackgroundImage = Properties.Resources.开启变量;
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
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
                dateTimePicker2.Visible = true;
                textBox1.Visible = false;
                label12.Text = "请设置时间一";
                button4.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                dateTimePicker2.Visible = false;
                textBox1.Visible = true;
                label12.Text = "请输入读取的变量名";
                button4.BackgroundImage = Properties.Resources.开启变量;
            }


        }


    }
}
