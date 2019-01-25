using System;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 判断窗口 : Form
    {
        public 判断窗口()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "判断窗口(" + textBox4.Text + ",\"" + textBox1.Text + "\"," + comboBox1.Text + ");" + textBox3.Text);
                    if (textBox4.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox4.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 3, "注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 4, "﹂如果末;");
                    }
                    else
                    {
                        if (textBox2.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox2.Text + ");");
                        }
                    }
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Add("判断窗口(" + textBox4.Text + ",\"" + textBox1.Text + "\"," + comboBox1.Text + ");" + textBox3.Text);
                    if (textBox4.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("﹁如果始" + "(" + textBox4.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Add("注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Add("﹂如果末;");
                    }
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
            int 选中行;
            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);
            发条.发条窗口.listBox1.Items.Insert(选中行, "判断窗口(" + textBox4.Text + ",\"" + textBox1.Text + "\"," + comboBox1.Text + ");" + textBox3.Text);
            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            //发条.发条窗口.添加变量(textBox4.Text);
            Close();
        }

        private void 判断窗口_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void 判断窗口_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 判断窗口_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip1.Items)
            {
                if (items.Selected == true)
                    textBox1.Text = items.ToString();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            var windows = 枚举窗口.FindAll();
            for (int i = 0; i < windows.Count; i++)
            {
                var window = windows[i];
                contextMenuStrip1.Items.Add(window.Title.ToString());
            }
            if (contextMenuStrip1.Items.Count > 0)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }
    }
}
