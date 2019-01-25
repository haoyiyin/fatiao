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
    public partial class 下载文件 : Form
    {
        public 下载文件()
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
            if (textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "下载文件(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + textBox4.Text  + textBox5.Text + "\"," + comboBox1.Text + ");" + textBox2.Text);
                    if (textBox6.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "﹁如果始" + "(" + textBox6.Text + "等于" + "\"" + "真" + "\"" + ");");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 3, "注释(添加内容到事件里);");
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 4, "﹂如果末;");
                    }
                    else
                    {
                        if (textBox2.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                    }
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Add("下载文件(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + textBox4.Text  + textBox5.Text + "\"," + comboBox1.Text + ");" + textBox2.Text);
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
                /*//发条.发条窗口.添加变量(textBox6.Text);*/
                Close();
            }
            else
            {
                label21.Text = "网址或保存目录及文件名数据不能为空";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox5.Text != "" && textBox4.Text != "")
            {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);


                发条.发条窗口.listBox1.Items.Insert(选中行, "下载文件(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + textBox4.Text + textBox5.Text + "\"," + comboBox1.Text + ");" + textBox2.Text);

                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox6.Text);
                Close();
            }
            else
            {
                label21.Text = "网址或保存目录及文件名数据不能为空";
            }
        }

        private void textBox4_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog wjj = new FolderBrowserDialog();//实例化  提示用户选择文件夹。
            wjj.Description = "请选择下载文件保存目录";//对话框  提示信息
            if (wjj.ShowDialog() == DialogResult.OK) //判断是否点击了 OK 按钮
            {
                string foldPath = wjj.SelectedPath;//将选择的路径 赋值给 foldPath 变量
                textBox4.Clear();//清空路径 文本框
                textBox4.Text = foldPath+@"\";//将 路径 赋值给 文本框
                //path = txtlj.Text + @"\";//将路径 最后 加上 \ 赋值给 path 路径。
            }
        }

        private void 下载文件_Load(object sender, EventArgs e)
        {
/*            label21.Text = "";*/
        }

        private void 下载文件_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 下载文件_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";
            }
        }
    }
}
