using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;
using EmailAPI.Models.Dto;
using EmailAPI.Services.Core;
using EmailAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmailAPI.Services
{
    public class GroupReceiverService : ServiceBase<GroupReceiver>, IGroupReceiverService
    {
        private readonly IGroupRepository groupRepository;
        public GroupReceiverService(IGroupReceiverRepository groupReceiverRepository, IGroupRepository groupRepository)
        {
            this.repository = groupReceiverRepository;
            this.groupRepository = groupRepository;
        }

        public List<GroupReceiver> GetGroupAndReceivers(int groupId)
        {
            var group = groupRepository.Get(groupId);
            if (group != null)
            {
                var groupReceiversIds = repository.GetBy(f => f.GroupId == groupId).Select(s=> s.Id).ToList();

                var groupWithReceivers = repository.GetQuery()
                                       .Include(i => i.Group)
                                       .Include(i=> i.Receiver)
                                       .Where(w => groupReceiversIds.Contains(w.Id))
                                       .ToList();

                return groupWithReceivers;
            }

            return null;
        }
    }
}
