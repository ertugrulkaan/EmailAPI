using EmailAPI.DataAccess.Base;
using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;

namespace EmailAPI.DataAccess
{
    public class GroupReceiverRepository : RepositoryBase<GroupReceiver>, IGroupReceiverRepository
    {
        public GroupReceiverRepository(ModelContext context) : base(context)
        {

        }
    }
}
