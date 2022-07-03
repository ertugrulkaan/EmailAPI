using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;
using EmailAPI.Services.Core;
using EmailAPI.Services.Interfaces;

namespace EmailAPI.Services
{
    public class GroupService : ServiceBase<Group>, IGroupService
    {
        public GroupService(IGroupRepository groupRepository)
        {
            this.repository = groupRepository;
        }
    }
}
