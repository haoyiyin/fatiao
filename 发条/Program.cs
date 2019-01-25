using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace fatiao
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            _ = new System.Threading.Mutex(true, Application.ProductName, out bool 只打开一个);
            if (只打开一个)
            {

                string steamDLL = Environment.CurrentDirectory + @"\steam_api.dll";
                string 流程文件夹 = Environment.CurrentDirectory + @"\Generate\";
                string 快捷命令文件夹 = Environment.CurrentDirectory + @"\Command\";
                string 流程 = Environment.CurrentDirectory + @"\Generate\process";
                string 流程变量 = Environment.CurrentDirectory + @"\Generate\variable.ini";
                string 快捷命令变量 = Environment.CurrentDirectory + @"\Generate\subvariable.ini";

                if (!File.Exists(steamDLL))
                {
                    MessageBox.Show("动态链接库文件丢失\r\n发条应用不完整，请重新下载", "发条错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示
                    return;
                }

                if (!Directory.Exists(流程文件夹))//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(流程文件夹);//创建该文件夹
                }

                if (!Directory.Exists(快捷命令文件夹))//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(快捷命令文件夹);//创建该文件夹
                }

                if (File.Exists(流程变量))
                {
                    File.Delete(流程变量);//删除
                }
                if (File.Exists(快捷命令变量))
                {
                    File.Delete(快捷命令变量);//删除
                }

                if (!File.Exists(流程))
                {
                    File.Create(流程).Dispose();//生成文件后解除占用
                }
                if (!File.Exists(流程变量))
                {
                    File.Create(流程变量).Dispose();//生成文件后解除占用
                }
                if (!File.Exists(快捷命令变量))
                {
                    File.Create(快捷命令变量).Dispose();//生成文件后解除占用
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    Application.Run(new 发条());
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Application.Exit();//退出程序   
            }
        }
    }
}
