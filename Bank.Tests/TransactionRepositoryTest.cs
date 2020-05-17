using Banksystem.Repositories.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bank.Tests

//Test that you can't withdraw/transfer more money than the balance on the account
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

        //Test that you can't deposit, transfer or withdraw a negative amount

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


        //Test that a correct transaction is created (a correct transaction does not throw an invalid error)
        [TestMethod]

        public void When_Transfering_Money_It_Doesnt_Throw_Error()
        {
            var sut = new TransactionRepository(null);

            try
            {
                sut.CreateTransferTransaction(0, 0, 1000, 1010, 0);
            }
            catch (Exception e)
            {
                if (e.GetType() == new InvalidOperationException().GetType())
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void When_Deposit_Money_It_Doesnt_Throw_Error()
        {
            var sut = new TransactionRepository(null);

            try
            {
                sut.CreateDepositTransaction(0, 1000, 50);
            }
            catch (Exception e)
            {
                if (e.GetType() == new InvalidOperationException().GetType())
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void When_Withdrawing_Money_It_Doesnt_Throw_Error()
        {
            var sut = new TransactionRepository(null);

            try
            {
                sut.CreateWithdrawalTransaction(0, 1000, 500);
            }
            catch (Exception e)
            {
                if (e.GetType() == new InvalidOperationException().GetType())
                {
                    Assert.Fail();
                }
            }
        }

    }
}
