using System.ComponentModel.DataAnnotations;

namespace EmailAPI.Models.Identity
{
    public class ChangePasswordModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
