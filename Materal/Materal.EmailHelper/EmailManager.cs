using System;
using Materal.Common;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Materal.StringHelper;

namespace Materal.EmailHelper
{
    public class EmailManager
    {
        private readonly EmailConfigModel _config;

        public EmailManager(EmailConfigModel config)
        {
            _config = config;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件体</param>
        /// <param name="targetAddress">目标地址</param>
        /// <param name="ccAddress">抄送地址</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="priority">优先级</param>
        /// <param name="isHtml">是否为Html邮件</param>
        public void SendMail(string address, string displayName, string title, string body, ICollection<string> targetAddress, ICollection<string> ccAddress, Encoding encoding, MailPriority priority = MailPriority.Normal, bool isHtml = true)
        {
            MailMessage mailMessage = GetSendMailMessage(address, displayName, title, body, targetAddress, ccAddress, encoding, priority, isHtml);
            SendMail(mailMessage);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailMessage"></param>
        public void SendMail(MailMessage mailMessage)
        {
            try
            {
                var client = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = _config.Host,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(_config.UserName, _config.Password)
                };
                client.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                throw new MateralException("收件人接收失败。", ex);
            }
            catch (SmtpException ex)
            {
                throw new MateralException("邮件发送出错。", ex);
            }
        }
        /// <summary>
        /// 获得发送邮件消息
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件体</param>
        /// <param name="targetAddress">目标地址</param>
        /// <param name="ccAddress">抄送地址</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="priority">优先级</param>
        /// <param name="isHtml">是否为Html邮件</param>
        /// <returns></returns>
        public MailMessage GetSendMailMessage(string address, string displayName, string title, string body, ICollection<string> targetAddress, ICollection<string> ccAddress, Encoding encoding, MailPriority priority = MailPriority.Normal,  bool isHtml = true)
        {
            try
            {
                if(!address.IsEMail()) throw new MateralException("发送地址必须为邮箱格式");
                var result = new MailMessage
                {
                    From = new MailAddress(address, displayName)

                };
                foreach (string target in targetAddress)
                {
                    if (!target.IsEMail()) throw new MateralException("目标地址必须为邮箱格式");
                    result.To.Add(target);
                }
                foreach (string cc in ccAddress)
                {
                    if (!cc.IsEMail()) throw new MateralException("抄送地址必须为邮箱格式");
                    result.CC.Add(cc);
                }
                result.Subject = title;
                result.Body = body;
                result.BodyEncoding = encoding;
                result.IsBodyHtml = isHtml;
                result.Priority = priority;
                return result;
            }
            catch (MateralException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MateralException("组装邮件消息出错", ex);
            }
        }
    }
}
