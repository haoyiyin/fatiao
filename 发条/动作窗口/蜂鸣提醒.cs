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
    public partial class 蜂鸣提醒 : Form
    {
        public 蜂鸣提醒()
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
                
                if (发条.发条窗口.listBox1.SelectedIndex != -1) 
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "蜂鸣提醒(" + textBox3.Text + ");" + textBox2.Text);
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                    }
                }
               else
                {
                    发条.发条窗口.listBox1.Items.Add("蜂鸣提醒(" + textBox3.Text + ");" + textBox2.Text);
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }

                Close();
            }
            else
            {
                label16.Text = "蜂鸣次数不能为空";
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;
            
            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);
            

            发条.发条窗口.listBox1.Items.Insert(选中行, "蜂鸣提醒(" + textBox3.Text + ");" + textBox2.Text);

            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void 蜂鸣提醒_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void 蜂鸣提醒_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 蜂鸣提醒_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
            }
        }
    }
}
