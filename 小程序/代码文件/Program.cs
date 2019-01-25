using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using winapp.Properties;
using 动作库;

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
            /*            _ = new System.Threading.Mutex(true, Application.ProductName, out bool 只打开一个);
                        if (只打开一个)
                        {*/

            string 流程文件夹 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Generate\";
            string 快捷命令文件夹 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Command\";
            string 流程 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Generate\process";
            string 流程变量 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Generate\variable.ini";
            string 快捷命令变量 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Generate\subvariable.ini";
            string 快捷命令 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\Command\subprocess";

            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp"))//存在就删除
                {
                    DelectDir(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp");
                }
                ZipFile.ExtractToDirectory(Environment.CurrentDirectory + @"\winapp.dll", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("动态链接库文件丢失\r\n应用不完整，请重新下载", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示
                return;
            }


            string 小程序配置 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Winapp" + @"\config.ini";

            if (!File.Exists(小程序配置))
            {
                MessageBox.Show("配置文件不存在，请重新下载", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示
                return;
            }
            读取配置文件();

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
            if (!File.Exists(快捷命令))
            {
                File.Create(快捷命令).Dispose();//生成文件后解除占用
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new winapp());
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            /*            }
                        else
                        { 
                            Application.Exit();//退出程序   
                        }*/

            void DelectDir(string srcPath)//删除文件夹内的所有文件
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

            void 读取配置文件()
            {
                Settings.Default.程序名 = 动作.读取ini("程序", "程序名", "空", 小程序配置);
                Settings.Default.运行次数 = 动作.读取ini("程序", "运行次数", "空", 小程序配置);
                Settings.Default.按键一名 = 动作.读取ini("程序", "按键一名", "空", 小程序配置);
                Settings.Default.按键二名 = 动作.读取ini("程序", "按键二名", "空", 小程序配置);
                Settings.Default.按键三名 = 动作.读取ini("程序", "按键三名", "空", 小程序配置);
                Settings.Default.按键四名 = 动作.读取ini("程序", "按键四名", "空", 小程序配置);

                Settings.Default.打开程序 = 动作.读取ini("事件", "打开程序", "空", 小程序配置);
                Settings.Default.关闭程序 = 动作.读取ini("事件", "关闭程序", "空", 小程序配置);
                Settings.Default.按键一 = 动作.读取ini("事件", "按键一", "空", 小程序配置);
                Settings.Default.按键二 = 动作.读取ini("事件", "按键二", "空", 小程序配置);
                Settings.Default.按键三 = 动作.读取ini("事件", "按键三", "空", 小程序配置);
                Settings.Default.按键四 = 动作.读取ini("事件", "按键四", "空", 小程序配置);

                Settings.Default.快捷键一 = 动作.读取ini("热键", "快捷键一", "空", 小程序配置);
                Settings.Default.快捷键二 = 动作.读取ini("热键", "快捷键二", "空", 小程序配置);
                Settings.Default.快捷键三 = 动作.读取ini("热键", "快捷键三", "空", 小程序配置);
                Settings.Default.快捷键四 = 动作.读取ini("热键", "快捷键四", "空", 小程序配置);

                Settings.Default.按键一位置X = int.Parse(动作.读取ini("设计", "按键一位置X", "空", 小程序配置));
                Settings.Default.按键二位置X = int.Parse(动作.读取ini("设计", "按键二位置X", "空", 小程序配置));
                Settings.Default.按键三位置X = int.Parse(动作.读取ini("设计", "按键三位置X", "空", 小程序配置));
                Settings.Default.按键四位置X = int.Parse(动作.读取ini("设计", "按键四位置X", "空", 小程序配置));
                Settings.Default.文本位置X = int.Parse(动作.读取ini("设计", "文本位置X", "空", 小程序配置));
                Settings.Default.运行次数位置X = int.Parse(动作.读取ini("设计", "运行次数位置X", "空", 小程序配置));

                Settings.Default.窗口大小X = int.Parse(动作.读取ini("设计", "窗口大小X", "空", 小程序配置));

                Settings.Default.按键一位置Y = int.Parse(动作.读取ini("设计", "按键一位置Y", "空", 小程序配置));
                Settings.Default.按键二位置Y = int.Parse(动作.读取ini("设计", "按键二位置Y", "空", 小程序配置));
                Settings.Default.按键三位置Y = int.Parse(动作.读取ini("设计", "按键三位置Y", "空", 小程序配置));
                Settings.Default.按键四位置Y = int.Parse(动作.读取ini("设计", "按键四位置Y", "空", 小程序配置));
                Settings.Default.文本位置Y = int.Parse(动作.读取ini("设计", "文本位置Y", "空", 小程序配置));
                Settings.Default.运行次数位置Y = int.Parse(动作.读取ini("设计", "运行次数位置Y", "空", 小程序配置));

                Settings.Default.窗口大小Y = int.Parse(动作.读取ini("设计", "窗口大小Y", "空", 小程序配置));

            }

        }
    }
}
