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
using 动作库;

namespace fatiao
{
    public partial class 生成程序 : Form
    {
        public static 生成程序 程序窗口 = null; //用来引用主窗口
        public 生成程序()
        {
            InitializeComponent();
            程序窗口 = this; //赋值主窗口
        }

        public int 按键一位置X, 按键二位置X, 按键三位置X, 按键四位置X, 文本位置X, 运行次数位置X,窗口大小X, 按键一位置Y, 按键二位置Y, 按键三位置Y, 按键四位置Y, 文本位置Y, 运行次数位置Y, 窗口大小Y;
        private void 生成程序_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(@".\Process"))//存在
            {
                发条.发条窗口.DelectDir(@".\Process");//清空文件夹
            }

            string 小程序命令 = Environment.CurrentDirectory + @"\winapp.dll";
            if (File.Exists(小程序命令))
            {
                File.Delete(小程序命令);//删除
            }
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        触发条件 触发条件 = new 触发条件();
        private void label15_Click(object sender, EventArgs e)
        {
            触发条件.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 生成程序_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                if (comboBox1.Text == "") 
                {
                    comboBox1.Text = "启用";
                }
                label16.Text = "";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text!="")
            {
                string 小程序命令 = Environment.CurrentDirectory + @"\winapp.dll";
                if (File.Exists(小程序命令))
                {
                    File.Delete(小程序命令);//删除
                }

                if (!Directory.Exists(@".\Process"))//不存在就创建
                {
                    Directory.CreateDirectory(@".\Process");
                }
                else
                {
                    发条.发条窗口.DelectDir(@".\Process");//清空文件夹
                }
                string 小程序配置 = Environment.CurrentDirectory + @"\Process\config.ini";
                if (File.Exists(小程序配置))
                {
                    File.Delete(小程序配置);//删除
                }
                if (!File.Exists(小程序配置))
                {
                    File.Create(小程序配置).Dispose();//生成文件后解除占用
                }
                动作.写入ini("程序", "程序名", textBox3.Text, 小程序配置);
                动作.写入ini("程序", "运行次数", comboBox1.Text, 小程序配置);
                动作.写入ini("程序", "按键一名", listView1.Items[2].SubItems[2].Text, 小程序配置);
                动作.写入ini("程序", "按键二名", listView1.Items[3].SubItems[2].Text, 小程序配置);
                动作.写入ini("程序", "按键三名", listView1.Items[4].SubItems[2].Text, 小程序配置);
                动作.写入ini("程序", "按键四名", listView1.Items[5].SubItems[2].Text, 小程序配置);

                动作.写入ini("事件", "打开程序", listView1.Items[0].SubItems[1].Text, 小程序配置);
                动作.写入ini("事件", "关闭程序", listView1.Items[1].SubItems[1].Text, 小程序配置);
                动作.写入ini("事件", "按键一", listView1.Items[2].SubItems[1].Text, 小程序配置);
                动作.写入ini("事件", "按键二", listView1.Items[3].SubItems[1].Text, 小程序配置);
                动作.写入ini("事件", "按键三", listView1.Items[4].SubItems[1].Text, 小程序配置);
                动作.写入ini("事件", "按键四", listView1.Items[5].SubItems[1].Text, 小程序配置);

                动作.写入ini("热键", "快捷键一", listView1.Items[2].SubItems[3].Text, 小程序配置);
                动作.写入ini("热键", "快捷键二", listView1.Items[3].SubItems[3].Text, 小程序配置);
                动作.写入ini("热键", "快捷键三", listView1.Items[4].SubItems[3].Text, 小程序配置);
                动作.写入ini("热键", "快捷键四", listView1.Items[5].SubItems[3].Text, 小程序配置);

                动作.写入ini("设计", "按键一位置X", 按键一位置X.ToString(), 小程序配置);
                动作.写入ini("设计", "按键二位置X", 按键二位置X.ToString(), 小程序配置);
                动作.写入ini("设计", "按键三位置X", 按键三位置X.ToString(), 小程序配置);
                动作.写入ini("设计", "按键四位置X", 按键四位置X.ToString(), 小程序配置);
                动作.写入ini("设计", "文本位置X", 文本位置X.ToString(), 小程序配置);
                动作.写入ini("设计", "运行次数位置X", 运行次数位置X.ToString(), 小程序配置);

                动作.写入ini("设计", "窗口大小X", 窗口大小X.ToString(), 小程序配置);

                动作.写入ini("设计", "按键一位置Y", 按键一位置Y.ToString(), 小程序配置);
                动作.写入ini("设计", "按键二位置Y", 按键二位置Y.ToString(), 小程序配置);
                动作.写入ini("设计", "按键三位置Y", 按键三位置Y.ToString(), 小程序配置);
                动作.写入ini("设计", "按键四位置Y", 按键四位置Y.ToString(), 小程序配置);
                动作.写入ini("设计", "文本位置Y", 文本位置Y.ToString(), 小程序配置);
                动作.写入ini("设计", "运行次数位置Y", 运行次数位置Y.ToString(), 小程序配置);

                动作.写入ini("设计", "窗口大小Y", 窗口大小Y.ToString(), 小程序配置);

                发条.发条窗口.CopyDirectory(@".\Command", @".\Process\Command");
                发条.发条窗口.CopyDirectory(@".\Generate", @".\Process\Generate");
                发条.发条窗口.CopyDirectory(@".\素材库", @".\Process\素材库");
                string 压缩目录 = @".\Process";

                ZipFile.CreateFromDirectory(压缩目录, 小程序命令);

                if (!Directory.Exists(@".\Process"))//不存在就创建
                {
                    Directory.CreateDirectory(@".\Process");
                }
                else
                {
                    发条.发条窗口.DelectDir(@".\Process");
                }


                File.Copy(@".\.NET Framework环境.exe", @".\Process\.NET Framework环境.exe");
                File.Copy(@".\winapp.dll", @".\Process\winapp.dll");
                File.Copy(@".\winapp.exe", @".\Process\winapp.exe");

                SaveFileDialog saveDlg = new SaveFileDialog//用户选择保存目录和自定义文件名
                {
                    Filter = "文件名(*.*) | *.*"
                };
                saveDlg.Title = "生成程序：";
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    string 生成路径 = saveDlg.FileName;//saveDlg.FileName是自定义目录+文件名
                    if (File.Exists(生成路径))//如果存在就删除（变相覆盖）
                    {
                        File.Delete(生成路径);
                    }
                    发条.发条窗口.CopyDirectory(@".\Process", 生成路径);
                    发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "", "生成程序成功", ToolTipIcon.None);
                }
                Close();
            }
            else
            {
                label16.Text = "参数不能为空";
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {
            winapp界面 winapp界面 = new winapp界面();
            winapp界面.ShowDialog();
;        }
    }
}
