using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Services.Interfaces
{
    public interface ITransactionService
    {
        void CreateDepositTransaction(int accountId, decimal balance, decimal transferAmount);

        void CreateWithdrawalTransaction(int accountId, decimal balance, decimal transferAmount);

        void CreateTransferTransaction(int fromAccount, int toAccount, decimal transferAmount, decimal fromBalance, decimal toBalance);

    }
}
