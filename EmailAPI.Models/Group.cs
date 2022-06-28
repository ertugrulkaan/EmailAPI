using System.ComponentModel.DataAnnotations;

namespace EmailAPI.Models
{
    public class Group : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string CreatedDate { get; set; }

    }
}
