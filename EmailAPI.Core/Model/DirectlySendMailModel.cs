using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace EmailAPI.Core.Model
{
    public class DirectlySendMailModel
    {
        [Required]
        public string Host { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Port cannot be less then zero or bigger then 2147483647")]
        public int Port { get; set; }
        [Required]
        public string FromVisibleName { get; set; }
        [Required]
        public string FromEmail { get; set; }
        [Required]
        public string FromPassword { get; set; }
        [Required]
        public string TO { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
