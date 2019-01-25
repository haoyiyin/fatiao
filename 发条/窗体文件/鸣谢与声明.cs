using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 鸣谢与声明 : Form
    {
        public 鸣谢与声明()
        {
            InitializeComponent();
        }

        private void 用户协议(object sender, EventArgs e)
        {
            Process.Start("https://store.steampowered.com//eula/1416190_eula_0");
        }

        private void 鸣谢与声明_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }
    }
}
