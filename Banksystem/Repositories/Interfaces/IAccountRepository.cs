using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        List<Accounts> GetAccounts();
        Accounts GetAccountsById(int id);
        IQueryable<Accounts> GetAccountsByCustomerId(int customerId);

        List<Transactions> GetTransactionsById(int id, int skip);

        decimal GetAccountBalance(int accountId);

    }
}
