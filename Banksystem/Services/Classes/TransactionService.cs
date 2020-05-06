using Banksystem.Repositories.Interfaces;
using Banksystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Services.Classes
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService (ITransactionRepository transaction)
        {
            _transactionRepository = transaction;
        }
        public void CreateDepositTransaction(int accountId, decimal balance, decimal transferAmount)
        {
            _transactionRepository.CreateDepositTransaction(accountId, balance, transferAmount);
        }

        public void CreateTransferTransaction(int fromAccount, int toAccount, decimal transferAmount, decimal fromBalance, decimal toBalance)
        {
            _transactionRepository.CreateTransferTransaction(fromAccount, toAccount, transferAmount, fromBalance, toBalance);
        }

        public void CreateWithdrawalTransaction(int accountId, decimal balance, decimal transferAmount)
        {
            _transactionRepository.CreateWithdrawalTransaction(accountId, balance, transferAmount);
        }
    }
}
