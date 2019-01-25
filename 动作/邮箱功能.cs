using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace 动作库
{
   public class 邮箱功能
    {

        public static string 发送邮件(string 服务网址, int 端口, string 发件邮箱, string 密码, string 收件邮箱, string 邮件标题, string 邮件内容)
        {
            //确定smtp服务器地址 实例化一个Smtp客户端
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Host = 服务网址;
            smtpclient.Port = 端口;

            //确定发件地址与收件地址
            MailAddress sendAddress = new MailAddress(发件邮箱);
            MailAddress receiveAddress = new MailAddress(收件邮箱);

            //构造一个Email的Message对象 内容信息
            MailMessage mailMessage = new MailMessage(sendAddress, receiveAddress);
            mailMessage.Subject = 邮件标题 + DateTime.Now;
            mailMessage.SubjectEncoding = Encoding.Default;
            mailMessage.Body = 邮件内容;
            mailMessage.BodyEncoding = Encoding.Default;

            //邮件发送方式  通过网络发送到smtp服务器
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //如果服务器支持安全连接，则将安全连接设为true
            smtpclient.EnableSsl = true;
            try
            {
                //是否使用默认凭据，若为false，则使用自定义的证书，就是下面的networkCredential实例对象
                smtpclient.UseDefaultCredentials = false;

                //指定邮箱账号和密码,需要注意的是，这个密码是你在QQ邮箱设置里开启服务的时候给你的那个授权码
                NetworkCredential networkCredential = new NetworkCredential(发件邮箱, 密码);
                smtpclient.Credentials = networkCredential;

                //发送邮件
                smtpclient.Send(mailMessage);
                Console.WriteLine("发送邮件成功");
                return "真";
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message, "发送邮件失败");
                return "假";
            }
        }


    }
}
