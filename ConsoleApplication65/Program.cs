using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSharing.Frameworks.Email;
using SSharing.Frameworks.Common.Extends;
using SSharing.Frameworks.Common.Consts;

namespace ConsoleApplication65
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //1，使用.net自带的SmtpClient
                //SendEmailBySmtpClient();

                //2，使用MailKet
                //SendEmailByMailKet();

                //3，使用公共组件（基于MailKet封装）
                //SendEmailByFrameworks();

                ReceiveMailByFrameworks();
            }
            catch (Exception ex)
            {

            }

            Console.ReadKey();
        }

        static void ReceiveMailByFrameworks()
        {
            var emails= EmailHelper.Instace.Receive(new ReceiveMailRequest
            {
                Host= "pop3.ubtrip.com",
                UserName= "luning@ssharing.com",
                Password= "P@ssw0rd.123"
            });

            if (emails.HasValue())
            {
                Console.WriteLine(string.Format("开始接收邮件，共{0}条记录...", emails.Count));
                int i = 1;
                foreach (var item in emails)
                {
                    Console.WriteLine(string.Format("{0}，发件人：{1}，主题：{2}，时间：{3}", i, item.From, item.Subject, item.PostTime.ToString(DateTimeTypeConst.DATETIME)));
                    i++;
                }
            }

        }

        static void SendEmailByFrameworks()
        {
            EmailHelper.Instace.Send(new SendMailRequest
            {
                Host = "smtp.163.com",
                UserName = "mcgrady_525",
                Password = "P@ssw0rd.123",
                From = "mcgrady_525@163.com",
                To = "luning@ssharing.com,jiangbingyang@ssharing.com",
                Subject = "test send email by component",
                Body = @"<h3>hello world！</h3>",
                BodyFormat= BodyFormatType.HTML
            });            
        }

        static void SendEmailByMailKet()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", "mcgrady_525@163.com"));
            message.To.Add(new MailboxAddress("", "luning@ssharing.com"));
            message.Subject = "test send email by mailket";
            message.Body = new TextPart(BodyFormatType.HTML)
            {
                Text = @"<h3>hello world！</h3>"
                //Text = @"hello world！"
            };
            
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.163.com", 25, false);
                client.Authenticate("mcgrady_525", "P@ssw0rd.123");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        static void SendEmailBySmtpClient()
        {
            using (var smtpClient = new System.Net.Mail.SmtpClient("smtp.163.com", 25))
            {
                //smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("mcgrady_525", "P@ssw0rd.123");
                using (var mailMessage = new System.Net.Mail.MailMessage())
                {
                    mailMessage.From = new System.Net.Mail.MailAddress("mcgrady_525@163.com", "mcgrady_525");
                    mailMessage.To.Add("luning@ssharing.com");
                    mailMessage.Subject = "test send email by smtp client";
                    mailMessage.Body = "hello world!";

                    smtpClient.Send(mailMessage);
                }
            }
        }
    }

    public class EmailEntity
    {
        public EmailEntity()
        {
            Port = 25;
            UseSsl = false;
            BodyFormat = BodyFormatType.HTML;
        }

        /// <summary>
        /// Smtp服务器
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口，默认：25
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 是否使用ssl，默认：false
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Smtp服务帐号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Smtp服务密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 发件人地址，如：mcgrady_525@163.com
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 收件人地址，多个收件人以","分隔。如：aaa@163.com,bbb@163.com
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 抄送人地址，多个抄送人以","分隔。如：ccc@163.com,ddd@163.com
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 暗送人地址，多个暗送人以","分隔。如：eee@163.com,fff@163.com
        /// </summary>
        public string BCC { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 正文格式，如：plain，html
        /// </summary>
        public string BodyFormat { get; set; }
    }

    public sealed class BodyFormatType
    { 
        /// <summary>
        /// 文本
        /// </summary>
        public const string PLAIN = "plain";

        /// <summary>
        /// Html
        /// </summary>
        public const string HTML = "html";
    }
}
