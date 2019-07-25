using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Account;

namespace Store.Persistence.Repositories
{
    public class AccountRepository : Repository<AccountEntity>, IAccountRepository
    {
        public AccountRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<AccountEntity> GetByUserName(string userName)
        {
            return await FindByCondition(p => p.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task CreateAccount(AccountEntity accountEntity)
        {
            Create(accountEntity);
            await SaveAsync();
        }
    }
}
