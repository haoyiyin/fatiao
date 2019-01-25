using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 动作库
{
    public class 动作
    {

        public static TSPlugLib.TSPlugInterFace fatiao = new TSPlugLib.TSPlugInterFace();//新的dll

        static void Delay(int milliSecond)//不堵塞delay延迟函数
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        static Random 随机数 = new Random();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);//键盘判断

        [DllImport("user32.dll ")]
        public static extern bool GetCursorPos(out Point lpPoint);//获取鼠标坐标值

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(int hWnd, ref RECT lpRect);//获取窗口大小

        // 获得客户区矩形
        [DllImport("user32.dll")]
        static extern bool GetClientRect(int hWnd, out RECT lpRect);

        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }

        byte[] byData = new byte[100];
        char[] charData = new char[1000];

        [DllImport("user32", CharSet = CharSet.Unicode)]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标
        public const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public static void 单击左键()
        {

            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }), null);
        }

        public static void 双击左键()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Delay(随机数.Next(100, 200));
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }), null);

        }

        public static void 长按左键()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            }), null);

        }

        public static void 松开左键()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }), null);

        }

        public static void 单击右键()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }), null);

        }

        public static void 长按右键()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            }), null);

        }

        public static void 松开右键()
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
            {
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }), null);

        }

        public static string 文本读取(string 路径)
        {
            try
            {
                string str = File.ReadAllText(路径);

                //Console.WriteLine("{0}", str);
                return str;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                return "发条";
            }
        }

        public static string 文本写入(string 文本内容, string 路径)
        {
            try
            {
                FileStream fs = new FileStream(路径, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                sw.WriteLine(文本内容);
                sw.Flush();
                sw.Close();
                fs.Close();
                return "真";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "假";
            }
        }

        public static void 获取窗口大小(int 句柄, out int 宽, out int 高)
        {
            RECT rc = new RECT();
            GetWindowRect(句柄, ref rc);
            宽 = rc.Right - rc.Left;                        //窗口的宽度
            高 = rc.Bottom - rc.Top;                   //窗口的高度
        }

        public static void 获取客户区大小(int 句柄, out int 宽, out int 高)
        {
            RECT rc;
            GetClientRect(句柄, out rc);
            宽 = rc.Right - rc.Left;                        //客户区的宽度
            高 = rc.Bottom - rc.Top;                   //客户区的高度
        }


        public static void 关机()
        {
            Process.Start("shutdown.exe", "-s -t 0");
            //Replace xx with Seconds example 10,20 etc
        }

        public static void 重启()
        {
            Process.Start("shutdown.exe", "-r -t 0");
            //Replace xx with Seconds example 10,20 etc
        }

        public static void 注销()
        {
            Process.Start("shutdown.exe", "-l");
        }

        public static bool 判断按键(int 键码值)
        {
            if (GetKeyState(键码值) < 0)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public string 图片转Base64(string 图片目录)
        {
            try
            {
                Bitmap bmp = new Bitmap(图片目录);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Bmp);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                bmp.Dispose(); bmp = null;
                return Convert.ToBase64String(arr);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string 下载文件(string 链接, string 目录)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(链接, 目录);
                return "真";
            }
            catch (Exception)
            {
                return "假";
            }
        }

        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        public static string CPU码()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string 硬盘码()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <returns></returns>
        public static string 设备码()
        {
            string strNum = CPU码() + 硬盘码();//获得24位Cpu和硬盘序列号
            string strMNum = strNum.Substring(0, 24);//从生成的字符串中取出前24个字符做为机器码
            return strMNum;
        }
        public int[] intCode = new int[127];//存储密钥
        public int[] intNumber = new int[25];//存机器码的Ascii值
        public char[] Charcode = new char[25];//存储机器码字
        public void setIntCode()//给数组赋值小于10的数
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        /// <summary>
        /// 生成注册码
        /// </summary>
        /// <returns></returns>
        public string 注册码()
        {
            setIntCode();//初始化127位数组
            string MNum = 设备码();//获取注册码
            for (int i = 1; i < Charcode.Length; i++)//把设备码存入数组中
            {
                Charcode[i] = Convert.ToChar(MNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储注册码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }
                }
            }
            return strAsciiName;//返回注册码
        }

        public static void 删除判断画面图片()
        {
            if (File.Exists(Environment.CurrentDirectory + @"\screen.bmp"))//如果存在就删除（变相覆盖）
            {
                File.Delete(Environment.CurrentDirectory + @"\screen.bmp");
            }
        }

        //INI文件名

        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string sectionName, string key, string value, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string sectionName, string key, string defaultValue, byte[] returnBuffer, int size, string filePath);

        //写INI文件
        public static string 变量写入ini(string 节, string 键, string 值, string 路径)
        {
            if (File.Exists(路径))
            {
                long OpStation = WritePrivateProfileString(节, 键, 值, 路径);
                if (OpStation == 0)
                {
                    return "假";
                }
                else
                {
                    //添加日志("保存值：" + 值);
                    return "真";
                }
            }
            else
            {
                return "假";
            }
        }

        public static string 变量读取ini(string 节, string 键, string 错误, string 路径)
        {
            byte[] buffer = new byte[2048];
            int length = GetPrivateProfileString(节, 键, 错误, buffer, 999, 路径);
            string rs = Encoding.Default.GetString(buffer, 0, length);
            //添加日志("读取值：" + rs);
            return rs;
        }





        public static string 写入ini(string 节, string 键, string 值, string 路径)
        {
            if (File.Exists(路径))
            {
                long OpStation = WritePrivateProfileString(节, 键, 值, 路径);
                if (OpStation == 0)
                {
                    return "假";
                }
                else
                {
                    return "真";
                }
            }
            else
            {
                return "假";
            }
        }

        public static string 读取ini(string 节, string 键, string 错误, string 路径)
        {
            byte[] buffer = new byte[2048];
            int length = GetPrivateProfileString(节, 键, 错误, buffer, 999, 路径);
            string rs = Encoding.Default.GetString(buffer, 0, length);
            return rs;
        }

        public static List<string> 读取键(string 节, string 路径)
        {
            byte[] buffer = new byte[2048];
            int length = GetPrivateProfileString(节, null, "", buffer, 999, 路径);
            String[] rs = Encoding.Default.GetString(buffer, 0, length).Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
            return rs.ToList();
        }

        public static string 读网络文本(string 出错, string 链接)
        {
            try
            {
                string content = "";
                //创建一个请求对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(链接);
                //超时时间
                request.Timeout = 60000;
                //获取回写流
                WebResponse response = request.GetResponse();
                //把网页对象读成流
                using (Stream stream = response.GetResponseStream())
                {
                    //用stramreader 读取 stram到string
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //读取到结尾赋值给Content
                        content = reader.ReadToEnd();
                        reader.Close();
                        reader.Dispose();
                    }
                    stream.Close();
                    stream.Dispose();
                }
                response.Close();
                response.Dispose();
                return content;
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                return 出错;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 出错;
            }
        }



        /// <summary>
        /// Http上传文件
        /// </summary>
        public static string 上传文件(string 链接, string 路径)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(链接) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
                byte[] itemBoundaryBytes = Encoding.Default.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endBoundaryBytes = Encoding.Default.GetBytes("\r\n--" + boundary + "--\r\n");
                int pos = 路径.LastIndexOf("\\");
                string fileName = 路径.Substring(pos + 1);

                //请求头部信息
                StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
                byte[] postHeaderBytes = Encoding.Default.GetBytes(sbHeader.ToString());

                FileStream fs = new FileStream(路径, FileMode.Open, FileAccess.Read);
                byte[] bArr = new byte[fs.Length];
                fs.Read(bArr, 0, bArr.Length);
                fs.Close();

                Stream postStream = request.GetRequestStream();
                postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                postStream.Write(bArr, 0, bArr.Length);
                postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                postStream.Close();

                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream instream = response.GetResponseStream();
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                Console.WriteLine("上传成功");
                return "真";
            }
            catch (Exception)
            {
                Console.WriteLine("上传失败");
                return "假";
            } 
        }

        public static void 坐标转百分比(int 句柄, int X, int Y, out double 百分比X, out double 百分比Y)
        {
           fatiao.GetClientSize(句柄, out object w, out object h);

            百分比X = Convert.ToDouble(X) / Convert.ToDouble(w);
            百分比Y = Convert.ToDouble(Y) / Convert.ToDouble(h);
        }

        public static void 百分比转坐标(int 句柄, out int X, out int Y, double 百分比X, double 百分比Y)
        {
            fatiao.GetClientSize(句柄, out object w, out object h);
            X = Convert.ToInt32(Convert.ToDouble(w) * (百分比X / 100));
            Y = Convert.ToInt32(Convert.ToDouble(h) * (百分比Y / 100));
        }

        public string[] 显示所有窗口名()
        {
            string[] 窗口名 = null;

            return 窗口名;
        }

        [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SteamAPI_Init(); //steam初始化
        public static bool 订阅验证()
        {
            if (SteamAPI_Init() && SteamApps.BIsSubscribedApp((AppId_t)1416190))//当初始化成功
            {
                return true;
            }
            else//初始化失败
            {
                return  false;
            }
        }

        private const Int32 WM_SYSCOMMAND = 274;
        private const UInt32 SC_CLOSE = 61536;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int RegisterWindowMessage(string lpString);

        //显示屏幕键盘
        public static int 显示屏幕键盘()
        {
            try
            {
                dynamic file = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                if (!System.IO.File.Exists(file))
                    return 0;
                Process.Start(file);
                //return SetUnDock(); //不知SetUnDock()是什么，所以直接注释返回1
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //隐藏屏幕键盘
        public static void 隐藏屏幕键盘()
        {
            IntPtr TouchhWnd = new IntPtr(0);
            TouchhWnd = FindWindow("IPTip_Main_Window", null);
            if (TouchhWnd == IntPtr.Zero)
                return;
            PostMessage(TouchhWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
        }



    }
}
