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
using winapp.代码文件;
using 动作库;

namespace fatiao
{
    public partial class 用户输入窗 : Form
    {

        public 用户输入窗()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible)
            {
                winapp.小程序窗口.输入变量值 = textBox1.Text;
                winapp.小程序窗口.快捷命令输入变量值 = textBox1.Text;
            }
            else
            {
                winapp.小程序窗口.输入变量值 = dateTimePicker1.Text;
                winapp.小程序窗口.快捷命令输入变量值 = dateTimePicker1.Text;
            }
            Console.WriteLine(winapp.小程序窗口.快捷命令输入变量值);
                Close();       
        }

        private void 用户输入窗_Load(object sender, EventArgs e)
        {
            winapp.小程序窗口.输入变量值 = "";
            winapp.小程序窗口.快捷命令输入变量值 = "";
            if (label2.Text == "时间模式")
            {
                label2.Text = "文本模式";
                textBox1.Visible = false;
                label11.Visible = false;
                label3.Visible = false;
                dateTimePicker1.Visible = true;
            }
            else
            {
                label2.Text = "时间模式";
                textBox1.Visible = true;
                label11.Visible = true;
                label3.Visible = true;
                dateTimePicker1.Visible = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            退出线程 = true;
        }

        private void 选择图片ToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            textBox1.Clear();
            winapp.小程序窗口.参数窗口 = "用户输入窗";
            截图 截图 = new 截图();
            截图.ShowDialog();

            if (winapp.小程序窗口.参数窗口 != "用户输入窗")
            {
                textBox1.Text = winapp.小程序窗口.参数窗口;
            }

        }

        private void 选择文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "请选择程序或文件",
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

        private void 选择文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择文件夹:";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        #region 参数菜单
        工具字幕 字幕 = new 工具字幕();
        bool 退出线程 = false;
        public void 窗口信息操作()
        {
            退出线程 = false;
            int hwnd;
            字幕.Show();
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                while (true)
                {
                    hwnd = winapp.小程序窗口.fatiao.GetMousePointWindow();//获取鼠标指向窗口句柄
                    string 窗口名 = winapp.小程序窗口.fatiao.GetWindowTitle(hwnd);//获得窗口标题
                    winapp.小程序窗口.fatiao.GetClientSize(hwnd, out object w, out object h);
                    动作.获取窗口大小(hwnd, out int 宽, out int 高);
                    字幕.label1.Text = "窗口大小：" + 宽 + "," + 高 + "\r\n客户区大小：" + (int)w + "," + (int)h + "\r\n标题：" + 窗口名 + "   空格键获取标题" + "\r\n句柄：" + hwnd + "   Ctrl+空格键获取句柄";

                    if (!动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        textBox1.Text = 窗口名;//将窗口标题写入编辑框
                        字幕.label1.Text = "";
                        return;
                    }

                    if (动作.判断按键(17) && 动作.判断按键(32))//快捷键Ctrl+空格
                    {
                        textBox1.Text = hwnd.ToString();//将窗口句柄写入编辑框
                        字幕.label1.Text = "";
                        return;
                    }
                    if (退出线程 == true || 动作.判断按键(27))//退出线程
                    {
                        字幕.label1.Text = "";
                        return;
                    }
                    
                }
            }), null);
        }

        public void 坐标与颜色操作()
        {
            退出线程 = false;
            int hwnd;//窗口句柄变量
            string 锁定窗口名 = "";
            object x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            int X, Y;
            字幕.Show();
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                winapp.小程序窗口.fatiao.BindWindow(winapp.小程序窗口.fatiao.GetSpecialWindow(0), "normal", "normal", "normal", 0);//前台基于窗口
                while (true)
                {
                    hwnd = winapp.小程序窗口.fatiao.GetMousePointWindow();//获取鼠标指向窗口句柄
                    string 窗口名 = winapp.小程序窗口.fatiao.GetWindowTitle(hwnd);//获得窗口标题
                    动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标
                    if (动作.判断按键(192) == true)//快捷键“~”
                    {
                        锁定窗口名 = 窗口名;
                        winapp.小程序窗口.fatiao.GetClientRect(hwnd, out x1, out y1, out object x2, out object y2);
                        
                    }

                    if (锁定窗口名 == "")
                    {
                        winapp.小程序窗口.fatiao.GetClientRect(hwnd, out x1, out y1, out x2, out y2);
                        X = 坐标.X - (int)x1;
                        Y = 坐标.Y - (int)y1;
                    }
                    else
                    {
                        X = 坐标.X - (int)x1;
                        Y = 坐标.Y - (int)y1;
                    }

                    string color = winapp.小程序窗口.fatiao.GetColor(坐标.X, 坐标.Y);//获取坐标颜色
                    if (锁定窗口名 != "")
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 锁定窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标r\n颜色值：" + color + "   “C”键获取颜色值";
                    }
                    else
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标r\n颜色值：" + color + "   “C”键获取颜色值";
                    }

                    if (!动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        textBox1.Text = 坐标.X + "," + 坐标.Y;//将窗口内坐标写入编辑框
                        字幕.label1.Text = "";
                        
                        return;
                    }

                    if (动作.判断按键(17) && 动作.判断按键(32))//快捷键Ctrl+空格
                    {
                        textBox1.Text = X + "," + Y;//将窗口内坐标写入编辑框
                        字幕.label1.Text = "";
                        
                        return;
                    }

                    if (动作.判断按键(67))//快捷键“C”
                    {
                        textBox1.Text = color;//将窗口坐标与颜色写入剪贴板
                        字幕.label1.Text = "";
                        
                        return;
                    }

                    if (退出线程 == true || 动作.判断按键(27))//退出线程
                    {
                        字幕.label1.Text = "";
                        
                        return;
                    }

                }
            }), null);
        }
        #endregion

        private void 窗口信息_Click(object sender, EventArgs e)
        {
            窗口信息操作();
        }

        private void 坐标与颜色_Click(object sender, EventArgs e)
        {
            坐标与颜色操作();
        }

        private void 用户输入窗_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "1";
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "2";
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "4";
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "5";
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "6";
        }

        private void 音量加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "175";
        }

        private void 音量减ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "174";
        }

        private void 静音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "173";
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "179";
        }

        private void 浏览器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "172";
        }

        private void 邮件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "180";
        }

        private void 搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "170";
        }

        private void 收藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "171";
        }

        private void 浏览器后退ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "166";
        }

        private void 浏览器收藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "171";
        }

        private void 浏览器前进ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "167";
        }

        private void 浏览器主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "172";
        }

        private void 网页刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "168";
        }

        private void 搜索内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "170";
        }

        private void 停止加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "169";
        }

        void 键码值输入(object sender, KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder();
            keyValue.Length = 0;
            keyValue.Append("");
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    keyValue.Append("17");
                if (e.Alt)
                    keyValue.Append("18");
                if (e.Shift)
                    keyValue.Append("16");
            }
            else
            {
                if ((e.KeyValue >= 32 && e.KeyValue <= 47) ||
                    (e.KeyValue >= 8 && e.KeyValue <= 20) ||
                    (e.KeyValue >= 48 && e.KeyValue <= 57) ||
                    (e.KeyValue >= 186 && e.KeyValue <= 222) ||
                    (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                    (e.KeyValue >= 96 && e.KeyValue <= 123))   //F1-F12
                {
                    keyValue.Append(e.KeyValue);
                }
                else if (e.KeyValue == 27)
                {
                    keyValue.Append("27");
                }
                else if (e.KeyValue == 91)
                {
                    keyValue.Append("91");
                }
                else if (e.KeyValue == 92)
                {
                    keyValue.Append("92");
                }
            }
            ActiveControl.Text = "";
            //设置当前活动控件的文本内容
            ActiveControl.Text = keyValue.ToString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (label11.Text == "关闭键盘输入")
            {
                textBox1.ReadOnly = true;
                键码值输入(sender,e);
            }
            else
            {
                textBox1.ReadOnly = false;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            if (label11.Text == "开启键盘")
            {
                label11.Text = "关闭键盘";
                textBox1.Focus();
            }
            else
            {
                label11.Text = "开启键盘";
            }
        }

        private void 用户输入窗_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (label2.Text == "时间模式")
            {
                label2.Text = "文本模式";
                textBox1.Visible = false;
                label11.Visible = false;
                label3.Visible = false;
                dateTimePicker1.Visible = true;
            }
            else
            {
                label2.Text = "时间模式";
                textBox1.Visible = true;
                label11.Visible = true;
                label3.Visible = true;
                dateTimePicker1.Visible = false;
            }
        }

        private void 鼠标左键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标左键" + "[1]";
        }

        private void 鼠标右键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标右键" + "[2]";
        }

        private void 鼠标中键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标中键" + "[4]";
        }

        private void 鼠标X1键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标X1键" + "[5]";
        }

        private void 鼠标X2键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "鼠标X2键" + "[6]";
        }

        private void 音量加键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "音量加键" + "[175]";
        }

        private void 音量减键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "音量减键" + "[174]";
        }

        private void 静音键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "静音键" + "[173]";
        }

        private void 停止键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "停止键" + "[179]";
        }

        private void 浏览器键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "浏览器键" + "[172]";
        }

        private void 邮件键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "邮件键" + "[180]";
        }

        private void 收藏键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "收藏键" + "[171]";
        }

        private void 网页退后键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "网页退后键" + "[166]";
        }

        private void 网页前进键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "网页前进键" + "[167]";
        }

        private void 打开主页键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "打开主页键" + "[172]";
        }

        private void 网页刷新键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "网页刷新键" + "[168]";
        }

        private void 搜索栏键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "搜索栏键" + "[170]";
        }

        private void 停止加载键_Click(object sender, EventArgs e)
        {
            textBox1.Text = "停止加载键" + "[169]";
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Tab键" + "[9]";
        }




    }
}
