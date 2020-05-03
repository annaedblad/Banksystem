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
        public AccountController(IAccountService account)
        {
            _accountService = account;
        }
        public IActionResult AccountTransactionDetails(int accountId, int skip)
        {
            var detailModel = new AccountTransactions();
            detailModel.ListOfTransactions = _accountService.GetAccountTransactions(accountId, skip);
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
                //var hejsan = PartialView(@"Account\_TransactionView.cshtml", detailModel.ListOfTransactions);
                return PartialView("~/Views/Account/_TransactionView.cshtml", detailModel.ListOfTransactions);
            }
        }
    }
}