using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fatiao
{
    class 编辑动作
    {
        public void 编辑动作操作(object sender, EventArgs e)
        {
            try
            {
                if (发条.发条窗口.listBox1.SelectedIndices.Count == 1)//选中项等于1（仅单选）
                {
                    string 注释参数;
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
                    int 左括号;
                    int 右括号;
                    int 右至左;
                    //object 坐标一 = 0;//找图找色返回坐标
                    //object 坐标二 = 0;//找图找色返回坐标
                    /*      int 图色;*/
                    string[] 参数;
                    string 参数九;

                    每行内容 = 发条.发条窗口.listBox1.Items[发条.发条窗口.listBox1.SelectedIndex].ToString();//读取运行行内容

                    if (每行内容 == "")
                    {
                        return;
                    }

                    if (每行内容 == "﹂循环末;" || 每行内容 == "﹂如果末;" || 每行内容 == "停用:﹂循环末;" || 每行内容 == "停用:﹂如果末;")
                    {
                        每行内容 += "()";
                    }

                    左括号 = 每行内容.IndexOf("(");//定位左括号的位置（从左到右定位）
                    参数九 = 每行内容.Substring(左括号);//选取左括号+之后的内容
                    右括号 = 参数九.LastIndexOf(")");//定位右括号的位置（从右到左定位）

                    参数一 = 参数九.Substring(1, 右括号 - 1);//括号内的参数（从第二个字符串开始，至右括号前一个位置）
                    //参数一 = 参数一.Replace(" ", "");//条件去除空格
                    参数 = 参数一.Split(',');//简单切割所有参数

                    动作类 = 每行内容.Split('(');//分割命令名和参数

                    switch (动作类[0])
                    {
                        case "点击图片":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数三 = 参数二[2].Split(new string[] { "\"," }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                            参数七 = 参数三[0];

                            if (File.Exists(Environment.CurrentDirectory + @".\素材库\" + 参数七))
                            {
                                Image img = Image.FromFile(Environment.CurrentDirectory + @".\素材库\" + 参数七);
                                Image bmp = new Bitmap(img);
                                img.Dispose();
                                发条.发条窗口.点击图片.pictureBox1.Image = bmp;
                                发条.发条窗口.点击图片.label11.Text = 参数七;
                            }
                            else
                            {
                                发条.发条窗口.点击图片.pictureBox1.Image = null;
                                发条.发条窗口.点击图片.label11.Text = "";
                            }

                            发条.发条窗口.点击图片.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.点击图片.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.点击图片.textBox6.Text = 参数二[0];
                            发条.发条窗口.点击图片.textBox3.Text = 参数六;
                            发条.发条窗口.点击图片.trackBar1.Value = int.Parse(参数三[1]);
                            发条.发条窗口.点击图片.textBox2.Text = 注释参数;

                            发条.发条窗口.点击图片.Show();
                            break;

                        case "点击位置":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数二 = 参数一.Split(new string[] { "\"," }, StringSplitOptions.None);//分割自定义内容最左端

                            参数六 = 参数二[0].Substring(1, 参数二[0].Length - 1);//掐头
                            参数三 = 参数二[1].Split(',');//简单切割所有参数

                            发条.发条窗口.点击位置.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.点击位置.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.点击位置.textBox3.Text = 参数六;
                            发条.发条窗口.点击位置.textBox4.Text = 参数三[0];
                            发条.发条窗口.点击位置.textBox5.Text = 参数三[1];
                            发条.发条窗口.点击位置.textBox2.Text = 注释参数;

                            发条.发条窗口.点击位置.Show();
                            break;

                        case "输出日志":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            if (参数一.Contains("\""))
                            {
                                参数六 = 参数一.Substring(1, 参数一.Length - 2);//掐头去尾

                                发条.发条窗口.输出日志.button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色 
                            }
                            else
                            {
                                参数六 = 参数一;
                                发条.发条窗口.输出日志.button17.ForeColor = Color.Maroon;//红色
                            }

                            发条.发条窗口.输出日志.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.输出日志.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.输出日志.textBox4.Text = 参数六;
                            发条.发条窗口.输出日志.textBox2.Text = 注释参数;

                            发条.发条窗口.输出日志.Show();
                            break;

                        case "获取时间":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.获取时间.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.获取时间.button6.Visible = true;//显示“修改”按键
                            发条.发条窗口.获取时间.textBox4.Text = 参数[0];
                            发条.发条窗口.获取时间.comboBox1.Text = 参数[1];
                            发条.发条窗口.获取时间.textBox3.Text = 注释参数;

                            发条.发条窗口.获取时间.Show();
                            break;

                        case "上传文件":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//定位保存目录名最左端
                            右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//保存目录+文件名
                            string 上传网址 = 参数[1].Substring(1, 参数[1].Length - 2);//定位网址

                            发条.发条窗口.上传文件.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.上传文件.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.上传文件.textBox2.Text = 注释参数;
                            发条.发条窗口.上传文件.textBox6.Text = 参数[0];
                            发条.发条窗口.上传文件.textBox3.Text = 上传网址;
                            发条.发条窗口.上传文件.textBox4.Text = 参数六;

                            发条.发条窗口.上传文件.Show();
                            break;

                        case "获取设备码":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            发条.发条窗口.获取设备码.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.获取设备码.button6.Visible = true;//显示“修改”按键
                            发条.发条窗口.获取设备码.textBox4.Text = 参数[0];
                            发条.发条窗口.获取设备码.comboBox1.Text = 参数[1];
                            发条.发条窗口.获取设备码.textBox3.Text = 注释参数;

                            发条.发条窗口.获取设备码.Show();
                            break;

                        case "读写文件":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[参数二.Length - 1];//获取最后一组数据
                            参数六 = 参数六.Substring(0, 参数六.Length - 1);//去尾

                            switch (参数[1])
                            {
                                case "文本读取":

                                    break;
                                case "配置读取":
                                    if (参数[2].Contains("\""))
                                    {
                                        参数[2] = 参数[2].Substring(1, 参数[2].Length - 2);//掐头去尾
                                        发条.发条窗口.读写文件.textBox5.Text = 参数[2];
                                        发条.发条窗口.读写文件.button3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                    }
                                    else
                                    {
                                        发条.发条窗口.读写文件.textBox5.Text = 参数[2];
                                        发条.发条窗口.读写文件.button3.ForeColor = Color.Maroon;//等于红色
                                    }


                                    if (参数[3].Contains("\""))
                                    {
                                        参数[3] = 参数[3].Substring(1, 参数[3].Length - 2);//掐头去尾
                                        发条.发条窗口.读写文件.textBox6.Text = 参数[3];
                                        发条.发条窗口.读写文件.button7.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                    }
                                    else
                                    {
                                        发条.发条窗口.读写文件.textBox6.Text = 参数[3];
                                        发条.发条窗口.读写文件.button7.ForeColor = Color.Maroon;//等于红色
                                    }


                                    break;
                                case "文本写入":

                                    参数七 = 参数二[参数二.Length - 2];//获取最后2组数据
                                    if (参数七.Contains(","))
                                    {
                                        参数三 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割
                                        参数七 = 参数三[参数三.Length - 1];//获取最后2组数据
                                        发条.发条窗口.读写文件.button5.ForeColor = Color.Maroon;//等于红色
                                    }
                                    else
                                    {
                                        参数七 = 参数七.Substring(0, 参数七.Length - 1);//去尾
                                        发条.发条窗口.读写文件.button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色 
                                    }
                                    发条.发条窗口.读写文件.textBox9.Text = 参数七;

                                    break;
                                default://配置写入
                                    参数七 = 参数二[参数二.Length - 2];//获取最后2组数据
                                    if (参数七.Contains(","))
                                    {
                                        参数三 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割
                                        参数七 = 参数三[参数三.Length - 1];//获取最后2组数据
                                        发条.发条窗口.读写文件.button5.ForeColor = Color.Maroon;//等于红色
                                    }
                                    else
                                    {
                                        参数七 = 参数七.Substring(0, 参数七.Length - 1);//去尾
                                        发条.发条窗口.读写文件.button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色 
                                    }
                                    发条.发条窗口.读写文件.textBox9.Text = 参数七;

                                    if (参数[2].Contains("\""))
                                    {
                                        参数[2] = 参数[2].Substring(1, 参数[2].Length - 2);//掐头去尾
                                        发条.发条窗口.读写文件.textBox5.Text = 参数[2];
                                        发条.发条窗口.读写文件.button3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                    }
                                    else
                                    {
                                        发条.发条窗口.读写文件.textBox5.Text = 参数[2];
                                        发条.发条窗口.读写文件.button3.ForeColor = Color.Maroon;//等于红色
                                    }


                                    if (参数[3].Contains("\""))
                                    {
                                        参数[3] = 参数[3].Substring(1, 参数[3].Length - 2);//掐头去尾
                                        发条.发条窗口.读写文件.textBox6.Text = 参数[3];
                                        发条.发条窗口.读写文件.button7.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                    }
                                    else
                                    {
                                        发条.发条窗口.读写文件.textBox6.Text = 参数[3];
                                        发条.发条窗口.读写文件.button7.ForeColor = Color.Maroon;//等于红色
                                    }


                                    break;
                            }

                            发条.发条窗口.读写文件.textBox4.Text = 参数[0];
                            发条.发条窗口.读写文件.comboBox1.Text = 参数[1];
                            发条.发条窗口.读写文件.textBox1.Text = 参数六;
                            发条.发条窗口.读写文件.textBox3.Text = 注释参数;
                            发条.发条窗口.读写文件.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.读写文件.button6.Visible = true;//显示“修改”按键

                            发条.发条窗口.读写文件.Show();
                            break;

                        case "获取缩放率":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.获取缩放率.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.获取缩放率.button6.Visible = true;//显示“修改”按键
                            发条.发条窗口.获取缩放率.textBox4.Text = 参数一;
                            发条.发条窗口.获取缩放率.textBox3.Text = 注释参数;

                            发条.发条窗口.获取缩放率.Show();
                            break;

                        case "变量赋值":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数三 = 参数一.Split(new string[] { "=" }, StringSplitOptions.None);//分割条件和结果

                            if (参数一.Contains("=\""))
                            {
                                参数二 = 参数一.Split(new string[] { "=\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                发条.发条窗口.变量选项 = "输入数据";
                                发条.发条窗口.变量赋值.textBox3.Text = 参数六;
                            }
                            else
                            {
                                发条.发条窗口.变量选项 = 参数三[1];
                            }



                            发条.发条窗口.变量赋值.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.变量赋值.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.变量赋值.textBox4.Text = 参数三[0];

                            发条.发条窗口.变量赋值.textBox2.Text = 注释参数;

                            发条.发条窗口.变量赋值.Show();
                            break;

                        case "每次请求输入":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容

                            发条.发条窗口.请求输入.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.请求输入.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.请求输入.textBox4.Text = 参数[0];
                            发条.发条窗口.请求输入.comboBox1.Text = "每次请求";
                            发条.发条窗口.请求输入.textBox3.Text = 参数六;
                            发条.发条窗口.请求输入.textBox2.Text = 注释参数;

                            发条.发条窗口.请求输入.Show();
                            break;

                        case "首次请求输入":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容

                            发条.发条窗口.请求输入.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.请求输入.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.请求输入.textBox4.Text = 参数[0];
                            发条.发条窗口.请求输入.comboBox1.Text = "首次请求";
                            发条.发条窗口.请求输入.textBox3.Text = 参数六;
                            发条.发条窗口.请求输入.textBox2.Text = 注释参数;

                            发条.发条窗口.请求输入.Show();
                            break;

                        case "获取剪贴板":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.获取剪贴板.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.获取剪贴板.button6.Visible = true;//显示“修改”按键
                            发条.发条窗口.获取剪贴板.textBox4.Text = 参数一;
                            发条.发条窗口.获取剪贴板.textBox3.Text = 注释参数;

                            发条.发条窗口.获取剪贴板.Show();
                            break;
                        case "运行命令":
                            //注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            //参数六 = 参数一.Substring(0, 参数一.Length - 3);
                            参数一 = 参数一.Substring(参数一.Length - 3, 3);
                            //发条.发条窗口.运行快捷命令.comboBox1.Text = 参数六;
                            if (参数一 == ",异步")
                            {
                                发条.发条窗口.运行快捷命令.checkBox1.Checked = true;
                            }
                            else
                            {
                                发条.发条窗口.运行快捷命令.checkBox1.Checked = false;
                            }

                            发条.发条窗口.点击快捷命令 = 参数[0];
                            发条.发条窗口.运行快捷命令.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.运行快捷命令.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.运行快捷命令.Show();

                            break;
                        case "随机数字":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                            发条.发条窗口.随机数字.textBox2.Text = 注释参数;
                            发条.发条窗口.随机数字.textBox3.Text = 参数[0];
                            发条.发条窗口.随机数字.textBox4.Text = 参数[1];
                            发条.发条窗口.随机数字.textBox5.Text = 参数[2];
                            发条.发条窗口.随机数字.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.随机数字.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.随机数字.Show();

                            break;
                        case "判断按键":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果


                            发条.发条窗口.判断按键.textBox3.Text = 注释参数;
                            发条.发条窗口.判断按键.textBox4.Text = 参数[0];
                            发条.发条窗口.判断按键.comboBox1.Text = 参数[1];
                            参数[2] = 参数[2].Substring(1, 参数[2].Length - 2);//掐头去尾
                            发条.发条窗口.判断按键.textBox1.Text = 参数[2];
                            发条.发条窗口.判断按键.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.判断按键.button6.Visible = true;//显示“修改”按键

                            发条.发条窗口.判断按键.Show();

                            break;
                        case "判断窗口":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                            参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容

                            发条.发条窗口.判断窗口.textBox3.Text = 注释参数;
                            发条.发条窗口.判断窗口.textBox1.Text = 参数六;
                            发条.发条窗口.判断窗口.textBox4.Text = 参数[0];
                            发条.发条窗口.判断窗口.comboBox1.Text = 参数七;
                            发条.发条窗口.判断窗口.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.判断窗口.button6.Visible = true;//显示“修改”按键

                            发条.发条窗口.判断窗口.Show();

                            break;
                        case "计算时间差":

                            if (参数[2].Contains("\""))
                            {
                                参数六 = 参数[2].Substring(1, 参数[2].Length - 2);//掐头去尾

                                发条.发条窗口.计算时间差.button4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                switch (参数[1])
                                {
                                    case "年月日时分秒":
                                        发条.发条窗口.计算时间差.dateTimePicker2.Value = DateTime.ParseExact(参数六, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                                        break;
                                    case "年月日":
                                        发条.发条窗口.计算时间差.dateTimePicker2.Value = DateTime.ParseExact(参数六, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                                        break;
                                    case "时分秒":
                                        发条.发条窗口.计算时间差.dateTimePicker2.Value = DateTime.ParseExact(参数六, "HH:mm:ss", CultureInfo.InvariantCulture);
                                        break;
                                    case "仅时钟":
                                        发条.发条窗口.计算时间差.dateTimePicker2.Value = DateTime.ParseExact(参数六, "HH", CultureInfo.InvariantCulture);
                                        break;
                                    case "仅分钟":
                                        发条.发条窗口.计算时间差.dateTimePicker2.Value = DateTime.ParseExact(参数六, "mm", CultureInfo.InvariantCulture);
                                        break;
                                    case "仅秒钟":
                                        发条.发条窗口.计算时间差.dateTimePicker2.Value = DateTime.ParseExact(参数六, "ss", CultureInfo.InvariantCulture);
                                        break;
                                }
                            }
                            else
                            {
                                参数六 = 参数[2];
                                发条.发条窗口.计算时间差.textBox1.Text = 参数六;
                                发条.发条窗口.计算时间差.button4.ForeColor = Color.Maroon;//红色
                            }

                            if (参数[3].Contains("\""))
                            {
                                参数七 = 参数[3].Substring(1, 参数[3].Length - 2);//掐头去尾

                                发条.发条窗口.计算时间差.button3.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                switch (参数[1])
                                {
                                    case "年月日时分秒":
                                        发条.发条窗口.计算时间差.dateTimePicker1.Value = DateTime.ParseExact(参数七, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                                        break;
                                    case "年月日":
                                        发条.发条窗口.计算时间差.dateTimePicker1.Value = DateTime.ParseExact(参数七, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                                        break;
                                    case "时分秒":
                                        发条.发条窗口.计算时间差.dateTimePicker1.Value = DateTime.ParseExact(参数七, "HH:mm:ss", CultureInfo.InvariantCulture);
                                        break;
                                    case "仅时钟":
                                        发条.发条窗口.计算时间差.dateTimePicker1.Value = DateTime.ParseExact(参数七, "HH", CultureInfo.InvariantCulture);
                                        break;
                                    case "仅分钟":
                                        发条.发条窗口.计算时间差.dateTimePicker1.Value = DateTime.ParseExact(参数七, "mm", CultureInfo.InvariantCulture);
                                        break;
                                    case "仅秒钟":
                                        发条.发条窗口.计算时间差.dateTimePicker1.Value = DateTime.ParseExact(参数七, "ss", CultureInfo.InvariantCulture);
                                        break;
                                }
                            }
                            else
                            {
                                参数七 = 参数[3];
                                发条.发条窗口.计算时间差.textBox11.Text = 参数七;
                                发条.发条窗口.计算时间差.button3.ForeColor = Color.Maroon;//红色
                            }

                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.计算时间差.textBox3.Text = 注释参数;
                            发条.发条窗口.计算时间差.textBox4.Text = 参数[0];
                            发条.发条窗口.计算时间差.comboBox1.Text = 参数[1];

                            发条.发条窗口.计算时间差.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.计算时间差.button6.Visible = true;//显示“修改”按键

                            发条.发条窗口.计算时间差.Show();

                            break;
                        case "判断画面":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                            if (发条.发条窗口.判断数字(参数[2]) == "非数字")
                            {
                                发条.发条窗口.判断画面.button8.ForeColor = Color.Maroon;//红色
                            }
                            if (发条.发条窗口.判断数字(参数[3]) == "非数字")
                            {
                                发条.发条窗口.判断画面.button11.ForeColor = Color.Maroon;//红色
                            }
                            if (发条.发条窗口.判断数字(参数[4]) == "非数字")
                            {
                                发条.发条窗口.判断画面.button9.ForeColor = Color.Maroon;//红色
                            }
                            if (发条.发条窗口.判断数字(参数[5]) == "非数字")
                            {
                                发条.发条窗口.判断画面.button10.ForeColor = Color.Maroon;//红色
                            }

                            发条.发条窗口.判断画面.textBox2.Text = 注释参数;
                            发条.发条窗口.判断画面.textBox3.Text = 参数[1];
                            发条.发条窗口.判断画面.textBox4.Text = 参数[2];
                            发条.发条窗口.判断画面.textBox7.Text = 参数[3];
                            发条.发条窗口.判断画面.textBox5.Text = 参数[4];
                            发条.发条窗口.判断画面.textBox8.Text = 参数[5];
                            发条.发条窗口.判断画面.trackBar1.Value = int.Parse(参数[6]);
                            发条.发条窗口.判断画面.textBox6.Text = 参数[0];
                            发条.发条窗口.判断画面.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.判断画面.button7.Visible = true;//显示“修改”按键

                            发条.发条窗口.判断画面.Show();

                            break;
                        case "查找图片":
                            try
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）

                                注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容
                                参数四 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割出自定义内容之后的参数
                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                                if (!参数六.Contains("|"))
                                {
                                    if (File.Exists(Environment.CurrentDirectory + @".\素材库\" + 参数六))
                                    {
                                        Image img = Image.FromFile(Environment.CurrentDirectory + @".\素材库\" + 参数六);
                                        Image bmp = new Bitmap(img);
                                        img.Dispose();
                                        发条.发条窗口.查找图片.pictureBox1.Image = bmp;
                                    }
                                    else
                                    {
                                        发条.发条窗口.查找图片.pictureBox1.Image = null;
                                    }
                                }
                                else
                                {
                                    发条.发条窗口.查找图片.pictureBox1.Image = null;
                                }

                                发条.发条窗口.查找图片.textBox6.Text = 参数[0];
                                发条.发条窗口.查找图片.textBox7.Text = 参数[1];
                                发条.发条窗口.查找图片.textBox8.Text = 参数[2];
                                发条.发条窗口.查找图片.textBox2.Text = 注释参数;
                                发条.发条窗口.查找图片.textBox3.Text = 参数六;
                                发条.发条窗口.查找图片.textBox4.Text = 参数四[0];
                                发条.发条窗口.查找图片.textBox9.Text = 参数四[1];
                                发条.发条窗口.查找图片.textBox5.Text = 参数四[2];
                                发条.发条窗口.查找图片.textBox10.Text = 参数四[3];
                                发条.发条窗口.查找图片.trackBar1.Value = int.Parse(参数四[4]);

                                if (发条.发条窗口.判断数字(参数四[0]) == "非数字")
                                {
                                    发条.发条窗口.查找图片.button15.ForeColor = Color.Maroon;
                                }
                                if (发条.发条窗口.判断数字(参数四[2]) == "非数字")
                                {
                                    发条.发条窗口.查找图片.button17.ForeColor = Color.Maroon;
                                }
                                if (发条.发条窗口.判断数字(参数四[1]) == "非数字")
                                {
                                    发条.发条窗口.查找图片.button14.ForeColor = Color.Maroon;
                                }
                                if (发条.发条窗口.判断数字(参数四[3]) == "非数字")
                                {
                                    发条.发条窗口.查找图片.button16.ForeColor = Color.Maroon;
                                }

                                发条.发条窗口.查找图片.button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            catch
                            {
                                注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                                发条.发条窗口.查找图片.textBox6.Text = 参数[0];
                                发条.发条窗口.查找图片.textBox7.Text = 参数[1];
                                发条.发条窗口.查找图片.textBox8.Text = 参数[2];
                                发条.发条窗口.查找图片.textBox2.Text = 注释参数;
                                发条.发条窗口.查找图片.textBox3.Text = 参数[3];
                                发条.发条窗口.查找图片.textBox4.Text = 参数[4];
                                发条.发条窗口.查找图片.textBox9.Text = 参数[5];
                                发条.发条窗口.查找图片.textBox5.Text = 参数[6];
                                发条.发条窗口.查找图片.textBox10.Text = 参数[7];
                                发条.发条窗口.查找图片.trackBar1.Value = int.Parse(参数[8]);

                                发条.发条窗口.查找图片.button5.ForeColor = Color.Maroon;
                            }

                            发条.发条窗口.查找图片.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.查找图片.button13.Visible = true;//显示“修改”按键

                            发条.发条窗口.查找图片.Show();

                            break;
                        case "查找颜色":
                            try
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）

                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容
                                参数四 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割出自定义内容之后的参数
                                注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                                发条.发条窗口.查找颜色.textBox2.Text = 注释参数;
                                发条.发条窗口.查找颜色.textBox6.Text = 参数[0];
                                发条.发条窗口.查找颜色.textBox7.Text = 参数[1];
                                发条.发条窗口.查找颜色.textBox8.Text = 参数[2];
                                发条.发条窗口.查找颜色.textBox3.Text = 参数六;
                                发条.发条窗口.查找颜色.textBox4.Text = 参数四[0];
                                发条.发条窗口.查找颜色.textBox9.Text = 参数四[1];
                                发条.发条窗口.查找颜色.textBox5.Text = 参数四[2];
                                发条.发条窗口.查找颜色.textBox10.Text = 参数四[3];
                                发条.发条窗口.查找颜色.trackBar1.Value = int.Parse(参数四[4]);

                                if (发条.发条窗口.判断数字(参数四[0]) == "非数字")
                                {
                                    发条.发条窗口.查找颜色.button15.ForeColor = Color.Maroon;
                                }
                                if (发条.发条窗口.判断数字(参数四[2]) == "非数字")
                                {
                                    发条.发条窗口.查找颜色.button17.ForeColor = Color.Maroon;
                                }
                                if (发条.发条窗口.判断数字(参数四[1]) == "非数字")
                                {
                                    发条.发条窗口.查找颜色.button14.ForeColor = Color.Maroon;
                                }
                                if (发条.发条窗口.判断数字(参数四[3]) == "非数字")
                                {
                                    发条.发条窗口.查找颜色.button16.ForeColor = Color.Maroon;
                                }
                                发条.发条窗口.查找颜色.button6.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            catch
                            {
                                注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                                参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                                发条.发条窗口.查找颜色.textBox2.Text = 注释参数;
                                发条.发条窗口.查找颜色.textBox6.Text = 参数[0];
                                发条.发条窗口.查找颜色.textBox7.Text = 参数[1];
                                发条.发条窗口.查找颜色.textBox8.Text = 参数[2];
                                发条.发条窗口.查找颜色.textBox3.Text = 参数[3];
                                发条.发条窗口.查找颜色.textBox4.Text = 参数[4];
                                发条.发条窗口.查找颜色.textBox9.Text = 参数[5];
                                发条.发条窗口.查找颜色.textBox5.Text = 参数[6];
                                发条.发条窗口.查找颜色.textBox10.Text = 参数[7];
                                发条.发条窗口.查找颜色.trackBar1.Value = int.Parse(参数[8]);

                                发条.发条窗口.查找颜色.button6.ForeColor = Color.Maroon;
                            }

                            发条.发条窗口.查找颜色.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.查找颜色.button12.Visible = true;//显示“修改”按键

                            发条.发条窗口.查找颜色.Show();
                            break;
                        case "判断颜色":
                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                            参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容
                            参数四 = 参数七.Split(new string[] { "," }, StringSplitOptions.None);//分割出自定义内容之后的参数
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割条件和结果

                            if (发条.发条窗口.判断数字(参数[1]) == "非数字")
                            {
                                发条.发条窗口.判断颜色.button8.ForeColor = Color.Maroon;//红色
                            }
                            if (发条.发条窗口.判断数字(参数[2]) == "非数字")
                            {
                                发条.发条窗口.判断颜色.button9.ForeColor = Color.Maroon;//红色
                            }

                            发条.发条窗口.判断颜色.textBox2.Text = 注释参数;
                            发条.发条窗口.判断颜色.textBox4.Text = 参数[0];
                            发条.发条窗口.判断颜色.textBox3.Text = 参数[1];
                            发条.发条窗口.判断颜色.textBox7.Text = 参数[2];
                            发条.发条窗口.判断颜色.textBox5.Text = 参数六;
                            发条.发条窗口.判断颜色.trackBar1.Value = int.Parse(参数四[0]);
                            发条.发条窗口.判断颜色.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.判断颜色.button6.Visible = true;//显示“修改”按键

                            发条.发条窗口.判断颜色.Show();

                            break;
                        case "等待时间":
                            参数六 = 参数一;//

                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            if (发条.发条窗口.判断数字(参数六) == "非数字")
                            {
                                发条.发条窗口.等待时间.button4.ForeColor = Color.Maroon;//红色
                            }

                            发条.发条窗口.等待时间.textBox1.Text = 参数六;
                            发条.发条窗口.等待时间.textBox2.Text = 注释参数;
                            发条.发条窗口.等待时间.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.等待时间.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.等待时间.Show();

                            break;
                        case "删除文件":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾

                            发条.发条窗口.删除文件.textBox3.Text = 参数[0];
                            发条.发条窗口.删除文件.textBox5.Text = 参数六;
                            发条.发条窗口.删除文件.textBox2.Text = 注释参数;
                            发条.发条窗口.删除文件.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.删除文件.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.删除文件.Show();
                            break;
                        case "运行与打开":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                            参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾

                            发条.发条窗口.运行与打开.textBox3.Text = 参数[0];
                            发条.发条窗口.运行与打开.textBox5.Text = 参数六;
                            发条.发条窗口.运行与打开.textBox2.Text = 注释参数;
                            发条.发条窗口.运行与打开.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.运行与打开.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.运行与打开.Show();
                            break;
                        case "基于平面":
                            if (参数[1] == "桌面内")
                            {
                                发条.发条窗口.基于平面.comboBox2.Text = 参数[2];
                            }
                            else
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                参数七 = 参数二[1].Remove(0, 右至左 + 2);//右边位置之后的内容
                                发条.发条窗口.基于平面.comboBox2.Text = 参数七;
                                发条.发条窗口.基于平面.textBox3.Text = 参数六;
                                发条.发条窗口.基于平面.comboBox1.Text = 参数[2];
                            }


                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.基于平面.textBox4.Text = 参数[0];
                            发条.发条窗口.基于平面.comboBox4.Text = 参数[1];


                            发条.发条窗口.基于平面.textBox2.Text = 注释参数;
                            发条.发条窗口.基于平面.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.基于平面.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.基于平面.Show();
                            break;
                        case "设置窗口":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数七 = 参数二[1].Substring(0, 右至左);//自定义内容
                            参数九 = 参数二[1].Substring(右至左 + 2);//选项
                            参数八 = 参数九.Split(new string[] { "," }, StringSplitOptions.None);//定位自定义内容最左端

                            发条.发条窗口.设置窗口.textBox2.Text = 注释参数;
                            发条.发条窗口.设置窗口.textBox3.Text = 参数[0];
                            发条.发条窗口.设置窗口.textBox8.Text = 参数七;
                            发条.发条窗口.设置窗口.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.设置窗口.button3.Visible = true;//显示“修改”按键

                            switch (参数八[0])
                            {
                                case "窗口大小":
                                    发条.发条窗口.设置窗口.comboBox1.Text = 参数八[0];
                                    发条.发条窗口.设置窗口.textBox4.Text = 参数八[1];
                                    发条.发条窗口.设置窗口.textBox5.Text = 参数八[2];
                                    break;
                                case "客户区大小":
                                    发条.发条窗口.设置窗口.comboBox1.Text = 参数八[0];
                                    发条.发条窗口.设置窗口.textBox4.Text = 参数八[1];
                                    发条.发条窗口.设置窗口.textBox5.Text = 参数八[2];
                                    break;
                                case "窗口状态":
                                    发条.发条窗口.设置窗口.comboBox1.Text = 参数八[0];
                                    发条.发条窗口.设置窗口.comboBox2.Text = 参数八[1];
                                    break;
                                case "窗口标题":
                                    发条.发条窗口.设置窗口.comboBox1.Text = 参数九;
                                    参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                    右至左 = 参数二[2].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                                    参数六 = 参数二[2].Substring(0, 右至左);//自定义内容
                                    发条.发条窗口.设置窗口.textBox6.Text = 参数六;
                                    break;
                                case "窗口位置":
                                    发条.发条窗口.设置窗口.comboBox1.Text = 参数八[0];
                                    发条.发条窗口.设置窗口.textBox7.Text = 参数八[1] + "," + 参数八[2];
                                    break;
                                case "窗口透明度":
                                    发条.发条窗口.设置窗口.comboBox1.Text = 参数八[0];
                                    发条.发条窗口.设置窗口.trackBar1.Value = int.Parse(参数八[1]);
                                    break;
                            }

                            发条.发条窗口.设置窗口.Show();
                            break;
                        case "鼠标操作":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.鼠标操作.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.鼠标操作.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.鼠标操作.textBox2.Text = 注释参数;

                            发条.发条窗口.鼠标操作.comboBox1.Text = 参数[0];

                            switch (参数[0])
                            {
                                case "绝对移动":
                                    if (发条.发条窗口.判断数字(参数[2]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button17.ForeColor = Color.Maroon;
                                    }
                                    if (发条.发条窗口.判断数字(参数[3]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button16.ForeColor = Color.Maroon;
                                    }
                                    发条.发条窗口.鼠标操作.comboBox2.Text = 参数[1];
                                    发条.发条窗口.鼠标操作.textBox3.Text = 参数[2];
                                    发条.发条窗口.鼠标操作.textBox10.Text = 参数[3];
                                    break;
                                case "随机移动":
                                    if (发条.发条窗口.判断数字(参数[2]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button15.ForeColor = Color.Maroon;
                                    }
                                    if (发条.发条窗口.判断数字(参数[3]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button14.ForeColor = Color.Maroon;
                                    }
                                    if (发条.发条窗口.判断数字(参数[4]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button8.ForeColor = Color.Maroon;
                                    }
                                    if (发条.发条窗口.判断数字(参数[5]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button7.ForeColor = Color.Maroon;
                                    }
                                    发条.发条窗口.鼠标操作.comboBox2.Text = 参数[1];
                                    发条.发条窗口.鼠标操作.textBox4.Text = 参数[2];
                                    发条.发条窗口.鼠标操作.textBox9.Text = 参数[3];
                                    发条.发条窗口.鼠标操作.textBox5.Text = 参数[4];
                                    发条.发条窗口.鼠标操作.textBox6.Text = 参数[5];
                                    break;
                                case "相对移动":
                                    if (发条.发条窗口.判断数字(参数[2]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button17.ForeColor = Color.Maroon;
                                    }
                                    if (发条.发条窗口.判断数字(参数[3]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button16.ForeColor = Color.Maroon;
                                    }
                                    发条.发条窗口.鼠标操作.comboBox2.Text = 参数[1];
                                    发条.发条窗口.鼠标操作.textBox3.Text = 参数[2];
                                    发条.发条窗口.鼠标操作.textBox10.Text = 参数[3];
                                    break;
                                case "轨迹移动":
                                    if (发条.发条窗口.判断数字(参数[2]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button17.ForeColor = Color.Maroon;
                                    }
                                    if (发条.发条窗口.判断数字(参数[3]) == "非数字")
                                    {
                                        发条.发条窗口.鼠标操作.button16.ForeColor = Color.Maroon;
                                    }
                                    发条.发条窗口.鼠标操作.comboBox2.Text = 参数[1];
                                    发条.发条窗口.鼠标操作.textBox4.Text = 参数[2];
                                    发条.发条窗口.鼠标操作.textBox9.Text = 参数[3];
                                    发条.发条窗口.鼠标操作.textBox5.Text = 参数[4];
                                    发条.发条窗口.鼠标操作.textBox6.Text = 参数[5];
                                    发条.发条窗口.鼠标操作.numericUpDown1.Value = int.Parse(参数[6]);
                                    发条.发条窗口.鼠标操作.numericUpDown2.Value = int.Parse(参数[7]);
                                    break;
                                case "单击":
                                    发条.发条窗口.鼠标操作.comboBox3.Text = 参数[1];
                                    break;
                                case "双击":
                                    发条.发条窗口.鼠标操作.comboBox3.Text = 参数[1];
                                    break;
                                case "长按":
                                    发条.发条窗口.鼠标操作.comboBox3.Text = 参数[1];
                                    break;
                                case "松开":
                                    发条.发条窗口.鼠标操作.comboBox3.Text = 参数[1];
                                    break;
                                case "滚轮单击":
                                    发条.发条窗口.鼠标操作.comboBox2.Text = 参数[1];
                                    break;
                                case "滚轮上滑":
                                    发条.发条窗口.鼠标操作.comboBox2.Text = 参数[1];
                                    break;
                                case "滚轮下滑":
                                    发条.发条窗口.鼠标操作.comboBox2.Text = 参数[1];
                                    break;
                            }

                            发条.发条窗口.鼠标操作.Show();
                            break;
                        case "按键操作":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.按键操作.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.按键操作.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.按键操作.textBox2.Text = 注释参数;

                            发条.发条窗口.按键操作.comboBox1.Text = 参数[0];
                            if (参数[1].Contains("\""))
                            {
                                参数[1] = 参数[1].Substring(1, 参数[1].Length - 2);//掐头去尾
                                发条.发条窗口.按键操作.textBox3.Text = 参数[1];
                                发条.发条窗口.按键操作.button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                if (发条.发条窗口.判断数字(参数[1]) == "非数字")
                                {
                                    发条.发条窗口.按键操作.textBox4.Text = 参数[1];
                                    发条.发条窗口.按键操作.button5.ForeColor = Color.Maroon;//红色
                                }
                                else
                                {
                                    发条.发条窗口.按键操作.textBox3.Text = 参数[1];
                                    发条.发条窗口.按键操作.button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                }
                            }

                            发条.发条窗口.按键操作.Show();
                            break;
                        case "系统电源":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.系统电源.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.系统电源.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.系统电源.textBox2.Text = 注释参数;

                            发条.发条窗口.系统电源.comboBox1.Text = 参数一;
                            发条.发条窗口.系统电源.Show();
                            break;
                        case "蜂鸣提醒":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.蜂鸣提醒.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.蜂鸣提醒.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.蜂鸣提醒.textBox2.Text = 注释参数;

                            发条.发条窗口.蜂鸣提醒.textBox3.Text = 参数一;
                            发条.发条窗口.蜂鸣提醒.Show();
                            break;
                        case "下载文件":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//定位保存目录名最左端
                            右至左 = 参数二[1].LastIndexOf("\",");//定位右边的位置（从右到左定位）
                            参数六 = 参数二[1].Substring(0, 右至左);//保存目录+文件名
                            参数九 = Path.GetFileName(参数六);//文件名
                            参数六 = Path.GetDirectoryName(参数六) + @"\";//仅目录
                            参数七 = 参数二[1].Remove(0, 右至左 + 2);//是否覆盖
                            string 下载网址 = 参数[1].Substring(1, 参数[1].Length - 2);//定位网址

                            发条.发条窗口.下载文件.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.下载文件.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.下载文件.textBox2.Text = 注释参数;
                            发条.发条窗口.下载文件.textBox5.Text = 参数九;
                            发条.发条窗口.下载文件.textBox6.Text = 参数[0];
                            发条.发条窗口.下载文件.textBox3.Text = 下载网址;
                            发条.发条窗口.下载文件.textBox4.Text = 参数六;
                            发条.发条窗口.下载文件.comboBox1.Text = 参数七;

                            发条.发条窗口.下载文件.Show();
                            break;
                        case "写到剪贴板":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容


                            if (参数一.Contains("\""))
                            {
                                参数二 = 参数一.Split(new string[] { "\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                参数一 = 参数二[1].Substring(0, 参数二[1].Length);//去尾
                                发条.发条窗口.写到剪贴板.button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                发条.发条窗口.写到剪贴板.button17.ForeColor = Color.Maroon;//红色
                            }


                            发条.发条窗口.写到剪贴板.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.写到剪贴板.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.写到剪贴板.textBox2.Text = 注释参数;

                            发条.发条窗口.写到剪贴板.textBox3.Text = 参数一;
                            发条.发条窗口.写到剪贴板.Show();
                            break;
                        case "发送文本":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            if (参数一.Contains("\",\""))
                            {
                                参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                参数七 = 参数二[0].Substring(1);//去头
                                参数六 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                                发条.发条窗口.发送文本.button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                参数二 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割自定义内容最左端
                                参数七 = 参数二[0].Substring(1, 参数二[0].Length - 2);//掐头去尾
                                参数六 = 参数二[1];
                                发条.发条窗口.发送文本.button17.ForeColor = Color.Maroon;//红色
                            }


                            发条.发条窗口.发送文本.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.发送文本.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.发送文本.textBox2.Text = 注释参数;

                            发条.发条窗口.发送文本.textBox3.Text = 参数七;
                            发条.发条窗口.发送文本.textBox4.Text = 参数六;
                            发条.发条窗口.发送文本.Show();
                            break;
                        case "发送邮件":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容
                            参数二 = 参数一.Split(new string[] { "\",\"" }, StringSplitOptions.None);//分割自定义内容最左端

                            参数四 = 参数二[0].Split(new string[] { ",\"" }, StringSplitOptions.None);//分割端口和服务器一次
                            参数三 = 参数四[1].Split(new string[] { "\"," }, StringSplitOptions.None);//分割端口和服务器二次
                            参数五 = 参数三[0].Split(new string[] { "“" }, StringSplitOptions.None);//分割服务器
                            参数八 = 参数二[4].Split(new string[] { "\"" }, StringSplitOptions.None);//分割内容

                            发条.发条窗口.发送邮件.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.发送邮件.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.发送邮件.textBox2.Text = 注释参数;
                            发条.发条窗口.发送邮件.textBox10.Text = 参数[0];//变量
                            发条.发条窗口.发送邮件.textBox3.Text = 参数五[0];//服务器
                            发条.发条窗口.发送邮件.textBox4.Text = 参数三[1];//端口
                            发条.发条窗口.发送邮件.textBox5.Text = 参数四[2];//发件邮箱
                            发条.发条窗口.发送邮件.textBox6.Text = 参数二[1];//授权码
                            发条.发条窗口.发送邮件.textBox7.Text = 参数二[2];//收件邮箱
                            发条.发条窗口.发送邮件.textBox8.Text = 参数二[3];//标题
                            发条.发条窗口.发送邮件.textBox9.Text = 参数八[0];//内容
                            发条.发条窗口.发送邮件.Show();
                            break;
                        case "打开网址":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            if (参数一.Contains(",\""))
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//分割自定义内容最左端
                                参数一 = 参数二[1].Substring(0, 参数二[1].Length - 1);//去尾
                                发条.发条窗口.打开网址.button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                参数二 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割自定义内容最左端
                                参数一 = 参数二[1];
                                发条.发条窗口.打开网址.button17.ForeColor = Color.Maroon;//红色
                            }


                            发条.发条窗口.打开网址.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.打开网址.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.打开网址.textBox2.Text = 注释参数;
                            发条.发条窗口.打开网址.textBox4.Text = 参数[0];
                            发条.发条窗口.打开网址.textBox3.Text = 参数一;
                            发条.发条窗口.打开网址.Show();
                            break;
                        case "消息提示":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            if (参数一.Contains(",\""))
                            {
                                参数二 = 参数一.Split(new string[] { ",\"" }, StringSplitOptions.None);//定位自定义内容最左端
                                右至左 = 参数二[1].LastIndexOf("\"");//定位右边的位置（从右到左定位）
                                参数六 = 参数二[1].Substring(0, 右至左);//自定义内容
                                发条.发条窗口.消息提示.textBox3.Text = 参数六;
                                if (参数[1] == "弹窗消息")
                                {
                                    参数七 = 参数二[1].Substring(参数二[1].Length - 4, 4);//窗口类型
                                    发条.发条窗口.消息提示.comboBox2.Text = 参数七;
                                }
                                发条.发条窗口.消息提示.button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                发条.发条窗口.消息提示.textBox3.Text = 参数[2];
                                if (参数[1] == "弹窗消息")
                                {
                                    发条.发条窗口.消息提示.comboBox2.Text = 参数[3];
                                }
                                发条.发条窗口.消息提示.button5.ForeColor = Color.Maroon;//红色
                            }

                            发条.发条窗口.消息提示.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.消息提示.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.消息提示.textBox2.Text = 注释参数;

                            发条.发条窗口.消息提示.comboBox1.Text = 参数[1];

                            发条.发条窗口.消息提示.textBox6.Text = 参数[0];

                            发条.发条窗口.消息提示.Show();
                            break;
                        case "整数运算":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.整数运算.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.整数运算.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.整数运算.textBox2.Text = 注释参数;

                            if (参数一.Contains("+=")) //是否包含+=复合运算符号
                            {
                                参数四 = 参数一.Split(new string[] { "+=" }, StringSplitOptions.None);//关系符分割
                                发条.发条窗口.整数运算.comboBox3.Text = "+=";
                            }
                            else
                            {
                                if (参数一.Contains("++")) //是否包含“自增”符号
                                {
                                    参数四 = 参数一.Split(new string[] { "++" }, StringSplitOptions.None);//关系符分割
                                    发条.发条窗口.整数运算.comboBox3.Text = "++";
                                }
                                else
                                {
                                    if (参数一.Contains("--")) //是否包含“自减”符号
                                    {
                                        参数四 = 参数一.Split(new string[] { "--" }, StringSplitOptions.None);//关系符分割
                                        发条.发条窗口.整数运算.comboBox3.Text = "--";
                                    }
                                    else
                                    {

                                        if (参数一.Contains("-=")) //是否包含“减等于”符号
                                        {
                                            参数四 = 参数一.Split(new string[] { "-=" }, StringSplitOptions.None);//关系符分割
                                            发条.发条窗口.整数运算.comboBox3.Text = "-=";
                                        }
                                        else
                                        {
                                            参数四 = 参数一.Split(new string[] { "=" }, StringSplitOptions.None);//关系符分割
                                            发条.发条窗口.整数运算.comboBox3.Text = "=";

                                        }
                                    }
                                }
                            }

                            发条.发条窗口.整数运算.textBox3.Text = 参数四[0];
                            发条.发条窗口.整数运算.textBox4.Text = 参数四[1];

                            发条.发条窗口.整数运算.Show();
                            break;

                        case "注释":

                            发条.发条窗口.注释.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.注释.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.注释.textBox3.Text = 参数一;

                            发条.发条窗口.注释.Show();
                            break;

                        case "结束进程":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            参数六 = 参数一.Substring(1, 参数一.Length - 2);//掐头去尾

                            发条.发条窗口.结束进程.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.结束进程.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.结束进程.textBox3.Text = 注释参数;

                            if (参数一 == "结束自身")
                            {
                                发条.发条窗口.结束进程.checkBox1.Checked = true;
                            }
                            else
                            {
                                发条.发条窗口.结束进程.textBox1.Text = 参数六;
                            }

                            发条.发条窗口.结束进程.Show();
                            break;
                        case "﹁如果始":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.如果事件.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.如果事件.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.如果事件.textBox2.Text = 注释参数;

                            if (参数一.Contains("不等于"))
                            {
                                参数三 = 参数一.Split(new string[] { "不等于" }, StringSplitOptions.None);//关系符分割
                                发条.发条窗口.如果事件.comboBox3.Text = "不等于";
                            }
                            else if (参数一.Contains("等于"))
                            {
                                参数三 = 参数一.Split(new string[] { "等于" }, StringSplitOptions.None);//关系符分割
                                发条.发条窗口.如果事件.comboBox3.Text = "等于";
                            }
                            else if (参数一.Contains("大于"))
                            {
                                参数三 = 参数一.Split(new string[] { "大于" }, StringSplitOptions.None);//关系符分割
                                发条.发条窗口.如果事件.comboBox3.Text = "大于";
                            }
                            else
                            {
                                参数三 = 参数一.Split(new string[] { "小于" }, StringSplitOptions.None);//关系符分割
                                发条.发条窗口.如果事件.comboBox3.Text = "小于";
                            }

                            if (参数三[0].Contains("\""))
                            {
                                参数三[0] = 参数三[0].Substring(1, 参数三[0].Length - 2);//掐头去尾
                                发条.发条窗口.如果事件.button4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                发条.发条窗口.如果事件.button4.ForeColor = Color.Maroon;//红色
                            }
                            发条.发条窗口.如果事件.textBox3.Text = 参数三[0];

                            if (参数三[1].Contains("\""))
                            {
                                参数三[1] = 参数三[1].Substring(1, 参数三[1].Length - 2);//掐头去尾
                                发条.发条窗口.如果事件.button5.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                发条.发条窗口.如果事件.button5.ForeColor = Color.Maroon;//红色
                            }
                            发条.发条窗口.如果事件.textBox4.Text = 参数三[1];

                            发条.发条窗口.如果事件.Show();
                            break;
                        case "﹁循环始":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            if (发条.发条窗口.判断数字(参数一) == "非数字")
                            {
                                发条.发条窗口.循环过程.button4.ForeColor = Color.Maroon;//红色
                            }

                            发条.发条窗口.循环过程.textBox3.Text = 参数一;
                            发条.发条窗口.循环过程.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.循环过程.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.循环过程.textBox2.Text = 注释参数;

                            发条.发条窗口.循环过程.Show();
                            break;
                        case "跳出循环":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.跳出循环.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.跳出循环.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.跳出循环.textBox2.Text = 注释参数;

                            发条.发条窗口.跳出循环.Show();
                            break;

                        case "停止运行":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            发条.发条窗口.停止运行.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.停止运行.button3.Visible = true;//显示“修改”按键
                            发条.发条窗口.停止运行.textBox2.Text = 注释参数;

                            发条.发条窗口.停止运行.Show();
                            break;

                        case "字符拼接":
                            注释参数 = 参数九.Substring(右括号 + 2);//选取注释内容

                            参数二 = 参数一.Split(new string[] { "," }, StringSplitOptions.None);//分割自定义内容最左端

                            if (参数二[0].Contains("\""))
                            {
                                参数二[0] = 参数二[0].Substring(1, 参数二[0].Length - 2);//掐头去尾
                                发条.发条窗口.字符拼接.button17.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                                
                            }
                            else
                            {
                                发条.发条窗口.字符拼接.button17.ForeColor = Color.Maroon;//红色
                            }

                            if (参数二[1].Contains("\""))
                            {
                                参数二[1] = 参数二[1].Substring(1, 参数二[1].Length - 2);//掐头去尾
                                发条.发条窗口.字符拼接.button4.ForeColor = Color.FromArgb(0, 120, 215);//蓝色
                            }
                            else
                            {
                                发条.发条窗口.字符拼接.button4.ForeColor = Color.Maroon;//红色
                                
                            }

                            发条.发条窗口.字符拼接.button1.Visible = false;//隐藏“完成”按键
                            发条.发条窗口.字符拼接.button3.Visible = true;//显示“修改”按键

                            发条.发条窗口.字符拼接.textBox4.Text = 参数二[0];
                            发条.发条窗口.字符拼接.textBox3.Text = 参数二[1];
                            发条.发条窗口.字符拼接.textBox5.Text = 参数二[2];
                            发条.发条窗口.字符拼接.textBox2.Text = 注释参数;

                            发条.发条窗口.字符拼接.Show();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                动作类型.添加日志(ex.Message);
                发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "发条错误", "当前行参数不完整或不正确，请尝试重新编辑后运行", ToolTipIcon.Error);
                return;
            }
        }
    }
}
