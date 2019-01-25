using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 触边滚轮 : Form
    {
        public 触边滚轮()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox4.Text)
            {
                case "左边":
                    发条.发条窗口.listView1.Items[0].SubItems[1].Text = label15.Text;
                    发条.发条窗口.listView1.Items[0].SubItems[2].Text = label2.Text;
                    发条.发条窗口.listView1.Items[0].SubItems[3].Text = label3.Text;
                    break;
                case "右边":
                    发条.发条窗口.listView1.Items[1].SubItems[1].Text = label15.Text;
                    发条.发条窗口.listView1.Items[1].SubItems[2].Text = label2.Text;
                    发条.发条窗口.listView1.Items[1].SubItems[3].Text = label3.Text;
                    break;
                case "顶边":
                    发条.发条窗口.listView1.Items[2].SubItems[1].Text = label15.Text;
                    发条.发条窗口.listView1.Items[2].SubItems[2].Text = label2.Text;
                    发条.发条窗口.listView1.Items[2].SubItems[3].Text = label3.Text;
                    break;
                case "底边":
                    发条.发条窗口.listView1.Items[3].SubItems[1].Text = label15.Text;
                    发条.发条窗口.listView1.Items[3].SubItems[2].Text = label2.Text;
                    发条.发条窗口.listView1.Items[3].SubItems[3].Text = label3.Text;
                    break;
            }
            //Close();
        }

        private void 触边滚轮_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 触边滚轮_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible==true)
            {
                label15.Text = 发条.发条窗口.滚轮下滑;
                label2.Text = 发条.发条窗口.滚轮上滑;
                label3.Text = 发条.发条窗口.滚轮点击;
                comboBox4.Text = "左边";
            }
        }

        int 点击 = 0;
        private void label15_Click(object sender, EventArgs e)
        {
            点击 = 1;
            contextMenuStrip2.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            contextMenuStrip2.Items.Add("无");
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip2.Items.Add(发条.发条窗口.listBox4.Items[i].ToString());
            }
            if (contextMenuStrip2.Items.Count > 0)
            {
                contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            点击 = 2;
            contextMenuStrip2.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            contextMenuStrip2.Items.Add("无");
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip2.Items.Add(发条.发条窗口.listBox4.Items[i].ToString());
            }
            if (contextMenuStrip2.Items.Count > 0)
            {
                contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            点击 = 3;
            contextMenuStrip2.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            contextMenuStrip2.Items.Add("无");
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip2.Items.Add(发条.发条窗口.listBox4.Items[i].ToString());
            }
            if (contextMenuStrip2.Items.Count > 0)
            {
                contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem items in contextMenuStrip2.Items)
            {
                if (items.Selected == true)
                    switch (点击)
                    {
                        case 1:
                            label15.Text = items.ToString();
                            break;
                        case 2:
                            label2.Text = items.ToString();
                            break;
                        case 3:
                            label3.Text = items.ToString();
                            break;
                    }

            }
        }
    }
}
