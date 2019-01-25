using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fatiao.动作库
{
    public partial class 获取缩放率 : Form
    {
        public 获取缩放率()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 分辨率缩放率_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "获取缩放率(" + textBox4.Text+");" + textBox3.Text);
                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox2.Text + ");");
                    }
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Add("获取缩放率(" + textBox4.Text +");" + textBox3.Text);
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
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);

                发条.发条窗口.listBox1.Items.Insert(选中行, "获取缩放率(" + textBox4.Text + ");" + textBox3.Text);
                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox4.Text);
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void 分辨率缩放率_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
            }
        }
    }
}
