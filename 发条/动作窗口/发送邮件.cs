using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 发送邮件 : Form
    {
        public 发送邮件()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "")
            {
                label16.Text = "请耐心等待结果，切勿重复点击测试";
                ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                {
                    CheckForIllegalCrossThreadCalls = false;
                    if (邮箱功能.发送邮件(textBox3.Text, int.Parse(textBox4.Text), textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text) == "真")
                    {
                        label16.Text = "发送邮件成功";
                    }
                    else
                    {
                        label16.Text = "发送邮件失败";
                    }
                }), null);
            }
            else
            {
                label16.Text = "发送邮件参数不能为空";
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "")
            {

                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "发送邮件(" + textBox10.Text + ",\"" + textBox3.Text + "\"," + textBox4.Text + ",\"" + textBox5.Text + "\",\"" + textBox6.Text + "\",\"" + textBox7.Text + "\",\"" + textBox8.Text + "\",\"" + textBox9.Text + "\");" + textBox2.Text);
                    if (textBox10.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox10.Text + "等于" + "\"" + "真" + "\"" + ");");
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
                    发条.发条窗口.listBox1.Items.Add("发送邮件(" + textBox10.Text + ",\"" + textBox3.Text + "\"," + textBox4.Text + ",\"" + textBox5.Text + "\",\"" + textBox6.Text + "\",\"" + textBox7.Text + "\",\"" + textBox8.Text + "\",\"" + textBox9.Text + "\");" + textBox2.Text);
                    if (textBox10.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("﹁如果始" + "(" + textBox10.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Add("注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Add("﹂如果末;");
                    }
                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }
                //发条.发条窗口.添加变量(textBox10.Text);
                Close();
            }
            else
            {
                label16.Text = "发送邮件参数不能为空";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;

            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);


            发条.发条窗口.listBox1.Items.Insert(选中行, "发送邮件(" + textBox10.Text + ",\"" + textBox3.Text + "\"," + textBox4.Text + ",\"" + textBox5.Text + "\",\"" + textBox6.Text + "\",\"" + textBox7.Text + "\",\"" + textBox8.Text + "\",\"" + textBox9.Text + "\");" + textBox2.Text);

            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            //发条.发条窗口.添加变量(textBox10.Text);
            Close();
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    textBox10.Text = items.ToString();
            }
        }


        private void 发送邮件_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void 发送邮件_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 发送邮件_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
            }
        }
    }
}
