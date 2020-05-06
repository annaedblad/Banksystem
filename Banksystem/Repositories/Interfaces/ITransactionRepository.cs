using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void CreateDepositTransaction(int accountId, decimal balance, decimal transferAmount);

        void UpdateAccountBalance(int accountId, decimal balance);

        void CreateWithdrawalTransaction(int accountId, decimal balance, decimal transferAmount);

        void CreateTransferTransaction(int fromAccount, int toAccount, decimal transferAmount, decimal fromBalance, decimal toBalance);
    }
}
