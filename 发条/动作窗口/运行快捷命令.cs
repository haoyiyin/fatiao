using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 运行快捷命令 : Form
    {
        public 运行快捷命令()
        {
            InitializeComponent();
        }

        private void 运行快捷命令_Load(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        快捷命令 快捷命令 = new 快捷命令();

        private void button1_Click(object sender, EventArgs e)
        {

            string 快捷命令名称 = comboBox1.Items[comboBox1.SelectedIndex].ToString();//读取快捷命令名称

            if (checkBox1.Checked)
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "运行命令(" + 快捷命令名称 + ",异步);");
                }
                else
                {
                        发条.发条窗口.listBox1.Items.Add("运行命令(" + 快捷命令名称 + ",异步);");
                }
            }
            else
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "运行命令(" + 快捷命令名称 + ",同步);");
                }
                else
                {
                        发条.发条窗口.listBox1.Items.Add("运行命令(" + 快捷命令名称 + ",同步);");
                }
            }
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;
            string 快捷命令名称 = comboBox1.Items[comboBox1.SelectedIndex].ToString();//读取快捷命令名称
            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);
            if (checkBox1.Checked)
            {
                发条.发条窗口.listBox1.Items.Insert(选中行, "运行命令(" + 快捷命令名称 + ",异步);");
            }
            else
            {
                发条.发条窗口.listBox1.Items.Insert(选中行, "运行命令(" + 快捷命令名称 + ",同步);");
            }
            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void 运行快捷命令_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 运行快捷命令_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible==true)
            {
                comboBox1.Items.Clear();
                int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
                for (int i = 0; i <= iCount; i++)
                {
                    comboBox1.Items.Add(发条.发条窗口.listBox4.Items[i].ToString());
                }
                comboBox1.Text = 发条.发条窗口.点击快捷命令;
                if (comboBox1.Text == "")
                {
                    comboBox1.SelectedIndex = 0;
                }

            }
        }
    }
}
