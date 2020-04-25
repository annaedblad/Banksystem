using Banksystem.Models;
using Banksystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Repositories.Classes
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDBContext _bankDBContext;
        public AccountRepository (BankDBContext context)
        {
            _bankDBContext = context;
        }

        public List<Accounts> GetAccounts()
        {
            return _bankDBContext.Accounts.ToList();
        }
    }
}
