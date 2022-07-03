using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmailAPI.Models
{
    public class Group : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public List<GroupReceiver> GroupReceivers { get; set; }

    }
}
