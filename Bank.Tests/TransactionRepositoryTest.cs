using Banksystem.Repositories.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class TransactionRepositoryTest
    {
        [TestMethod]
        public void When_Withdrawing_Larger_Amount_Than_Balance_Throw_Error()
        {
            var sut = new TransactionRepository(null);
            Assert.ThrowsException<InvalidOperationException>(() => sut.CreateWithdrawalTransaction(0, 7000, 8000));
        }

        [TestMethod]
        public void When_Transfering_Larger_Amount_Than_Balance_Throw_Error()
        {
            var sut = new TransactionRepository(null);
            Assert.ThrowsException<InvalidOperationException>(() => sut.CreateTransferTransaction(0, 0, 3000, 2000, 0));
        }

        [TestMethod]
        public void When_Depositing_Negative_Amount_Throw_Error()
        {
            var sut = new TransactionRepository(null);
            Assert.ThrowsException<InvalidOperationException>(() => sut.CreateDepositTransaction(0, 3000, -20));
        }

        [TestMethod]
        public void When_Withdrawing_Negative_Amount_Throw_Error()
        {
            var sut = new TransactionRepository(null);
            Assert.ThrowsException<InvalidOperationException>(() => sut.CreateWithdrawalTransaction(0, 1000, -50));
        }

        [TestMethod]
        public void When_Transfering_Negative_Amount_Throw_Error()
        {
            var sut = new TransactionRepository(null);
            Assert.ThrowsException<InvalidOperationException>(() => sut.CreateTransferTransaction(0, 0, -800, 1000, 0));
        }

    }
}
