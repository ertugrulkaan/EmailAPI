using EmailAPI.Models;
using EmailAPI.Models.Dto;
using EmailAPI.Services.Core;
using System.Collections.Generic;

namespace EmailAPI.Services.Interfaces
{
    public interface IGroupReceiverService : IServiceBase<GroupReceiver>
    {
        /// <summary>
        /// Gets group and its receivers information by groupId
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>If found returns List of GroupReceiver, if not returns null</returns>
        List<GroupReceiver> GetGroupAndReceivers(int groupId);
    }
}
