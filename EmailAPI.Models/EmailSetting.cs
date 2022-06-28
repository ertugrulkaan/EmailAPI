using System.ComponentModel.DataAnnotations;

namespace EmailAPI.Models
{
    public class EmailSetting : BaseModel
    {
        [Required]
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        [Required]
        public string DefaultSenderAddress { get; set; }

    }
}
