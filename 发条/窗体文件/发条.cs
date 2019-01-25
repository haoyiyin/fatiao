using fatiao.代码文件;
using fatiao.动作库;
using fatiao.动作窗口;
using fatiao.窗体文件;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{

    public partial class 发条 : Form
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetF(); //获得本窗体的句柄
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetF(IntPtr hWnd); //设置此窗体为活动窗体

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();//桌面句柄

        public FileDropHandler FileDroper;

        public static 快捷命令 快捷命令 = new 快捷命令();

        public static 触边钩子 触边钩子 = new 触边钩子();

        public 动作类型 动作类型 = new 动作类型();

        public bool 是否订阅;

        public string 参数窗口;

        public static 发条 发条窗口 = null; //用来引用主窗口

        public string 输入内容;

        public string 输入变量值;

        public string 快捷命令输入变量值;

        public string 参数文字;

        //public static CancellationTokenSource 线程开关 = new CancellationTokenSource();//控制退出线程

        public 发条()
        {
            InitializeComponent();
            发条窗口 = this; //赋值主窗口

            //设置窗体的双缓冲
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

        }

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }


        public TSPlugLib.TSPlugInterFace fatiao = new TSPlugLib.TSPlugInterFace();//新的dll

        public static int[] 整数数值;

        public static string[] 逻辑数值;

        public string[] 逻辑变量名;//获取自定义变量字符串

        public string[] 整数变量名;//获取自定义变量字符串

        public static string[] 文本数值;

        public string[] 文本变量名;//获取自定义变量字符串



        public bool 暂停作用 = false;//用作暂停线程

        public bool 停止作用 = false;//用作停止线程

        public bool 缓停止作用 = false;//用作缓停止线程

        public bool 调试作用 = false;//用作调试线程

        public bool 点击逐行 = false;//用作调试线程

        public bool 动作行快捷键 = true;

        private void 暂停()
        {
            while (暂停作用 == true)
            {
                Delay(100);
            }
        }

        protected override CreateParams CreateParams//解决界面闪烁的问题
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }



        [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SteamAPI_RestartAppIfNecessary(int unOwnAppID); //steam验证

        string 程序版本 = "2.8.9";

    private void 发条_Load(object sender, EventArgs e)
        {
            动作类型.添加日志("当前版本："+程序版本);

            if (分辨率缩放.ScaleX > 1)
            {
                动作类型.添加日志("当前缩放(" + (分辨率缩放.ScaleX * 100).ToString() + "%)不等于100%，截图功能将无法清晰显示");
            }

            是否订阅 = 动作.订阅验证();

            if (是否订阅)//当初始化成功
            {
                加载订阅();//创意工坊订阅
            }
            else//初始化失败
            {
/*                string 服务器版本 = 动作.读网络文本(程序版本, "https://howie.oss-cn-hangzhou.aliyuncs.com/fatiao/Config.ini");
                if (程序版本 != 服务器版本)
                {
                    label6.Visible = true;
                }*/
            }

            读取();

            FileDroper = new FileDropHandler(tabPage1); //初始化

            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;

            if (checkBox2.Checked)//最小化启动
            {
                Hide();
                ShowInTaskbar = false;//在任务栏中显示该窗口
                notifyIcon1.ShowBalloonTip(1000, "", "发条已启动", ToolTipIcon.None);
            }

            if (checkBox3.Checked)//启用轮盘
            {
                启用轮盘ToolStripMenuItem.Text = "停用轮盘";
            }
            else
            {
                启用轮盘ToolStripMenuItem.Text = "启用轮盘";
            }

            触边钩子.SetHook();//注册钩子
        }


        public void toolStripButton5_Click(object sender, EventArgs e)
        {
            运行流程.当前流程();
        }

        public void 停止发条()
        {
            listBox1.SelectedIndex = -1;
/*            快捷命令.停止作用 = true;
            快捷命令.暂停作用 = false;*/
            停止作用 = true;
            暂停作用 = false;
            toolStripButton13.Text = "调试";
            toolStripButton13.Enabled = true;
            调试作用 = false;
            动作行快捷键 = true;
            toolStripButton5.Text = "运行";
            toolStripButton5.Image = Properties.Resources.运行;
            toolStripButton1.Text = "暂停";
            toolStripButton1.Image = Properties.Resources.暂停;

            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            /*notifyIcon1.ShowBalloonTip(1000, "", "发条已停止", ToolTipIcon.None);*/

        }

        public 等待时间 等待时间 = new 等待时间();
        public 循环过程 循环过程 = new 循环过程();
        public 如果事件 如果事件 = new 如果事件();
        public 运行与打开 运行与打开 = new 运行与打开();
        public 计算时间差 计算时间差 = new 计算时间差();
        public 判断窗口 判断窗口 = new 判断窗口();
        public 判断按键 判断按键 = new 判断按键();
        public 随机数字 随机数字 = new 随机数字();
        public 基于平面 基于平面 = new 基于平面();
        public 设置窗口 设置窗口 = new 设置窗口();
        public 判断颜色 判断颜色 = new 判断颜色();
        public 查找颜色 查找颜色 = new 查找颜色();
        public 查找图片 查找图片 = new 查找图片();
        public 判断画面 判断画面 = new 判断画面();
        public 鼠标操作 鼠标操作 = new 鼠标操作();
        public 按键操作 按键操作 = new 按键操作();
        public 系统电源 系统电源 = new 系统电源();
        public 蜂鸣提醒 蜂鸣提醒 = new 蜂鸣提醒();
        public 下载文件 下载文件 = new 下载文件();
        public 写到剪贴板 写到剪贴板 = new 写到剪贴板();
        public 发送文本 发送文本 = new 发送文本();
        public 打开网址 打开网址 = new 打开网址();
        public 发送邮件 发送邮件 = new 发送邮件();
        public 消息提示 消息提示 = new 消息提示();
        public 注释 注释 = new 注释();
        public 字符拼接 字符拼接 = new 字符拼接();
        public 停止运行 停止运行 = new 停止运行();
        public 读写文件 读写文件 = new 读写文件();
        public 结束进程 结束进程 = new 结束进程();
        public 跳出循环 跳出循环 = new 跳出循环();
        public 鸣谢与声明 鸣谢与声明 = new 鸣谢与声明();
        public 新增快捷命令 新增快捷命令 = new 新增快捷命令();
        public 整数运算 整数运算 = new 整数运算();
        public 运行快捷命令 运行快捷命令 = new 运行快捷命令();
        public 获取剪贴板 获取剪贴板 = new 获取剪贴板();
        public 请求输入 请求输入 = new 请求输入();
        public 删除文件 删除文件 = new 删除文件();
        public 变量赋值 变量赋值 = new 变量赋值();
        public 获取缩放率 获取缩放率 = new 获取缩放率();
        public 获取设备码 获取设备码 = new 获取设备码();
        public 上传文件 上传文件 = new 上传文件();
        public 获取时间 获取时间 = new 获取时间();
        public 输出日志 输出日志 = new 输出日志();
        public 点击位置 点击位置 = new 点击位置();
        public 点击图片 点击图片 = new 点击图片();

        private void 点击图片窗口(object sender, EventArgs e)
        {
            点击图片.button1.Visible = true;//显示“完成”键
            点击图片.button3.Visible = false;//隐藏“修改”键
            点击图片.Show();
        }

        private void 点击位置窗口(object sender, EventArgs e)
        {
            点击位置.button1.Visible = true;//显示“完成”键
            点击位置.button3.Visible = false;//隐藏“修改”键
            点击位置.Show();
        }

        private void 输出日志窗口(object sender, EventArgs e)
        {
            输出日志.button1.Visible = true;//显示“完成”键
            输出日志.button3.Visible = false;//隐藏“修改”键
            输出日志.Show();
        }
        private void 获取时间窗口(object sender, EventArgs e)
        {
            获取时间.button1.Visible = true;//显示“完成”键
            获取时间.button6.Visible = false;//隐藏“修改”键
            获取时间.Show();
        }
        private void 设备码窗口(object sender, EventArgs e)
        {
            获取设备码.button1.Visible = true;//显示“完成”键
            获取设备码.button6.Visible = false;//隐藏“修改”键
            获取设备码.Show();
        }
        private void 缩放率窗口(object sender, EventArgs e)
        {
            获取缩放率.button1.Visible = true;//显示“完成”键
            获取缩放率.button6.Visible = false;//隐藏“修改”键
            获取缩放率.Show();
        }

        private void 读写文件窗口(object sender, EventArgs e)
        {
            读写文件.button1.Visible = true;//显示“完成”键
            读写文件.button6.Visible = false;//隐藏“修改”键
            读写文件.Show();
        }

        private void 变量赋值窗口(object sender, EventArgs e)
        {
            变量赋值.button1.Visible = true;//显示“完成”键
            变量赋值.button3.Visible = false;//隐藏“修改”键
            变量赋值.Show();
        }

        private void 等待时间窗口(object sender, EventArgs e)
        {
            等待时间.button1.Visible = true;//显示“完成”键
            等待时间.button3.Visible = false;//隐藏“修改”键
            等待时间.Show();
        }

        private void 过程循环窗口(object sender, EventArgs e)
        {
            循环过程.button1.Visible = true;//显示“完成”键
            循环过程.button3.Visible = false;//隐藏“修改”键
            循环过程.Show();
        }

        private void 如果事件窗口(object sender, EventArgs e)
        {
            如果事件.button1.Visible = true;//显示“完成”键
            如果事件.button3.Visible = false;//隐藏“修改”键
            如果事件.Show();
        }

        private void 运行与打开窗口(object sender, EventArgs e)
        {
            运行与打开.button1.Visible = true;//显示“完成”键
            运行与打开.button3.Visible = false;//隐藏“修改”键
            运行与打开.Show();
        }

        private void 计算时间差窗口(object sender, EventArgs e)
        {
            计算时间差.button1.Visible = true;//显示“完成”键
            计算时间差.button6.Visible = false;//隐藏“修改”键
            计算时间差.Show();
        }

        private void 判断窗口窗口(object sender, EventArgs e)
        {
            判断窗口.button1.Visible = true;//显示“完成”键
            判断窗口.button6.Visible = false;//隐藏“修改”键
            判断窗口.Show();
        }

        private void 判断按键窗口(object sender, EventArgs e)
        {
            判断按键.button1.Visible = true;//显示“完成”键
            判断按键.button6.Visible = false;//隐藏“修改”键
            判断按键.Show();
        }

        private void 随机数字窗口(object sender, EventArgs e)
        {
            随机数字.button1.Visible = true;//显示“完成”键
            随机数字.button3.Visible = false;//隐藏“修改”键
            随机数字.Show();
        }

        private void 基于平面窗口(object sender, EventArgs e)
        {
            基于平面.button1.Visible = true;//显示“完成”键
            基于平面.button3.Visible = false;//隐藏“修改”键
            基于平面.Show();
        }

        private void 设置窗口窗口(object sender, EventArgs e)
        {
            设置窗口.button1.Visible = true;//显示“完成”键
            设置窗口.button3.Visible = false;//隐藏“修改”键
            设置窗口.Show();
        }

        private void 判断颜色窗口(object sender, EventArgs e)
        {
            判断颜色.button1.Visible = true;//显示“完成”键
            判断颜色.button6.Visible = false;//隐藏“修改”键
            判断颜色.Show();
        }

        private void 查找颜色窗口(object sender, EventArgs e)
        {
            查找颜色.button1.Visible = true;//显示“完成”键
            查找颜色.button12.Visible = false;//隐藏“修改”键
            查找颜色.Show();
        }

        private void 查找图片窗口(object sender, EventArgs e)
        {
            查找图片.button1.Visible = true;//显示“完成”键
            查找图片.button13.Visible = false;//隐藏“修改”键
            查找图片.Show();
        }

        private void 判断画面窗口(object sender, EventArgs e)
        {
            判断画面.button1.Visible = true;//显示“完成”键
            判断画面.button7.Visible = false;//隐藏“修改”键
            判断画面.Show();
        }

        private void 鼠标操作窗口(object sender, EventArgs e)
        {
            鼠标操作.button1.Visible = true;//显示“完成”键
            鼠标操作.button3.Visible = false;//隐藏“修改”键
            鼠标操作.Show();
        }

        private void 按键操作窗口(object sender, EventArgs e)
        {
            按键操作.button1.Visible = true;//显示“完成”键
            按键操作.button3.Visible = false;//隐藏“修改”键
            按键操作.Show();
        }

        private void 系统电源窗口(object sender, EventArgs e)
        {
            系统电源.button1.Visible = true;//显示“完成”键
            系统电源.button3.Visible = false;//隐藏“修改”键
            系统电源.Show();
        }

        private void 蜂鸣提醒窗口(object sender, EventArgs e)
        {
            蜂鸣提醒.button1.Visible = true;//显示“完成”键
            蜂鸣提醒.button3.Visible = false;//隐藏“修改”键
            蜂鸣提醒.Show();
        }

        private void 上传文件窗口(object sender, EventArgs e)
        {
            上传文件.button1.Visible = true;//显示“完成”键
            上传文件.button3.Visible = false;//隐藏“修改”键
            上传文件.Show();
        }

        private void 下载文件窗口(object sender, EventArgs e)
        {
            下载文件.button1.Visible = true;//显示“完成”键
            下载文件.button3.Visible = false;//隐藏“修改”键
            下载文件.Show();
        }

        private void 写到剪贴板窗口(object sender, EventArgs e)
        {
            写到剪贴板.button1.Visible = true;//显示“完成”键
            写到剪贴板.button3.Visible = false;//隐藏“修改”键
            写到剪贴板.Show();
        }

        private void 发送文本窗口(object sender, EventArgs e)
        {
            发送文本.button1.Visible = true;//显示“完成”键
            发送文本.button3.Visible = false;//隐藏“修改”键
            发送文本.Show();
        }

        private void 打开网址窗口(object sender, EventArgs e)
        {
            打开网址.button1.Visible = true;//显示“完成”键
            打开网址.button3.Visible = false;//隐藏“修改”键
            打开网址.Show();
        }

        private void 发送邮件窗口(object sender, EventArgs e)
        {
            发送邮件.button1.Visible = true;//显示“完成”键
            发送邮件.button3.Visible = false;//隐藏“修改”键
            发送邮件.Show();
        }

        private void 获取剪贴板窗口(object sender, EventArgs e)
        {
            获取剪贴板.button1.Visible = true;//显示“完成”键
            获取剪贴板.button6.Visible = false;//隐藏“修改”键
            获取剪贴板.Show();
        }

        private void 请求输入窗口(object sender, EventArgs e)
        {
            请求输入.button1.Visible = true;//显示“完成”键
            请求输入.button3.Visible = false;//隐藏“修改”键
            请求输入.Show();
        }

        private void 删除文件窗口(object sender, EventArgs e)
        {
            删除文件.button1.Visible = true;//显示“完成”键
            删除文件.button3.Visible = false;//隐藏“修改”键
            删除文件.Show();
        }

        private void 消息提示窗口(object sender, EventArgs e)
        {
            消息提示.button1.Visible = true;//显示“完成”键
            消息提示.button3.Visible = false;//隐藏“修改”键
            消息提示.Show();
        }

        private void 注释窗口(object sender, EventArgs e)
        {
            注释.button1.Visible = true;//显示“完成”键
            注释.button3.Visible = false;//隐藏“修改”键
            注释.Show();
        }

        private void 停止运行窗口(object sender, EventArgs e)
        {
            停止运行.button1.Visible = true;//显示“完成”键
            停止运行.button3.Visible = false;//隐藏“修改”键
            停止运行.Show();
        }

        private void 结束进程窗口(object sender, EventArgs e)
        {
            结束进程.button1.Visible = true;//显示“完成”键
            结束进程.button3.Visible = false;//隐藏“修改”键
            结束进程.Show();
        }

        private void 跳出循环窗口(object sender, EventArgs e)
        {
            跳出循环.button1.Visible = true;//显示“完成”键
            跳出循环.button3.Visible = false;//隐藏“修改”键
            跳出循环.Show();
        }

        private void 整数运算窗口(object sender, EventArgs e)
        {
            整数运算.button1.Visible = true;//显示“完成”键
            整数运算.button3.Visible = false;//隐藏“修改”键
            整数运算.Show();
        }

        public int indexoftarget = -1;
        /// <summary>
        /// 源位置
        /// </summary>
        public int indexofsource = -1;

        编辑动作 编辑动作 = new 编辑动作();
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)//双击
            {
                编辑动作.编辑动作操作(sender, e);
            }
            else
            {
                indexofsource = ((ListBox)sender).IndexFromPoint(e.X, e.Y);

                if (indexofsource != ListBox.NoMatches)
                {
                    ((ListBox)sender).DoDragDrop(((ListBox)sender).Items[indexofsource].ToString(), DragDropEffects.Move);
                }

                if (e.Button == MouseButtons.Right)//判断是否右键为击
                {
                    string 数据 = Clipboard.GetText();
                    if (数据.Contains("http") == true)//判断剪贴板是否为链接
                    {
                        toolStripMenuItem1.Visible = true;//显示粘贴文件链接按键
                    }
                    else
                    {
                        toolStripMenuItem1.Visible = false;//隐藏粘贴文件链接按键
                    }

                    int index = listBox1.IndexFromPoint(e.Location);//右键弹出菜单并选中行
                    if (index >= 0)
                    {
                        listBox1.SelectedIndex = index;
                        contextMenuStrip1.Show(listBox1, e.Location);
                    }
                }
            }

        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            indexoftarget = listbox.IndexFromPoint(listbox.PointToClient(new Point(e.X, e.Y)));
            if (indexoftarget != ListBox.NoMatches)
            {
                //如果目标位置在源位置下方
                if (indexoftarget > indexofsource)
                {
                    listbox.Items.Insert(indexoftarget + 1, listbox.Items[indexofsource]);
                    listbox.Items.RemoveAt(indexofsource);
                    listbox.SelectedIndex = indexoftarget;
                }
                //如果目标位置在源位置上方
                else if (indexoftarget < indexofsource)
                {
                    listbox.Items.Insert(indexoftarget, listbox.Items[indexofsource]);
                    listbox.Items.RemoveAt(indexofsource + 1);
                    listbox.SelectedIndex = indexoftarget;
                }
                else
                { }


            }
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            //拖动源和放置的目的地一定是一个ListBox
            if (e.Data.GetDataPresent(typeof(string)) && ((ListBox)sender).Equals(listBox1))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void 删除_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection sic = listBox1.SelectedIndices;//得到选择的Item的下标

            if (sic.Count == 0)

                return;

            else

            {
                //  将选择的Item放入list中

                List<int> list = new List<int>();

                for (int i = 0; i < sic.Count; i++)

                {
                    list.Add(sic[i]);

                }

                list.Sort();//对list进行排序（库里默认的排序结果一般指的是从下到大的排序）

                while (list.Count != 0)//按照下标从大到小的顺序从ListBox控件里删除选择的Item

                //如果这里采用其它顺序则可能破坏下标的有效性

                {
                    listBox1.Items.RemoveAt(list[list.Count - 1]);

                    list.RemoveAt(list.Count - 1);

                }

            }

        }

        private void 清空_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有动作行？", "清空", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//判断是否 单击 OK按钮
            {
                listBox1.Items.Clear();

                FileInfo file = new FileInfo(@".\Generate\variable.ini");
                if (file.Exists)//判断文件是否存在
                {
                    file.Delete();//文件存在直接删除
                }
                file.Create().Dispose();//直接再新建文件

                if (Directory.Exists(@".\素材库"))//存在就清空
                {
                    DelectDir(@".\素材库");
                }
            }
        }

        private void 启停用_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection sic = listBox1.SelectedIndices;//得到选择的Item的下标

            if (sic.Count == 0)

                return;

            else
            {
                string 每行内容;
                string 分割;
                string 启用;
                //  将选择的Item放入list中

                List<int> list = new List<int>();

                for (int i = 0; i < sic.Count; i++)

                {
                    list.Add(sic[i]);

                }

                list.Sort();//对list进行排序（库里默认的排序结果一般指的是从下到大的排序）
                while (list.Count != 0)//按照下标从大到小的顺序从ListBox控件里删除选择的Item

                //如果这里采用其它顺序则可能破坏下标的有效性

                {
                    每行内容 = listBox1.Items[list[list.Count - 1]].ToString();//读取运行行内容

                    if (每行内容 != "")
                    {
                        分割 = 每行内容.Substring(0, 2);//读取前两个字符
                        if (分割 == "停用")
                        {
                            启用 = 每行内容.Substring(3);//删除前三个字符（"停用:"）
                            listBox1.Items[list[list.Count - 1]] = 启用;//修改当前行内容;
                        }
                        else if (分割 != "注释")
                        {
                            listBox1.Items[list[list.Count - 1]] = "停用:" + 每行内容;//修改当前行内容
                        }
                    }

                    list.RemoveAt(list.Count - 1);

                }

            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                       e.Font,
                       e.Bounds,
                       e.Index,
                       e.State ^ DrawItemState.Selected,
                       e.ForeColor,
                       Color.LightSlateGray);//背景颜色

            if (e.Index >= 0)
            {
                e.DrawBackground();
                _ = Brushes.Black;
                Brush mybsh;
                // 判断是什么类型的标签，改变字体颜色
                if (listBox1.Items[e.Index].ToString().IndexOf("停用:") != -1)
                {
                    mybsh = Brushes.MediumAquamarine;
                }
                else if (listBox1.Items[e.Index].ToString().IndexOf("﹁循环始") != -1)
                {
                    mybsh = Brushes.Orchid;
                }
                else if (listBox1.Items[e.Index].ToString().IndexOf("﹂循环末;") != -1)
                {
                    mybsh = Brushes.Orchid;
                }
                else if (listBox1.Items[e.Index].ToString().IndexOf("﹁如果始") != -1)
                {
                    mybsh = Brushes.Coral;
                }
                else if (listBox1.Items[e.Index].ToString().IndexOf("﹂如果末;") != -1)
                {
                    mybsh = Brushes.Coral;
                }
                else if (listBox1.Items[e.Index].ToString().IndexOf("跳出循环") != -1)
                {
                    mybsh = Brushes.Crimson;
                }
                else
                {
                    mybsh = Brushes.White;
                }
                // 焦点框
                e.DrawFocusRectangle();
                //文本 
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);
            }
        }

        private const int WM_HOTKEY = 0x312; //窗口消息：热键
        private const int WM_CREATE = 0x1; //窗口消息：创建
        private const int WM_DESTROY = 0x2; //窗口消息：销毁

        protected override void WndProc(ref Message m) //全局热键
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_HOTKEY: //窗口消息：热键
                    switch (m.WParam.ToInt32())
                    {
                        case 100: //热键ID
                            toolStripButton5.PerformClick();//运行/停止
                            break;
                        case 101: //热键ID
                            toolStripButton1.PerformClick();//暂停/继续
                            break;
                        case 102: //热键ID
                            toolStripButton13.PerformClick();  //执行调试动作
                            break;
                    }
                    break;
                case WM_CREATE: //窗口消息：创建
                    if (Properties.Settings.Default.运行快捷键 != "")
                    {
                        string[] 按键 = Properties.Settings.Default.运行快捷键.Replace(" ", "").Split('+');
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
                                Keys key = (Keys)Enum.Parse(typeof(Keys), Properties.Settings.Default.运行快捷键);
                                热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.None, key);
                                break;
                        }
                    }
                    if (Properties.Settings.Default.暂停快捷键 != "")
                    {
                        string[] 按键 = Properties.Settings.Default.暂停快捷键.Replace(" ", "").Split('+');
                        //注册热键Id号为101。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
                        switch (按键.Count())
                        {
                            case 4:
                                Keys key3 = (Keys)Enum.Parse(typeof(Keys), 按键[3]);
                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key3);
                                break;
                            case 3:
                                Keys key2 = (Keys)Enum.Parse(typeof(Keys), 按键[2]);
                                switch (按键[0])
                                {
                                    case "Alt":
                                        switch (按键[1])
                                        {
                                            case "Ctrl":
                                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Alt, key2);
                                                break;
                                            case "Shift":
                                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Alt, key2);
                                                break;
                                        }
                                        break;

                                    case "Ctrl":
                                        switch (按键[1])
                                        {
                                            case "Alt":
                                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                                break;
                                            case "Shift":
                                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                                break;
                                        }
                                        break;

                                    case "Shift":
                                        switch (按键[1])
                                        {
                                            case "Alt":
                                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Shift, key2);
                                                break;
                                            case "Ctrl":
                                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key2);
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
                                        热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt, key1);
                                        break;

                                    case "Ctrl":

                                        热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Ctrl, key1);
                                        break;

                                    case "Shift":

                                        热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Shift, key1);
                                        break;
                                }
                                break;
                            case 1:
                                Keys key = (Keys)Enum.Parse(typeof(Keys), Properties.Settings.Default.暂停快捷键);
                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.None, key);
                                break;
                        }
                    }
                    if (Properties.Settings.Default.调试快捷键 != "")
                    {
                        string[] 按键 = Properties.Settings.Default.调试快捷键.Replace(" ", "").Split('+');
                        //注册热键Id号为102。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
                        switch (按键.Count())
                        {
                            case 4:
                                Keys key3 = (Keys)Enum.Parse(typeof(Keys), 按键[3]);
                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key3);
                                break;
                            case 3:
                                Keys key2 = (Keys)Enum.Parse(typeof(Keys), 按键[2]);
                                switch (按键[0])
                                {
                                    case "Alt":
                                        switch (按键[1])
                                        {
                                            case "Ctrl":
                                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Alt, key2);
                                                break;
                                            case "Shift":
                                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Alt, key2);
                                                break;
                                        }
                                        break;

                                    case "Ctrl":
                                        switch (按键[1])
                                        {
                                            case "Alt":
                                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                                break;
                                            case "Shift":
                                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                                break;
                                        }
                                        break;

                                    case "Shift":
                                        switch (按键[1])
                                        {
                                            case "Alt":
                                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Shift, key2);
                                                break;
                                            case "Ctrl":
                                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key2);
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
                                        热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt, key1);
                                        break;

                                    case "Ctrl":

                                        热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Ctrl, key1);
                                        break;

                                    case "Shift":

                                        热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Shift, key1);
                                        break;
                                }
                                break;
                            case 1:
                                Keys key = (Keys)Enum.Parse(typeof(Keys), Properties.Settings.Default.调试快捷键);
                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.None, key);
                                break;
                        }
                    }

                    break;

                case WM_DESTROY: //窗口消息：销毁
                                 //注销Id号为100的热键设定

                    热键.HotKey.UnregisterHotKey(Handle, 100);

                    //注销Id号为101的热键设定

                    热键.HotKey.UnregisterHotKey(Handle, 101);

                    //注销Id号为102的热键设定

                    热键.HotKey.UnregisterHotKey(Handle, 102);

                    //注销Id号为102的热键设定

                    热键.HotKey.UnregisterHotKey(Handle, 103);
                    break;
