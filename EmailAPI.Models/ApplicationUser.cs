using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmailAPI.Models
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

    }
    public class ApplicationRole : Microsoft.AspNetCore.Identity.IdentityRole<int>
    {
        [JsonIgnore]
        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
        [JsonIgnore]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

    }
}
