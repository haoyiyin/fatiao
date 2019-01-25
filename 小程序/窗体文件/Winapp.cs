using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using winapp.Properties;
using System.Linq;
using System.Globalization;
using winapp.代码文件;
using 动作库;
using System.IO.Compression;

namespace fatiao
{

    public partial class winapp : Form
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetF(); //获得本窗体的句柄
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetF(IntPtr hWnd); //设置此窗体为活动窗体

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();//桌面句柄

        public FileDropHandler FileDroper;

        public static 悬浮窗 悬浮窗 = new 悬浮窗();

        public  快捷命令 快捷命令 = new 快捷命令();

        public string 参数窗口;

        public bool 悬浮窗开关 = false;//判断悬浮窗是否隐藏

        public static winapp 小程序窗口 = null; //用来引用主窗口

        public string 输入内容;

        public string 输入变量值;

        public string 快捷命令输入变量值;

        protected override CreateParams CreateParams//解决界面闪烁的问题
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        public winapp()
        {
            InitializeComponent();
            小程序窗口 = this; //赋值主窗口

            //设置窗体的双缓冲
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);


            if (Settings.Default.窗口大小X != 0 && Settings.Default.窗口大小Y != 0)
            {
                Size = new Size(Settings.Default.窗口大小X, Settings.Default.窗口大小Y);
            }
            if (Settings.Default.按键一位置X != 0 && Settings.Default.按键一位置Y != 0)
            {
                button1.Location = new Point(Settings.Default.按键一位置X, Settings.Default.按键一位置Y);
            }
            if (Settings.Default.按键二位置X != 0 && Settings.Default.按键二位置Y != 0)
            {
                button2.Location = new Point(Settings.Default.按键二位置X, Settings.Default.按键二位置Y);
            }
            if (Settings.Default.按键三位置X != 0 && Settings.Default.按键三位置Y != 0)
            {
                button3.Location = new Point(Settings.Default.按键三位置X, Settings.Default.按键三位置Y);
            }
            if (Settings.Default.按键四位置X != 0 && Settings.Default.按键四位置Y != 0)
            {
                button4.Location = new Point(Settings.Default.按键四位置X, Settings.Default.按键四位置Y);
            }
            if (Settings.Default.文本位置X != 0 && Settings.Default.文本位置Y != 0)
            {
                label1.Location = new Point(Settings.Default.文本位置X, Settings.Default.文本位置Y);
            }
            if (Settings.Default.运行次数位置X != 0 && Settings.Default.运行次数位置Y != 0)
            {
                textBox1.Location = new Point(Settings.Default.运行次数位置X, Settings.Default.运行次数位置Y);
            }

