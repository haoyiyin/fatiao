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
    public partial class 整数运算 : Form
    {

        public 整数运算()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (textBox3.Text != "")
                {
                    if (发条.发条窗口.listBox1.SelectedIndex != -1)
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "整数运算(" + textBox3.Text + comboBox3.Text + textBox4.Text + ");" + textBox2.Text);
                        if (textBox1.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                    }
                    else
                    {
                        发条.发条窗口.listBox1.Items.Add("整数运算("  + textBox3.Text + comboBox3.Text + textBox4.Text + ");" + textBox2.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);

                发条.发条窗口.listBox1.Items.Insert(选中行, "整数运算("+ textBox3.Text + comboBox3.Text + textBox4.Text + ");" + textBox2.Text);
                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox3.Text);
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.Text)
            {
                case "=":
                    textBox4.Visible = true;
                    break;
                case "+=":
                    textBox4.Visible = true;
                    break;
                case "-=":
                    textBox4.Visible = true;
                    break;
                default:
                    textBox4.Text = "";
                    textBox4.Visible = false;
                    break;
            }
        }

        private void 整数运算_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void 整数运算_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 整数运算_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                if (comboBox3.Text=="") 
                {
                    comboBox3.Text = "=";
                }

                label16.Text = "";
            }
        }
    }
}
