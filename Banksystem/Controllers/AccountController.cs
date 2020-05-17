using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banksystem.Services.Interfaces;
using Banksystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Banksystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        public AccountController(IAccountService account, ICustomerService customer)
        {
            _accountService = account;
            _customerService = customer;
        }
        public IActionResult AccountTransactionDetails(int accountId, int skip)
        {
            var detailModel = new AccountTransactions();
            detailModel.CustomerId = _customerService.GetCustomerByAccountId(accountId).CustomerId;
            detailModel.ListOfTransactions = _accountService.GetAccountTransactions(accountId, skip);
            detailModel.AccountId = accountId;
            if(skip==0)
            {
                return View(detailModel);
            }

            if (detailModel.ListOfTransactions.Count == 0)
            {
                ViewBag.Message = "All transactions are now loaded";
                return PartialView("~/Views/Account/_TransactionView.cshtml");

            }
            else
            {
                return PartialView("~/Views/Account/_TransactionView.cshtml", detailModel);
            }
        }
    }
}