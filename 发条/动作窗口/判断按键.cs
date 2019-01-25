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
    public partial class 判断按键 : Form
    {
        public 判断按键()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            int 选中行;
            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);
            发条.发条窗口.listBox1.Items.Insert(选中行, "判断按键(" + textBox4.Text + "," + comboBox1.Text + ",\"" + textBox1.Text + "\");" + textBox3.Text);
            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            //发条.发条窗口.添加变量(textBox4.Text);
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (发条.发条窗口.listBox1.SelectedIndex != -1)
            {
                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "判断按键(" + textBox4.Text + "," + comboBox1.Text + ",\"" + textBox1.Text + "\");" + textBox3.Text);
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
                发条.发条窗口.listBox1.Items.Add("判断按键(" + textBox4.Text + "," + comboBox1.Text + ",\"" + textBox1.Text + "\");" + textBox3.Text);
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder();
            keyValue.Length = 0;
            keyValue.Append("");
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    //keyValue.Append("17");
                    keyValue.Append("Ctrl");
                if (e.Alt)
                    //keyValue.Append("18");
                    keyValue.Append("Alt");
                if (e.Shift)
                    //keyValue.Append("16");
                    keyValue.Append("Shift");
            }
            else
            {
                if ((e.KeyValue >= 32 && e.KeyValue <= 47) ||
                    (e.KeyValue >= 8 && e.KeyValue <= 20) ||
                    (e.KeyValue >= 186 && e.KeyValue <= 222) ||
                    (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                    (e.KeyValue >= 96 && e.KeyValue <= 123))   //F1-F12
                {
                    keyValue.Append(e.KeyCode);
                }
                else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
                {
                    keyValue.Append(e.KeyCode.ToString().Substring(1));
                }
                else if (e.KeyValue == 27)
                {
                    keyValue.Append(e.KeyCode);
                }
                else if (e.KeyValue == 91)
                {
                    keyValue.Append(e.KeyCode);
                }
                else if (e.KeyValue == 92)
                {
                    keyValue.Append(e.KeyCode);
                }
            }
            ActiveControl.Text = "";
            //设置当前活动控件的文本内容
            ActiveControl.Text = keyValue.ToString() + "键[" + e.KeyValue + "]";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标左键" + "[1]";
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标右键" + "[2]";
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标中键" + "[4]";
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标X1键" + "[5]";
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标X2键" + "[6]";
        }

        private void 音量加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "音量加键" + "[175]";
        }

        private void 音量减ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "音量减键" + "[174]";
        }

        private void 静音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "静音键" + "[173]";
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "停止键" + "[179]";
        }

        private void 浏览器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "浏览器键" + "[172]";
        }

        private void 邮件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "邮件键" + "[180]";
        }

        private void 收藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "收藏键" + "[171]";
        }

        private void 浏览器后退ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "网页退后键" + "[166]";
        }

        private void 浏览器前进ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "网页前进键" + "[167]";
        }

        private void 浏览器主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "打开主页键" + "[172]";
        }

        private void 网页刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "网页刷新键" + "[168]";
        }

        private void 搜索内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "搜索栏键" + "[170]";
        }

        private void 停止加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "停止加载键" + "[169]";
        }

        private void 判断按键_Load(object sender, EventArgs e)
        {
/*            label16.Text = "";*/
        }

        private void 判断按键_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 判断按键_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Show(MousePosition.X, MousePosition.Y);
        }

        private void tAB键ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Tab键" + "[9]";
        }
    }
}
