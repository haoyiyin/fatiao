using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace fatiao
{
    public class 动作类型
    {
        Random 随机数 = new Random();

        [DllImport("user32.dll ")]
        public static extern bool GetCursorPos(out Point lpPoint);//获取鼠标坐标值




        public 快捷命令 快捷命令 = new 快捷命令();

        /*        object x1, x2, y1, y2;*/

        public TSPlugLib.TSPlugInterFace fatiao = new TSPlugLib.TSPlugInterFace();//新的dll

        public static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }


        public void 轨迹移动(int 目标x, int 目标y, int 速度, int 幅度)
        {
            while (true)
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
                if (winapp.小程序窗口.停止作用 == true || 快捷命令.停止作用 == true)//停止循环的开关
                {
                    winapp.小程序窗口.停止作用 = false;
                    快捷命令.停止作用 = false;
                    return;
                }
                Delay(随机数.Next(0, 5));
            }

        }

        public void 等待时间(int 参数)
        {
            for (int 循环 = 0; 循环 < 参数 / 15; 循环++)
            {
                Delay(1);
                if (winapp.小程序窗口.停止作用 == true)//停止循环的开关
                {
                    return;
                }
            }
        }

        public void 蜂鸣提醒(int 参数)
        {
            for (int 循环1 = 0; 循环1 < 参数; 循环1++)
            {
                Console.Beep(1000, 500);
                Delay(500);
                if (winapp.小程序窗口.停止作用 == true)//停止循环的开关
                {
                    break;
                }
            }
        }

    }
}

