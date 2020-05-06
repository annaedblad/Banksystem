using Banksystem.Models;
using Banksystem.Repositories.Classes;
using Banksystem.Repositories.Interfaces;
using Banksystem.Services.Interfaces;
using Banksystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Banksystem.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository repository)
        {
            _accountRepository = repository;
        }

        public int TotalAccountAmount()
        {
            var totalAmount = _accountRepository.GetAccounts().Count();
            return totalAmount;
        }

        public decimal TotalAccountBalanceSum()
        {
            var balance = _accountRepository.GetAccounts().Select(o => o.Balance).ToList().Sum();
            return balance;

        }

        public BalanceAndAccounts ShowAccountsDetails (int customerId)
        {
            var accounts = _accountRepository.GetAccountsByCustomerId(customerId);
            var balanceTotal = accounts.Sum(x => x.Balance);

                return new BalanceAndAccounts
                {
                    TotalBalance = balanceTotal,
                    ListOfAccounts = accounts
                };
        }

        public List<Transactions> GetAccountTransactions(int accountId, int skip)
        {
            return _accountRepository.GetTransactionsById(accountId, skip);
        }

        public Accounts GetAccountById(int accountId)
        {
            return _accountRepository.GetAccountsById(accountId);
        }

        public decimal GetAccountBalance(int accountId)
        {
            return _accountRepository.GetAccountBalance(accountId);
        }
    }
}
