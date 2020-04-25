using Banksystem.Models;
using Banksystem.Repositories.Classes;
using Banksystem.Repositories.Interfaces;
using Banksystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var accounts = _accountRepository.GetAccounts().Select(o => o.Balance).ToList().Sum();
            return accounts;

        }
    }
}
