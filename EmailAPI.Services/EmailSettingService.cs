using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;
using EmailAPI.Services.Core;
using EmailAPI.Services.Interfaces;

namespace EmailAPI.Services
{
    public class EmailSettingService : ServiceBase<EmailSetting>, IEmailSettingService
    {
        public EmailSettingService(IEmailSettingRepository emailSettingRepository)
        {
            this.repository = emailSettingRepository;
        }
    }
}
