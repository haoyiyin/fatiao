using Gma.System.MouseKeyHook;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using 动作库;

namespace fatiao
{
    public class 触边钩子
    {

        private IKeyboardMouseEvents hook;

        public static 快捷命令 快捷命令 = new 快捷命令();

        public void SetHook()//注册钩子
        {
            hook = Hook.GlobalEvents();     //全局
            hook.MouseDownExt += 鼠标点击;
            hook.MouseWheelExt += 鼠标滚动;
            hook.MouseDownExt += 鼠标轮盘;
            hook.MouseUpExt += 松开鼠标;
            hook.MouseMoveExt += 移动鼠标;
        }

        void 鼠标滚动(object sender, MouseEventExtArgs e)//滚轮滚动
        {


            int 当前x, 当前y;
            int 屏幕高 = Screen.PrimaryScreen.Bounds.Height;
            int 屏幕宽 = Screen.PrimaryScreen.Bounds.Width;

            动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标
            当前x = 坐标.X;
            当前y = 坐标.Y;

            if (当前x <= 10 && 当前y >= 10)//左边事件
            {

                if (e.Delta > 0)        //鼠标滚轮向前
                {
                    switch (发条.发条窗口.listView1.Items[0].SubItems[2].Text)
                    {
                        case "无":

                            break;
                        case "当前流程":
                            发条.发条窗口.toolStripButton5_Click(sender, e);
                            break;
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[0].SubItems[2].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
                else
                {
                    switch (发条.发条窗口.listView1.Items[0].SubItems[1].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[0].SubItems[1].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
            }

            if (当前x > 屏幕宽 - 10 && 当前y < 屏幕高 - 10)//右边事件
            {
                if (e.Delta > 0)        //鼠标滚轮向前
                {
                    switch (发条.发条窗口.listView1.Items[1].SubItems[2].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[1].SubItems[2].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
                else
                {
                    switch (发条.发条窗口.listView1.Items[1].SubItems[1].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[1].SubItems[1].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }

            }

            if (当前y <= 10 && 当前x >= 10)//顶边事件
            {
                if (e.Delta > 0)        //鼠标滚轮向前
                {
                    switch (发条.发条窗口.listView1.Items[2].SubItems[2].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[2].SubItems[2].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }

                }
                else
                {
                    switch (发条.发条窗口.listView1.Items[2].SubItems[1].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[2].SubItems[1].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }

                }

            }

            if (当前y > 屏幕高 - 10 && 当前x < 屏幕宽 - 10)//底边事件
            {
                if (e.Delta > 0)        //鼠标滚轮向前
                {
                    switch (发条.发条窗口.listView1.Items[3].SubItems[2].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[3].SubItems[2].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
                else
                {
                    switch (发条.发条窗口.listView1.Items[3].SubItems[1].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[3].SubItems[1].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }

            }
        }

        void 鼠标点击(object sender, MouseEventExtArgs e)//滚轮点击
        {
            int 当前x, 当前y;
            int 屏幕高 = Screen.PrimaryScreen.Bounds.Height;
            int 屏幕宽 = Screen.PrimaryScreen.Bounds.Width;

            动作类型.GetCursorPos(out Point 坐标);//获取鼠标坐标
            当前x = 坐标.X;
            当前y = 坐标.Y;

            if (当前x <= 10 && 当前y >= 10)//左边事件
            {
                if (e.Button == MouseButtons.Middle)
                {
                    switch (发条.发条窗口.listView1.Items[0].SubItems[3].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[0].SubItems[3].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
            }

            if (当前x > 屏幕宽 - 10 && 当前y < 屏幕高 - 10)//右边事件
            {
                if (e.Button == MouseButtons.Middle)
                {
                    switch (发条.发条窗口.listView1.Items[1].SubItems[3].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[1].SubItems[3].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
            }

            if (当前y <= 10 && 当前x >= 10)//顶边事件
            {
                if (e.Button == MouseButtons.Middle)
                {
                    switch (发条.发条窗口.listView1.Items[2].SubItems[3].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[2].SubItems[3].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
            }

            if (当前y > 屏幕高 - 10 && 当前x < 屏幕宽 - 10)//底边事件
            {
                if (e.Button == MouseButtons.Middle)
                {
                    switch (发条.发条窗口.listView1.Items[3].SubItems[3].Text)
                    {
                        case "无":

                            break;
                        /*                        case "当前流程":
                                                    发条.发条窗口.toolStripButton5_Click(sender, e);
                                                    break;*/
                        default:
                            发条.发条窗口.快捷命令载入(发条.发条窗口.listView1.Items[3].SubItems[3].Text);
                            快捷命令.快捷命令运行();
                            break;
                    }
                }
            }
        }


        void 鼠标轮盘(object sender, MouseEventExtArgs e)
        {
            if (发条.发条窗口.checkBox3.Checked)
            {
                if (发条.发条窗口.启动器.Visible)
                {
                    发条.发条窗口.启动器.Hide();//鼠标轮盘
                }
                switch (发条.发条窗口.comboBox1.Text)
                {
                    case "鼠标中键":
                        if (e.Button == MouseButtons.Middle)
                        {
                            hook.MouseMoveExt += 拖动展开;
                            
                        }
                        break;
                    case "鼠标右键":
                        if (e.Button == MouseButtons.Right)
                        {
                            hook.MouseMoveExt += 拖动展开;
                        }
                        break;
                    case "鼠标X1键":
                        if (e.Button == MouseButtons.XButton1)
                        {
                            hook.MouseMoveExt += 拖动展开;
                        }
                        break;
                    case "鼠标X2键":
                        if (e.Button == MouseButtons.XButton2)
                        {
                            hook.MouseMoveExt += 拖动展开;
                        }
                        break;
                }
            }
        }

        void 拖动展开(object sender, MouseEventArgs e)
        {
            if (发条.发条窗口.checkBox3.Checked)
            {
                switch (发条.发条窗口.comboBox1.Text)
                {
                    case "鼠标中键":
                        if (动作.判断按键(4))
                        {

                            发条.发条窗口.启动器.Show();//鼠标轮盘

                        }
                        break;
                    case "鼠标右键":
                        if (动作.判断按键(2))
                        {

                            发条.发条窗口.启动器.Show();//鼠标轮盘

                        }
                        break;
                    case "鼠标X1键":
                        if (动作.判断按键(5))
                        {

                            发条.发条窗口.启动器.Show();//鼠标轮盘

                        }
                        break;
                    case "鼠标X2键":
                        if (动作.判断按键(6))
                        {

                            发条.发条窗口.启动器.Show();//鼠标轮盘

                        }
                        break;
                }
                hook.MouseMoveExt -= 拖动展开;
            }

        }

        Rectangle 关闭轮盘 = new Rectangle(128, 128, 45, 45);
        Rectangle 上轮盘 = new Rectangle(96, 4, 109, 105);
        Rectangle 下轮盘 = new Rectangle(96, 186, 109, 114);
        Rectangle 左轮盘 = new Rectangle(1, 107, 113, 87);
        Rectangle 右轮盘 = new Rectangle(187, 107, 112, 87);
        Point 坐标;
        void 松开鼠标(object sender, MouseEventArgs e)
        {
           if(发条.发条窗口.启动器.Visible)
            {

                动作类型.GetCursorPos(out 坐标);

                if (关闭轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)))
                {
                    发条.发条窗口.启动器.Hide();
                    return;
                }

                if (上轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)))
                {
                    发条.发条窗口.启动器.Hide();
                    if (发条.发条窗口.label12.Text!="无")
                    {
                        发条.发条窗口.快捷命令载入(发条.发条窗口.label12.Text);
                        发条.快捷命令.快捷命令运行();
                    }

                    return;
                }

                if (下轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)))
                {
                    发条.发条窗口.启动器.Hide();
                    if (发条.发条窗口.label13.Text != "无")
                    {
                        发条.发条窗口.快捷命令载入(发条.发条窗口.label13.Text);
                        发条.快捷命令.快捷命令运行();
                    }

                    return;
                }


                if (左轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)))
                {
                    发条.发条窗口.启动器.Hide();
                    if (发条.发条窗口.label9.Text != "无")
                    {
                        发条.发条窗口.快捷命令载入(发条.发条窗口.label9.Text);
                        发条.快捷命令.快捷命令运行();
                    }

                    return;
                }


                if (右轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)))
                {
                    发条.发条窗口.启动器.Hide();
                    if (发条.发条窗口.label14.Text != "无")
                    {
                        发条.发条窗口.快捷命令载入(发条.发条窗口.label14.Text);
                        发条.快捷命令.快捷命令运行();
                    }

                    return;
                }

                if (!关闭轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && !上轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && !下轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && !左轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && !右轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)))
                {
                    发条.发条窗口.启动器.Hide();
                    return;
                }
            }

        }

        void 移动鼠标(object sender, MouseEventArgs e)
        {
            if (发条.发条窗口.启动器.Visible)
            {
                动作类型.GetCursorPos(out 坐标);

                if (关闭轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && 发条.发条窗口.启动器.BackgroundImage != Properties.Resources.高亮关闭)
                {
                    发条.发条窗口.启动器.BackgroundImage = Properties.Resources.高亮关闭;
                }

                if (上轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && 发条.发条窗口.启动器.BackgroundImage != Properties.Resources.高亮上)
                {
                    发条.发条窗口.启动器.BackgroundImage = Properties.Resources.高亮上;
                }


                if (下轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && 发条.发条窗口.启动器.BackgroundImage != Properties.Resources.高亮下)
                {
                    发条.发条窗口.启动器.BackgroundImage = Properties.Resources.高亮下;
                }


                if (左轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && 发条.发条窗口.启动器.BackgroundImage != Properties.Resources.高亮左)
                {
                    发条.发条窗口.启动器.BackgroundImage = Properties.Resources.高亮左;
                }


                if (右轮盘.Contains(发条.发条窗口.启动器.PointToClient(坐标)) && 发条.发条窗口.启动器.BackgroundImage != Properties.Resources.高亮右)
                {
                    发条.发条窗口.启动器.BackgroundImage = Properties.Resources.高亮右;
                }
            }
      

        }

        public void UnSetHook()//卸载钩子
        {
            hook.MouseDownExt -= 鼠标点击;
            hook.MouseWheelExt -= 鼠标滚动;
            hook.MouseDownExt -= 鼠标轮盘;
            hook.MouseUpExt -= 松开鼠标;
            hook.MouseMoveExt -= 移动鼠标;
            hook.Dispose();
        }

    }
}
