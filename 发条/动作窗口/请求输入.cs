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
    public partial class 请求输入 : Form
    {
        public 请求输入()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                switch (comboBox1.Text)
                {
                    case "每次请求":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "每次请求输入(" + textBox4.Text + ",\"" + textBox3.Text + "\");" + textBox2.Text);
                        break;
                    case "首次请求":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "首次请求输入(" + textBox4.Text+ ",\"" + textBox3.Text + "\");" + textBox2.Text);
                        break;
                }
                发条.发条窗口.listBox1.SelectedIndex = 选中行;

                    //发条.发条窗口.添加变量(textBox4.Text);

                Close();
            }
            else
            {
                label16.Text = "变量保存名不能为空";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    switch (comboBox1.Text)
                    {
                        case "每次请求":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "每次请求输入(" + textBox4.Text + ",\"" + textBox3.Text + "\");" + textBox2.Text);

                            break;
                        case "首次请求":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "首次请求输入(" + textBox4.Text +",\"" + textBox3.Text + "\");" + textBox2.Text);

                            break;
                    }

                        if (textBox2.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                }
                else
                {
                    switch (comboBox1.Text)
                    {
                        case "每次请求":
                            发条.发条窗口.listBox1.Items.Add("每次请求输入(" + textBox4.Text +",\"" + textBox3.Text + "\");" + textBox2.Text);
                            break;
                        case "首次请求":
                            发条.发条窗口.listBox1.Items.Add("首次请求输入(" + textBox4.Text + ",\"" + textBox3.Text + "\");" + textBox2.Text);
                            break;
                    }

                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }

                    //发条.发条窗口.添加变量(textBox4.Text);

                Close();
            }
            else
            {
                label16.Text = "变量保存名不能为空";
            }
        }


        private void 请求输入_Load(object sender, EventArgs e)
        {
/*            if (comboBox1.Text=="")
            {
                comboBox1.SelectedIndex = 0;
            }
            if (comboBox3.Text == "")
            {
                comboBox3.SelectedIndex = 0;
            }

            label16.Text = "";*/
        }

        private void 请求输入_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 请求输入_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible==true)
            {
                if (comboBox1.Text == "")
                {
                    comboBox1.SelectedIndex = 0;
                }

                label16.Text = "";
            }
        }
    }
}
