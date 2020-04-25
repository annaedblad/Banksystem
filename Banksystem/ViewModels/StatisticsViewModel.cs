using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.ViewModels
{
    public class StatisticsViewModel
    {
        public int AmountOfCustomers { get; set; }
        public int AmountOfAccounts { get; set; }

        public decimal SumOfAccountBalances { get; set; }
    }
}
