using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.ViewModels
{
    public class BalanceAndAccounts
    {
        public decimal TotalBalance { get; set; }

        public IQueryable<Accounts> ListOfAccounts { get; set; }
    }
}
