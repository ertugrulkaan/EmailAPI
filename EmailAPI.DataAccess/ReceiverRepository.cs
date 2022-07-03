using EmailAPI.DataAccess.Base;
using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;

namespace EmailAPI.DataAccess
{
    public class ReceiverRepository : RepositoryBase<Receiver>, IReceiverRepository
    {
        public ReceiverRepository(ModelContext context) : base(context)
        {

        }
    }
}
