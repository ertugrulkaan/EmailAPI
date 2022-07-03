using EmailAPI.Core.Model;
using System.Threading.Tasks;

namespace EmailAPI.Services.Interfaces
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Sends same mail from given host to given addresses by .NET SMTP. Use ';' between multiple addresses.
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task SendBySmtp(DirectlySendMailModel mail);
        /// <summary>
        /// Sends same mail from given host to given addresses by MailKit. Use ';' between multiple addresses.
        /// </summary>
        /// <param name="mail"></param>
        void SendByMailKit(SendByMailkitModel mail);
    }
}
