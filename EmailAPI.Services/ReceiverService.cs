using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;
using EmailAPI.Services.Core;
using EmailAPI.Services.Interfaces;

namespace EmailAPI.Services
{
    public class ReceiverService : ServiceBase<Receiver>, IReceiverService  
    {
        public ReceiverService(IReceiverRepository receiverRepository)
        {
            this.repository = receiverRepository;
        }
    }
}
