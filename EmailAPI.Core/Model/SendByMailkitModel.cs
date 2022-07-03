using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace EmailAPI.Core.Model
{
    public class SendByMailkitModel
    {
        [Required]
        public string TO { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string MessageAsHtml { get; set; }
    }
}