/*                default:
                    break;*/
            }
        }

        public void 退出发条应用(object sender, EventArgs e)
        {
            保存();
            /*            if (MessageBox.Show("未退出经发条启动的应用时\r\n\r\nSteam将无法停止运行发条", "是否退出？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) //判断是否点击了 OK按钮
                        {*/
            触边钩子.UnSetHook();//卸载钩子
            Dispose();//释放资源
            fatiao.UnBindWindow();//解除基于窗口
            Environment.Exit(0); // 彻底关闭进程
            //}
        }

        void 流程保存()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                try
                {
                    string Path = Environment.CurrentDirectory + @"\Generate\process";
                    FileStream fs = new FileStream(Path, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    int iCount = listBox1.Items.Count - 1;
                    for (int i = 0; i <= iCount; i++)
                    {
                        sw.WriteLine(listBox1.Items[i].ToString());
                    }
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return;
                }
            }), null);
        }

        void 流程读取()
        {


            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                FileInfo fi = new FileInfo(Environment.CurrentDirectory + @"\Generate\process");
                if (fi.Length != 0)
                {
                    listBox1.Items.Clear();


                    Enabled = false;//不可用
                    StreamReader _rstream = new StreamReader(Environment.CurrentDirectory + @"\Generate\process", Encoding.UTF8);//读取流程
                    string line;
                    while ((line = _rstream.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                    }
                    _rstream.Close();//读取流程

                    Enabled = true;//可用
                }

            }), null);
        }


        void 快捷命令读取()
        {
            listBox4.Items.Clear();
            DirectoryInfo TheFolder = new DirectoryInfo(Environment.CurrentDirectory + @"\Command\");
            //遍历文件名
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                listBox4.Items.Add(NextFile.Name);
        }

        public void 保存()
        {
            try
            {
                Properties.Settings.Default.执行次数 = toolStripTextBox1.Text;
                Properties.Settings.Default.运行快捷键 = textBox1.Text;
                Properties.Settings.Default.暂停快捷键 = textBox2.Text;
                Properties.Settings.Default.调试快捷键 = textBox3.Text;
                Properties.Settings.Default.开机自启动 = checkBox1.Checked;
                Properties.Settings.Default.最小化启动 = checkBox2.Checked;
                Properties.Settings.Default.触边滚轮1 = listView1.Items[0].SubItems[1].Text;
                Properties.Settings.Default.触边滚轮2 = listView1.Items[0].SubItems[2].Text;
                Properties.Settings.Default.触边滚轮3 = listView1.Items[0].SubItems[3].Text;
                Properties.Settings.Default.触边滚轮4 = listView1.Items[1].SubItems[1].Text;
                Properties.Settings.Default.触边滚轮5 = listView1.Items[1].SubItems[2].Text;
                Properties.Settings.Default.触边滚轮6 = listView1.Items[1].SubItems[3].Text;
                Properties.Settings.Default.触边滚轮7 = listView1.Items[2].SubItems[1].Text;
                Properties.Settings.Default.触边滚轮8 = listView1.Items[2].SubItems[2].Text;
                Properties.Settings.Default.触边滚轮9 = listView1.Items[2].SubItems[3].Text;
                Properties.Settings.Default.触边滚轮10 = listView1.Items[3].SubItems[1].Text;
                Properties.Settings.Default.触边滚轮11 = listView1.Items[3].SubItems[2].Text;
                Properties.Settings.Default.触边滚轮12 = listView1.Items[3].SubItems[3].Text;
                Properties.Settings.Default.运行完成勾选 = checkBox4.Checked;
                Properties.Settings.Default.鼠标面板 = comboBox1.Text;
                Properties.Settings.Default.启用面板 = checkBox3.Checked;
                Properties.Settings.Default.轮盘上 = label12.Text;
                Properties.Settings.Default.轮盘下 = label13.Text;
                Properties.Settings.Default.轮盘左 = label9.Text;
                Properties.Settings.Default.轮盘右 = label14.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                动作类型.添加日志(ex.Message);
                Console.WriteLine(ex);
            }

            流程保存();
        }

        void 读取()
        {
            try
            {
                toolStripTextBox1.Text = Properties.Settings.Default.执行次数;
                textBox1.Text = Properties.Settings.Default.运行快捷键;
                textBox2.Text = Properties.Settings.Default.暂停快捷键;
                textBox3.Text = Properties.Settings.Default.调试快捷键;
                checkBox1.Checked = Properties.Settings.Default.开机自启动;
                checkBox2.Checked = Properties.Settings.Default.最小化启动;
                listView1.Items[0].SubItems[1].Text = Properties.Settings.Default.触边滚轮1;
                listView1.Items[0].SubItems[2].Text = Properties.Settings.Default.触边滚轮2;
                listView1.Items[0].SubItems[3].Text = Properties.Settings.Default.触边滚轮3;
                listView1.Items[1].SubItems[1].Text = Properties.Settings.Default.触边滚轮4;
                listView1.Items[1].SubItems[2].Text = Properties.Settings.Default.触边滚轮5;
                listView1.Items[1].SubItems[3].Text = Properties.Settings.Default.触边滚轮6;
                listView1.Items[2].SubItems[1].Text = Properties.Settings.Default.触边滚轮7;
                listView1.Items[2].SubItems[2].Text = Properties.Settings.Default.触边滚轮8;
                listView1.Items[2].SubItems[3].Text = Properties.Settings.Default.触边滚轮9;
                listView1.Items[3].SubItems[1].Text = Properties.Settings.Default.触边滚轮10;
                listView1.Items[3].SubItems[2].Text = Properties.Settings.Default.触边滚轮11;
                listView1.Items[3].SubItems[3].Text = Properties.Settings.Default.触边滚轮12;
                checkBox4.Checked = Properties.Settings.Default.运行完成勾选;
                checkBox3.Checked = Properties.Settings.Default.启用面板;
                comboBox1.Text= Properties.Settings.Default.鼠标面板;
                label12.Text= Properties.Settings.Default.轮盘上;
                label13.Text = Properties.Settings.Default.轮盘下;
                label9.Text = Properties.Settings.Default.轮盘左;
                label14.Text = Properties.Settings.Default.轮盘右;
            }
            catch (Exception ex)
            {
                动作类型.添加日志(ex.Message);
                Console.WriteLine(ex);
            }

            流程读取();
            快捷命令读取();
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        public void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否执行完本次流程后，自动停止发条", "缓停止", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//判断是否 单击 OK按钮
            {
                toolStripButton2.Enabled = false;
                缓停止作用 = true;
            }
        }

        public void toolStripButton1_Click(object sender, EventArgs e)//暂停继续
        {
            if (toolStripButton1.Text == "暂停")
            {
                暂停作用 = true;
                快捷命令.暂停作用 = false;
                toolStripButton1.Text = "继续";
                toolStripButton1.Image = Properties.Resources.继续;
                /*                notifyIcon1.ShowBalloonTip(1000, "", "发条已暂停", ToolTipIcon.None);*/

                暂停();
            }
            else
            {
                暂停作用 = false;
                快捷命令.暂停作用 = false;
                toolStripButton1.Text = "暂停";
                toolStripButton1.Image = Properties.Resources.暂停;
                /*                notifyIcon1.ShowBalloonTip(1000, "", "发条已继续", ToolTipIcon.None);*/

            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)//双击托盘
        {
                Show();
                Activate();//激活窗体，并给予焦点。
            
        }

        public string 变量选项 = "";

        void 复制快捷键()
        {
            int selectedCount = listBox1.SelectedIndices.Count;//最大选中行数
            for (int i = 0; i <= selectedCount - 1; i++)//滤遍所有选中行
            {
                int index = listBox1.SelectedIndices[i];//当前所选中行

                object tmp = listBox1.Items[index];//当前所选中行内容
                listBox3.Items.Add(tmp);//添加所选中内容
            }
            动作类型.添加日志("复制成功");
            if (Handle != GetF()) //如果本窗口没有获得焦点
                SetF(Handle); //设置本窗口获得焦点
        }

        void 剪切快捷键()
        {
            int selectedCount = listBox1.SelectedIndices.Count;//最大选中行数
            for (int i = 0; i <= selectedCount - 1; i++)//滤遍所有选中行
            {
                int index = listBox1.SelectedIndices[i];//当前所选中行

                object tmp = listBox1.Items[index];//当前所选中行内容
                listBox3.Items.Add(tmp);//添加所选中内容
            }//先复制

            删除.PerformClick(); //再执行删除动作  
            if (Handle != GetF()) //如果本窗口没有获得焦点
                SetF(Handle); //设置本窗口获得焦点
        }

        void 粘贴快捷键()
        {
            int Count = listBox3.Items.Count;//最大行数
            string 每行内容;
            for (int i = 0; i <= Count - 1; i++)//滤遍所有行
            {
                每行内容 = listBox3.Items[i].ToString();//读取运行行内容
                listBox1.Items.Insert(listBox1.SelectedIndex + i + 1, 每行内容);
            }
            if (Count > 0)
            {
                if (Handle != GetF()) //如果本窗口没有获得焦点
                    SetF(Handle); //设置本窗口获得焦点
            }
        }

        private void 快捷键KeyDown(object sender, KeyEventArgs e)//命令行快捷键
        {
            if (动作行快捷键 == true)
            {
                if (listBox1.SelectedIndices.Count > 0)//选中项大于0（单选或多选）
                {
                    if (e.KeyCode == Keys.C && e.Control)
                    {
                        e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件 
                        listBox3.Items.Clear();//清空数据
                        复制快捷键();
                    }

                    if (e.KeyCode == Keys.X && e.Control)
                    {
                        e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件 
                        listBox3.Items.Clear();//清空数据
                        剪切快捷键();
                    }

                    if (e.KeyCode == Keys.Back)
                    {
                        e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                        删除.PerformClick(); //执行删除动作   
                    }

                    if (e.KeyCode == Keys.Space)
                    {
                        e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                        启停用.PerformClick(); //执行停用或启用动作
                    }
                }
                else//未选中
                {
                    if (e.KeyCode == Keys.V && e.Control)
                    {
                        e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                        粘贴快捷键();
                    }
                }

                if (listBox1.SelectedIndices.Count == 1)//选中项等于1（仅单选）
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                                                //编辑动作(sender, e); //执行编辑命令动作   
                        listBox1.Items.Insert(listBox1.SelectedIndex + 1, "");
                    }

                    if (e.KeyCode == Keys.V && e.Control)
                    {
                        e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                        粘贴快捷键();
                    }
                }
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
            tabControl1.Focus();
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
                notifyIcon1.ShowBalloonTip(1000, "发条错误", "当前快捷键冲突，请重新设置快捷键", ToolTipIcon.Error);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(sender, e);
            if (textBox2.Text != "")
            {
                //注销Id号为101的热键设定

                热键.HotKey.UnregisterHotKey(Handle, 101);
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUp(sender, e);
            try
            {
                if (textBox2.Text != "")
                {
                    string[] 按键 = textBox2.Text.Replace(" ", "").Split('+');
                    //注册热键Id号为101。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
                    switch (按键.Count())
                    {
                        case 4:
                            Keys key3 = (Keys)Enum.Parse(typeof(Keys), 按键[3]);
                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key3);
                            break;
                        case 3:
                            Keys key2 = (Keys)Enum.Parse(typeof(Keys), 按键[2]);
                            switch (按键[0])
                            {
                                case "Alt":
                                    switch (按键[1])
                                    {
                                        case "Ctrl":
                                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Alt, key2);
                                            break;
                                        case "Shift":
                                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Alt, key2);
                                            break;
                                    }
                                    break;

                                case "Ctrl":
                                    switch (按键[1])
                                    {
                                        case "Alt":
                                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                            break;
                                        case "Shift":
                                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                            break;
                                    }
                                    break;

                                case "Shift":
                                    switch (按键[1])
                                    {
                                        case "Alt":
                                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Shift, key2);
                                            break;
                                        case "Ctrl":
                                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key2);
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
                                    热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Alt, key1);
                                    break;

                                case "Ctrl":

                                    热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Ctrl, key1);
                                    break;

                                case "Shift":

                                    热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.Shift, key1);
                                    break;
                            }
                            break;
                        case 1:
                            Keys key = (Keys)Enum.Parse(typeof(Keys), textBox2.Text);
                            热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.None, key);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                notifyIcon1.ShowBalloonTip(1000, "发条错误", "当前快捷键冲突，请重新设置快捷键", ToolTipIcon.Error);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(sender, e);
            if (textBox2.Text != "")
            {
                //注销Id号为101的热键设定

                热键.HotKey.UnregisterHotKey(Handle, 102);
            }
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUp(sender, e);
            try
            {
                if (textBox3.Text != "")
                {
                    string[] 按键 = textBox3.Text.Replace(" ", "").Split('+');
                    //注册热键Id号为102。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
                    switch (按键.Count())
                    {
                        case 4:
                            Keys key3 = (Keys)Enum.Parse(typeof(Keys), 按键[3]);
                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key3);
                            break;
                        case 3:
                            Keys key2 = (Keys)Enum.Parse(typeof(Keys), 按键[2]);
                            switch (按键[0])
                            {
                                case "Alt":
                                    switch (按键[1])
                                    {
                                        case "Ctrl":
                                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Alt, key2);
                                            break;
                                        case "Shift":
                                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Alt, key2);
                                            break;
                                    }
                                    break;

                                case "Ctrl":
                                    switch (按键[1])
                                    {
                                        case "Alt":
                                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                            break;
                                        case "Shift":
                                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Shift | 热键.HotKey.KeyModifiers.Ctrl, key2);
                                            break;
                                    }
                                    break;

                                case "Shift":
                                    switch (按键[1])
                                    {
                                        case "Alt":
                                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt | 热键.HotKey.KeyModifiers.Shift, key2);
                                            break;
                                        case "Ctrl":
                                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Ctrl | 热键.HotKey.KeyModifiers.Shift, key2);
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
                                    热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Alt, key1);
                                    break;

                                case "Ctrl":

                                    热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Ctrl, key1);
                                    break;

                                case "Shift":

                                    热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.Shift, key1);
                                    break;
                            }
                            break;
                        case 1:
                            Keys key = (Keys)Enum.Parse(typeof(Keys), textBox3.Text);
                            热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.None, key);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                notifyIcon1.ShowBalloonTip(1000, "发条错误", "当前快捷键冲突，请重新设置快捷键", ToolTipIcon.Error);
            }
        }

        private void 另存为(object sender, EventArgs e)
        {
            if (!Directory.Exists(@".\Process"))//不存在就创建
            {
                Directory.CreateDirectory(@".\Process");
            }
            else
            {
                DelectDir(@".\Process");
            }
            保存();
            SaveFileDialog saveDlg = new SaveFileDialog//用户选择保存目录和自定义文件名
            {
                Filter = "流程|*.fatiao"
            };
            saveDlg.Title = "流程另存为：";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                CopyDirectory(@".\Command", @".\Process\Command");
                CopyDirectory(@".\Generate", @".\Process\Generate");
                CopyDirectory(@".\素材库", @".\Process\素材库");
                string 流程目录 = @".\Process";
                string 压缩文件 = saveDlg.FileName;//saveDlg.FileName是自定义目录+文件名
                if (File.Exists(压缩文件))//如果存在就删除（变相覆盖）
                {
                    File.Delete(压缩文件);
                }

                ZipFile.CreateFromDirectory(流程目录, 压缩文件);
                notifyIcon1.ShowBalloonTip(1000, "", "流程另存为成功", ToolTipIcon.None);
            }

            if (Directory.Exists(@".\Process"))//存在就清空
            {
                DelectDir(@".\Process");
            }
        }

        public void 流程文件报错()

        {
            notifyIcon1.ShowBalloonTip(1000, "", "流程文件已损坏", ToolTipIcon.None);
            /*            弹幕内容.label1.Text = "流程文件格式错误或已损坏";
                        弹幕内容.Show();*/
        }


        private void 打开文件_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@".\Process"))//不存在就创建
            {
                Directory.CreateDirectory(@".\Process");
            }
            else
            {
                DelectDir(@".\Process");
            }

            if (!Directory.Exists(@".\素材库"))//不存在就创建
            {
                Directory.CreateDirectory(@".\素材库");
            }

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "请选择文件",
                Filter = "流程|*.fatiao"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] names = fileDialog.FileNames;

                foreach (string file in names)
                {
                    string 文件目录 = file;
                    string 解压目录 = Environment.CurrentDirectory + @"\Process\";
                    string 命令目录 = Environment.CurrentDirectory + @"\Generate\";
                    try
                    {
                        DelectDir(命令目录);
                        ZipFile.ExtractToDirectory(文件目录, 解压目录);
                        CopyDirectory(@".\Process\Command", @".\Command");
                        CopyDirectory(@".\Process\Generate", @".\Generate");
                        if (Directory.Exists(@".\素材库"))//存在就清空
                        {
                            DelectDir(@".\素材库");
                        }
                        CopyDirectory(@".\Process\素材库", @".\素材库");

                        CopyDirectory(@".\Process\默认文档", @".\素材库");

                        流程读取();
                        快捷命令读取();

                        if (Directory.Exists(@".\Process"))//存在就删除
                        {
                            DelectDir(@".\Process");
                        }
                    }
                    catch
                    {
                        流程文件报错();
                    }
                }

            }

        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)//链接导入
        {
            notifyIcon1.ShowBalloonTip(1000, "", "正在加载流程", ToolTipIcon.None);
            /*            弹幕内容.label1.Text = "正在加载流程";
                        弹幕内容.Show();*/
            Delay(1);
            string 网址 = Clipboard.GetText();
            if (!Directory.Exists(@".\Process"))//不存在就创建
            {
                Directory.CreateDirectory(@".\Process");
            }
            else
            {
                DelectDir(@".\Process");
            }
            if (!Directory.Exists(@".\素材库"))//文档不存在就创建
            {
                Directory.CreateDirectory(@".\素材库");
            }
            string 返回下载结果 = "";
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                返回下载结果 = 动作.下载文件(网址, Environment.CurrentDirectory + @"\link.fatiao");
            }), null);

            while (返回下载结果 == "")
            {
                Delay(100);
            }
            if (返回下载结果 == "假")
            {
                notifyIcon1.ShowBalloonTip(1000, "发条错误", "粘贴链接失败，请检查网络状况", ToolTipIcon.Error);
                return;
            }

            string filePaths = Environment.CurrentDirectory + @"\link.fatiao";//下载文件的路径
            string 文件目录 = filePaths;
            string 解压目录 = Environment.CurrentDirectory + @"\Process\";
            string 命令目录 = Environment.CurrentDirectory + @"\Generate\";
            try
            {
                DelectDir(命令目录);
                ZipFile.ExtractToDirectory(文件目录, 解压目录);
                CopyDirectory(@".\Process\Generate", @".\Generate");
                if (Directory.Exists(@".\素材库"))//存在就清空
                {
                    DelectDir(@".\素材库");
                }
                CopyDirectory(@".\Process\素材库", @".\素材库");

                CopyDirectory(@".\Process\默认文档", @".\素材库");

                listBox1.Items.Clear();
                流程读取();

                if (Directory.Exists(@".\Process"))//存在就删除
                {
                    DelectDir(@".\Process");
                }
            }
            catch
            {
                流程文件报错();
            }
            if (File.Exists(filePaths))//如果存在就删除下载文件
            {
                File.Delete(filePaths);
            }
        }


        public void CopyDirectory(string sourceDirPath, string saveDirPath)//复制文件夹内的所有文件
        {
            try
            {
                if (!Directory.Exists(saveDirPath))
                {
                    Directory.CreateDirectory(saveDirPath);
                }
                string[] files = Directory.GetFiles(sourceDirPath);
                foreach (string file in files)
                {
                    string pFilePath = saveDirPath + "\\" + Path.GetFileName(file);
                    if (File.Exists(pFilePath))
                        continue;
                    File.Copy(file, pFilePath, true);
                }

                string[] dirs = Directory.GetDirectories(sourceDirPath);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, saveDirPath + "\\" + Path.GetFileName(dir));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void DelectDir(string srcPath)//删除文件夹内的所有文件
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        void 加载订阅()
        {
            uint 订阅数量 = SteamUGC.GetNumSubscribedItems();
            PublishedFileId_t[] 订阅ID = new PublishedFileId_t[订阅数量];
            SteamUGC.GetSubscribedItems(订阅ID, 订阅数量);
            foreach (PublishedFileId_t x in 订阅ID)
            {
                uint 状态值 = SteamUGC.GetItemState(x);
                if (((EItemState)状态值 & EItemState.k_EItemStateInstalled) == EItemState.k_EItemStateInstalled)
                {
                    uint 目录长度 = 300;
                    bool 是否成功;
                    string 文件目录;
                    string 解压目录;
                    string 流程目录;
                    是否成功 = SteamUGC.GetItemInstallInfo(x, out ulong 文件大小, out string 订阅目录, 目录长度, out uint 更新时间);
                    if (是否成功)//成功
                    {
                        Console.WriteLine(订阅目录);
                        if (!Directory.Exists(@".\Process"))//不存在就创建
                        {
                            Directory.CreateDirectory(@".\Process");
                        }
                        else
                        {
                            DelectDir(@".\Process");
                        }

                        if (!Directory.Exists(@".\素材库"))//不存在就创建
                        {
                            Directory.CreateDirectory(@".\素材库");
                        }
                        var 流程文件 = Directory.GetFiles(订阅目录, "*.fatiao");
                        foreach (var file in 流程文件)
                        {
                            文件目录 = file;
                            解压目录 = Environment.CurrentDirectory + @"\Process\";
                            流程目录 = Environment.CurrentDirectory + @"\Generate\";
                            Console.WriteLine(file);
                            try
                            {
                                if (File.Exists(流程目录 + "process"))//如果存在就删除（变相覆盖）
                                {
                                    File.Delete(流程目录 + "process");
                                }
                                if (File.Exists(流程目录 + "variable"))//如果存在就删除（变相覆盖）
                                {
                                    File.Delete(流程目录 + "variable");
                                }
                                ZipFile.ExtractToDirectory(文件目录, 解压目录);
                                CopyDirectory(@".\Process\Command", @".\Command");
                                CopyDirectory(@".\Process\Generate", @".\Generate");
                                if (Directory.Exists(@".\素材库"))//存在就清空
                                {
                                    DelectDir(@".\素材库");
                                }
                                CopyDirectory(@".\Process\素材库", @".\素材库");

                                CopyDirectory(@".\Process\默认文档", @".\素材库");

                                File.Delete(文件目录);
                                if (Directory.Exists(@".\Process"))//存在就删除
                                {
                                    DelectDir(@".\Process");
                                }
                            }
                            catch
                            {
                                流程文件报错();
                            }
                        }

                    }
                }

            }
        }

        private void listBox4_DrawItem(object sender, DrawItemEventArgs e)//自定义快捷命令重绘
        {

            if (e.Index < 0) return;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                       e.Font,
                       e.Bounds,
                       e.Index,
                       e.State ^ DrawItemState.Selected,
                       e.ForeColor,
                       Color.FromArgb(215, 228, 242));//背景颜色

            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(listBox4.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds);
        }

        private void 鸣谢窗口(object sender, EventArgs e)
        {
            鸣谢与声明.Show();
        }

        public string 点击快捷命令;

        private void listBox4_MouseDown(object sender, MouseEventArgs e)
        {
            listBox4.SelectedIndex = -1;//取消高亮
            int index = listBox4.IndexFromPoint(e.Location);//获取点击的位置
            listBox4.SelectedIndex = index;//高亮点击那行
            if (e.Clicks < 2)//单击
            {
                if (e.Button == MouseButtons.Right)//判断是否右键点击
                {
                    if (listBox4.SelectedIndex >= 0)
                    {
                        contextMenuStrip3.Show(listBox4, e.Location);
                    }
                }
                else
                {
                    if (listBox4.SelectedIndex >= 0)//左键单击
                    {

                        if (listBox1.SelectedIndex != -1)
                        {
                            快捷命令载入(listBox4.SelectedItem.ToString());
                            int iCount = listBox6.Items.Count - 1;
                            int s = 1;
                            for (int i = 0; i <= iCount; i++)
                            {
                                listBox1.Items.Insert(listBox1.SelectedIndex + s, listBox6.Items[i].ToString());
                                s++;
                            }
                            快捷命令.快捷命令运行();
                        }
                        else
                        {
                            快捷命令载入(listBox4.SelectedItem.ToString());
                            int iCount = listBox6.Items.Count - 1;
                            for (int i = 0; i <= iCount; i++)
                            {
                                listBox1.Items.Add(listBox6.Items[i].ToString());
                            }
                            快捷命令.快捷命令运行();
                        }


                    }
                    listBox4.SelectedIndex = -1;
                }
            }

        }
        void 打开快捷命令()
        {
            try
            {
                if (listBox4.SelectedIndex >= 0)
                {
                    string 快捷命令名称 = listBox4.SelectedItem.ToString();
                    listBox4.SelectedIndex = -1;//取消高亮
                    if (!Directory.Exists(@".\Process"))//不存在就创建
                    {
                        Directory.CreateDirectory(@".\Process");
                    }
                    else
                    {
                        DelectDir(@".\Process");
                    }

                    ZipFile.ExtractToDirectory(Environment.CurrentDirectory + @"\Command\" + 快捷命令名称, @".\Process\");
                    CopyDirectory(@".\Process\素材库", @".\素材库");

                    CopyDirectory(@".\Process\默认文档", @".\素材库");

                    string 打开快捷命令 = Environment.CurrentDirectory + @"\Process\process";
                    string 流程 = Environment.CurrentDirectory + @"\Generate\process";
                    File.Copy(打开快捷命令, 流程, true);
                    listBox1.Items.Clear();
                    流程读取();

                    if (Directory.Exists(@".\Process"))//存在就清空
                    {
                        DelectDir(@".\Process");
                    }
                }
            }
            catch (Exception ex)
            {
                动作类型.添加日志(ex.Message);
                Console.WriteLine(ex);
            }
        }

        void 删除快捷命令()
        {
            try
            {
                if (listBox4.SelectedIndex >= 0)
                {
                    string 快捷命令名称 = listBox4.SelectedItem.ToString();
                    string 删除快捷命令 = Environment.CurrentDirectory + @"\Command\" + 快捷命令名称;
                    File.Delete(删除快捷命令);
                    listBox4.Items.Remove(listBox4.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                动作类型.添加日志(ex.Message);
                Console.WriteLine(ex);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否永久删除命令？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//判断是否 单击 OK按钮
            {
                删除快捷命令();
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)//调试
        {
            if (toolStripButton13.Text == "调试")
            {
                toolStripButton13.Enabled = false;
                调试作用 = true;
                toolStripButton13.Text = "逐行";
                toolStripButton5.PerformClick();//按下运行
            }
            else
            {
                toolStripButton13.Enabled = false;
                点击逐行 = true;
            }
        }

        public bool 关闭进程(string exe)//通过进程名关闭应用
        {

            try
            {
                Process[] ps = Process.GetProcessesByName(exe);
                foreach (Process p in ps)
                {
                    p.Kill();
                    p.WaitForExit();//关键，等待外部程序退出后才能往下执行
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)//运行快捷命令
        {
            try
            {
                string 快捷命令名称 = listBox4.SelectedItem.ToString();
                listBox4.SelectedIndex = -1;//取消高亮
                if (toolStripMenuItem5.Text == "运行命令")
                {
                    快捷命令.停止作用 = false;
                    快捷命令.暂停作用 = false;
                    动作行快捷键 = false;
                    toolStripMenuItem5.Text = "停止命令";
                    toolStripMenuItem5.Image = Properties.Resources.停止;

                    快捷命令载入(快捷命令名称);

                    快捷命令.快捷命令运行();
                }
                else
                {
                    停止快捷命令();
                }
            }
            catch (Exception ex)
            {
                动作类型.添加日志(ex.Message);
                Console.WriteLine(ex);
            }
        }

        public void 停止快捷命令()
        {
            快捷命令.停止作用 = true;
            快捷命令.暂停作用 = false;
            //线程开关.Cancel();
            动作行快捷键 = true;
            toolStripMenuItem5.Text = "运行命令";
            toolStripMenuItem5.Image = Properties.Resources.运行;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)//打开快捷命令
        {
            if (MessageBox.Show("是否打开命令覆盖当前流程？", "打开命令", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//判断是否 单击 OK按钮
            {
                打开快捷命令();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && textBox6.Text != "搜索动作")
            {
                button1.BackgroundImage = Properties.Resources.清除;
                switch (tabControl2.SelectedTab.Name)
                {
                    case "tabPage4":
                        for (int i = 0; i < toolStrip3.Items.Count; i++)
                        {
                            string 筛选 = toolStrip3.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip3.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip3.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage5":
                        for (int i = 0; i < toolStrip6.Items.Count; i++)
                        {
                            string 筛选 = toolStrip6.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip6.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip6.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage6":
                        for (int i = 0; i < toolStrip7.Items.Count; i++)
                        {
                            string 筛选 = toolStrip7.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip7.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip7.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage7":
                        for (int i = 0; i < toolStrip8.Items.Count; i++)
                        {
                            string 筛选 = toolStrip8.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip8.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip8.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage9":
                        for (int i = 0; i < toolStrip10.Items.Count; i++)
                        {
                            string 筛选 = toolStrip10.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip10.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip10.Items[i].Visible = false;
                            }
                        }
                        break;
                }
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.搜索;
                switch (tabControl2.SelectedTab.Name)
                {
                    case "tabPage4":
                        for (int i = 0; i < toolStrip3.Items.Count; i++)
                        {
                            toolStrip3.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage5":
                        for (int i = 0; i < toolStrip6.Items.Count; i++)
                        {
                            toolStrip6.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage6":
                        for (int i = 0; i < toolStrip7.Items.Count; i++)
                        {
                            toolStrip7.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage7":
                        for (int i = 0; i < toolStrip8.Items.Count; i++)
                        {
                            toolStrip8.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage9":
                        for (int i = 0; i < toolStrip10.Items.Count; i++)
                        {
                            toolStrip10.Items[i].Visible = true;
                        }
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "搜索动作")
            {
                textBox6.Clear();
                textBox6.Text = "搜索动作";
                textBox6.ForeColor = Color.DarkGray;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (tabControl2.SelectedTab.Name)
            {
                case "tabPage4"://所有动作
                    toolTip1.SetToolTip(tabControl2, "所有动作");
                    break;

                case "tabPage5":
                    toolTip1.SetToolTip(tabControl2, "编程类动作");
                    break;

                case "tabPage6":
                    toolTip1.SetToolTip(tabControl2, "窗口类动作");
                    break;

                case "tabPage7":
                    toolTip1.SetToolTip(tabControl2, "坐标类动作");
                    break;

                case "tabPage9":
                    toolTip1.SetToolTip(tabControl2, "系统类动作");
                    break;
                case "tabPage2":
                    toolTip1.SetToolTip(tabControl2, "自定义命令");
                    break;
            }

            textBox6.Visible = true;
            button1.Visible = true;
            button3.Visible = false;
            if (textBox6.Text != "" && textBox6.Text != "搜索动作")
            {
                button1.BackgroundImage = Properties.Resources.清除;
                switch (tabControl2.SelectedTab.Name)
                {
                    case "tabPage4"://所有动作
                        for (int i = 0; i < toolStrip3.Items.Count; i++)
                        {
                            string 筛选 = toolStrip3.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip3.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip3.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage5":
                        for (int i = 0; i < toolStrip6.Items.Count; i++)
                        {
                            string 筛选 = toolStrip6.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip6.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip6.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage6":
                        for (int i = 0; i < toolStrip7.Items.Count; i++)
                        {
                            string 筛选 = toolStrip7.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip7.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip7.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage7":
                        for (int i = 0; i < toolStrip8.Items.Count; i++)
                        {
                            string 筛选 = toolStrip8.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip8.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip8.Items[i].Visible = false;
                            }
                        }
                        break;

                    case "tabPage9":
                        for (int i = 0; i < toolStrip10.Items.Count; i++)
                        {
                            string 筛选 = toolStrip10.Items[i].ToString();
                            //在这里做筛选
                            if (筛选.Contains(textBox6.Text) == true)
                            {
                                toolStrip10.Items[i].Visible = true;
                            }
                            else
                            {
                                toolStrip10.Items[i].Visible = false;
                            }
                        }
                        break;
                    case "tabPage2":
                        textBox6.Visible = false;
                        button1.Visible = false;
                        button3.Visible = true;
                        break;
                }
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.搜索;
                switch (tabControl2.SelectedTab.Name)
                {
                    case "tabPage4":
                        for (int i = 0; i < toolStrip3.Items.Count; i++)
                        {
                            toolStrip3.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage5":
                        for (int i = 0; i < toolStrip6.Items.Count; i++)
                        {
                            toolStrip6.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage6":
                        for (int i = 0; i < toolStrip7.Items.Count; i++)
                        {
                            toolStrip7.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage7":
                        for (int i = 0; i < toolStrip8.Items.Count; i++)
                        {
                            toolStrip8.Items[i].Visible = true;
                        }
                        break;

                    case "tabPage9":
                        for (int i = 0; i < toolStrip10.Items.Count; i++)
                        {
                            toolStrip10.Items[i].Visible = true;
                        }
                        break;
                    case "tabPage2":
                        textBox6.Visible = false;
                        button1.Visible = false;
                        button3.Visible = true;
                        break;
                }
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Clear();
                textBox6.Text = "搜索动作";
                textBox6.ForeColor = Color.DarkGray;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "搜索动作")
            {
                textBox6.Clear();
            }
            textBox6.ForeColor = Color.Black;
        }

        void 编辑器退出()
        {
            listBox1.Items.Clear();
            流程读取();
        }

        public string 判断数字(string num1)
        {
            int num2;
            double num3;
            if (int.TryParse(num1, out num2))
                return "整数";
            if (double.TryParse(num1, out num3))
                return "浮点数";

            else
                return "非数字";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (是否订阅)//当初始化成功
            {
                保存();
                新增快捷命令.Show();
            }
            else//失败
            {
                notifyIcon1.ShowBalloonTip(1000, "未订阅", "Steam客户端未运行或未订阅应用", ToolTipIcon.Info);
                Process.Start("https://store.steampowered.com/app/1416190/Fatiao/");
            }
        }

        private void listBox4_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox4.IndexFromPoint(e.Location);//获取点击的位置
            listBox4.SelectedIndex = index;//高亮点击那行
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            开机自启动 开机启动 = new 开机自启动();
            if (checkBox1.Checked) //设置开机自启动    
            {
                开机启动.SetMeAutoStart(true);
            }
            else //取消开机自启动    
            {
                开机启动.SetMeAutoStart(false);
            }
        }


        触边滚轮 触边滚轮 = new 触边滚轮();

        public string 滚轮上滑, 滚轮下滑, 滚轮点击;

        private void label15_Click(object sender, EventArgs e)
        {
            滚轮上滑 = listView1.Items[0].SubItems[2].Text;
            滚轮下滑 = listView1.Items[0].SubItems[1].Text;
            滚轮点击 = listView1.Items[0].SubItems[3].Text;
            触边滚轮.Show();
        }

        public void 快捷命令载入(string 快捷命令名)
        {
            try
            {
                停止快捷命令();
                if (!Directory.Exists(@".\Process"))//不存在就创建
                {
                    Directory.CreateDirectory(@".\Process");
                }
                else
                {
                    DelectDir(@".\Process");
                }

                ZipFile.ExtractToDirectory(Environment.CurrentDirectory + @"\Command\" + 快捷命令名, @".\Process\");
                CopyDirectory(@".\Process\素材库", @".\素材库");

                CopyDirectory(@".\Process\默认文档", @".\素材库");

                快捷命令.停止作用 = false;

                listBox6.Items.Clear();
                StreamReader _rstream = new StreamReader(Environment.CurrentDirectory + @"\Process\process", Encoding.UTF8);//读取流程
                string line;
                while ((line = _rstream.ReadLine()) != null)
                {
                    listBox6.Items.Add(line);
                }
                _rstream.Close();//读取快捷命令流程

                if (Directory.Exists(@".\Process"))//存在就清空
                {
                    DelectDir(@".\Process");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                动作类型.添加日志(ex.Message);
                notifyIcon1.ShowBalloonTip(1000, "运行失败", "命令不存在", ToolTipIcon.Error);
            }
        }

        public void toolStripMenuItem8_Click(object sender, EventArgs e)//重启发条
        {
            保存();
            Dispose();
            Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void contextMenuStrip4_Click(object sender, EventArgs e)
        {
            string 命令名称 = "";
            foreach (ToolStripItem items in contextMenuStrip4.Items)
            {
                if (items.Selected == true)
                    命令名称 = items.ToString();
            }

            switch (轮盘方向)
            {
                case 1:
                    label12.Text = 命令名称;
                    break;
                case 2:
                    label13.Text = 命令名称;
                    break;
                case 3:
                    label9.Text = 命令名称;
                    break;
                case 4:
                    label14.Text = 命令名称;
                    break;
            }

        }

        private void 快捷命令窗口(object sender, EventArgs e)
        {
            运行快捷命令.button1.Visible = true;//显示“完成”键
            运行快捷命令.button3.Visible = false;//隐藏“修改”键

            运行快捷命令.Show();
        }

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                toolStripTextBox1.Text = "1";
            }
        }

        private void 发条_Deactivate(object sender, EventArgs e)
        {
            listBox1.Enabled = false;
        }

        private void 发条_Activated(object sender, EventArgs e)
        {
            listBox1.Enabled = true;
        }

        private void label6_Click(object sender, EventArgs e)//下载更新
        {
            Process.Start("http://www.fatiao.win/");
        }

        生成程序 生成程序 = new 生成程序();
        private void 生成程序按键_Click(object sender, EventArgs e)
        {
            if (是否订阅)//当初始化成功
            {
                保存();
                生成程序.Show();

            }
            else//失败
            {
                notifyIcon1.ShowBalloonTip(1000, "未订阅", "Steam客户端未运行或未订阅应用", ToolTipIcon.Info);
                Process.Start("https://store.steampowered.com/app/1416190/Fatiao/");
            }

        }

        private void toolStripButton78_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed == false)
            {
                splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
                listBox2.Items.Clear();

            }
        }

        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断是否右键点击
            {
                contextMenuStrip5.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)//清空日志
        {
            listBox2.Items.Clear();
        }

        private void toolStripMenuItem7_Click_1(object sender, EventArgs e)///外部打开
        {
            try
            {
                保存();
                Process 编辑器 = Process.Start("notepad.exe", @".\Generate\process");

                编辑器.WaitForExit();//等待外部程序退出后才能往下执行

                编辑器退出();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                动作类型.添加日志(ex.Message);
                notifyIcon1.ShowBalloonTip(1000, "发条错误", "外部编辑器不存在或打开文件失败", ToolTipIcon.Error);
            }
        }

        private void 创意工坊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (是否订阅)//当初始化成功
            {
                steam创意工坊 创意工坊 = new steam创意工坊();
                保存();
                创意工坊.ShowDialog();

            }
            else//失败
            {
                notifyIcon1.ShowBalloonTip(1000, "未订阅", "Steam客户端未运行或未订阅应用", ToolTipIcon.Info);
                Process.Start("https://store.steampowered.com/app/1416190/Fatiao/");
            }

        }

        private void 鸣谢与声明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            鸣谢窗口(sender, e);
        }

        private void 运行ToolStripMenuItem_Click(object sender, EventArgs e)//停止所有
        {
            停止发条();
            停止快捷命令();
        }

        bool IsDragging = false;    //当前拖拽状态
        Point p = new Point(0, 0);  //记录鼠标按下去的坐标
        Point offset = new Point(0, 0); //记录动了多少距离
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            //当前为拖曳状态
            IsDragging = true;
            p.X = e.X;  //记录坐标X，Y
            p.Y = e.Y; 
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            //当前为不拖曳状态
            IsDragging = false;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                //距离计算：移动的坐标-鼠标按下记录的坐标
                offset.X = e.X - p.X;
                offset.Y = e.Y - p.Y;
                //控件位置
                Location = PointToScreen(offset);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)//联系我们
        {
            Process.Start("http://www.fatiao.win/");
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.悬停关闭窗口;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.关闭窗口;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.悬停最小化窗口;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.最小化窗口;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.关闭窗口;
            Hide();
            ShowInTaskbar = false;//在任务栏中显示该窗口
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.最小化窗口;
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = true;//在任务栏中显示该窗口
            }
        }

        public 启动器 启动器 = new 启动器();

        private void 启用轮盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (启用轮盘ToolStripMenuItem.Text == "启用轮盘")//启用轮盘
            {
                checkBox3.Checked = true;
                启用轮盘ToolStripMenuItem.Text = "停用轮盘";
            }
            else
            {
                checkBox3.Checked = false;
                启用轮盘ToolStripMenuItem.Text = "启用轮盘";
            }
        }

        int 轮盘方向 = 0;
        private void label12_Click(object sender, EventArgs e)
        {
            轮盘方向 = 1;
            contextMenuStrip4.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            contextMenuStrip4.Items.Add("无");
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip4.Items.Add(listBox4.Items[i].ToString());
            }
            if (contextMenuStrip4.Items.Count > 0)
            {
                contextMenuStrip4.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            轮盘方向 = 2;
            contextMenuStrip4.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            contextMenuStrip4.Items.Add("无");
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip4.Items.Add(listBox4.Items[i].ToString());
            }
            if (contextMenuStrip4.Items.Count > 0)
            {
                contextMenuStrip4.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            轮盘方向 = 3;
            contextMenuStrip4.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            contextMenuStrip4.Items.Add("无");
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip4.Items.Add(listBox4.Items[i].ToString());
            }
            if (contextMenuStrip4.Items.Count > 0)
            {
                contextMenuStrip4.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            轮盘方向 = 4;
            contextMenuStrip4.Items.Clear();
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;
            contextMenuStrip4.Items.Add("无");
            for (int i = 0; i <= iCount; i++)
            {
                contextMenuStrip4.Items.Add(listBox4.Items[i].ToString());
            }
            if (contextMenuStrip4.Items.Count > 0)
            {
                contextMenuStrip4.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void toolStripButton92_Click(object sender, EventArgs e)
        {
            字符拼接.button1.Visible = true;//显示“完成”键
            字符拼接.button3.Visible = false;//隐藏“修改”键
            字符拼接.Show();
        }

        private void tabPage1_DragEnter(object sender, DragEventArgs e)//拖放打开
        {
            string[] filePaths = e.Data.GetData(typeof(string[])) as string[];//拖放文件的路径
            if (!Directory.Exists(@".\Process"))//不存在就创建
            {
                Directory.CreateDirectory(@".\Process");
            }
            else
            {
                DelectDir(@".\Process");
            }

            if (!Directory.Exists(@".\素材库"))//不存在就创建
            {
                Directory.CreateDirectory(@".\素材库");
            }

            string 解压目录 = Environment.CurrentDirectory + @"\Process\";
            string 命令目录 = Environment.CurrentDirectory + @"\Generate\";
            string 文件目录 = filePaths[0];

            try
            {
                DelectDir(命令目录);
                ZipFile.ExtractToDirectory(文件目录, 解压目录);
                CopyDirectory(@".\Process\Command", @".\Command");
                CopyDirectory(@".\Process\Generate", @".\Generate");
                if (Directory.Exists(@".\素材库"))//存在就清空
                {
                    DelectDir(@".\素材库");
                }
                CopyDirectory(@".\Process\素材库", @".\素材库");

                CopyDirectory(@".\Process\默认文档", @".\素材库");

                流程读取();
                快捷命令读取();
                //notifyIcon1.ShowBalloonTip(1000, "", "加载成功", ToolTipIcon.None);
                /*                    弹幕内容.label1.Text = "加载成功";
                                    弹幕内容.Show();*/
                if (Directory.Exists(@".\Process"))//存在就删除
                {
                    DelectDir(@".\Process");
                }
            }
            catch
            {
                流程文件报错();
            }
        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }
        }



    }
}