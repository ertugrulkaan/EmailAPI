using EmailAPI.DataAccess.Base;
using EmailAPI.DataAccess.Interfaces;
using EmailAPI.Models;

namespace EmailAPI.DataAccess
{
    public class EmailSettingRepository : RepositoryBase<EmailSetting>, IEmailSettingRepository
    {
        public EmailSettingRepository(ModelContext context) : base(context)
        {

        }
    }
}
