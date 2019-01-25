using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 触发条件 : Form
    {
        public 触发条件()
        {
            InitializeComponent();
        }
        
        private void label15_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            
        }

        private void 触发条件_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    label15.Text = items.ToString();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
                    label15.Text = "不启用";
            
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            label15.Text = "运行当前流程";
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip2.Items.Add(发条.发条窗口.listBox4.Items[i].ToString());
            }
            if (contextMenuStrip2.Items.Count > 0)
            {
                contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            label15.Text = "停止当前流程";
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            label15.Text = "停止命令";
        }

        private void 触发条件_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label16.Text = "";
                comboBox4.Text = "打开程序";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "打开程序"|| comboBox4.Text == "关闭程序")
            {
                textBox3.Text = "不适用";
                textBox3.Enabled = false;
            }
            else
            {
                textBox3.Text = "开始";
                textBox3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                switch (comboBox4.Text)
                {
                    case "打开程序":
                        生成程序.程序窗口.listView1.Items[0].SubItems[1].Text = label15.Text;
                        生成程序.程序窗口.listView1.Items[0].SubItems[2].Text = "不适用";
                        生成程序.程序窗口.listView1.Items[0].SubItems[3].Text = "不适用";
                        break;
                    case "关闭程序":
                        生成程序.程序窗口.listView1.Items[1].SubItems[1].Text = label15.Text;
                        生成程序.程序窗口.listView1.Items[1].SubItems[2].Text = "不适用";
                        生成程序.程序窗口.listView1.Items[1].SubItems[3].Text = "不适用";
                        break;
                    case "按键一":
                        生成程序.程序窗口.listView1.Items[2].SubItems[1].Text = label15.Text;
                        if (label15.Text=="不启用")
                        {
                            生成程序.程序窗口.listView1.Items[2].SubItems[2].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[2].SubItems[2].Text = textBox3.Text;
                        }
                        if (textBox1.Text == "")
                        {
                            生成程序.程序窗口.listView1.Items[2].SubItems[3].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[2].SubItems[3].Text = textBox1.Text;
                        }

                        break;
                    case "按键二":
                        生成程序.程序窗口.listView1.Items[3].SubItems[1].Text = label15.Text;
                        if (label15.Text == "不启用")
                        {
                            生成程序.程序窗口.listView1.Items[3].SubItems[2].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[3].SubItems[2].Text = textBox3.Text;
                        }
                        if (textBox1.Text == "")
                        {
                            生成程序.程序窗口.listView1.Items[3].SubItems[3].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[3].SubItems[3].Text = textBox1.Text;
                        }
                        break;
                    case "按键三":
                        生成程序.程序窗口.listView1.Items[4].SubItems[1].Text = label15.Text;
                        if (label15.Text == "不启用")
                        {
                            生成程序.程序窗口.listView1.Items[4].SubItems[2].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[4].SubItems[2].Text = textBox3.Text;
                        }
                        if (textBox1.Text == "")
                        {
                            生成程序.程序窗口.listView1.Items[4].SubItems[3].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[4].SubItems[3].Text = textBox1.Text;
                        }
                        break;
                    case "按键四":
                        生成程序.程序窗口.listView1.Items[5].SubItems[1].Text = label15.Text;
                        if (label15.Text == "不启用")
                        {
                            生成程序.程序窗口.listView1.Items[5].SubItems[2].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[5].SubItems[2].Text = textBox3.Text;
                        }
                        if (textBox1.Text == "")
                        {
                            生成程序.程序窗口.listView1.Items[5].SubItems[3].Text = "不适用";
                        }
                        else
                        {
                            生成程序.程序窗口.listView1.Items[5].SubItems[3].Text = textBox1.Text;
                        }
                        break;
                }

            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(sender, e);
            if (textBox1.Text != "")
            {
                //注销Id号为100的热键设定

                热键.HotKey.UnregisterHotKey(Handle, 100);
            }
        }

        private new void KeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder
            {
                Length = 0
            };
            keyValue.Append("");
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    keyValue.Append("Ctrl + ");
                if (e.Alt)
                    keyValue.Append("Alt + ");
                if (e.Shift)
                    keyValue.Append("Shift + ");
            }
            if ((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                (e.KeyValue >= 112 && e.KeyValue <= 123))   //F1-F12
            {
                keyValue.Append(e.KeyCode);
            }
            else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
            {
                keyValue.Append(e.KeyCode.ToString().Substring(1));
            }
            ActiveControl.Text = "";
            //设置当前活动控件的文本内容
            ActiveControl.Text = keyValue.ToString();
        }

        private new void KeyUp(object sender, KeyEventArgs e)
        {
            string str = ActiveControl.Text.TrimEnd();
            int len = str.Length;
            if (len >= 1 && str.Substring(str.Length - 1) == "+")
            {
                ActiveControl.Text = "";
            }
            label1.Focus();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUp(sender, e);
            try
            {
                if (textBox1.Text != "")
                {
                    string[] 按键 = textBox1.Text.Replace(" ", "").Split('+');
                    //注册热键Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
                    switch (按键.Count())
                    {
                        case 4:
                            Keys key3 = (Keys)Enum.Parse(typeof(Keys), 按键[3]);
                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key3);
                            break;
                        case 3:
                            Keys key2 = (Keys)Enum.Parse(typeof(Keys), 按键[2]);
                            switch (按键[0])
                            {
                                case "Alt":
                                    switch (按键[1])
                                    {
                                        case "Ctrl":
                                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Alt, key2);
                                            break;
                                        case "Shift":
                                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Alt, key2);
                                            break;
                                    }
                                    break;

                                case "Ctrl":
                                    switch (按键[1])
                                    {
                                        case "Alt":
                                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                            break;
                                        case "Shift":
                                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                            break;
                                    }
                                    break;

                                case "Shift":
                                    switch (按键[1])
                                    {
                                        case "Alt":
                                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Shift, key2);
                                            break;
                                        case "Ctrl":
                                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key2);
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 2:
                            Keys key1 = (Keys)Enum.Parse(typeof(Keys), 按键[1]);
                            switch (按键[0])
                            {
                                case "Alt":
                                    热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Alt, key1);
                                    break;

                                case "Ctrl":

                                    热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Ctrl, key1);
                                    break;

                                case "Shift":

                                    热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.Shift, key1);
                                    break;
                            }
                            break;
                        case 1:
                            Keys key = (Keys)Enum.Parse(typeof(Keys), textBox1.Text);
                            热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.None, key);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
