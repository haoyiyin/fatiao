using Steamworks;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace fatiao
{
    public partial class steam创意工坊 : Form
    {

        [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SteamAPI_Init(); //steam初始化

        [DllImport("steam_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SteamAPI_RunCallbacks(); //steam回调

        public string foldPath;

        bool 退出线程 = false;

        public PublishedFileId_t 物品ID;

        public steam创意工坊()
        {
            InitializeComponent();
        }

        private CallResult<CreateItemResult_t> 创建物品回调;//注册创建物品回调

        private CallResult<SubmitItemUpdateResult_t> 上传物品回调;//注册上传物品回调

        动作类型 动作类型 = new 动作类型();
        private void steam创意工坊_Load(object sender, EventArgs e)
        {
                label10.Text = "";
                textBox1.Text = "流程" + DateTime.Now.ToString("yyyyMMddHHmmss");
            int iCount = 发条.发条窗口.listBox4.Items.Count - 1;

            if (发条.发条窗口.是否订阅)//当初始化成功
            {
                创建物品回调 = CallResult<CreateItemResult_t>.Create(完成创建物品);
                上传物品回调 = CallResult<SubmitItemUpdateResult_t>.Create(完成上传物品);
                //CSteamID steamID  = SteamUser.GetSteamID();//获取用户id
            }
            else//失败
                {
                    发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "未订阅", "Steam客户端未运行或未订阅应用", ToolTipIcon.Info);
                    Process.Start("https://store.steampowered.com/app/1416190/Fatiao/");
                    退出线程 = true;
                    Dispose();
                    Close();
                }

                ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback((object s) =>
                {
                    CheckForIllegalCrossThreadCalls = false;
                    while (退出线程 == false)
                    {
                        SteamAPI_RunCallbacks();//监视回调
                    }
                }), null);

            
        }

        void 打包物品()
        {
                if (!Directory.Exists(@".\Process"))//不存在就创建
                {
                    Directory.CreateDirectory(@".\Process");
                }
                else
                {
                    发条.发条窗口.DelectDir(@".\Process");
                }
            发条.发条窗口.CopyDirectory(@".\Command", @".\Process\Command");
            发条.发条窗口.CopyDirectory(@".\Generate", @".\Process\Generate");
                发条.发条窗口.CopyDirectory(@".\素材库", @".\Process\素材库");

                string 流程目录 = @".\Process";
                string 压缩文件 = Environment.CurrentDirectory + @"\work.fatiao";//创意工坊压缩文件
                if (File.Exists(压缩文件))//如果存在就删除（变相覆盖）
                {
                    File.Delete(压缩文件);
                }
                ZipFile.CreateFromDirectory(流程目录, 压缩文件);
                if (Directory.Exists(@".\Process"))//存在就删除
                {
                    发条.发条窗口.DelectDir(@".\Process");
                }
        }

        void 更新物品(ulong workshopid)//更新物品
        {
            Console.WriteLine("正在提交更新");
            label10.Text = "正在提交更新，请稍后……";

            打包物品();
            物品ID = (PublishedFileId_t)workshopid;
            UGCUpdateHandle_t 开始上传 = SteamUGC.StartItemUpdate((AppId_t)1416190, 物品ID);//调用上传函数
            SteamUGC.SetItemTitle(开始上传, textBox1.Text);//物品标题
            SteamUGC.SetItemVisibility(开始上传, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);//所有人可见
            SteamUGC.SetItemPreview(开始上传, foldPath);//封面图

                var tagArray = new[] { "Process" };
                SteamUGC.SetItemTags(开始上传, tagArray);//标签
                SteamUGC.SetItemContent(开始上传, Environment.CurrentDirectory + @"\work.fatiao");//上传文件目录

            SteamAPICall_t 调用上传 = SteamUGC.SubmitItemUpdate(开始上传, textBox2.Text);//调用提交上传函数
            上传物品回调.Set(调用上传);
        }

        void 工坊创建物品()//创建物品
        {
            SteamAPICall_t 创建物品 = SteamUGC.CreateItem((AppId_t)1416190, EWorkshopFileType.k_EWorkshopFileTypeCommunity);
            创建物品回调.Set(创建物品);
        }

        void 完成创建物品(CreateItemResult_t 创建处理结果, bool bIOFailure)
        {
            label10.Text = "正在创建物品，请耐心等待……";
            Console.WriteLine(创建处理结果.m_eResult);
            if (创建处理结果.m_eResult == EResult.k_EResultOK)//如果返回值创建物品等于成功
            {
                打包物品();

                物品ID = 创建处理结果.m_nPublishedFileId;//创建的物品id
                UGCUpdateHandle_t 开始上传 = SteamUGC.StartItemUpdate((AppId_t)1416190, 物品ID);//调用上传函数
                SteamUGC.SetItemTitle(开始上传, textBox1.Text);//物品标题
                SteamUGC.SetItemDescription(开始上传, "物品数字ID:" + 物品ID);//简介显示物品ID
                SteamUGC.SetItemVisibility(开始上传, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);//所有人可见
                SteamUGC.SetItemPreview(开始上传, foldPath);//封面图

                    var tagArray = new[] { "Process" };
                    SteamUGC.SetItemTags(开始上传, tagArray);//标签
                    SteamUGC.SetItemContent(开始上传, Environment.CurrentDirectory + @"\work.fatiao");//上传文件目录

                SteamAPICall_t 调用上传 = SteamUGC.SubmitItemUpdate(开始上传, textBox2.Text);//调用提交上传函数
                上传物品回调.Set(调用上传);
            }
            else//错误：未成功创建物品
            {
                switch (创建处理结果.m_eResult)
                {
                    case EResult.k_EResultInsufficientPrivilege:
                        /*MessageBox.Show("当前用户无权限提交物品，请联系Steam支持", "Steam禁令", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "Steam禁令", "当前用户无权限提交物品，请联系Steam支持", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultNotLoggedOn:
                        /*MessageBox.Show("当前Steam客户端已离线，请重新运行Steam客户端", "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "连接失败", "当前Steam客户端已离线，请重新运行Steam客户端", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultInvalidParam:
                        /*MessageBox.Show("物品信息不正确，请修改后重新提交", "信息错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "信息错误", "物品信息不正确，请修改后重新提交", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultLimitExceeded:
                        /*MessageBox.Show("当前用户Steam云空间不足，请清理云空间后重新上传", "Steam云不足", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "Steam云不足", "当前用户Steam云空间不足，请清理云空间后重新上传", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultDuplicateName:
                        /*MessageBox.Show("当前物品名称与用户已分享物品名称冲突，请修改名称后重新提交", "物品名称冲突", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "物品名称冲突", "当前物品名称与用户已分享物品名称冲突，请修改名称后重新提交", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultServiceReadOnly:
                        /*MessageBox.Show("当前用户最近修改过Steam密码或邮箱，请保持账户活跃五天后重新提交", "账户安全限制", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "账户安全限制", "当前用户最近修改过Steam密码或邮箱，请保持账户活跃五天后重新提交", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultFileNotFound:
                        /*MessageBox.Show("当前物品ID不存在，请输入正确的数字ID", "数字ID错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "数字ID错误", "当前物品ID不存在，请输入正确的数字ID", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultAccessDenied:
                        /*MessageBox.Show("当前用户无权限提交物品", "账户安全限制", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "账户安全限制", "当前用户无权限提交物品", ToolTipIcon.Error);
                        break;
                    default://其他情况
                        /*MessageBox.Show("您的网络不佳请稍后再试", "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "连接失败", "您的网络不佳请稍后再试", ToolTipIcon.Error);
                        break;
                }
                label10.Text = "";
                button3.Enabled = true;
            }
        }

        void 完成上传物品(SubmitItemUpdateResult_t 上传处理结果, bool bIOFailure)
        {
            Console.WriteLine(上传处理结果.m_eResult);
            if (上传处理结果.m_eResult == EResult.k_EResultOK)//如果返回值创建物品等于成功
            {
                label10.Text = "提交物品成功";
                Process.Start("steam://url/CommunityFilePage/" + 物品ID);//打开已上传物品页面

                    File.Delete(Environment.CurrentDirectory + @"\work.fatiao");
            }
            else//错误：未成功上传物品
            {
                switch (上传处理结果.m_eResult)
                {
                    case EResult.k_EResultInsufficientPrivilege:
                        /*MessageBox.Show("当前用户无权限提交物品，请联系Steam支持", "Steam禁令", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "Steam禁令", "当前用户无权限提交物品，请联系Steam支持", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultNotLoggedOn:
                        /*MessageBox.Show("当前Steam客户端已离线，请重新运行Steam客户端", "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "连接失败", "当前Steam客户端已离线，请重新运行Steam客户端", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultInvalidParam:
                        /*MessageBox.Show("物品信息不正确，请修改后重新提交", "信息错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "信息错误", "物品信息不正确，请修改后重新提交", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultLimitExceeded:
                        /*MessageBox.Show("当前用户Steam云空间不足，请清理云空间后重新上传", "Steam云不足", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "Steam云不足", "当前用户Steam云空间不足，请清理云空间后重新上传", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultDuplicateName:
                        /*MessageBox.Show("当前物品名称与用户已分享物品名称冲突，请修改名称后重新提交", "物品名称冲突", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "物品名称冲突", "当前物品名称与用户已分享物品名称冲突，请修改名称后重新提交", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultServiceReadOnly:
                        /*MessageBox.Show("当前用户最近修改过Steam密码或邮箱，请保持账户活跃五天后重新提交", "账户安全限制", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "账户安全限制", "当前用户最近修改过Steam密码或邮箱，请保持账户活跃五天后重新提交", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultFileNotFound:
                        /*MessageBox.Show("当前物品ID不存在，请输入正确的数字ID", "数字ID错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "数字ID错误", "当前物品ID不存在，请输入正确的数字ID", ToolTipIcon.Error);
                        break;

                    case EResult.k_EResultAccessDenied:
                        /*MessageBox.Show("当前用户无权限提交物品", "账户安全限制", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "账户安全限制", "当前用户无权限提交物品", ToolTipIcon.Error);
                        break;
                    default://其他情况
                        /*MessageBox.Show("您的网络不佳请稍后再试", "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);//提示*/
                        发条.发条窗口.notifyIcon1.ShowBalloonTip(1000, "连接失败", "您的网络不佳请稍后再试", ToolTipIcon.Error);
                        break;
                }
                label10.Text = "";
                button3.Enabled = true;
            }
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)//创建
        {
            if (textBox3.Text != "")
            {
                if (发条.发条窗口.判断数字(textBox3.Text) == "非数字")
                {
                    label10.Text = "参数错误：物品ID必须为纯数字";
                    return;
                }
            }

            label10.Text = "提交物品信息，请耐心等待……";
            button3.Enabled = false;
            if (button3.Text == "创建")
            {
                工坊创建物品();
            }
            else//更新
            {
                ulong workshopid = ulong.Parse(textBox3.Text);
                更新物品(workshopid);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "选择一张1M以内的封面图",
                Filter = "封面图|*.jpg;*.png"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] names = fileDialog.FileNames;
                foreach (string file in names)
                {
                    foldPath = file;//将选择的路径 赋值给 foldPath 变量
                    pictureBox1.Load(foldPath);
                    label3.Visible = false;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label3_Click(sender, e);
        }

        private void label4_Click(object sender, EventArgs e)//创意工坊条款
        {
            Process.Start("http://steamcommunity.com/sharedfiles/workshoplegalagreement");//打开条款页面
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                button3.Text = "创建";
            }
            else
            {
                button3.Text = "更新";
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/sharedfiles/filedetails/?id=841130477");//举报物品
        }

        private void steam创意工坊_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出线程 = true;
            Dispose();//释放资源
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/app/1416190/workshop/");//前往订阅物品
        }


    }
}
