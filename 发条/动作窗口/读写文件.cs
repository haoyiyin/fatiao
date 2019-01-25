using System;
using System.Drawing;
using System.Windows.Forms;

namespace fatiao.动作库
{
    public partial class 读写文件 : Form
    {
        public 读写文件()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void 文本处理_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                if (comboBox1.Text == "")
                {
                    comboBox1.SelectedIndex = 0;
                }

                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox9.BackColor = Color.White;//白色
                    textBox9.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button5.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox9.BackColor = Color.Linen;//黄色
                    textBox9.ForeColor = Color.Maroon;//红色
                    button5.BackgroundImage = Properties.Resources.开启变量;
                }

                if (button3.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox5.BackColor = Color.White;//白色
                    textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button3.BackgroundImage = Properties.Resources.关闭变量;
                    label13.Text = "请输入节点名";
                    ;
                }
                else//红色
                {
                    textBox5.BackColor = Color.Linen;//黄色
                    textBox5.ForeColor = Color.Maroon;//红色
                    button3.BackgroundImage = Properties.Resources.开启变量;
                    label13.Text = "请输入读取的变量名";
                }

                if (button7.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox6.BackColor = Color.White;//白色
                    textBox6.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button7.BackgroundImage = Properties.Resources.关闭变量;
                    label14.Text = "请输入键名";
                    ;
                }
                else//红色
                {
                    textBox6.BackColor = Color.Linen;//黄色
                    textBox6.ForeColor = Color.Maroon;//红色
                    button7.BackgroundImage = Properties.Resources.开启变量;
                    label14.Text = "请输入读取的变量名";
                }

            }
            label16.Text = "";
        }

        private void 文本处理_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        string 内容一, 内容二, 内容三;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "")
            {
                if (button3.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox5.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox5.Text;
                }
                if (button7.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容二 = "\"" + textBox6.Text + "\"";
                }
                else//红色
                {
                    内容二 = textBox6.Text;
                }

                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容三 = "\"" + textBox9.Text + "\"";
                }
                else//红色
                {
                    内容三 = textBox9.Text;
                }

                if (发条.发条窗口.listBox1.SelectedIndex != -1)
                {
                    switch (comboBox1.Text)
                    {
                        case "文本读取":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "读写文件(" + textBox4.Text + "," + comboBox1.Text + ",\"" + textBox1.Text + "\"" + ");" + textBox3.Text);
                            break;
                        case "配置读取":

                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + ",\"" + textBox1.Text + "\"" + ");" + textBox3.Text);

                            break;
                        case "文本写入":
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容三 + ",\"" + textBox1.Text + "\");" + textBox3.Text);
                            break;
                        case "配置写入":

                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + "," + 内容三 + ",\"" + textBox1.Text + "\");" + textBox3.Text);

                            break;
                    }
                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox2.Text + ");");
                    }
                }
                else
                {
                    switch (comboBox1.Text)
                    {
                        case "文本读取":
                            发条.发条窗口.listBox1.Items.Add("读写文件(" + textBox4.Text + "," + comboBox1.Text + ",\"" + textBox1.Text + "\"" + ");" + textBox3.Text);

                            break;
                        case "配置读取":

                            发条.发条窗口.listBox1.Items.Add("读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + ",\"" + textBox1.Text + "\"" + ");" + textBox3.Text);

                            break;
                        case "文本写入":
                            发条.发条窗口.listBox1.Items.Add("读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容三 + ",\"" + textBox1.Text + "\");" + textBox3.Text);
                            break;
                        case "配置写入":
                            发条.发条窗口.listBox1.Items.Add("读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + "," + 内容三 + ",\"" + textBox1.Text + "\");" + textBox3.Text);
                            break;
                    }
                    if (textBox2.Text != "")
                    {
                        发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox2.Text + ");");
                    }
                }
                //发条.发条窗口.添加变量(textBox4.Text);

                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "")
            {
                if (button3.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容一 = "\"" + textBox5.Text + "\"";
                }
                else//红色
                {
                    内容一 = textBox5.Text;
                }
                if (button7.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容二 = "\"" + textBox6.Text + "\"";
                }
                else//红色
                {
                    内容二 = textBox6.Text;
                }
                if (button5.ForeColor != Color.Maroon)//等于蓝色
                {
                    内容三 = "\"" + textBox9.Text + "\"";
                }
                else//红色
                {
                    内容三 = textBox9.Text;
                }

                int 选中行;
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);
                switch (comboBox1.Text)
                {
                    case "文本读取":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "读写文件(" + textBox4.Text + "," + comboBox1.Text + ",\"" + textBox1.Text + "\"" + ");" + textBox3.Text);
                        break;
                    case "配置读取":

                        发条.发条窗口.listBox1.Items.Insert(选中行, "读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + ",\"" + textBox1.Text + "\"" + ");" + textBox3.Text);

                        break;
                    case "文本写入":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容三 + ",\"" + textBox1.Text + "\");" + textBox3.Text);
                        break;
                    case "配置写入":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "读写文件(" + textBox4.Text + "," + comboBox1.Text + "," + 内容一 + "," + 内容二 + "," + 内容三 + ",\"" + textBox1.Text + "\");" + textBox3.Text);

                        break;
                }
                发条.发条窗口.listBox1.SelectedIndex = 选中行;

                //发条.发条窗口.添加变量(textBox4.Text);
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "文本读取":
                    label10.Visible = false;
                    label12.Visible = false;
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    button3.Visible = false;
                    button7.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    label17.Visible = false;
                    textBox9.Visible = false;
                    button5.Visible = false;
                    label15.Visible = false;
                    break;
                case "配置读取":
                    label10.Visible = true;
                    label12.Visible = true;
                    textBox5.Visible = true;
                    textBox6.Visible = true;
                    button3.Visible = true;
                    button7.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label17.Visible = false;
                    textBox9.Visible = false;
                    button5.Visible = false;
                    label15.Visible = false;
                    break;
                case "文本写入":
                    label10.Visible = false;
                    label12.Visible = false;
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    button3.Visible = false;
                    button7.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    label17.Visible = true;
                    textBox9.Visible = true;
                    button5.Visible = true;
                    label15.Visible = true;
                    break;
                case "配置写入":
                    label10.Visible = true;
                    label12.Visible = true;
                    textBox5.Visible = true;
                    textBox6.Visible = true;
                    button3.Visible = true;
                    button7.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label17.Visible = true;
                    textBox9.Visible = true;
                    button5.Visible = true;
                    label15.Visible = true;
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            if (button3.ForeColor != Color.Maroon)//等于蓝色
            {
                button3.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button3.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox5.BackColor = Color.White;//白色
                textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button3.BackgroundImage = Properties.Resources.关闭变量;
                label13.Text = "请输入节点名";
                ;
            }
            else//红色
            {
                textBox5.BackColor = Color.Linen;//黄色
                textBox5.ForeColor = Color.Maroon;//红色
                button3.BackgroundImage = Properties.Resources.开启变量;
                label13.Text = "请输入读取的变量名";
            }

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip1.Items)
            {
                if (items.Selected == true)
                    textBox5.Text = items.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            if (button7.ForeColor != Color.Maroon)//等于蓝色
            {
                button7.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button7.ForeColor = Color.FromArgb(0, 120, 215);//蓝色

            }

            if (button7.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox6.BackColor = Color.White;//白色
                textBox6.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button7.BackgroundImage = Properties.Resources.关闭变量;
                label14.Text = "请输入键名";
                ;
            }
            else//红色
            {
                textBox6.BackColor = Color.Linen;//黄色
                textBox6.ForeColor = Color.Maroon;//红色
                button7.BackgroundImage = Properties.Resources.开启变量;
                label14.Text = "请输入读取的变量名";
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox9.Text = "";
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
                textBox9.BackColor = Color.White;//白色
                textBox9.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button5.BackgroundImage = Properties.Resources.关闭变量;
                label15.Text = "请输入数据内容";
;            }
            else//红色
            {
                textBox9.BackColor = Color.Linen;//黄色
                textBox9.ForeColor = Color.Maroon;//红色
                button5.BackgroundImage = Properties.Resources.开启变量;
                label15.Text = "请输入读取的变量名";
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "请选择读取的文件",
                Filter = "全部文件|*.*"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] names = fileDialog.FileNames;
                foreach (string file in names)
                {
                    textBox1.Text = file;//将选择的路径 赋值给 foldPath 变量           
                }
            }
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    textBox9.Text = items.ToString();
            }
        }

        private void contextMenuStrip3_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip3.Items)
            {
                if (items.Selected == true)
                    textBox6.Text = items.ToString();
            }
        }
    }
}