            if (Settings.Default.按键一名 != "发条")
            {
                button1.Text = Settings.Default.按键一名;
            }
            if (Settings.Default.按键二名 != "发条")
            {
                button2.Text = Settings.Default.按键二名;
            }
            if (Settings.Default.按键三名 != "发条")
            {
                button3.Text = Settings.Default.按键三名;
            }
            if (Settings.Default.按键四名 != "发条")
            {
                button4.Text = Settings.Default.按键四名;
            }

        }


        public TSPlugLib.TSPlugInterFace fatiao = new TSPlugLib.TSPlugInterFace();//新的dll

        public  int[] 整数数值;

        public  string[] 逻辑数值;

        public string[] 逻辑变量名;//获取自定义变量字符串

        public string[] 整数变量名;//获取自定义变量字符串

        public  string[] 文本数值;

        public string[] 文本变量名;//获取自定义变量字符串

        public bool 暂停作用 = false;//用作暂停线程

        public bool 停止作用 = false;//用作停止线程

        public bool 调试作用 = false;//用作调试线程

        public bool 点击逐行 = false;//用作调试线程

        public bool 动作行快捷键 = true;



        private void 发条_Load(object sender, EventArgs e)
        {
            if (Settings.Default.运行次数 == "不启用")
            {
                label1.Visible = false;
                textBox1.Visible = false;
            }
            if (Settings.Default.按键一 == "不启用")
            {
                button1.Visible = false;
            }
            if (Settings.Default.按键二 == "不启用")
            {
                button2.Visible = false;
            }
            if (Settings.Default.按键三 == "不启用")
            {
                button3.Visible = false;
            }
            if (Settings.Default.按键四 == "不启用")
            {
                button4.Visible = false;
            }
            读取();

            悬浮窗.toolStripMenuItem2.Enabled = false;
            悬浮窗.toolStripMenuItem4.Enabled = false;
            悬浮窗.BackColor = Color.IndianRed;//悬浮窗颜色（红）
        }

        void 触发事件(String 事件)
        {
            switch (事件)
            {
                case "不启用":

                    break;
                case "运行当前流程":
                    运行流程.当前流程();
                    break;
                case "停止当前流程":
                    停止发条();
                    break;
                case "停止命令":
                    停止快捷命令();
                    break;
                default:
                    快捷命令载入(事件);
                    快捷命令.快捷命令运行();
                    break;
            }
        }

        public void 停止发条()
        {
            listBox1.SelectedIndex = -1;
/*            快捷命令.停止作用 = true;
            快捷命令.暂停作用 = false;*/
            停止作用 = true;
            暂停作用 = false;
            调试作用 = false;
            动作行快捷键 = true;

            悬浮窗.运行ToolStripMenuItem.Text = "运行流程";
            悬浮窗.toolStripMenuItem2.Text = "暂停流程";

            悬浮窗.toolStripMenuItem2.Enabled = false;
            悬浮窗.toolStripMenuItem4.Enabled = false;
            悬浮窗.BackColor = Color.IndianRed;//悬浮窗颜色（红）
            /*notifyIcon1.ShowBalloonTip(1000, "", "发条已停止", ToolTipIcon.None);*/

        }


        public int indexoftarget = -1;
        /// <summary>
        /// 源位置
        /// </summary>
        public int indexofsource = -1;





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

        private void 发条_SizeChanged(object sender, EventArgs e)//窗体大小改变时  事件
        {
            /*            if (WindowState == FormWindowState.Minimized)//判断当前 窗体是否为 最小化
                        {
                            Hide();
                            notifyIcon1.ShowBalloonTip(1000, "", "发条已最小化", ToolTipIcon.None);
                        }*/
        }





        public void 发条_FormClosing(object sender, FormClosingEventArgs e)
        {
            触发事件(Settings.Default.关闭程序);
            退出发条应用(sender, e);
        }

        public void 退出发条应用(object sender, EventArgs e)
        {
            if (Directory.Exists(我的文档路径 + @"\Winapp"))//存在就删除
            {
                DelectDir(我的文档路径 + @"\Winapp");

            }

            Dispose();//释放资源
            fatiao.UnBindWindow();//解除基于窗口
            Environment.Exit(0); // 彻底关闭进程
        }


        public string 我的文档路径 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);



        void 流程读取()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                FileInfo fi = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Generate\process");
                if (fi.Length != 0)
                {
                    listBox1.Items.Clear();

                    Text = "加载中……";
                    Enabled = false;//不可用
                    StreamReader _rstream = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Generate\process", Encoding.UTF8);//读取流程
                    string line;
                    while ((line = _rstream.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                    }
                    _rstream.Close();//读取流程
                    Text = Settings.Default.程序名;
                    Enabled = true;//可用
                }
            }), null);
        }

        void 读取()
        {
            流程读取();
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

        private const int WM_HOTKEY = 0x312; //窗口消息：热键
        private const int WM_CREATE = 0x1; //窗口消息：创建
        private const int WM_DESTROY = 0x2; //窗口消息：销毁

        protected override void WndProc(ref Message msg) //全局热键
        {

            base.WndProc(ref msg);

            switch (msg.Msg)
            {
                case WM_HOTKEY: //窗口消息：热键
                    switch (msg.WParam.ToInt32())
                    {
                        case 100: //热键ID
                            button1.PerformClick();//按键一
                            break;
                        case 101: //热键ID
                            button2.PerformClick();//按键二
                            break;
                        case 102: //热键ID
                            button3.PerformClick();  //按键三
                            break;
                        case 103: //热键ID
                            button4.PerformClick();  //按键四
                            break;
                    }
                    break;
                case WM_CREATE: //窗口消息：创建
                    if (Settings.Default.快捷键一 != "不启用" && Settings.Default.快捷键一 != "发条")
                    {
                        string[] 按键 = Settings.Default.快捷键一.Replace(" ", "").Split('+');
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
                                Keys key = (Keys)Enum.Parse(typeof(Keys), Settings.Default.快捷键一);
                                热键.HotKey.RegisterHotKey(Handle, 100, 热键.HotKey.KeyModifiers.None, key);
                                break;
                        }
                    }
                    if (Settings.Default.快捷键二 != "不启用" && Settings.Default.快捷键二 != "发条")
                    {
                        string[] 按键 = Settings.Default.快捷键二.Replace(" ", "").Split('+');
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
                                Keys key = (Keys)Enum.Parse(typeof(Keys), Settings.Default.快捷键二);
                                热键.HotKey.RegisterHotKey(Handle, 101, 热键.HotKey.KeyModifiers.None, key);
                                break;
                        }
                    }
                    if (Settings.Default.快捷键三 != "不启用" && Settings.Default.快捷键三 != "发条")
                    {
                        string[] 按键 = Settings.Default.快捷键三.Replace(" ", "").Split('+');
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
                                Keys key = (Keys)Enum.Parse(typeof(Keys), Settings.Default.快捷键三);
                                热键.HotKey.RegisterHotKey(Handle, 102, 热键.HotKey.KeyModifiers.None, key);
                                break;
                        }
                    }
                    if (Settings.Default.快捷键四 != "不启用" && Settings.Default.快捷键四 != "发条")
                    {
                        string[] 按键 = Settings.Default.快捷键四.Replace(" ", "").Split('+');
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
                                Keys key = (Keys)Enum.Parse(typeof(Keys), Settings.Default.快捷键四);
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

                    //注销Id号为103的热键设定

                    热键.HotKey.UnregisterHotKey(Handle, 103);
                    break;
                default:
                    break;
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

        public void 停止快捷命令()
        {
            快捷命令.停止作用 = true;
            快捷命令.暂停作用 = false;
            动作行快捷键 = true;
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



        public void 快捷命令载入(string 快捷命令名)
        {
            try
            {
                停止快捷命令();
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @".\Process"))//不存在就创建
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @".\Process");
                }
                else 
                {
                    DelectDir(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @".\Process");
                }

                ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Command\" + 快捷命令名, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Winapp" + @".\Process\");
                CopyDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @".\Process\素材库", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ @"\Winapp" + @".\素材库");

                CopyDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @".\Process\默认文档", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Winapp" + @".\素材库");

                快捷命令.停止作用 = false;

                listBox6.Items.Clear();
                StreamReader _rstream = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Process\process", Encoding.UTF8);//读取流程
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
                notifyIcon1.ShowBalloonTip(1000, "运行失败", "命令不存在", ToolTipIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            触发事件(Settings.Default.按键一);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            触发事件(Settings.Default.按键二);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            触发事件(Settings.Default.按键三);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            触发事件(Settings.Default.按键四);
        }

        private void winapp_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                触发事件(Settings.Default.打开程序);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Process.Start("https://store.steampowered.com/app/1416190/Fatiao/");
        }
    }
}