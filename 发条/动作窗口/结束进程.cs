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
    public partial class 结束进程 : Form
    {
        public 结束进程()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int 选中行;

                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                if (checkBox1.Checked == true)
                {
                    发条.发条窗口.listBox1.Items.Insert(选中行, "结束进程(" + "结束自身" + ");" + textBox3.Text);
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Insert(选中行, "结束进程(\"" + textBox1.Text + "\");" + textBox3.Text);
                }

                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                Close();
            }
            else
            {
                label21.Text = "进程名不能为空";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    if (checkBox1.Checked == true)
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "结束进程(" +"结束自身"+ ");" + textBox3.Text);
                    }
                    else
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "结束进程(\"" + textBox1.Text + "\");" + textBox3.Text);
                    }
                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox2.Text + ");");
                    }
                    Close();
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        发条.发条窗口.listBox1.Items.Add("结束进程(" + "结束自身" + ");" + textBox3.Text);
                    }
                    else
                    {
                        发条.发条窗口.listBox1.Items.Add("结束进程(\"" + textBox1.Text + "\");" + textBox3.Text);
                    }
                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox2.Text + ");");
                    }
                    Close();
                }
            }
            else
            {
                label21.Text = "进程名不能为空";
            }
        }

        private void 结束进程_Load(object sender, EventArgs e)
        {
            /*            label21.Text = "";*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = "结束指定进程，进程名不包括扩展后缀名";
        }

        private void 结束进程_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 结束进程_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
            }
        }
    }
}
