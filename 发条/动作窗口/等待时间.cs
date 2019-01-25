using System;
using System.Drawing;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 等待时间 : Form
    {
        public 等待时间()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (发条.发条窗口.listBox1.SelectedIndex != -1)
            {

                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "等待时间(" + textBox1.Text + ");" + textBox2.Text);

            }
            else
            {

                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");" + textBox2.Text);

            }

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;
            
            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);

                    发条.发条窗口.listBox1.Items.Insert(选中行, "等待时间(" + textBox1.Text + ");" + textBox2.Text);


            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (button4.ForeColor != Color.Maroon)//等于蓝色
            {
                button4.ForeColor = Color.Maroon;//红色

            }
            else
            {
                button4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button4.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox1.BackColor = Color.White;//白色
                textBox1.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label5.Text = "自定义等待毫秒数";
                button4.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox1.BackColor = Color.Linen;//黄色
                textBox1.ForeColor = Color.Maroon;//红色
                label5.Text = "请输入读取的变量名";
                button4.BackgroundImage = Properties.Resources.开启变量;
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

        private void 等待时间_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

                    button4.Visible = true;
                    label5.Text = "自定义等待毫秒数，或“+”选择整数变量";

        }

        private void 等待时间_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 等待时间_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";

                if (button4.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox1.BackColor = Color.White;//白色
                    textBox1.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label5.Text = "自定义等待毫秒数";
                    button4.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox1.BackColor = Color.Linen;//黄色
                    textBox1.ForeColor = Color.Maroon;//红色
                    label5.Text = "请输入读取的变量名";
                    button4.BackgroundImage = Properties.Resources.开启变量;
                }
            }
        }
    }
}
