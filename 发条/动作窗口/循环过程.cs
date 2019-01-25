using System;
using System.Drawing;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 循环过程 : Form
    {
        public 循环过程()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (发条.发条窗口.listBox1.SelectedIndex != -1)
            {
                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "﹁循环始(" + textBox3.Text + ");" + textBox2.Text);
                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "注释(添加内容到过程里);");
                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 3, "﹂循环末;");
                if (textBox1.Text != "")
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 4, "等待时间(" + textBox1.Text + ");");
                }
            }
            else
            {
                发条.发条窗口.listBox1.Items.Add("﹁循环始(" + textBox3.Text + ");" + textBox2.Text);
                发条.发条窗口.listBox1.Items.Add("注释(添加内容到过程里);");
                发条.发条窗口.listBox1.Items.Add("﹂循环末;");
                if (textBox1.Text != "")
                {
                    发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                }
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
            

            发条.发条窗口.listBox1.Items.Insert(选中行, "﹁循环始("+ textBox3.Text+");" + textBox2.Text);

            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void 过程循环_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
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
                textBox3.BackColor = Color.White;//白色
                textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label5.Text = "请输入循环次数，0表示无限次";
                button4.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                label5.Text = "请输入读取的变量名";
                button4.BackgroundImage = Properties.Resources.开启变量;
            }
        }
    }
}
