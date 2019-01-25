using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 动作库;

namespace fatiao.动作库
{
    public partial class 点击图片 : Form
    {
        public 点击图片()
        {
            InitializeComponent();
        }

        private void 点击图片_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 点击图片_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                if (pictureBox1.Image != null)
                {
                    label19.Visible = false;
                }
                else
                {
                    label19.Visible = true;
                }

                label9.Text = "";
            }
        }

        private void 点击图片_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip1.Items)
            {
                if (items.Selected == true)
                    textBox3.Text = items.ToString();
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            发条.发条窗口.参数窗口 = "点击图片";
            截图 截图 = new 截图();
            截图.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label19_Click(sender, e);
        }

        private void label5_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;

            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);

            发条.发条窗口.listBox1.Items.Insert(选中行, "点击图片(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + label11.Text + "\"," + trackBar1.Value + ");" + textBox2.Text);

            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && pictureBox1.Image != null)
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "点击图片("+ textBox6.Text + ",\"" + textBox3.Text + "\",\"" + label11.Text + "\"," + trackBar1.Value + ");" + textBox2.Text);
                    if (textBox6.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox6.Text + "等于" + "\"" + "真" + "\"" + ");");
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
                    发条.发条窗口.listBox1.Items.Add("点击图片(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + label11.Text + "\"," + trackBar1.Value + ");" + textBox2.Text);

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
                Close();
            }
            else
            {
                label9.Text = "参数不能为空";
            }
        }
    }
}
