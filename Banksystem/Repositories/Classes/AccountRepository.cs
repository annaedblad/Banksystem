using Banksystem.Models;
using Banksystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Accounts> GetAccountsByCustomerId(int customerId)
        {
            var listOfAccountIds = _bankDBContext.Dispositions.Where(o => o.CustomerId == customerId).Select(x => x.AccountId);
            return _bankDBContext.Accounts.Where(o => listOfAccountIds.Contains(o.AccountId));
        }

        public List<Transactions> GetTransactionsById(int id)
        {
            return _bankDBContext.Transactions.ToList();
        }
    }
}
