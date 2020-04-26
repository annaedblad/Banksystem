using Banksystem.Models;
using Banksystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Services.Interfaces
{
    public interface IAccountService
    {
        int TotalAccountAmount();

        decimal TotalAccountBalanceSum();

        BalanceAndAccounts ShowAccountDetails(int customerId);
    }
}
