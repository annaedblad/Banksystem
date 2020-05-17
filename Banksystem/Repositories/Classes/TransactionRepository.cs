using Banksystem.Models;
using Banksystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Repositories.Classes
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankDBContext _bankDBcontext;
        public TransactionRepository(BankDBContext bankDB)
        {
            _bankDBcontext = bankDB;
        }

        public void CreateDepositTransaction(int accountId, decimal balance, decimal transferAmount)
        {
            if (transferAmount <= 0)
            {
                throw new InvalidOperationException();
            }

            var deposit = new Transactions
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Type = "Credit",
                Amount = transferAmount,
                Balance = balance += transferAmount,
                Operation = "Deposit",
            };

            _bankDBcontext.Transactions.Add(deposit);
            _bankDBcontext.SaveChanges();
            UpdateAccountBalance(accountId, deposit.Balance);

        }

        public void CreateWithdrawalTransaction(int accountId, decimal balance, decimal transferAmount)
        {
            if (transferAmount > balance || transferAmount <= 0)
            {
                throw new InvalidOperationException();
            }
            var deposit = new Transactions
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Type = "Debit",
                Amount = -transferAmount,
                Balance = balance -= transferAmount,
                Operation = "Withdrawal",
            };

            _bankDBcontext.Transactions.Add(deposit);
            _bankDBcontext.SaveChanges();
            UpdateAccountBalance(accountId, deposit.Balance);
        }

        public void CreateTransferTransaction(int fromAccount, int toAccount, decimal transferAmount, decimal fromBalance, decimal toBalance)
        {

            if (transferAmount > fromBalance || transferAmount <= 0)
            {
                throw new InvalidOperationException();
            }

            var depositFrom = new Transactions
            {
                AccountId = fromAccount,
                Date = DateTime.Now,
                Type = "Debit",
                Amount = -transferAmount,
                Balance = fromBalance -= transferAmount,
                Operation = "Transfer to another Account",
            };

            var depositTo = new Transactions
            {
                AccountId = toAccount,
                Date = DateTime.Now,
                Type = "Credit",
                Amount = transferAmount,
                Balance = toBalance += transferAmount,
                Operation = "Transfer from another Account",
            };

            _bankDBcontext.Transactions.Add(depositFrom);
            _bankDBcontext.Transactions.Add(depositTo);
            _bankDBcontext.SaveChanges();
            UpdateAccountBalance(fromAccount, depositFrom.Balance);
            UpdateAccountBalance(toAccount, depositTo.Balance);
        }

        public void UpdateAccountBalance(int accountId, decimal balance)
        {
            var accountToUpdate = _bankDBcontext.Accounts.Where(x => x.AccountId == accountId).FirstOrDefault();
            accountToUpdate.Balance = balance;

            _bankDBcontext.Accounts.Update(accountToUpdate);
            _bankDBcontext.SaveChanges();
        }
    }
}
