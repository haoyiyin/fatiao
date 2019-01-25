using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fatiao
{
    public partial class 新增快捷命令 : Form
    {
        public 新增快捷命令()
        {
            InitializeComponent();
        }
        public static 弹幕 弹幕内容 = new 弹幕();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
                    for (int i = 0; i <= iCount; i++)
                    {
                        //Console.WriteLine(发条.发条窗口.listBox4.Items[i]);
                        if (发条.发条窗口.listBox4.Items[i].ToString() == textBox1.Text)
                        {
                            发条.发条窗口.listBox4.Items.Remove(发条.发条窗口.listBox4.Items[i]);
                            break;
                        }
                    }
                    发条.发条窗口.listBox4.Items.Add(textBox1.Text);
                    新增快捷命令保存();
                    Close();
                }
                else
                {
                    label10.Text = "快捷命令名不可用";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        void 新增快捷命令保存()
        {
            if (!Directory.Exists(@".\Process"))//不存在就创建
            {
                Directory.CreateDirectory(@".\Process");
            }
            else
            {
                发条.发条窗口.DelectDir(@".\Process");
            }

            string 快捷命令文件夹 = Environment.CurrentDirectory + @"\Command\";
            string 新增快捷命令 = Environment.CurrentDirectory + @"\Command\" + textBox1.Text;
            string 流程 = Environment.CurrentDirectory + @"\Generate\process";
            if (!Directory.Exists(快捷命令文件夹))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(快捷命令文件夹);//创建该文件夹
            }

            File.Copy(流程, @".\Process\process", true);

            发条.发条窗口.CopyDirectory(@".\素材库", @".\Process\素材库");

            if (File.Exists(新增快捷命令))//如果存在就删除（变相覆盖）
            {
                File.Delete(新增快捷命令);
            }

            ZipFile.CreateFromDirectory(@".\Process\", 新增快捷命令);

            if (Directory.Exists(@".\Process"))//存在就清空
            {
                发条.发条窗口.DelectDir(@".\Process");
            }

        }

        private void 新增快捷命令_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 新增快捷命令_Load(object sender, EventArgs e)
        {
/*            label10.Text = "";*/
        }

        private void 新增快捷命令_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible==true)
            {
                label10.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
