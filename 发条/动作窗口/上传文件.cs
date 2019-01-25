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
    public partial class 上传文件 : Form
    {
        public 上传文件()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "")
            {
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "上传文件(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + textBox4.Text + "\"" +  ");" + textBox2.Text);
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
                    发条.发条窗口.listBox1.Items.Add("上传文件(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + textBox4.Text + "\"" + ");" + textBox2.Text);
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
              /*  //发条.发条窗口.添加变量(textBox6.Text);*/
                Close();
            }
            else
            {
                label21.Text = "参数不能为空";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "")
            {
                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);


                发条.发条窗口.listBox1.Items.Insert(选中行, "上传文件(" + textBox6.Text + ",\"" + textBox3.Text + "\",\"" + textBox4.Text + "\"" + ");" + textBox2.Text);

                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                //发条.发条窗口.添加变量(textBox6.Text);
                Close();
            }
            else
            {
                label21.Text = "参数不能为空";
            }
        }

        private void 上传文件_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 上传文件_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "所有文件|*.*";
            ofd.Title = "请选择待上传文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox4.Clear();//清空文本框
                    string[] str = ofd.FileNames;//所选文件路径
                    textBox4.Text = str[0];
                }
                catch//如果报错
                {
                    发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "发条错误", "请选择有效路径文件", ToolTipIcon.Error);
                }
            }
        }
    }
}
