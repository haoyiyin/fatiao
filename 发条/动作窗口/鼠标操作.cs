using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public partial class 鼠标操作 : Form
    {

        public static 动作类型 动作类型 = new 动作类型();

        public 鼠标操作()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "绝对移动":
                    label8.Enabled = true;
                    comboBox2.Enabled = true;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = true;
                    textBox10.Enabled = true;
                    label18.Enabled = true;

                    label5.Enabled = false;
                    comboBox3.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
                case "随机移动":
                    label8.Enabled = true;
                    comboBox2.Enabled = true;

                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                    textBox9.Enabled = true;
                    textBox6.Enabled = true;
                    label12.Enabled = true;
                    label13.Enabled = true;
                    label14.Enabled = true;
                    label15.Enabled = true;
                    label20.Enabled = true;
                    label22.Enabled = true;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label5.Enabled = false;
                    comboBox3.Enabled = false;

                    label11.Enabled = false;
                    label9.Enabled = false;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label18.Enabled = false;

                    numericUpDown1.Enabled = false;
                    break;
                case "相对移动":
                    label8.Enabled = true;
                    comboBox2.Enabled = true;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = true;
                    textBox10.Enabled = true;
                    label18.Enabled = true;

                    label5.Enabled = false;
                    comboBox3.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
                case "轨迹移动":
                    label8.Enabled = true;
                    comboBox2.Enabled = true;

                    label11.Enabled = false;
                    label18.Enabled = false;
                    label9.Enabled = false;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;

                    label16.Enabled = true;
                    label17.Enabled = true;
                    numericUpDown1.Enabled = true;
                    label19.Enabled = true;
                    numericUpDown2.Enabled = true;

                    label5.Enabled = false;
                    comboBox3.Enabled = false;

                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                    textBox9.Enabled = true;
                    textBox6.Enabled = true;
                    label12.Enabled = true;
                    label13.Enabled = true;
                    label14.Enabled = true;
                    label15.Enabled = true;
                    label20.Enabled = true;
                    label22.Enabled = true;
                    break;
                case "单击":
                    label8.Enabled = false;
                    comboBox2.Enabled = false;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label5.Enabled = true;
                    comboBox3.Enabled = true;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label11.Enabled = false;
                    label9.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
                case "双击":
                    label8.Enabled = false;
                    comboBox2.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;

                    label5.Enabled = true;
                    comboBox3.Enabled = true;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label11.Enabled = false;
                    label9.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
                case "长按":
                    label8.Enabled = false;
                    comboBox2.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label5.Enabled = true;
                    comboBox3.Enabled = true;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label11.Enabled = false;
                    label9.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
                case "松开":
                    label8.Enabled = false;
                    comboBox2.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label5.Enabled = true;
                    comboBox3.Enabled = true;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label11.Enabled = false;
                    label9.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
                case "滚轮单击":
                    label8.Enabled = false;
                    comboBox2.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label5.Enabled = false;
                    comboBox3.Enabled = false;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label11.Enabled = false;
                    label9.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
                case "滚轮上滑":
                    label8.Enabled = false;
                    comboBox2.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label5.Enabled = false;
                    comboBox3.Enabled = false;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label11.Enabled = false;
                    label9.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;

                case "滚轮下滑":
                    label8.Enabled = false;
                    comboBox2.Enabled = false;
                    label19.Enabled = false;
                    numericUpDown2.Enabled = false;

                    label11.Enabled = true;
                    label9.Enabled = true;
                    textBox3.Enabled = false;
                    textBox10.Enabled = false;
                    textBox9.Enabled = false;
                    textBox6.Enabled = false;
                    label5.Enabled = false;
                    comboBox3.Enabled = false;

                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    label15.Enabled = false;
                    label11.Enabled = false;
                    label9.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label20.Enabled = false;
                    label22.Enabled = false;
                    numericUpDown1.Enabled = false;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hwnd;//窗口句柄变量
            int 条件窗口是否存在;//变量

            hwnd = 发条.发条窗口.fatiao.FindWindow("", "条件判断");
            条件窗口是否存在 = 发条.发条窗口.fatiao.GetWindowState(hwnd, 0);
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                if (条件窗口是否存在 == 0) //如果不存在
                {
                    
                    if (发条.发条窗口.listBox1.SelectedIndex != -1)
                    {
                        switch (comboBox1.Text)
                        {
                            case "绝对移动":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox3.Text + "," + textBox10.Text + ");" + textBox2.Text);
                                break;
                            case "随机移动":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox6.Text + ");" + textBox2.Text);
                                break;
                            case "相对移动":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox3.Text + "," + textBox10.Text + ");" + textBox2.Text);
                                break;
                            case "轨迹移动":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox6.Text + "," + numericUpDown1.Value + "," + numericUpDown2.Value + ");" + textBox2.Text);
                                break;
                            case "单击":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "双击":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "长按":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "松开":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "滚轮单击":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                                break;
                            case "滚轮上滑":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                                break;
                            case "滚轮下滑":
                                发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 1, "鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                                break;
                        }
                        if (textBox1.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Insert(发条.发条窗口.listBox1.SelectedIndex + 2, "等待时间(" + textBox1.Text + ");");
                        }
                    }
                    else
                    {
                        switch (comboBox1.Text)
                        {
                            case "绝对移动":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox3.Text + "," + textBox10.Text + ");" + textBox2.Text);
                                break;
                            case "随机移动":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox6.Text + ");" + textBox2.Text);
                                break;
                            case "相对移动":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox3.Text + "," + textBox10.Text + ");" + textBox2.Text);
                                break;
                            case "轨迹移动":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox6.Text + "," + numericUpDown1.Value + "," + numericUpDown2.Value + ");" + textBox2.Text);
                                break;
                            case "单击":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "双击":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "长按":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "松开":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                                break;
                            case "滚轮单击":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                                break;
                            case "滚轮上滑":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                                break;
                            case "滚轮下滑":
                                发条.发条窗口.listBox1.Items.Add("鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                                break;
                        }
                        if (textBox1.Text != "")
                        {
                            发条.发条窗口.listBox1.Items.Add("等待时间(" + textBox1.Text + ");");
                        }
                    }
                }
                Close();
            }
            else//参数为空
            {
                label21.Text = "坐标参数不能为空";
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                int 选中行;
                
                选中行 = 发条.发条窗口.listBox1.SelectedIndex;
                发条.发条窗口.listBox1.Items.RemoveAt(选中行);


                switch (comboBox1.Text)
                {
                    case "绝对移动":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox3.Text + "," + textBox10.Text + ");" + textBox2.Text);
                        break;
                    case "随机移动":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox6.Text + ");" + textBox2.Text);
                        break;
                    case "相对移动":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox3.Text + "," + textBox10.Text + ");" + textBox2.Text);
                        break;
                    case "轨迹移动":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox2.Text + "," + textBox4.Text + "," + textBox9.Text + "," + textBox5.Text + "," + textBox6.Text + "," + numericUpDown1.Value+ "," + numericUpDown2.Value + ");" + textBox2.Text);
                        break;
                    case "单击":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                        break;
                    case "双击":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                        break;
                    case "长按":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                        break;
                    case "松开":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + "," + comboBox3.Text + ");" + textBox2.Text);
                        break;
                    case "滚轮单击":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                        break;
                    case "滚轮上滑":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                        break;
                    case "滚轮下滑":
                        发条.发条窗口.listBox1.Items.Insert(选中行, "鼠标操作(" + comboBox1.Text + ");" + textBox2.Text);
                        break;
                }

                发条.发条窗口.listBox1.SelectedIndex = 选中行;
                Close();
            }
            else//参数为空
            {
                label21.Text = "坐标参数不能为空";
            }
        }


        int 判断编辑框 = 0;

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            判断编辑框 = 1;
            退出线程 = true;
            坐标与颜色操作();

        }

        private void label20_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 2;
            Delay(200);
            发条.发条窗口.参数窗口 = "鼠标操作";
            选取范围 选取范围 = new 选取范围();
            选取范围.Show();
        }


        #region 参数菜单

        工具字幕 字幕 = new 工具字幕();
        bool 退出线程 = false;

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
                //CheckForIllegalCrossThreadCalls = false;
                发条.发条窗口.fatiao.BindWindow(发条.发条窗口.fatiao.GetSpecialWindow(0), "normal", "normal", "normal", 0);//前台基于窗口
                while (true)
                {
                    hwnd = 发条.发条窗口.fatiao.GetMousePointWindow();//获取鼠标指向窗口句柄
                    string 窗口名 = 发条.发条窗口.fatiao.GetWindowTitle(hwnd);//获得窗口标题
                    动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标
                    if (动作.判断按键(192) == true)//快捷键“~”
                    {
                        锁定窗口名 = 窗口名;
                        发条.发条窗口.fatiao.GetClientRect(hwnd, out x1, out y1, out object x2, out object y2);
                        
                    }

                    if (锁定窗口名 == "")
                    {
                        发条.发条窗口.fatiao.GetClientRect(hwnd, out x1, out y1, out x2, out y2);
                        X = 坐标.X - (int)x1;
                        Y = 坐标.Y - (int)y1;
                    }
                    else
                    {
                        X = 坐标.X - (int)x1;
                        Y = 坐标.Y - (int)y1;
                    }

                    string color = 发条.发条窗口.fatiao.GetColor(坐标.X, 坐标.Y);//获取坐标颜色
                    if (锁定窗口名 != "")
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 锁定窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标";
                    }
                    else
                    {
                        字幕.label1.Text = "窗口名：" + 窗口名 + "   “~”键指定窗口内坐标\r\n" + "屏幕坐标：" + 坐标.X + "," + 坐标.Y + "   “空格”键获取桌面内坐标\r\n" + 窗口名 + " 窗口内坐标：" + X + "," + Y + "   Ctrl+空格键获取窗口内坐标";
                    }

                    if (!动作.判断按键(17) && 动作.判断按键(32))//快捷键“空格”
                    {
                        switch (判断编辑框)
                        {
                            case 1:
                                textBox3.Text = 坐标.X.ToString();//将窗口内坐标写入编辑框
                                textBox10.Text = 坐标.Y.ToString();//将窗口内坐标写入编辑框
                                break;
                            case 2:
                                textBox4.Text = 坐标.X.ToString();//将窗口内坐标写入编辑框
                                textBox9.Text = 坐标.Y.ToString();//将窗口内坐标写入编辑框
                                break;
                            case 3:
                                textBox5.Text = 坐标.X.ToString();//将窗口内坐标写入编辑框
                                textBox6.Text = 坐标.Y.ToString();//将窗口内坐标写入编辑框
                                break;
                        }
                        字幕.label1.Text = "";
                        
                        return;
                    }

                    if (动作.判断按键(17) && 动作.判断按键(32))//快捷键Ctrl+空格
                    {
                        switch (判断编辑框)
                        {
                            case 1:
                                textBox3.Text = X.ToString();//将窗口内坐标写入编辑框
                                textBox10.Text = Y.ToString();//将窗口内坐标写入编辑框
                                break;
                            case 2:
                                textBox4.Text = X.ToString();//将窗口内坐标写入编辑框
                                textBox9.Text = Y.ToString();//将窗口内坐标写入编辑框
                                break;
                            case 3:
                                textBox5.Text = X.ToString();//将窗口内坐标写入编辑框
                                textBox6.Text = Y.ToString();//将窗口内坐标写入编辑框
                                break;
                        }
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

        private void button17_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            if (button17.ForeColor != Color.Maroon)//等于蓝色
            {
                button17.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button17.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox3.BackColor = Color.White;//白色
                textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label18.Visible = true;
                button17.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label18.Visible = false;
                textBox3.BackColor = Color.Linen;//黄色
                textBox3.ForeColor = Color.Maroon;//红色
                button17.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
            if (button16.ForeColor != Color.Maroon)//等于蓝色
            {
                button16.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button16.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button16.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox10.BackColor = Color.White;//白色
                textBox10.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button16.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox10.BackColor = Color.Linen;//黄色
                textBox10.ForeColor = Color.Maroon;//红色
                button16.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            if (button15.ForeColor != Color.Maroon)//等于蓝色
            {
                button15.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button15.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button15.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox4.BackColor = Color.White;//白色
                textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label20.Visible = true;
                button15.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label20.Visible = false;
                textBox4.BackColor = Color.Linen;//黄色
                textBox4.ForeColor = Color.Maroon;//红色
                button15.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            if (button14.ForeColor != Color.Maroon)//等于蓝色
            {
                button14.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button14.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button14.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox9.BackColor = Color.White;//白色
                textBox9.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                button14.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                textBox9.BackColor = Color.Linen;//黄色
                textBox9.ForeColor = Color.Maroon;//红色
                button14.BackgroundImage = Properties.Resources.开启变量;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            if (button8.ForeColor != Color.Maroon)//等于蓝色
            {
                button8.ForeColor = Color.Maroon;//红色
            }
            else
            {
                button8.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
            }

            if (button8.ForeColor != Color.Maroon)//等于蓝色
            {
                textBox5.BackColor = Color.White;//白色
                textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                label22.Visible = true;
                button8.BackgroundImage = Properties.Resources.关闭变量;
            }
            else//红色
            {
                label22.Visible = false;
                textBox5.BackColor = Color.Linen;//黄色
                textBox5.ForeColor = Color.Maroon;//红色
                button8.BackgroundImage = Properties.Resources.开启变量;
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
            }
            else//红色
            {
                textBox6.BackColor = Color.Linen;//黄色
                textBox6.ForeColor = Color.Maroon;//红色
                button7.BackgroundImage = Properties.Resources.开启变量;
            }
        }


        private void 鼠标操作_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
            e.Cancel = true;    //取消"关闭窗口"事件
            Hide();
        }

        private void 鼠标操作_Load(object sender, EventArgs e)
        {
/*            label21.Text = "";*/
        }

        private void 鼠标操作_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;//表示已经处理了键盘消息
            }
        }

        private void 鼠标操作_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                label21.Text = "";

                if (button17.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox3.BackColor = Color.White;//白色
                    textBox3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label18.Visible = true;
                    button17.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label18.Visible = false;
                    textBox3.BackColor = Color.Linen;//黄色
                    textBox3.ForeColor = Color.Maroon;//红色
                    button17.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button16.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox10.BackColor = Color.White;//白色
                    textBox10.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button16.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox10.BackColor = Color.Linen;//黄色
                    textBox10.ForeColor = Color.Maroon;//红色
                    button16.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button15.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox4.BackColor = Color.White;//白色
                    textBox4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label20.Visible = true;
                    button15.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label20.Visible = false;
                    textBox4.BackColor = Color.Linen;//黄色
                    textBox4.ForeColor = Color.Maroon;//红色
                    button15.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button8.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox5.BackColor = Color.White;//白色
                    textBox5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    label22.Visible = true;
                    button8.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    label22.Visible = false;
                    textBox5.BackColor = Color.Linen;//黄色
                    textBox5.ForeColor = Color.Maroon;//红色
                    button8.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button14.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox9.BackColor = Color.White;//白色
                    textBox9.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button14.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox9.BackColor = Color.Linen;//黄色
                    textBox9.ForeColor = Color.Maroon;//红色
                    button14.BackgroundImage = Properties.Resources.开启变量;
                }
                if (button7.ForeColor != Color.Maroon)//等于蓝色
                {
                    textBox6.BackColor = Color.White;//白色
                    textBox6.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                    button7.BackgroundImage = Properties.Resources.关闭变量;
                }
                else//红色
                {
                    textBox6.BackColor = Color.Linen;//黄色
                    textBox6.ForeColor = Color.Maroon;//红色
                    button7.BackgroundImage = Properties.Resources.开启变量;
                }

            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            退出线程 = true;
            判断编辑框 = 3;
            Delay(200);
            发条.发条窗口.参数窗口 = "鼠标操作";
            选取范围 选取范围 = new 选取范围();
            选取范围.Show();
        }
    }
}
