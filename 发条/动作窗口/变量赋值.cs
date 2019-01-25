using System;
using System.Windows.Forms;

namespace fatiao.动作库
{
    public partial class 变量赋值 : Form
    {
        public 变量赋值()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 变量赋值_Load(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;

                发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                    发条.发条窗口.listBox1.Items.Insert(选中行, "变量赋值(" + textBox4.Text + "=\"" + textBox3.Text + "\");" + textBox2.Text);
                //发条.发条窗口.添加变量(textBox4.Text);
                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                Close();

            }
            else
            {
                label16.Text = "变量名不能为空";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "变量赋值(" + textBox4.Text + "=\"" + textBox3.Text + "\");" + textBox2.Text);
                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                    }
                    //发条.发条窗口.添加变量(textBox4.Text);
                    Close();

                }
                else
                {
                        发条.发条窗口.listBox1.Items.Add("变量赋值(" + textBox4.Text + "=\"" + textBox3.Text + "\");" + textBox2.Text);

                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                    }
                    //发条.发条窗口.添加变量(textBox4.Text);
                    Close();

                }


            }
            else
            {
                label16.Text = "变量名不能为空";
            }
        }

        private void 变量赋值_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 变量赋值_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
/*                comboBox1.Items.Add("上个结果");
                int iCount = 动作.读取键("变量名", @".\Generate\variable.ini").Count - 1;
                for (int i = 0; i <= iCount; i++)
                {
                    comboBox1.Items.Add(动作.读取键("变量名", @".\Generate\variable.ini")[i].ToString());
                }*/

                label16.Text = "";
            }
        }

    }
}
