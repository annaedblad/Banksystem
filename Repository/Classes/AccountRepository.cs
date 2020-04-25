using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Classes
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDBContext _bankDBContext;
        public AccountRepository (BankDBContext context)
        {
            _bankDBContext = context;
        }
    }
}
