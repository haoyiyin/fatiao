using System;
using System.Drawing;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 发送文本 : Form
    {
        public 发送文本()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox3.Text != "")
            {

                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox4.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox4.Text;
                }

                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "发送文本(\"" + textBox3.Text + "\"," + 内容一 + ");" + textBox2.Text);

                        if (textBox1.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                    
                }
                else
                {

                            发条.发条窗口.listBox1.Items.Add("发送文本(\"" + textBox3.Text + "\"," + 内容一 + ");" + textBox2.Text);

                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                }
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            } 
        }

        string 内容一;
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox3.Text != "")
            {

                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox4.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox4.Text;
                }

                int 选中行;

                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);


                发条.发条窗口.listBox1.Items.Insert(选中行, "发送文本(\"" + textBox3.Text + "\"," + 内容一 + ");" + textBox2.Text);


                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }


        private void 发送文本_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 发送文本_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button17.BackgroundImage = Properties.Resources.关闭变量;
                    label17.Text = "请输入需向窗口发送的文本";
                }
                else//红色
                {
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    button17.BackgroundImage = Properties.Resources.开启变量;
                    label17.Text = "请输入读取的变量名";
                }
            }
        }


        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip1.Items)
            {
                if (items.Selected == true)
                    textBox3.Text = items.ToString();
            }
        }

        private void label9_Click(object sender, EventArgs e)
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

        private void button17_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            if (button17.ForeColor != Color.Maroon)//等于蓝色
            {
                button17.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button17.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox4.BackColor = Color.White;//白色
                textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button17.BackgroundImage = Properties.Resources.关闭变量;
                label17.Text = "请输入需向窗口发送的文本";
            }
            else//红色
            {
                textBox4.BackColor = Color.Linen;//黄色
                textBox4.ForeColor = Color.Maroon;//红色
                button17.BackgroundImage = Properties.Resources.开启变量;
                label17.Text = "请输入读取的变量名";
            }
        }
    }
}
