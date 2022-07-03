using System.Collections.Generic;

namespace EmailAPI.Models.Dto
{
    public class GroupWithReceivers
    {
        public Group Group { get; set; }
        public List<Receiver> Receivers { get; set; }
    }
}
