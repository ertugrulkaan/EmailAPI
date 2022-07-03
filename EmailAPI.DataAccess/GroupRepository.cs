using EmailAPI.DataAccess.Base;
using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;

namespace EmailAPI.DataAccess
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(ModelContext context) : base(context)
        {

        }
    }
}
