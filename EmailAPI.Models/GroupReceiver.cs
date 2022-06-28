using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmailAPI.Models
{
    public class GroupReceiver : BaseModel
    {
        public long GroupId { get; set; }
        public long ReceiverId { get; set; }

        [JsonIgnore]
        [ForeignKey("ReceiverIdId")]
        public Receiver Receiver { get; set; }
        [JsonIgnore]
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

    }
}
