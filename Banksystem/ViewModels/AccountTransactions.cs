using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.ViewModels
{
    public class AccountTransactions
    {
        public List<Transactions> ListOfTransactions { get; set; }

        public int CustomerId { get; set; }

        public int AccountId { get; set; }
    }
}
