using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Account;
using Store.Core.Events.Common;

namespace Store.Persistence.Repositories
{
    public class AccountRepository : Repository<AccountEntity>, IAccountRepository
    {
        public AccountRepository(StoreDbContext context, IEventDispatcher domainEventsDispatcher) : base(context, domainEventsDispatcher)
        {
        }

        public async Task<AccountEntity> GetByUserName(string userName)
        {
            return await FindByCondition(p => p.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<AccountEntity> GetByUserNameAndPassword(string userName, string password)
        {
            return await FindByCondition(p => p.UserName == userName && p.Password == password).FirstOrDefaultAsync();
        }

        public async Task CreateAccount(AccountEntity accountEntity)
        {
            Create(accountEntity);
            await SaveAsync();
        }      
    }
}
