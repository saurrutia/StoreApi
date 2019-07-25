using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Interfaces;

namespace Store.Core.Account
{
    public interface IAccountRepository : IRepository<AccountEntity>
    {
        Task<AccountEntity> GetByUserName(string userName);
        Task CreateAccount(AccountEntity accountEntity);
    }
}
