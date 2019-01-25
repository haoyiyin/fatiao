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
    public partial class 停止运行 : Form
    {
        public 停止运行()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;

            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);


            发条.发条窗口.listBox1.Items.Insert(选中行, "停止运行();" + textBox2.Text);

            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (发条.发条窗口.listBox1.SelectedIndex != -1)
            {
                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "停止运行();" + textBox2.Text);
                if (textBox1.Text != "")
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                }
            }
            else
            {
                发条.发条窗口.listBox1.Items.Add("停止运行();" + textBox2.Text);
                if (textBox1.Text != "")
                {
                    发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                }
            }
            Close();
        }

        private void 停止运行_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }
    }
}
