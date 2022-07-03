using EmailAPI.Core.Model;
using EmailAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailSenderService emailSenderService;

        public EmailSenderController(IEmailSenderService emailSenderService)
        {
            this.emailSenderService = emailSenderService;
        }

        /// <summary>
        /// Sends same mail from given host to given addresses by .NET SMTP. Use ';' between multiple addresses.
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendBySmtp")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SendBySmtp([FromBody] DirectlySendMailModel mail)
        {
            await emailSenderService.SendBySmtp(mail);
            
            return new OkResult();
        }
        /// <summary>
        /// Sends same mail from given host to given addresses by MailKit. Use ';' between multiple addresses.
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendByMailKit")]
        [Authorize(Roles = "Administrator")]
        public IActionResult SendByMailKit([FromBody] SendByMailkitModel mail)
        {
            emailSenderService.SendByMailKit(mail);

            return new OkResult();
        }
    }
}
