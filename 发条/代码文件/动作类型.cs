using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace fatiao
{
    public class 动作类型
    {
        static Random 随机数 = new Random();

        [DllImport("user32.dll ")]
        public static extern bool GetCursorPos(out Point lpPoint);//获取鼠标坐标值

        
        public 快捷命令 快捷命令 = new 快捷命令();


        static TSPlugLib.TSPlugInterFace fatiao = new TSPlugLib.TSPlugInterFace();//新的dll

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }


        public static void 轨迹移动(int 目标x, int 目标y, int 速度, int 幅度)
        {
            while (发条.发条窗口.停止作用 == false || 快捷命令.停止作用 == false)
            {
                GetCursorPos(out Point 坐标);//获取鼠标坐标
                int 当前x = 坐标.X;
                int 当前y = 坐标.Y;
                if (当前x == 目标x)
                {
                    fatiao.MoveR(0 + 随机数.Next(-幅度, 幅度), (Math.Sign((目标y - 当前y))) * 速度 + 随机数.Next(-幅度, 幅度));
                }
                else if (当前y == 目标y)
                {
                    fatiao.MoveR(Math.Sign((目标x - 当前x)) * 速度 + 随机数.Next(-幅度, 幅度), 0 + 随机数.Next(-幅度, 幅度));
                }
                else if (当前x <= 目标x + 5 && 当前x >= 目标x - 5 && 当前y <= 目标y + 5 && 当前y >= 目标y - 5)
                {
                    return;
                }
                else if (Math.Abs(当前x - 目标x) > Math.Abs(当前y - 目标y))
                {
                    fatiao.MoveR(Math.Sign((目标x - 当前x)) * 速度 + 随机数.Next(-幅度, 幅度), (Math.Sign((目标y - 当前y))) * 速度 / (Math.Abs(当前x - 目标x) / Math.Abs(当前y - 目标y)) + 随机数.Next(-幅度, 幅度));
                }
                else
                {
                    fatiao.MoveR(Math.Sign((目标x - 当前x)) * 速度 / (Math.Abs(当前y - 目标y) / Math.Abs(当前x - 目标x)) + 随机数.Next(-幅度, 幅度), (Math.Sign((目标y - 当前y))) * 速度 + 随机数.Next(-幅度, 幅度));
                }

                Delay(随机数.Next(0, 5));
            }

        }

        public static void 等待时间(int 参数)
        {
            for (int 循环 = 0; 循环 < 参数 / 15; 循环++)
            {
                Delay(1);
                if (发条.发条窗口.停止作用 == true)//停止循环的开关
                {
                    return;
                }
            }
        }

        public static void 蜂鸣提醒(int 参数)
        {
            for (int 循环1 = 0; 循环1 < 参数; 循环1++)
            {
                Console.Beep(1000, 500);
                Delay(500);
                if (发条.发条窗口.停止作用 == true)//停止循环的开关
                {
                    break;
                }
            }
        }




        public static void 添加日志(string 内容)
        {
            bool scroll = false;
            if (发条.发条窗口.listBox2.TopIndex == 发条.发条窗口.listBox2.Items.Count - (int)(发条.发条窗口.listBox2.Height / 发条.发条窗口.listBox2.ItemHeight))
            {
                scroll = true;
            }
            发条.发条窗口.listBox2.Items.Add("["+ DateTime.Now.ToLongTimeString().ToString() + "]   "+内容);
            
            if (发条.发条窗口.listBox2.Items.Count>1000)
            {
                发条.发条窗口.listBox2.Items.RemoveAt(0);
            }
            if (scroll)
            {
                发条.发条窗口.listBox2.TopIndex = 发条.发条窗口.listBox2.Items.Count - (int)(发条.发条窗口.listBox2.Height / 发条.发条窗口.listBox2.ItemHeight);
            }
        }


    }
}
