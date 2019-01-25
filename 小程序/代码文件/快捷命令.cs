using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Globalization;
using 动作库;

namespace fatiao
{
    public class 快捷命令
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetF(); //获得本窗体的句柄
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetF(IntPtr hWnd); //设置此窗体为活动窗体


        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();//桌面句柄

        Random 随机数 = new Random();

        public int hwnd;//窗口句柄

        public string 输入内容;

        public TSPlugLib.TSPlugInterFace fatiao = new TSPlugLib.TSPlugInterFace();//新的dll

        public string[] 逻辑变量名;//获取自定义变量字符串

        public string[] 整数变量名;//获取自定义变量字符串

        public static int[] 整数数值;

        public static string[] 逻辑数值;

        public string[] 文本变量名;//获取自定义变量字符串

        public static string[] 文本数值;

        public static 弹幕 弹幕内容 = new 弹幕();

        public static 动作类型 动作类型 = new 动作类型();

        public int 当前行;//行数变量

        public bool 停止作用 = false;//用作停止线程

        public bool 暂停作用 = false;//用作暂停线程

        private void 暂停()
        {
            while (暂停作用 == true)
            {
                Delay(100);
            }
        }

        public  void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }


        public void 快捷命令运行()
        {
            try
            {
                fatiao.BindWindow(fatiao.GetSpecialWindow(0), "normal", "normal", "normal", 0);//前台基于窗口
                int 坐标x1 = -1, 坐标x2 = -1, 坐标y1 = -1, 坐标y2 = -1;
                hwnd = 0;//句柄


                int 遇到循环始, 循环次数 = 0;
                string 基于模式 = "前台模式";
                int 数字传递 = 0;
                string 返回变量 = "无";
                string 返回变量二 = "无";
                object x1, x2, y1, y2;
                int 当前行 = 0;
                string 每行内容;
                string[] 动作类;
                string 参数一;
                string[] 参数二;
                string[] 参数三;
                string[] 参数四;
                string[] 参数五;
                string 参数六;
                string 参数七;
                string[] 参数八;
                string 参数九;
                int 左括号;
                int 右括号;
                int 右至左;
                int 图色;
                string[] 参数;
                string 当前时间;
                double 比较时间 = 0;
                string 图片目录;
                object 坐标一;//找图找色返回坐标
                object 坐标二;//找图找色返回坐标
                int 最大行 = winapp.小程序窗口.listBox6.Items.Count;//最大行数下限

                fatiao.SetShowErrorMsg(0);//不显示报错弹窗

                while (当前行 < 最大行)//运行行数小于最大行数时
                {
                    if (停止作用 == true)//停止循环的一道开关
                    {
                        停止作用 = false;
                        return;
                    }
                    每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                    winapp.小程序窗口.listBox6.SelectedIndex = -1;//取消前一个高亮
                    winapp.小程序窗口.listBox6.SelectedIndex = 当前行;//高亮当前运行行
                    当前行++;//运行行数递增+1

                    if (每行内容 == "")
                    {
                        每行内容 = "注释(空格);";
                    }

                    if (每行内容 == "﹂循环末;" || 每行内容 == "﹂如果末;" || 每行内容 == "停用:﹂循环末;" || 每行内容 == "停用:﹂如果末;")
                    {
                        每行内容 += "()";
                    }

                    动作类 = 每行内容.Split('(');//分割命令名和参数
                    左括号 = 每行内容.IndexOf("(");//定位左括号的位置（从左到右定位）
                    参数九 = 每行内容.Substring(左括号);//选取左括号+之后的内容
                    参数一 = 每行内容.Substring(左括号);//选取左括号+之后的内容
                    右括号 = 参数一.LastIndexOf(")");//定位右括号的位置（从右到左定位）
                    参数一 = 参数一.Substring(1, 右括号 - 1);//括号内的参数（从第二个字符串开始，至右括号前一个位置）
                    //参数一 = 参数一.Replace(" ", "");//条件去除空格
                    参数 = 参数一.Split(',');//简单切割所有参数

                    #region 具体命令
                    switch (动作类[0])
                    {
                        case "点击图片":
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数三 = 参数二[2].Split(new string[] { "\"," }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                            参数七 = 参数三[0];
                            int 句柄 = fatiao.FindWindow("", 参数六);
                            fatiao.GetClientSize(句柄, out object w, out object h);
                            坐标一 = -1;
                            坐标二 = -1;
                            图色 = 100;
                            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                            {
                                TSPlugLib.TSPlugInterFace 点图 = new TSPlugLib.TSPlugInterFace();//新的dll
                                点图.BindWindow(句柄, "normal", "normal", "normal", 0);

                                图色 = 点图.FindPic(0, 0, (int)w, (int)h, winapp.小程序窗口.我的文档路径 + @"\Winapp" + @".\素材库\" + 参数七, "000000", Convert.ToDouble(参数三[1]) / 10, 0, out 坐标一, out 坐标二);
                                if (图色 != -1)
                                {
                                    点图.MoveTo((int)坐标一, (int)坐标二);
                                    点图.LeftClick();

                                }
                                点图.UnBindWindow();
                            }), null);

                            while (图色 == 100)
                            {
                                Delay(1);
                            }

                            if (参数二[0] != "")
                            {
                                if (图色 != -1)
                                {
                                    赋值变量(参数二[0], "真");
                                }
                                else
                                {
                                    赋值变量(参数二[0], "假");
                                }
                            }

                            break;

                        case "点击位置":
                            参数二 = 参数一.Split(new string[] { "\"," }, StringSplitOptions.None);//分割自定义内容最左端

                            参数六 = 参数二[0].Substring(1, 参数二[0].Length - 1);//掐头
                            参数三 = 参数二[1].Split(',');//简单切割所有参数

                            句柄 = fatiao.FindWindow("", 参数六);

                            动作.百分比转坐标(句柄, out int 坐标X, out int 坐标Y, Convert.ToDouble(参数三[0]), Convert.ToDouble(参数三[1]));

                            图色 = 100;
                            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                            {
                                TSPlugLib.TSPlugInterFace 点位 = new TSPlugLib.TSPlugInterFace();//新的dll

                                点位.BindWindow(句柄, "normal", "normal", "normal", 0);

                                点位.MoveTo(坐标X, 坐标Y);
                                点位.LeftClick();
                                图色 = 10;
                                点位.UnBindWindow();
                            }), null);

                            while (图色 == 100)
                            {
                                Delay(1);
                            }

                            break;

                        case "获取时间":

                            switch (参数[1])
                            {
                                case "年月日时分秒":
                                    当前时间 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                    break;
                                case "年月日":
                                    当前时间 = DateTime.Now.ToString("yyyy/MM/dd");

                                    break;
                                case "时分秒":
                                    当前时间 = DateTime.Now.ToString("HH:mm:ss");
                                    break;
                                case "仅时钟":
                                    当前时间 = DateTime.Now.ToString("HH");
                                    break;
                                case "仅分钟":
                                    当前时间 = DateTime.Now.ToString("mm");
                                    break;
                                default://仅秒钟
                                    当前时间 = DateTime.Now.ToString("ss");
                                    break;
                            }

                            赋值变量(参数[0], 当前时间);

                            break;

                        case "上传文件":
                            参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//定位保存目录名最左端
                            右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//保存目录+文件名
                            string 上传网址 = 参数[1].Substring(1, 参数[1].Length - 2);//定位网址
                            string 上传结果 = 动作.上传文件(上传网址, 参数六);
                            if (参数[0] != "")
                            {
                                赋值变量(参数[0], 上传结果);
                            }

                            break;

                        case "获取设备码":

                            switch (参数[1])
                            {
                                case "CPU码":
                                    string 设备码 = 动作.CPU码();
                                    //添加文本(参数[0]);
                                    赋值变量(参数[0], 设备码);
                                    break;
                                case "硬盘码":
                                    设备码 = 动作.硬盘码();
                                    //添加文本(参数[0]);
                                    赋值变量(参数[0], 设备码);
                                    break;
                            }

                            break;

                        case "读写文件":

                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[参数二.Length - 1];//获取最后一组数据
                            参数六 = 参数六.Substring(0, 参数六.Length - 1);//去尾

                            switch (参数[1])
                            {
                                case "文本读取":
                                    string 读取 = 动作.文本读取(参数六);
                                    赋值变量(参数[0], 读取);
                                    break;
                                case "配置读取":
                                    if (参数[2].Contains("\""))
                                    {
                                        参数[2] = 参数[2].Substring(1, 参数[2].Length - 2);//掐头去尾

                                    }
                                    else
                                    {
                                        参数[2] = 引用变量(参数[2]);

                                    }

                                    if (参数[3].Contains("\""))
                                    {
                                        参数[3] = 参数[3].Substring(1, 参数[3].Length - 2);//掐头去尾
                                    }
                                    else
                                    {
                                        参数[3] = 引用变量(参数[3]);

                                    }
                                    读取 = 动作.读取ini(参数[2], 参数[3], "空", 参数六);
                                    赋值变量(参数[0], 读取);
                                    break;
                                case "文本写入":

                                    参数七 = 参数二[参数二.Length - 2];//获取最后2组数据
                                    if (参数七.Contains(","))
                                    {
                                        参数三 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割
                                        参数七 = 参数三[参数三.Length - 1];//获取最后2组数据
                                        参数七 = 引用变量(参数七);
                                    }
                                    else
                                    {
                                        参数七 = 参数七.Substring(0, 参数七.Length - 1);//去尾
                                    }

                                    读取 = 动作.文本写入(参数七, 参数六);
                                    赋值变量(参数[0], 读取);

                                    break;

                                default://配置写入
                                    参数七 = 参数二[参数二.Length - 2];//获取最后2组数据
                                    if (参数七.Contains(","))
                                    {
                                        参数三 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割
                                        参数七 = 参数三[参数三.Length - 1];//获取最后2组数据
                                        参数七 = 引用变量(参数七);
                                    }
                                    else
                                    {
                                        参数七 = 参数七.Substring(0, 参数七.Length - 1);//去尾
                                    }

                                    if (参数[2].Contains("\""))
                                    {
                                        参数[2] = 参数[2].Substring(1, 参数[2].Length - 2);//掐头去尾

                                    }
                                    else
                                    {
                                        参数[2] = 引用变量(参数[2]);

                                    }

                                    if (参数[3].Contains("\""))
                                    {
                                        参数[3] = 参数[3].Substring(1, 参数[3].Length - 2);//掐头去尾
                                    }
                                    else
                                    {
                                        参数[3] = 引用变量(参数[3]);

                                    }
                                    读取 = 动作.写入ini(参数[2], 参数[3], 参数七, 参数六);
                                    赋值变量(参数[0], 读取);

                                    break;
                            }

                            break;

                        case "获取缩放率":

                            int 缩放率 = (int)(分辨率缩放.ScaleX * 100);
                            //添加整数(参数一);
                            赋值变量(参数一, 缩放率.ToString());

                            break;

                        case "变量赋值":

                            参数三 = 参数一.Split(new string[] { "=" }, StringSplitOptions.None);//分割条件和结果

                            if (参数一.Contains("=\""))
                            {
                                参数二 = 参数一.Split(new string[] { "=\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                赋值变量(参数三[0], 参数六);
                            }
                            else
                            {
                                赋值变量(参数三[0], 参数三[1]);
                            }
                            break;

                        case "删除文件":
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                            Console.WriteLine(参数六);


                            try
                            {
                                if (File.Exists(参数六))//如果存在就删除
                                {
                                    File.Delete(参数六);
                                    if (参数[0] != "")
                                    {
                                        //添加逻辑(参数[0]);
                                        赋值变量(参数[0], "真");
                                    }
                                }
                                else
                                {
                                    if (参数[0] != "")
                                    {
                                        //添加逻辑(参数[0]);
                                        赋值变量(参数[0], "假");
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                if (参数[0] != "")
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "假");
                                }
                            }
                            break;

                        case "每次请求输入":

                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                            用户输入窗 用户输入窗 = new 用户输入窗();
                            用户输入窗.label1.Text = 参数六;

                            用户输入窗.ShowDialog();
                            赋值变量(参数[0], winapp.小程序窗口.快捷命令输入变量值);

                            break;

                        case "首次请求输入":

                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                            用户输入窗 用户输入窗1 = new 用户输入窗();
                            用户输入窗1.label1.Text = 参数六;


                            用户输入窗1.ShowDialog();
                            赋值变量(参数[0], winapp.小程序窗口.快捷命令输入变量值);


                            winapp.小程序窗口.listBox6.Items.Insert(当前行, "变量赋值(" + 参数[0] + "=\"" + winapp.小程序窗口.快捷命令输入变量值 + "\");");

                            winapp.小程序窗口.listBox6.Items.RemoveAt(当前行-1);//删除当前行

                            break;

                        case "获取剪贴板":
                            赋值变量(参数一, Clipboard.GetText());

                            break;

                        case "随机数字":
                            if (参数[0] != "")//变量未空
                            {
                                int 起始值 = 0, 最大值 = 0, 随机值 = 0;
                                if (winapp.小程序窗口.判断数字(参数[1]) == "整数" && winapp.小程序窗口.判断数字(参数[2]) == "整数")
                                {
                                    起始值 = int.Parse(参数[1]);
                                    最大值 = int.Parse(参数[2]);
                                }
                                else if (winapp.小程序窗口.判断数字(参数[1]) != "整数" && winapp.小程序窗口.判断数字(参数[2]) == "整数")
                                {
                                    起始值 = int.Parse(动作.读取ini("变量名", 参数[1], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                    最大值 = int.Parse(参数[2]);
                                }
                                else if (winapp.小程序窗口.判断数字(参数[1]) == "整数" && winapp.小程序窗口.判断数字(参数[2]) != "整数")
                                {
                                    起始值 = int.Parse(参数[1]);
                                    最大值 = int.Parse(动作.读取ini("变量名", 参数[2], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                else//两个都不是
                                {
                                    起始值 = int.Parse(动作.读取ini("变量名", 参数[1], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                    最大值 = int.Parse(动作.读取ini("变量名", 参数[2], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }

                                随机值 = 随机数.Next(起始值, 最大值);
                                //添加整数(参数[0]);
                                赋值变量(参数[0], 随机值.ToString());
                            }
                            break;
                        case "发送邮件":
                            参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//分割自定义内容最左端

                            参数四 = 参数二[0].Split(new string[] { ",\"" }, StringSplitOptions.None);//分割端口和服务器一次
                            参数三 = 参数四[1].Split(new string[] { "\"," }, StringSplitOptions.None);//分割端口和服务器二次
                            参数五 = 参数三[0].Split(new string[] { "“" }, StringSplitOptions.None);//分割服务器
                            参数八 = 参数二[4].Split(new string[] { "\"" }, StringSplitOptions.None);//分割内容
                            string 返回邮件结果 = "";

                            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                            {
                                返回邮件结果 = 邮箱功能.发送邮件(参数五[0], int.Parse(参数三[1]), 参数四[2], 参数二[1], 参数二[2], 参数二[3], 参数八[0]);

                            }), null);

                            while (返回邮件结果 == "")
                            {
                                Delay(100);
                            }
                            if (返回邮件结果 == "真")
                            {
                                if (参数[0] != "")
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "真");
                                }
                            }
                            else
                            {
                                if (参数[0] != "")
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "假");
                                }
                            }
                            break;

                        case "结束进程":
                            参数六 = 参数一.Substring(1, 参数一.Length - 2);//掐头去尾
                            if (参数一 == "结束自身")
                            {
                                winapp.小程序窗口.Dispose();//释放资源
                                Environment.Exit(0); // 彻底关闭进程
                            }
                            else
                            {
                                winapp.小程序窗口.关闭进程(参数六);
                            }
                            break;

                        case "停止运行":
                            winapp.小程序窗口.停止发条();
                            winapp.小程序窗口.停止快捷命令();
                            break;
                        case "等待时间":
                            参数六 = 参数一;

                            if (winapp.小程序窗口.判断数字(参数六) == "整数")
                            {
                                for (int 循环 = 0; 循环 < int.Parse(参数六) / 15; 循环++)
                                {
                                    Delay(1);
                                    if (停止作用 == true)//停止循环的开关
                                    {
                                        return;
                                    }
                                }
                            }
                            else//两个都不是
                            {
                                数字传递 = int.Parse(动作.读取ini("变量名", 参数六, "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));

                                for (int 循环 = 0; 循环 < 数字传递 / 15; 循环++)
                                {
                                    Delay(1);
                                    if (停止作用 == true)//停止循环的开关
                                    {
                                        return;
                                    }
                                }
                            }
                            break;
                        case "跳出循环":
                            当前行--;//运行行数递增-1，回到当前行
                            遇到循环始 = 0;
                            while (true)
                            {
                                if (当前行 >= 最大行) //运行行数小于最大行数时
                                {
                                    break;
                                }
                                当前行++;//运行行数递增+1
                                每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                                动作类 = 每行内容.Split('(');//分割命令名和参数
                                switch (动作类[0])
                                {
                                    case "﹁循环始":
                                        遇到循环始++;
                                        break;

                                    case "﹂循环末;":
                                        遇到循环始--;
                                        break;
                                }
                                if (遇到循环始 < 0) //运行行数小于最大行数时
                                {
                                    当前行++;
                                    break;
                                }
                            }
                            break;
                        case "运行与打开":
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                            try
                            {
                                Process.Start(参数六);
                                if (参数[0] != "")
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "真");
                                }
                            }
                            catch (Exception)
                            {
                                if (参数[0] != "")
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "假");
                                }
                            }
                            break;
                        case "基于平面":
                            if (参数[1] == "桌面内")
                            {
                                hwnd = fatiao.GetSpecialWindow(0);
                                switch (参数[2])
                                {
                                    case "前台模式":
                                        fatiao.BindWindow(hwnd, "normal", "normal", "normal", 0);
                                        基于模式 = "前台模式";
                                        break;
                                    case "前台模式二":
                                        fatiao.BindWindow(hwnd, "normal", "normal", "normal", 1);
                                        基于模式 = "前台模式二";
                                        break;
                                    case "前台模式三":
                                        fatiao.BindWindow(hwnd, "normal", "normal", "normal", 101);
                                        基于模式 = "前台模式三";
                                        break;
                                    case "伪后台模式":
                                        fatiao.BindWindow(hwnd, "normal", "windows", "windows", 0);
                                        基于模式 = "伪后台模式";
                                        break;
                                    case "后台模式":
                                        fatiao.BindWindow(hwnd, "gdi", "windows", "windows", 0);
                                        基于模式 = "后台模式";
                                        break;
                                    case "后台模式二":
                                        fatiao.BindWindow(hwnd, "gdi", "windows", "windows", 1);
                                        基于模式 = "后台模式二";
                                        break;
                                    case "后台模式三":
                                        fatiao.BindWindow(hwnd, "gdi", "windows", "windows", 101);
                                        基于模式 = "后台模式三";
                                        break;
                                    case "后台模式四":
                                        fatiao.BindWindow(hwnd, "dx", "windows", "windows", 0);
                                        基于模式 = "后台模式四";
                                        break;
                                    case "后台模式五":
                                        fatiao.BindWindow(hwnd, "dx", "windows", "windows", 1);
                                        基于模式 = "后台模式五";
                                        break;
                                    case "后台模式六":
                                        fatiao.BindWindow(hwnd, "dx", "windows", "windows", 101);
                                        基于模式 = "后台模式六";
                                        break;
                                }
                            }
                            else
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容

                                switch (参数[2])
                                {
                                    case "窗口标题":
                                        hwnd = fatiao.FindWindow("", 参数六);
                                        break;
                                    case "窗口句柄":
                                        hwnd = Int32.Parse(参数六);
                                        break;
                                    case "坐标位置":
                                        参数八 = 参数六.Split(',');//分割命坐标
                                        hwnd = fatiao.GetMousePointWindow();
                                        hwnd = fatiao.GetPointWindow(Int32.Parse(参数八[0]), Int32.Parse(参数八[1]));
                                        break;
                                }
                                //
                                switch (参数七)
                                {
                                    case "前台模式":
                                        fatiao.BindWindow(hwnd, "normal", "normal", "normal", 0);
                                        基于模式 = "前台模式";
                                        break;
                                    case "前台模式二":
                                        fatiao.BindWindow(hwnd, "normal", "normal", "normal", 1);
                                        基于模式 = "前台模式二";
                                        break;
                                    case "前台模式三":
                                        fatiao.BindWindow(hwnd, "normal", "normal", "normal", 101);
                                        基于模式 = "前台模式三";
                                        break;
                                    case "伪后台模式":
                                        fatiao.BindWindow(hwnd, "normal", "windows", "windows", 0);
                                        基于模式 = "伪后台模式";
                                        break;
                                    case "后台模式":
                                        fatiao.BindWindow(hwnd, "gdi", "windows", "windows", 0);
                                        基于模式 = "后台模式";
                                        break;
                                    case "后台模式二":
                                        fatiao.BindWindow(hwnd, "gdi", "windows", "windows", 1);
                                        基于模式 = "后台模式二";
                                        break;
                                    case "后台模式三":
                                        fatiao.BindWindow(hwnd, "gdi", "windows", "windows", 101);
                                        基于模式 = "后台模式三";
                                        break;
                                    case "后台模式四":
                                        fatiao.BindWindow(hwnd, "dx", "windows", "windows", 0);
                                        基于模式 = "后台模式四";
                                        break;
                                    case "后台模式五":
                                        fatiao.BindWindow(hwnd, "dx", "windows", "windows", 1);
                                        基于模式 = "后台模式五";
                                        break;
                                    case "后台模式六":
                                        fatiao.BindWindow(hwnd, "dx", "windows", "windows", 101);
                                        基于模式 = "后台模式六";
                                        break;
                                }
                            }

                            if (参数[0] != "")
                            {
                                //添加逻辑(参数[0]);
                                if (fatiao.IsBind(hwnd) == 1)
                                {
                                    赋值变量(参数[0], "真");
                                }
                                else
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            if (fatiao.IsBind(hwnd) != 1)
                            {
                                hwnd = 0;
                            }
                            break;
                        case "设置窗口":
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数七 = 参数二[1].Substring(0, 右至左);//自定义内容
                            参数九 = 参数二[1].Substring(右至左 + 2);//选项
                            参数八 = 参数九.Split(new string[] { "," }, StringSplitOptions.None);//定位自定义内容最左端

                            hwnd = fatiao.FindWindow("", 参数七);
                            switch (参数八[0])
                            {
                                case "窗口大小":
                                    数字传递 = fatiao.SetWindowSize(hwnd, int.Parse(参数八[1]), int.Parse(参数八[2]));
                                    break;
                                case "客户区大小":
                                    数字传递 = fatiao.SetClientSize(hwnd, int.Parse(参数八[1]), int.Parse(参数八[2]));
                                    break;
                                case "窗口状态":
                                    switch (参数八[1])
                                    {
                                        case "关闭窗口":
                                            数字传递 = fatiao.SetWindowState(hwnd, 0);
                                            break;
                                        case "置顶窗口":
                                            数字传递 = fatiao.SetWindowState(hwnd, 8);
                                            break;
                                        case "取消置顶":
                                            数字传递 = fatiao.SetWindowState(hwnd, 9);
                                            break;
                                        case "锁定窗口":
                                            数字传递 = fatiao.SetWindowState(hwnd, 10);
                                            break;
                                        case "取消锁定":
                                            数字传递 = fatiao.SetWindowState(hwnd, 11);
                                            break;
                                        case "隐藏窗口":
                                            数字传递 = fatiao.SetWindowState(hwnd, 6);
                                            break;
                                        case "显示窗口":
                                            数字传递 = fatiao.SetWindowState(hwnd, 7);
                                            break;
                                        case "最小化窗口":
                                            数字传递 = fatiao.SetWindowState(hwnd, 2);
                                            break;
                                        case "最大化窗口":
                                            数字传递 = fatiao.SetWindowState(hwnd, 4);
                                            break;
                                    }
                                    break;
                                case "窗口标题":
                                    参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                    右至左 = 参数二[2].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                                    参数六 = 参数二[2].Substring(0, 右至左);//自定义内容
                                    数字传递 = fatiao.SetWindowText(hwnd, 参数六);
                                    break;
                                case "窗口位置":
                                    数字传递 = fatiao.MoveWindow(hwnd, int.Parse(参数八[1]), int.Parse(参数八[2]));
                                    break;
                                case "窗口透明度":
                                    数字传递 = fatiao.SetWindowTransparent(hwnd, int.Parse(参数八[1]) * 25);
                                    break;
                            }
                            if (参数[0] != "")
                            {
                                //添加逻辑(参数[0]);
                                if (数字传递 == 1)
                                {
                                    赋值变量(参数[0], "真");
                                }
                                else
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            break;
                        case "鼠标操作":
                            try
                            {
                                if (winapp.小程序窗口.判断数字(参数[2]) == "整数")
                                {
                                    坐标x1 = int.Parse(参数[2]);
                                }
                                else//不是整数
                                {
                                    坐标x1 = int.Parse(动作.读取ini("变量名", 参数[2], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数[3]) == "整数")
                                {
                                    坐标y1 = int.Parse(参数[3]);
                                }
                                else//不是整数
                                {
                                    坐标y1 = int.Parse(动作.读取ini("变量名", 参数[3], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (参数[0] == "随机移动" || 参数[0] == "轨迹移动")
                                {
                                    if (winapp.小程序窗口.判断数字(参数[4]) == "整数")
                                    {
                                        坐标x2 = int.Parse(参数[4]);
                                    }
                                    else//不是整数
                                    {
                                        坐标x2 = int.Parse(动作.读取ini("变量名", 参数[4], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                    }
                                    if (winapp.小程序窗口.判断数字(参数[5]) == "整数")
                                    {
                                        坐标y2 = int.Parse(参数[5]);
                                    }
                                    else//不是整数
                                    {
                                        坐标y2 = int.Parse(动作.读取ini("变量名", 参数[5], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                    }
                                }
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }

                            if (基于模式 == "前台模式")
                            {
                                switch (参数[0])
                                {
                                    case "绝对移动":
                                        switch (参数[1])
                                        {
                                            case "无":
                                                fatiao.MoveTo(坐标x1, 坐标y1);
                                                break;
                                            case "单击左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                动作.单击左键();
                                                break;
                                            case "双击左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                动作.双击左键();

                                                break;
                                            case "长按左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                动作.长按左键();
                                                break;
                                            case "松开左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                动作.松开左键();
                                                break;
                                            case "单击右键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                动作.单击右键();
                                                break;
                                            case "长按右键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                动作.长按右键();
                                                break;
                                            case "松开右键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                动作.松开右键();
                                                break;
                                        }
                                        break;
                                    case "随机移动":
                                        switch (参数[1])
                                        {
                                            case "无":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));
                                                break;
                                            case "单击左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                动作.单击左键();
                                                break;
                                            case "双击左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                动作.双击左键();
                                                break;
                                            case "长按左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                动作.长按左键();
                                                break;
                                            case "松开左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                动作.松开左键();
                                                break;
                                            case "单击右键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                动作.单击右键();
                                                break;
                                            case "长按右键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                动作.长按右键();
                                                break;
                                            case "松开右键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                动作.松开右键();
                                                break;
                                        }
                                        break;
                                    case "相对移动":
                                        动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标
                                        if (hwnd != 0)
                                        {
                                            fatiao.GetClientRect(hwnd, out x1, out y1, out x2, out y2);
                                            坐标x2 = 坐标.X - (int)x1;
                                            坐标y2 = 坐标.Y - (int)y1;
                                        }
                                        else
                                        {
                                            坐标x2 = 坐标.X;
                                            坐标y2 = 坐标.Y;
                                        }

                                        switch (参数[1])
                                        {
                                            case "无":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);
                                                break;
                                            case "单击左键":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);

                                                动作.单击左键();
                                                break;
                                            case "双击左键":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);

                                                动作.双击左键();
                                                break;
                                            case "长按左键":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);

                                                动作.长按左键();
                                                break;
                                            case "松开左键":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);

                                                动作.松开左键();
                                                break;
                                            case "单击右键":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);

                                                动作.单击右键();
                                                break;
                                            case "长按右键":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);

                                                动作.长按右键();
                                                break;
                                            case "松开右键":
                                                fatiao.MoveTo(坐标x2 + 坐标x1, 坐标y2 + 坐标y1);

                                                动作.松开右键();
                                                break;
                                        }
                                        break;
                                    case "轨迹移动":
                                        坐标x1 = 随机数.Next(坐标x1, 坐标x2);
                                        坐标y1 = 随机数.Next(坐标y1, 坐标y2);
                                        if (hwnd != 0)
                                        {
                                            fatiao.GetClientRect(hwnd, out x1, out y1, out x2, out y2);
                                            坐标x1 += (int)x1;
                                            坐标y1 += (int)y1;
                                        }
                                        switch (参数[1])
                                        {
                                            case "无":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));
                                                break;
                                            case "单击左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                动作.单击左键();
                                                break;
                                            case "双击左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                动作.双击左键();
                                                break;
                                            case "长按左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                动作.长按左键();
                                                break;
                                            case "松开左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                动作.松开左键();
                                                break;
                                            case "单击右键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                动作.单击右键();
                                                break;
                                            case "长按右键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                动作.长按右键();
                                                break;
                                            case "松开右键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                动作.松开右键();
                                                break;
                                        }
                                        break;
                                    case "单击":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                动作.单击左键();
                                                break;
                                            case "右键":
                                                动作.单击右键();
                                                break;
                                        }
                                        break;
                                    case "双击":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                动作.单击左键();
                                                动作.单击左键();
                                                break;
                                            case "右键":
                                                动作.单击右键();

                                                动作.单击右键();
                                                break;
                                        }
                                        break;
                                    case "长按":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                动作.长按左键();
                                                break;
                                            case "右键":
                                                动作.长按右键();
                                                break;
                                        }
                                        break;
                                    case "松开":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                动作.松开左键();
                                                break;
                                            case "右键":
                                                动作.松开右键();
                                                break;
                                        }
                                        break;
                                    case "滚轮单击":
                                        fatiao.MiddleClick();
                                        break;
                                    case "滚轮上滑":
                                        fatiao.WheelUp();
                                        break;
                                    case "滚轮下滑":
                                        fatiao.WheelDown();
                                        break;
                                }
                            }
                            else
                            {
                                switch (参数[0])
                                {
                                    case "绝对移动":
                                        switch (参数[1])
                                        {
                                            case "无":
                                                fatiao.MoveTo(坐标x1, 坐标y1);
                                                break;
                                            case "单击左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                fatiao.LeftClick();
                                                break;
                                            case "双击左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                fatiao.LeftClick();
                                                Delay(随机数.Next(100, 200));
                                                fatiao.LeftClick();
                                                break;
                                            case "长按左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                fatiao.LeftDown();
                                                break;
                                            case "松开左键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                fatiao.LeftUp();
                                                break;
                                            case "单击右键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                fatiao.RightClick();
                                                break;
                                            case "长按右键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                fatiao.RightDown();
                                                break;
                                            case "松开右键":
                                                fatiao.MoveTo(坐标x1, 坐标y1);

                                                fatiao.RightUp();
                                                break;
                                        }
                                        break;
                                    case "随机移动":
                                        switch (参数[1])
                                        {
                                            case "无":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));
                                                break;
                                            case "单击左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                fatiao.LeftClick();
                                                break;
                                            case "双击左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                fatiao.LeftClick();
                                                Delay(随机数.Next(100, 200));
                                                fatiao.LeftClick();
                                                break;
                                            case "长按左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                fatiao.LeftDown();
                                                break;
                                            case "松开左键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                fatiao.LeftUp();
                                                break;
                                            case "单击右键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                fatiao.RightClick();
                                                break;
                                            case "长按右键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                fatiao.RightDown();
                                                break;
                                            case "松开右键":
                                                fatiao.MoveTo(随机数.Next(坐标x1, 坐标x2), 随机数.Next(坐标y1, 坐标y2));

                                                fatiao.RightUp();
                                                break;
                                        }
                                        break;
                                    case "相对移动":
                                        switch (参数[1])
                                        {
                                            case "无":
                                                fatiao.MoveR(坐标x1, 坐标y1);
                                                break;
                                            case "单击左键":
                                                fatiao.MoveR(坐标x1, 坐标y1);

                                                fatiao.LeftClick();
                                                break;
                                            case "双击左键":
                                                fatiao.MoveR(坐标x1, 坐标y1);

                                                fatiao.LeftClick();
                                                Delay(随机数.Next(100, 200));
                                                fatiao.LeftClick();
                                                break;
                                            case "长按左键":
                                                fatiao.MoveR(坐标x1, 坐标y1);

                                                fatiao.LeftDown();
                                                break;
                                            case "松开左键":
                                                fatiao.MoveR(坐标x1, 坐标y1);

                                                fatiao.LeftUp();
                                                break;
                                            case "单击右键":
                                                fatiao.MoveR(坐标x1, 坐标y1);

                                                fatiao.RightClick();
                                                break;
                                            case "长按右键":
                                                fatiao.MoveR(坐标x1, 坐标y1);

                                                fatiao.RightDown();
                                                break;
                                            case "松开右键":
                                                fatiao.MoveR(坐标x1, 坐标y1);

                                                fatiao.RightUp();
                                                break;
                                        }
                                        break;
                                    case "轨迹移动":
                                        坐标x1 = 随机数.Next(坐标x1, 坐标x2);
                                        坐标y1 = 随机数.Next(坐标y1, 坐标y2);
                                        if (hwnd != 0)
                                        {
                                            fatiao.GetClientRect(hwnd, out x1, out y1, out x2, out y2);
                                            坐标x1 += (int)x1;
                                            坐标y1 += (int)y1;
                                        }
                                        switch (参数[1])
                                        {
                                            case "无":

                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));
                                                break;
                                            case "单击左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                fatiao.LeftClick();
                                                break;
                                            case "双击左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                fatiao.LeftClick();
                                                Delay(随机数.Next(100, 200));
                                                fatiao.LeftClick();
                                                break;
                                            case "长按左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                fatiao.LeftDown();
                                                break;
                                            case "松开左键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                fatiao.LeftUp();
                                                break;
                                            case "单击右键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                fatiao.RightClick();
                                                break;
                                            case "长按右键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                fatiao.RightDown();
                                                break;
                                            case "松开右键":
                                                动作类型.轨迹移动(坐标x1, 坐标y1, int.Parse(参数[6]), int.Parse(参数[7]));

                                                fatiao.RightUp();
                                                break;
                                        }
                                        break;
                                    case "单击":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                fatiao.LeftClick();
                                                break;
                                            case "右键":
                                                fatiao.RightClick();
                                                break;
                                        }
                                        break;
                                    case "双击":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                fatiao.LeftClick();
                                                fatiao.LeftClick();
                                                break;
                                            case "右键":
                                                fatiao.RightClick();

                                                fatiao.RightClick();
                                                break;
                                        }
                                        break;
                                    case "长按":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                fatiao.LeftDown();
                                                break;
                                            case "右键":
                                                fatiao.RightDown();
                                                break;
                                        }
                                        break;
                                    case "松开":
                                        switch (参数[1])
                                        {
                                            case "左键":
                                                fatiao.LeftUp();
                                                break;
                                            case "右键":
                                                fatiao.RightUp();
                                                break;
                                        }
                                        break;
                                    case "滚轮单击":
                                        fatiao.MiddleClick();
                                        break;
                                    case "滚轮上滑":
                                        fatiao.WheelUp();
                                        break;
                                    case "滚轮下滑":
                                        fatiao.WheelDown();
                                        break;
                                }
                            }                   

                            break;
                        case "按键操作":
                            if (参数[1].Contains("\""))
                            {
                                参数二 = 参数[1].Split(new string[] { "[" }, StringSplitOptions.None);//分割自定义内容最左端

                                参数[1] = 参数二[1].Substring(0, 参数二[1].Length - 2);//去尾

                                switch (参数[0])
                                {
                                    case "点击":
                                        fatiao.KeyPress(int.Parse(参数[1]));
                                        break;
                                    case "长按":
                                        fatiao.KeyDown(int.Parse(参数[1]));
                                        break;
                                    case "松开":
                                        fatiao.KeyUp(int.Parse(参数[1]));
                                        break;
                                }

                            }
                            else
                            {
                                if (winapp.小程序窗口.判断数字(参数[1]) == "非数字")
                                {
                                    int 整数变量值 = int.Parse(动作.读取ini("变量名", 参数[1], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                    switch (参数[0])
                                    {
                                        case "点击":
                                            fatiao.KeyPress(整数变量值);
                                            break;
                                        case "长按":
                                            fatiao.KeyDown(整数变量值);
                                            break;
                                        case "松开":
                                            fatiao.KeyUp(整数变量值);
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (参数[0])
                                    {
                                        case "点击":
                                            fatiao.KeyPress(int.Parse(参数[1]));
                                            break;
                                        case "长按":
                                            fatiao.KeyDown(int.Parse(参数[1]));
                                            break;
                                        case "松开":
                                            fatiao.KeyUp(int.Parse(参数[1]));
                                            break;
                                    }
                                }
                            }
                            break;
                        case "系统电源":
                            switch (参数一)
                            {
                                case "电脑注销":
                                    动作.注销();
                                    break;
                                case "电脑关机":
                                    动作.关机();
                                    break;
                                case "电脑重启":
                                    动作.重启();
                                    break;
                            }
                            break;
                        case "蜂鸣提醒":
                            动作类型.蜂鸣提醒(int.Parse(参数一));
                            break;
                        case "下载文件":
                            参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//定位保存目录名最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//保存目录
                            参数七 = 参数二[1].Remove(0, 右至左 + 2);//是否覆盖
                            string 网址 = 参数[1].Substring(1, 参数[1].Length - 2);//定位网址
                            string 返回下载结果 = "";

                            if (!Directory.Exists(winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\素材库"))//文档不存在就创建
                            {
                                Directory.CreateDirectory(winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\素材库");
                            }
                            if (!File.Exists(参数六))//不存在
                            {
                                ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                                {
                                    返回下载结果 = 动作.下载文件(网址, 参数六);
                                }), null);

                                while (返回下载结果 == "")
                                {
                                    Delay(100);
                                }
                                if (返回下载结果 == "真")
                                {
                                    if (参数[0] != "")
                                    {
                                        //添加逻辑(参数[0]);
                                        赋值变量(参数[0], "真");
                                    }
                                }
                                else
                                {
                                    if (参数[0] != "")
                                    {
                                        //添加逻辑(参数[0]);
                                        赋值变量(参数[0], "假");
                                    }
                                }
                            }
                            else //文件已存在
                            {
                                if (参数七 == "覆盖文件")
                                {
                                    File.Delete(参数六);
                                    ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                                    {
                                        返回下载结果 = 动作.下载文件(网址, 参数六);
                                    }), null);

                                    while (返回下载结果 == "")
                                    {
                                        Delay(100);
                                    }
                                    if (返回下载结果 == "真")
                                    {
                                        if (参数[0] != "")
                                        {
                                            //添加逻辑(参数[0]);
                                            赋值变量(参数[0], "真");
                                        }
                                    }
                                    else
                                    {
                                        if (参数[0] != "")
                                        {
                                            //添加逻辑(参数[0]);
                                            赋值变量(参数[0], "假");
                                        }
                                    }
                                }
                            }
                            break;
                        case "写到剪贴板":
                            if (参数一.Contains("\""))
                            {
                                参数二 = 参数一.Split(new string[] { "\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                参数一 = 参数二[1].Substring(0, 参数二[1].Length);//去尾
                            }
                            else
                            {
                                参数一 = 引用变量(参数一);
                            }

                            Clipboard.SetDataObject(参数一);
                            break;
                        case "发送文本":
                            if (参数一.Contains("\",\""))
                            {
                                参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                参数七 = 参数二[0].Substring(1);//去头
                                参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                            }
                            else
                            {
                                参数二 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割自定义内容最左端
                                参数七 = 参数二[0].Substring(1, 参数二[0].Length - 2);//掐头去尾
                                参数六 = 引用变量(参数二[1]);
                            }

                            fatiao.SendString(fatiao.FindWindow("", 参数七), 参数六);
                            break;
                        case "打开网址":
                            if (参数一.Contains(",\""))
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                参数一 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                            }
                            else
                            {
                                参数二 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割自定义内容最左端
                                参数一 = 引用变量(参数二[1]);
                            }

                            try
                            {
                                Process.Start(参数一);
                                if (参数[0] != "")
                                {
                                    赋值变量(参数[0], "真");
                                }
                            }
                            catch (Exception)
                            {
                                if (参数[0] != "")
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            break;
                        case "消息提示":

                            if (参数一.Contains(",\""))
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                参数七 = 参数二[1].Substring(参数二[1].Length - 4, 4);//窗口类型
                            }
                            else
                            {
                                参数六 = 引用变量(参数[2]);
                                参数七 = 参数[3];
                            }

                            switch (参数[1])
                            {
                                case "弹幕消息":
                                    弹幕内容.label1.Text = 参数六;
                                    弹幕内容.Show();
                                    break;
                                case "弹窗消息":
                                    DialogResult 弹窗判断;
                                    switch (参数七)
                                    {
                                        case "信息窗口":
                                            弹窗判断 = MessageBox.Show(参数六, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);//信息
                                            break;
                                        case "警告窗口":
                                            弹窗判断 = MessageBox.Show(参数六, "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);//警告
                                            break;
                                        default:
                                            弹窗判断 = MessageBox.Show(参数六, "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);//报错
                                            break;
                                    }
                                    if (弹窗判断 == DialogResult.Yes)
                                    {
                                        if (参数[0] != "")
                                        {
                                            //添加逻辑(参数[0]);
                                            赋值变量(参数[0], "真");
                                        }
                                    }
                                    else if (弹窗判断 == DialogResult.No)
                                    {
                                        if (参数[0] != "")
                                        {
                                            //添加逻辑(参数[0]);
                                            赋值变量(参数[0], "假");
                                        }
                                    }
                                    break;
                                case "通知消息":
                                    switch (参数七)
                                    {
                                        case "信息窗口":
                                            winapp.小程序窗口.notifyIcon1.ShowBalloonTip(1000, "", 参数六, ToolTipIcon.Info);//信息
                                            break;
                                        case "警告窗口":
                                            winapp.小程序窗口.notifyIcon1.ShowBalloonTip(1000, "", 参数六, ToolTipIcon.Warning);//警告
                                            break;
                                        default:
                                            winapp.小程序窗口.notifyIcon1.ShowBalloonTip(1000, "", 参数六, ToolTipIcon.Error);//报错
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case "﹁循环始":
                            if (参数一 != "")
                            {
                                if (winapp.小程序窗口.判断数字(参数一) == "非数字")
                                {
                                    if (引用变量(参数一) != "空")
                                    {
                                        循环次数 = int.Parse(引用变量(参数一));
                                    }
                                }
                                else
                                {
                                    循环次数 = int.Parse(参数一);
                                }
                            }
                            if (循环次数 == 0)
                            {
                                循环次数 = -1000;
                            }
                            break;
                        case "﹂循环末;":
                            if (循环次数 == -1000)
                            {
                                Delay(1);
                                遇到循环始 = 0;
                                当前行--;//运行行数递增-1，回到当前行
                                while (true)
                                {
                                    当前行--;//运行行数递增-1
                                    每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                                    动作类 = 每行内容.Split('(');//分割命令名和参数
                                    switch (动作类[0])
                                    {
                                        case "﹁循环始":
                                            遇到循环始--;
                                            break;

                                        case "﹂循环末;":
                                            遇到循环始++;
                                            break;
                                    }
                                    if (遇到循环始 < 0) //运行行数小于最大行数时
                                    {
                                        当前行++;
                                        break;
                                    }
                                }
                            }
                            if (循环次数 > 1)
                            {
                                Delay(1);
                                遇到循环始 = 0;
                                当前行--;//运行行数递增-1，回到当前行
                                while (true)
                                {
                                    当前行--;//运行行数递增-1
                                    每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                                    动作类 = 每行内容.Split('(');//分割命令名和参数
                                    switch (动作类[0])
                                    {
                                        case "﹁循环始":
                                            遇到循环始--;
                                            break;

                                        case "﹂循环末;":
                                            遇到循环始++;
                                            break;
                                    }
                                    if (遇到循环始 < 0) //运行行数小于最大行数时
                                    {
                                        循环次数--;
                                        当前行++;
                                        break;
                                    }
                                }
                            }
                            break;
                        case "判断颜色":
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                            参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容
                            参数四 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割出自定义内容之后的参数
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果


                            if (winapp.小程序窗口.判断数字(参数[1]) == "整数")
                            {
                                坐标x1 = int.Parse(参数[1]);
                            }
                            else//不是整数
                            {
                                坐标x1 = int.Parse(动作.读取ini("变量名", 参数[1], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                            }
                            if (winapp.小程序窗口.判断数字(参数[2]) == "整数")
                            {
                                坐标y1 = int.Parse(参数[2]);
                            }
                            else//不是整数
                            {
                                坐标y1 = int.Parse(动作.读取ini("变量名", 参数[2], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                            }

                            图色 = 100;
                            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                            {
                                /*TSPlugLib.TSPlugInterFace 判色 = new TSPlugLib.TSPlugInterFace();//新的dll
                                判色.BindWindow(判色.GetSpecialWindow(0), "normal", "windows", "windows", 0);

                                if (hwnd != 0)
                                {
                                    switch (基于模式)
                                    {
                                        case "前台模式":
                                            判色.BindWindow(hwnd, "normal", "normal", "normal", 0);
                                            break;
                                        case "前台模式二":
                                            判色.BindWindow(hwnd, "normal", "normal", "normal", 1);
                                            break;
                                        case "前台模式三":
                                            判色.BindWindow(hwnd, "normal", "normal", "normal", 101);
                                            break;
                                        case "伪后台模式":
                                            判色.BindWindow(hwnd, "normal", "windows", "windows", 0);
                                            break;
                                        case "后台模式":
                                            判色.BindWindow(hwnd, "gdi", "windows", "windows", 0);
                                            break;
                                        case "后台模式二":
                                            判色.BindWindow(hwnd, "gdi", "windows", "windows", 1);
                                            break;
                                        case "后台模式三":
                                            判色.BindWindow(hwnd, "gdi", "windows", "windows", 101);
                                            break;
                                        case "后台模式四":
                                            判色.BindWindow(hwnd, "dx", "windows", "windows", 0);
                                            break;
                                        case "后台模式五":
                                            判色.BindWindow(hwnd, "dx", "windows", "windows", 1);
                                            break;
                                        case "后台模式六":
                                            判色.BindWindow(hwnd, "dx", "windows", "windows", 101);
                                            break;
                                    }
                                }*/

                                图色 = fatiao.CmpColor(坐标x1, 坐标y1, 参数六, Convert.ToDouble(参数四[0]) / 10);
                                //判色.UnBindWindow();
                            }), null);

                            while (图色 == 100)
                            {
                                Delay(1);
                            }

                            if (参数[0] != "")//变量未空
                            {
                                if (图色 == 0)//判断为真
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "真");
                                }
                                else//判断为假
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "假");
                                }
                            }
                            break;
                        case "查找颜色":
                            try
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义询问内容
                                参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容
                                参数四 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割出自定义内容之后的参数

                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                                参数九 = 参数四[4];



                                if (winapp.小程序窗口.判断数字(参数四[0]) == "整数")
                                {
                                    坐标x1 = int.Parse(参数四[0]);
                                }
                                else//不是整数
                                {
                                    坐标x1 = int.Parse(动作.读取ini("变量名", 参数四[0], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数四[1]) == "整数")
                                {
                                    坐标y1 = int.Parse(参数四[1]);
                                }
                                else//不是整数
                                {
                                    坐标y1 = int.Parse(动作.读取ini("变量名", 参数四[1], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数四[2]) == "整数")
                                {
                                    坐标x2 = int.Parse(参数四[2]);
                                }
                                else//不是整数
                                {
                                    坐标x2 = int.Parse(动作.读取ini("变量名", 参数四[2], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数四[3]) == "整数")
                                {
                                    坐标y2 = int.Parse(参数四[3]);
                                }
                                else//不是整数
                                {
                                    坐标y2 = int.Parse(动作.读取ini("变量名", 参数四[3], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                            }
                            catch
                            {
                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果



                                参数六 = 引用变量(参数[3]);

                                参数九 = 参数[8];



                                if (winapp.小程序窗口.判断数字(参数[4]) == "整数")
                                {
                                    坐标x1 = int.Parse(参数[4]);
                                }
                                else//不是整数
                                {
                                    坐标x1 = int.Parse(动作.读取ini("变量名", 参数[4], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数[5]) == "整数")
                                {
                                    坐标y1 = int.Parse(参数[5]);
                                }
                                else//不是整数
                                {
                                    坐标y1 = int.Parse(动作.读取ini("变量名", 参数[5], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数[6]) == "整数")
                                {
                                    坐标x2 = int.Parse(参数[6]);
                                }
                                else//不是整数
                                {
                                    坐标x2 = int.Parse(动作.读取ini("变量名", 参数[6], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数[7]) == "整数")
                                {
                                    坐标y2 = int.Parse(参数[7]);
                                }
                                else//不是整数
                                {
                                    坐标y2 = int.Parse(动作.读取ini("变量名", 参数[7], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                            }

                            图色 = 100;
                            坐标一 = -1;
                            坐标二 = -1;
                            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                            {
                                /*TSPlugLib.TSPlugInterFace 找色 = new TSPlugLib.TSPlugInterFace();//新的dll
                                找色.BindWindow(找色.GetSpecialWindow(0), "normal", "windows", "windows", 0);

                                if (hwnd != 0)
                                {
                                    switch (基于模式)
                                    {
                                        case "前台模式":
                                            找色.BindWindow(hwnd, "normal", "normal", "normal", 0);
                                            break;
                                        case "前台模式二":
                                            找色.BindWindow(hwnd, "normal", "normal", "normal", 1);
                                            break;
                                        case "前台模式三":
                                            找色.BindWindow(hwnd, "normal", "normal", "normal", 101);
                                            break;
                                        case "伪后台模式":
                                            找色.BindWindow(hwnd, "normal", "windows", "windows", 0);
                                            break;
                                        case "后台模式":
                                            找色.BindWindow(hwnd, "gdi", "windows", "windows", 0);
                                            break;
                                        case "后台模式二":
                                            找色.BindWindow(hwnd, "gdi", "windows", "windows", 1);
                                            break;
                                        case "后台模式三":
                                            找色.BindWindow(hwnd, "gdi", "windows", "windows", 101);
                                            break;
                                        case "后台模式四":
                                            找色.BindWindow(hwnd, "dx", "windows", "windows", 0);
                                            break;
                                        case "后台模式五":
                                            找色.BindWindow(hwnd, "dx", "windows", "windows", 1);
                                            break;
                                        case "后台模式六":
                                            找色.BindWindow(hwnd, "dx", "windows", "windows", 101);
                                            break;
                                    }
                                }*/

                                图色 = fatiao.FindColor(坐标x1, 坐标y1, 坐标x2, 坐标y2, 参数六, Convert.ToDouble(参数九) / 10, 0, out 坐标一, out 坐标二);
                                //找色.UnBindWindow();
                            }), null);

                            while (图色 == 100)
                            {
                                Delay(1);
                            }

                            if (参数[0] != "" && 参数[1] != "" && 参数[2] != "")//变量未空
                            {
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                    赋值变量(参数[1], 坐标一.ToString());
                                    赋值变量(参数[2], 坐标二.ToString());

                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] == "" && 参数[1] != "" && 参数[2] != "")//坐标整数变量未空
                            {
                                //添加整数(参数[1]);
                                //添加整数(参数[2]);
                                if (图色 != -1)//判断为真                                        if (图色!=-1)//判断为真
                                {
                                    赋值变量(参数[1], 坐标一.ToString());
                                    赋值变量(参数[2], 坐标二.ToString());

                                }
                            }
                            else if (参数[0] != "" && 参数[1] == "" && 参数[2] != "")//坐标整数变量未空
                            {
                                //添加逻辑(参数[0]);
                                //添加整数(参数[2]);
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                    赋值变量(参数[2], 坐标二.ToString());

                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] != "" && 参数[1] != "" && 参数[2] == "")//坐标整数变量未空
                            {
                                //添加逻辑(参数[0]);
                                //添加整数(参数[1]);
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                    赋值变量(参数[1], 坐标一.ToString());

                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] != "" && 参数[1] == "" && 参数[2] == "")//坐标整数变量未空
                            {
                                //添加逻辑(参数[0]);
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] == "" && 参数[1] != "" && 参数[2] == "")//坐标整数变量未空
                            {
                                //添加整数(参数[1]);
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[1], 坐标一.ToString());

                                }
                            }
                            else if (参数[0] == "" && 参数[1] == "" && 参数[2] != "")//坐标整数变量未空
                            {
                                //添加整数(参数[2]);
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[2], 坐标二.ToString());
                                }
                            }

                            break;

                        case "查找图片":
                            try
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容
                                参数四 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割出自定义内容之后的参数
                                参数九 = 参数四[4];
                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果
                                图片目录 = winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\素材库\" + 参数六.Replace("|", "|" + winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\素材库\"); //给每张图加上目录


                                if (winapp.小程序窗口.判断数字(参数四[0]) == "整数")
                                {
                                    坐标x1 = int.Parse(参数四[0]);
                                }
                                else//不是整数
                                {
                                    坐标x1 = int.Parse(动作.读取ini("变量名", 参数四[0], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数四[1]) == "整数")
                                {
                                    坐标y1 = int.Parse(参数四[1]);
                                }
                                else//不是整数
                                {
                                    坐标y1 = int.Parse(动作.读取ini("变量名", 参数四[1], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数四[2]) == "整数")
                                {
                                    坐标x2 = int.Parse(参数四[2]);
                                }
                                else
                                {
                                    坐标x2 = int.Parse(动作.读取ini("变量名", 参数四[2], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数四[3]) == "整数")
                                {
                                    坐标y2 = int.Parse(参数四[3]);
                                }
                                else
                                {
                                    坐标y2 = int.Parse(动作.读取ini("变量名", 参数四[3], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                            }
                            catch
                            {
                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果
                                参数九 = 参数[8];

                                图片目录 = winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\素材库\" + 引用变量(参数[3]).Replace("|", "|" + winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\素材库\"); //给每张图加上目录

                                if (winapp.小程序窗口.判断数字(参数[4]) == "整数")
                                {
                                    坐标x1 = int.Parse(参数[4]);
                                }
                                else//不是整数
                                {
                                    坐标x1 = int.Parse(动作.读取ini("变量名", 参数[4], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数[5]) == "整数")
                                {
                                    坐标y1 = int.Parse(参数[5]);
                                }
                                else//不是整数
                                {
                                    坐标y1 = int.Parse(动作.读取ini("变量名", 参数[5], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数[6]) == "整数")
                                {
                                    坐标x2 = int.Parse(参数[6]);
                                }
                                else//不是整数
                                {
                                    坐标x2 = int.Parse(动作.读取ini("变量名", 参数[6], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                                if (winapp.小程序窗口.判断数字(参数[7]) == "整数")
                                {
                                    坐标y2 = int.Parse(参数[7]);
                                }
                                else//不是整数
                                {
                                    坐标y2 = int.Parse(动作.读取ini("变量名", 参数[7], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                                }
                            }

                            图色 = 100;
                            坐标一 = -1;
                            坐标二 = -1;
                            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                            {
                               /* TSPlugLib.TSPlugInterFace 找图 = new TSPlugLib.TSPlugInterFace();//新的dll
                                找图.BindWindow(找图.GetSpecialWindow(0), "normal", "windows", "windows", 0);

                                if (hwnd != 0)
                                {
                                    switch (基于模式)
                                    {
                                        case "前台模式":
                                            找图.BindWindow(hwnd, "normal", "normal", "normal", 0);
                                            break;
                                        case "前台模式二":
                                            找图.BindWindow(hwnd, "normal", "normal", "normal", 1);
                                            break;
                                        case "前台模式三":
                                            找图.BindWindow(hwnd, "normal", "normal", "normal", 101);
                                            break;
                                        case "伪后台模式":
                                            找图.BindWindow(hwnd, "normal", "windows", "windows", 0);
                                            break;
                                        case "后台模式":
                                            找图.BindWindow(hwnd, "gdi", "windows", "windows", 0);
                                            break;
                                        case "后台模式二":
                                            找图.BindWindow(hwnd, "gdi", "windows", "windows", 1);
                                            break;
                                        case "后台模式三":
                                            找图.BindWindow(hwnd, "gdi", "windows", "windows", 101);
                                            break;
                                        case "后台模式四":
                                            找图.BindWindow(hwnd, "dx", "windows", "windows", 0);
                                            break;
                                        case "后台模式五":
                                            找图.BindWindow(hwnd, "dx", "windows", "windows", 1);
                                            break;
                                        case "后台模式六":
                                            找图.BindWindow(hwnd, "dx", "windows", "windows", 101);
                                            break;
                                    }
                                }*/

                                图色 = fatiao.FindPic(坐标x1, 坐标y1, 坐标x2, 坐标y2, 图片目录, "000000", Convert.ToDouble(参数九) / 10, 0, out 坐标一, out 坐标二);
                                //找图.UnBindWindow();
                            }), null);

                            while (图色 == 100)
                            {
                                Delay(1);
                            }

                            if (参数[0] != "" && 参数[1] != "" && 参数[2] != "")//变量未空
                            {
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                    赋值变量(参数[1], 坐标一.ToString());
                                    赋值变量(参数[2], 坐标二.ToString());
                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] == "" && 参数[1] != "" && 参数[2] != "")//坐标整数变量未空
                            {
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[1], 坐标一.ToString());
                                    赋值变量(参数[2], 坐标二.ToString());
                                }
                            }
                            else if (参数[0] != "" && 参数[1] == "" && 参数[2] != "")//坐标整数变量未空
                            {
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                    赋值变量(参数[2], 坐标二.ToString());
                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] != "" && 参数[1] != "" && 参数[2] == "")//坐标整数变量未空
                            {
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                    赋值变量(参数[1], 坐标一.ToString());
                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] != "" && 参数[1] == "" && 参数[2] == "")//坐标整数变量未空
                            {
                                //添加逻辑(参数[0]);
                                if (图色 != -1)//判断为真
                                {
                                    赋值变量(参数[0], "真");

                                }
                                else//判断为假
                                {
                                    赋值变量(参数[0], "假");
                                }
                            }
                            else if (参数[0] == "" && 参数[1] != "" && 参数[2] == "")//坐标整数变量未空
                            {
                                //添加整数(参数[1]);
                                if (图色 != -1)//判断为真
                                {
                                    //添加整数(参数[1]);
                                    赋值变量(参数[1], 坐标一.ToString());
                                }
                            }
                            else if (参数[0] == "" && 参数[1] == "" && 参数[2] != "")//坐标整数变量未空
                            {
                                //添加整数(参数[2]);
                                if (图色 != -1)//判断为真
                                {
                                    //添加整数(参数[2]);
                                    赋值变量(参数[2], 坐标二.ToString());
                                }
                            }

                            break;

                        case "判断画面":

                            if (winapp.小程序窗口.判断数字(参数[2]) == "整数")
                            {
                                坐标x1 = int.Parse(参数[2]);
                            }
                            else//不是整数
                            {
                                坐标x1 = int.Parse(动作.读取ini("变量名", 参数[2], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                            }
                            if (winapp.小程序窗口.判断数字(参数[3]) == "整数")
                            {
                                坐标y1 = int.Parse(参数[3]);
                            }
                            else//不是整数
                            {
                                坐标y1 = int.Parse(动作.读取ini("变量名", 参数[3], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                            }
                            if (winapp.小程序窗口.判断数字(参数[4]) == "整数")
                            {
                                坐标x2 = int.Parse(参数[4]);
                            }
                            else//不是整数
                            {
                                坐标x2 = int.Parse(动作.读取ini("变量名", 参数[4], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                            }
                            if (winapp.小程序窗口.判断数字(参数[5]) == "整数")
                            {
                                坐标y2 = int.Parse(参数[5]);
                            }
                            else//不是整数
                            {
                                坐标y2 = int.Parse(动作.读取ini("变量名", 参数[4], "-1", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini"));
                            }

                            动作.获取窗口大小(fatiao.GetSpecialWindow(0), out int 宽, out int 高);
                            if (hwnd != 0)
                            {
                                动作.获取窗口大小(hwnd, out 宽, out 高);
                            }

                            if (!基于模式.Contains("前台") && !基于模式.Contains("伪后台")) //是否是后台
                            {
                                fatiao.GetWindowRect(hwnd, out object xx, out object yy, out object xx1, out object yy2);//获取窗口在屏幕的位置

                                //窗口坐标转换成屏幕坐标
                                坐标x1 += (int)xx;
                                坐标x2 += (int)xx;
                                坐标y1 += (int)yy;
                                坐标y2 += (int)yy;

                                fatiao.GetClientRect(hwnd, out xx, out yy, out xx1, out yy2);//获取客户区在屏幕的位置

                                //屏幕坐标转换成客户区坐标
                                坐标x1 -= (int)xx;
                                坐标x2 -= (int)xx;
                                坐标y1 -= (int)yy;
                                坐标y2 -= (int)yy;
                            }

                            fatiao.Capture(坐标x1, 坐标y1, 坐标x2, 坐标y2, Environment.CurrentDirectory + @"\screen.bmp");

                            for (int 循环判断 = 0; 循环判断 < int.Parse(参数[1]) * 10; 循环判断++)
                            {
                                Delay(100);
                                if (fatiao.FindPic(0, 0, 宽, 高, Environment.CurrentDirectory + @"\screen.bmp", "000000", Convert.ToDouble(参数[6]) / 10, 0, out object X, out object Y) == -1)//活动
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "真");
                                    break;
                                }
                                else
                                {
                                    //添加逻辑(参数[0]);
                                    赋值变量(参数[0], "假");
                                }
                            }

                            动作.删除判断画面图片();

                            break;

                        case "计算时间差":
                            if (参数[2].Contains("\""))
                            {
                                参数六 = 参数[2].Substring(1, 参数[2].Length - 2);//掐头去尾                                      
                            }
                            else
                            {
                                参数六 = 引用变量(参数[2]);
                            }

                            if (参数[3].Contains("\""))
                            {
                                参数七 = 参数[3].Substring(1, 参数[3].Length - 2);//掐头去尾
                            }
                            else
                            {
                                参数七 = 引用变量(参数[3]);
                            }

                            switch (参数[1])
                            {
                                case "年月日时分秒":
                                    当前时间 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                    比较时间 = (DateTime.ParseExact(参数六, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) - DateTime.ParseExact(参数七, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)).TotalSeconds;

                                    break;
                                case "年月日":
                                    当前时间 = DateTime.Now.ToString("yyyy/MM/dd");
                                    比较时间 = (DateTime.ParseExact(参数六, "yyyy/MM/dd", CultureInfo.InvariantCulture) - DateTime.ParseExact(参数七, "yyyy/MM/dd", CultureInfo.InvariantCulture)).TotalSeconds;

                                    break;
                                case "时分秒":
                                    当前时间 = DateTime.Now.ToString("HH:mm:ss");
                                    比较时间 = (DateTime.ParseExact(参数六, "HH:mm:ss", CultureInfo.InvariantCulture) - DateTime.ParseExact(参数七, "HH:mm:ss", CultureInfo.InvariantCulture)).TotalSeconds;
                                    break;
                                case "仅时钟":
                                    当前时间 = DateTime.Now.ToString("HH");
                                    比较时间 = (DateTime.ParseExact(参数六, "HH", CultureInfo.InvariantCulture) - DateTime.ParseExact(参数七, "HH", CultureInfo.InvariantCulture)).TotalSeconds;
                                    break;
                                case "仅分钟":
                                    当前时间 = DateTime.Now.ToString("mm");
                                    比较时间 = (DateTime.ParseExact(参数六, "mm", CultureInfo.InvariantCulture) - DateTime.ParseExact(参数七, "mm", CultureInfo.InvariantCulture)).TotalSeconds;
                                    break;
                                case "仅秒钟":
                                    当前时间 = DateTime.Now.ToString("ss");
                                    比较时间 = (DateTime.ParseExact(参数六, "ss", CultureInfo.InvariantCulture) - DateTime.ParseExact(参数七, "ss", CultureInfo.InvariantCulture)).TotalSeconds;

                                    break;
                            }

                            赋值变量(参数[0], 比较时间.ToString());

                            break;

                        case "判断窗口":
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                            参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容

                            if (参数[0] != "")//变量未空
                            {
                                //添加逻辑(参数[0]);
                                hwnd = fatiao.FindWindow("", 参数六);
                                switch (参数七)
                                {
                                    case "是否存在":
                                        if (fatiao.GetWindowState(hwnd, 0) == 1)//是
                                        {
                                            赋值变量(参数[0], "真");
                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                    case "是否激活":
                                        if (fatiao.GetWindowState(hwnd, 1) == 1)//是
                                        {
                                            赋值变量(参数[0], "真");
                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                    case "是否最小化":
                                        if (fatiao.GetWindowState(hwnd, 3) == 1)//是
                                        {
                                            赋值变量(参数[0], "真");
                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                    case "是否最大化":
                                        if (fatiao.GetWindowState(hwnd, 4) == 1)//是
                                        {
                                            赋值变量(参数[0], "真");
                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                    case "是否置顶":
                                        if (fatiao.GetWindowState(hwnd, 5) == 1)//是
                                        {
                                            赋值变量(参数[0], "真");
                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                    case "是否无响应":
                                        if (fatiao.GetWindowState(hwnd, 6) == 1)//是
                                        {
                                            赋值变量(参数[0], "真");
                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                }
                            }
                            break;

                        case "判断按键":
                            参数二 = 参数一.Split(new string[] { "[" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("]\"");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                            if (参数[0] != "")//变量未空
                            {
                                switch (参数[1])
                                {
                                    case "是否点击":
                                        if (动作.判断按键(int.Parse(参数六)))//是
                                        {
                                            赋值变量(参数[0], "真");

                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                    case "是否长按":
                                        if (动作.判断按键(int.Parse(参数六)))//当按住判断是否长按
                                        {
                                            for (int 循环1 = 0; 循环1 < 80; 循环1++)//长按一秒
                                            {
                                                Delay(1);

                                                if (!动作.判断按键(int.Parse(参数六)))//松开了长按
                                                {
                                                    赋值变量(参数[0], "假");
                                                    break;//未长按
                                                }
                                            }
                                        }
                                        if (动作.判断按键(int.Parse(参数六)))//是
                                        {
                                            赋值变量(参数[0], "真");

                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                    case "是否松开":
                                        if (!动作.判断按键(int.Parse(参数六)))//是
                                        {
                                            赋值变量(参数[0], "真");

                                        }
                                        else
                                        {
                                            赋值变量(参数[0], "假");
                                        }
                                        break;
                                }
                            }
                            break;

                        case "﹁如果始":
                            if (参数一.Contains("不等于")) //是否包含“不等于”符号
                            {
                                参数三 = 参数一.Split(new string[] { "不等于" }, StringSplitOptions.None);//关系符分割

                                if (参数三[0].Contains("\""))
                                {
                                    参数三[0] = 参数三[0].Substring(1, 参数三[0].Length - 2);//掐头去尾
                                    返回变量 = 参数三[0];
                                }
                                else
                                {
                                    返回变量 = 动作.读取ini("变量名", 参数三[0], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                }

                                if (参数三[1].Contains("\""))
                                {
                                    参数三[1] = 参数三[1].Substring(1, 参数三[1].Length - 2);//掐头去尾
                                    返回变量二 = 参数三[1];
                                }
                                else
                                {
                                    返回变量二 = 动作.读取ini("变量名", 参数三[1], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                }
                                if (返回变量 == 返回变量二)//不符合就跳过事件
                                {
                                    int 判断计数 = 1;
                                    while (判断计数 > 0) while (判断计数 > 0)
                                        {
                                            每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                                            动作类 = 每行内容.Split('(');//分割命令名和参数
                                            当前行++;//运行行数递增+1
                                            switch (动作类[0])
                                            {
                                                case "﹁如果始":
                                                    判断计数++;
                                                    break;

                                                case "﹂如果末;":
                                                    判断计数--;
                                                    break;
                                            }
                                            if (当前行 >= 最大行)
                                            {
                                                break;
                                            }
                                        }

                                }
                            }
                            else if (参数一.Contains("等于") == true) //是否包含“等于”符号
                            {
                                参数三 = 参数一.Split(new string[] { "等于" }, StringSplitOptions.None);//关系符分割
                                if (参数三[0].Contains("\""))
                                {
                                    参数三[0] = 参数三[0].Substring(1, 参数三[0].Length - 2);//掐头去尾
                                    返回变量 = 参数三[0];
                                }
                                else
                                {
                                    返回变量 = 动作.读取ini("变量名", 参数三[0], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                }

                                if (参数三[1].Contains("\""))
                                {
                                    参数三[1] = 参数三[1].Substring(1, 参数三[1].Length - 2);//掐头去尾
                                    返回变量二 = 参数三[1];
                                }
                                else
                                {
                                    返回变量二 = 动作.读取ini("变量名", 参数三[1], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                }

                                if (返回变量 != 返回变量二)//不符合就跳过事件
                                {
                                    int 判断计数 = 1;
                                    while (判断计数 > 0)
                                    {
                                        每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                                        动作类 = 每行内容.Split('(');//分割命令名和参数
                                        当前行++;//运行行数递增+1
                                        switch (动作类[0])
                                        {
                                            case "﹁如果始":
                                                判断计数++;
                                                break;

                                            case "﹂如果末;":
                                                判断计数--;
                                                break;
                                        }
                                        if (当前行 >= 最大行)
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (参数一.Contains("大于") == true)//是否包含“大于”符号
                                {
                                    参数三 = 参数一.Split(new string[] { "大于" }, StringSplitOptions.None);//关系符分割
                                    if (参数三[0].Contains("\""))
                                    {
                                        参数三[0] = 参数三[0].Substring(1, 参数三[0].Length - 2);//掐头去尾
                                        返回变量 = 参数三[0];
                                    }
                                    else
                                    {
                                        返回变量 = 动作.读取ini("变量名", 参数三[0], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                    }

                                    if (参数三[1].Contains("\""))
                                    {
                                        参数三[1] = 参数三[1].Substring(1, 参数三[1].Length - 2);//掐头去尾
                                        返回变量二 = 参数三[1];
                                    }
                                    else
                                    {
                                        返回变量二 = 动作.读取ini("变量名", 参数三[1], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                    }


                                    if (int.Parse(返回变量) < int.Parse(返回变量二))//不符合就跳过事件
                                    {
                                        int 判断计数 = 1;
                                        while (判断计数 > 0)
                                        {
                                            if (当前行 >= 最大行) //运行行数小于最大行数时
                                            {
                                                break;
                                            }
                                            每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                                            动作类 = 每行内容.Split('(');//分割命令名和参数
                                            当前行++;//运行行数递增+1
                                            switch (动作类[0])
                                            {
                                                case "﹁如果始":
                                                    判断计数++;
                                                    break;

                                                case "﹂如果末;":
                                                    判断计数--;
                                                    break;
                                            }
                                        }
                                    }

                                }

                                else//是否包含“小于”符号
                                {
                                    参数三 = 参数一.Split(new string[] { "小于" }, StringSplitOptions.None);//关系符分割
                                    if (参数三[0].Contains("\""))
                                    {
                                        参数三[0] = 参数三[0].Substring(1, 参数三[0].Length - 2);//掐头去尾
                                        返回变量 = 参数三[0];
                                    }
                                    else
                                    {
                                        返回变量 = 动作.读取ini("变量名", 参数三[0], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                    }

                                    if (参数三[1].Contains("\""))
                                    {
                                        参数三[1] = 参数三[1].Substring(1, 参数三[1].Length - 2);//掐头去尾
                                        返回变量二 = 参数三[1];
                                    }
                                    else
                                    {
                                        返回变量二 = 动作.读取ini("变量名", 参数三[1], "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
                                    }

                                    if (int.Parse(返回变量) > int.Parse(返回变量二))//不符合就跳过事件
                                    {
                                        int 判断计数 = 1;
                                        while (判断计数 > 0)
                                        {
                                            if (当前行 >= 最大行) //运行行数小于最大行数时
                                            {
                                                break;
                                            }
                                            每行内容 = winapp.小程序窗口.listBox6.Items[当前行].ToString();//读取运行行内容
                                            动作类 = 每行内容.Split('(');//分割命令名和参数
                                            当前行++;//运行行数递增+1
                                            switch (动作类[0])
                                            {
                                                case "﹁如果始":
                                                    判断计数++;
                                                    break;

                                                case "﹂如果末;":
                                                    判断计数--;
                                                    break;
                                            }
                                        }

                                    }

                                }
                            }

                            break;
                        case "整数运算":
                            if (参数一.Contains("-=") == true) //是否包含-=复合运算符号
                            {
                                参数四 = 参数一.Split(new string[] { "-=" }, StringSplitOptions.None);//关系符分割

                                参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                int 重新赋值 = int.Parse(参数六) - int.Parse(参数四[1]);
                                赋值变量(参数四[0], 重新赋值.ToString());

                            }
                            else
                            {
                                if (参数一.Contains("++") == true) //是否包含“自增”符号
                                {
                                    参数四 = 参数一.Split(new string[] { "++" }, StringSplitOptions.None);//关系符分割


                                    参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                    int 重新赋值 = int.Parse(参数六) + 1;

                                    赋值变量(参数四[0], 重新赋值.ToString());
                                }
                                else
                                {
                                    if (参数一.Contains("--") == true) //是否包含“自减”符号
                                    {
                                        参数四 = 参数一.Split(new string[] { "--" }, StringSplitOptions.None);//关系符分割
                                        参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                        int 重新赋值 = int.Parse(参数六) - 1;

                                        赋值变量(参数四[0], 重新赋值.ToString());
                                    }
                                    else
                                    {
                                        if (参数一.Contains("+=") == true) //是否包含+=复合运算符号
                                        {
                                            参数四 = 参数一.Split(new string[] { "+=" }, StringSplitOptions.None);//关系符分割
                                            参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                            int 重新赋值 = int.Parse(参数六) + int.Parse(参数四[1]);

                                            赋值变量(参数四[0], 重新赋值.ToString());
                                        }
                                        else
                                        {
                                            if (参数一.Contains("=") == true) //是否包含“等于”符号
                                            {
                                                参数四 = 参数一.Split(new string[] { "=" }, StringSplitOptions.None);//关系符分割

                                                //添加整数(参数四[0]);

                                                if (参数四[1].Contains("+") == true) //是否包含“加”符号
                                                {
                                                    参数五 = 参数四[1].Split(new string[] { "+" }, StringSplitOptions.None);//算术符分割
                                                    参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                                    if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                    {
                                                        参数五[0] = 参数五[0];
                                                        参数五[1] = 参数五[1];
                                                    }
                                                    else if (winapp.小程序窗口.判断数字(参数五[0]) != "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                    {
                                                        参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                        参数五[1] = 参数五[1];
                                                    }
                                                    else if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) != "整数")
                                                    {
                                                        参数五[0] = 参数五[0];
                                                        参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                    }
                                                    else//两个都不是
                                                    {
                                                        参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                        参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                    }

                                                    int 重新赋值 = int.Parse(参数五[0]) + int.Parse(参数五[1]);

                                                    赋值变量(参数四[0], 重新赋值.ToString());

                                                }
                                                else
                                                {
                                                    if (参数四[1].Contains("-") == true) //是否包含“减”符号
                                                    {
                                                        参数五 = 参数四[1].Split(new string[] { "-" }, StringSplitOptions.None);//算术符分割
                                                        参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                                        if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                        {
                                                            参数五[0] = 参数五[0];
                                                            参数五[1] = 参数五[1];
                                                        }
                                                        else if (winapp.小程序窗口.判断数字(参数五[0]) != "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                        {
                                                            参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                            参数五[1] = 参数五[1];
                                                        }
                                                        else if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) != "整数")
                                                        {
                                                            参数五[0] = 参数五[0];
                                                            参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                        }
                                                        else//两个都不是
                                                        {
                                                            参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                            参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                        }

                                                        int 重新赋值 = int.Parse(参数五[0]) - int.Parse(参数五[1]);

                                                        赋值变量(参数四[0], 重新赋值.ToString());
                                                    }
                                                    else
                                                    {
                                                        if (参数四[1].Contains("*") == true) //是否包含“乘”符号
                                                        {
                                                            参数五 = 参数四[1].Split(new string[] { "*" }, StringSplitOptions.None);//算术符分割
                                                            参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                                            if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                            {
                                                                参数五[0] = 参数五[0];
                                                                参数五[1] = 参数五[1];
                                                            }
                                                            else if (winapp.小程序窗口.判断数字(参数五[0]) != "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                            {
                                                                参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                                参数五[1] = 参数五[1];
                                                            }
                                                            else if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) != "整数")
                                                            {
                                                                参数五[0] = 参数五[0];
                                                                参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                            }
                                                            else//两个都不是
                                                            {
                                                                参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                                参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                            }

                                                            int 重新赋值 = int.Parse(参数五[0]) * int.Parse(参数五[1]);

                                                            赋值变量(参数四[0], 重新赋值.ToString());
                                                        }
                                                        else
                                                        {
                                                            if (参数四[1].Contains("/") == true) //是否包含“除”符号
                                                            {
                                                                参数五 = 参数四[1].Split(new string[] { "/" }, StringSplitOptions.None);//算术符分割
                                                                if (int.Parse(参数五[1]) != 0)
                                                                {
                                                                    参数六 = 动作.读取ini("变量名", 参数四[0], "0", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");

                                                                    if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                                    {
                                                                        参数五[0] = 参数五[0];
                                                                        参数五[1] = 参数五[1];
                                                                    }
                                                                    else if (winapp.小程序窗口.判断数字(参数五[0]) != "整数" && winapp.小程序窗口.判断数字(参数五[1]) == "整数")
                                                                    {
                                                                        参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                                        参数五[1] = 参数五[1];
                                                                    }
                                                                    else if (winapp.小程序窗口.判断数字(参数五[0]) == "整数" && winapp.小程序窗口.判断数字(参数五[1]) != "整数")
                                                                    {
                                                                        参数五[0] = 参数五[0];
                                                                        参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                                    }
                                                                    else//两个都不是
                                                                    {
                                                                        参数五[0] = 动作.变量读取ini("变量名", 参数五[0], "-1", @".\Generate\variable.ini");
                                                                        参数五[1] = 动作.变量读取ini("变量名", 参数五[1], "-1", @".\Generate\variable.ini");
                                                                    }

                                                                    int 重新赋值 = int.Parse(参数五[0]) / int.Parse(参数五[1]);

                                                                    赋值变量(参数四[0], 重新赋值.ToString());
                                                                }
                                                                else
                                                                {
                                                                    winapp.小程序窗口.notifyIcon1.ShowBalloonTip(1000, "发条错误", "当前参数错误，分母不能为0", ToolTipIcon.Error);
                                                                    winapp.小程序窗口.停止快捷命令();
                                                                    return;
                                                                }
                                                            }
                                                            else//直接赋值数字
                                                            {
                                                                赋值变量(参数四[0], 参数四[1]);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case "字符拼接":

                            参数二 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割自定义内容最左端

                            if (参数二[0].Contains("\""))
                            {
                                参数二[0] = 参数二[0].Substring(1, 参数二[0].Length - 2);//掐头去尾

                            }
                            else
                            {
                                参数二[0] = 引用变量(参数二[0]);
                            }

                            if (参数二[1].Contains("\""))
                            {
                                参数二[1] = 参数二[1].Substring(1, 参数二[1].Length - 2);//掐头去尾
                            }
                            else
                            {
                                参数二[1] = 引用变量(参数二[1]);

                            }


                            赋值变量(参数二[2], 参数二[0] + 参数二[1]);

                            break;

                        case "运行命令":
                            winapp.小程序窗口.notifyIcon1.ShowBalloonTip(1000, "发条错误", "暂不可嵌套快捷命令，开发中……", ToolTipIcon.Error);
                            winapp.小程序窗口.停止快捷命令();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                winapp.小程序窗口.notifyIcon1.ShowBalloonTip(1000, "发条错误", "当前快捷命令参数不完整或不正确，请尝试重新编辑后运行", ToolTipIcon.Error);
                winapp.小程序窗口.停止快捷命令();
                return;
            }

            void 赋值变量(string 变量名, string 变量值)
            {
                动作.写入ini("变量名", 变量名, 变量值, winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
            }

            string 引用变量(string 变量名)
            {
                return 动作.读取ini("变量名", 变量名, "空", winapp.小程序窗口.我的文档路径 + @"\Winapp" + @"\Generate\subvariable.ini");
            }



        }
    }
}
