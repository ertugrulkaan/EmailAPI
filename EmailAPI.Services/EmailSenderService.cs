using EmailAPI.Core.Model;
using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailAPI.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IEmailSettingRepository emailSettingRepository;

        public EmailSenderService(IEmailSettingRepository emailSettingRepository)
        {
            this.emailSettingRepository = emailSettingRepository;
        }

        public async Task SendBySmtp(DirectlySendMailModel mail)
        {
            try
            {
                using (var client = new System.Net.Mail.SmtpClient(mail.Host, mail.Port))
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(mail.FromEmail, mail.FromPassword);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(mail.FromEmail, mail.FromVisibleName),
                        Subject = mail.Subject,
                        Body = mail.Message,
                        IsBodyHtml = true
                    };

                    ParseEmailListAndAddToListForSmtp(mailMessage.To, mail.TO);
                    ParseEmailListAndAddToListForSmtp(mailMessage.CC, mail.CC);
                    ParseEmailListAndAddToListForSmtp(mailMessage.Bcc, mail.BCC);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void SendByMailKit(SendByMailkitModel mail)
        {
            var emailSettings = emailSettingRepository.Get(1);
            if (emailSettings != null)
            {
                // create message
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(MailboxAddress.Parse(emailSettings.DefaultSenderAddress));                
                ParseEmailListAndAddToListForMailKit(mailMessage.To, mail.TO);
                ParseEmailListAndAddToListForMailKit(mailMessage.Cc, mail.CC);
                ParseEmailListAndAddToListForMailKit(mailMessage.Bcc, mail.BCC);
                mailMessage.Subject = mail.Subject;
                mailMessage.Body = new TextPart(TextFormat.Html) { Text = mail.MessageAsHtml };

                // send email
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect(emailSettings.SmtpServer, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    smtp.Authenticate(emailSettings.SmtpUserName, emailSettings.SmtpPassword);
                    smtp.Send(mailMessage);
                    //Todo: exceptions will be handled.
                    smtp.Disconnect(true);
                }
            }
        }
        public void ParseEmailListAndAddToListForMailKit(InternetAddressList list, string emails)
        {
            if (!String.IsNullOrEmpty(emails))
            {
                if (emails.Contains(";"))
                {
                    foreach (var email in emails.Split(";"))
                    {
                        if (!list.Contains(MailboxAddress.Parse(email)))
                        {
                            list.Add(MailboxAddress.Parse(email));
                        }
                    }
                }
                else
                {
                    list.Add(MailboxAddress.Parse(emails));
                }
            }
        }
        public void ParseEmailListAndAddToListForSmtp(MailAddressCollection list, string emails)
        {
            if (!String.IsNullOrEmpty(emails))
            {
                if (emails.Contains(";"))
                {
                    foreach (var email in emails.Split(";"))
                    {
                        if (!list.Contains(new MailAddress(email)))
                        {
                            list.Add(new MailAddress(email));
                        }
                    }
                }
                else
                {
                    list.Add(emails);
                }
            }
        }
    }
}
