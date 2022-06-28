using System.ComponentModel.DataAnnotations;

namespace EmailAPI.Models
{
    public class Receiver : BaseModel
    {
        [Required]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public bool IsValid { get; set; }

    }
}
