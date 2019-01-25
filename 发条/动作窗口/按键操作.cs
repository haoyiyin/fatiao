using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 按键操作 : Form
    {

        public 按键操作()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        string 内容一;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\""+textBox3.Text+"\"";
                }
                else//红色
                {
                    内容一 = textBox4.Text;
                }
                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "按键操作(" + comboBox1.Text + "," + 内容一 + ");" + textBox2.Text);

                    if (textBox1.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                    }
                }
                else
                {
                    发条.发条窗口.listBox1.Items.Add("按键操作(" + comboBox1.Text + "," + 内容一 + ");" + textBox2.Text);

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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int 选中行;
            if (button5.ForeColor != Color.Maroon)//等于蓝色
            {
                内容一 = "\"" + textBox3.Text + "\"";
            }
            else//红色
            {
                内容一 = textBox4.Text;
            }
            选中行 = 发条.发条窗口.listBox1.SelectedIndex;
            发条.发条窗口.listBox1.Items.RemoveAt(选中行);

            发条.发条窗口.listBox1.Items.Insert(选中行, "按键操作(" + comboBox1.Text + "," + 内容一 + ");" + textBox2.Text);

            发条.发条窗口.listBox1.SelectedIndex = 选中行;
            Close();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
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
            ActiveControl.Text = keyValue.ToString()+"键["+ e.KeyValue+"]";
        }

        private void label11_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Show(MousePosition.X, MousePosition.Y);
        }

        private void 鼠标左键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "鼠标左键" + "[1]";
        }

        private void 鼠标右键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "鼠标右键" + "[2]";
        }

        private void 鼠标中键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "鼠标中键" + "[4]";
        }

        private void 鼠标X1键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "鼠标X1键" + "[5]";
        }

        private void 鼠标X2键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "鼠标X2键" + "[6]";
        }

        private void 音量加键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "音量加键" + "[175]";
        }

        private void 音量减键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "音量减键" + "[174]";
        }

        private void 静音键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "静音键" + "[173]";
        }

        private void 停止键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "停止键" + "[179]";
        }

        private void 浏览器键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "浏览器键" + "[172]";
        }

        private void 邮件键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "邮件键" + "[180]";
        }

        private void 收藏键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "收藏键" + "[171]";
        }

        private void 网页退后键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "网页退后键" + "[166]";
        }

        private void 网页前进键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "网页前进键" + "[167]";
        }

        private void 打开主页键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "打开主页键" + "[172]";
        }

        private void 网页刷新键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "网页刷新键" + "[168]";
        }

        private void 搜索栏键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "搜索栏键" + "[170]";
        }

        private void 停止加载键_Click(object sender, EventArgs e)
        {
            textBox3.Text = "停止加载键" + "[169]";
        }


        private void textBox3_Enter(object sender, EventArgs e)
        {
            动作.显示屏幕键盘();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            动作.隐藏屏幕键盘();
        }

        private void 按键操作_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 按键操作_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label9.Text = "";

                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.Visible = true;
                    label11.Visible = true;
                    textBox4.Visible = false;
                    label5.Text = "按下键盘输入按键";
                    button5.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox3.Visible = false;
                    label11.Visible = false;
                    textBox4.Visible = true;
                    label5.Text = "请输入读取的变量名";
                    button5.BackgroundImage = Properties.Resources.开启变量;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.ForeColor != Color.Maroon)//等于蓝色
            {
                button5.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button5.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox3.Visible = true;
                label11.Visible = true;
                textBox4.Visible = false;
                label5.Text = "按下键盘输入键盘键码值";
                button5.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox3.Visible = false;
                label11.Visible = false;
                textBox4.Visible = true;
                label5.Text = "请输入读取的变量名";
                button5.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void tAB键ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox3.Text = "Tab键" + "[9]";
        }
    }
}
