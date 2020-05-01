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
        public IActionResult AccountTransactionDetails(int accountId)
        {
            var detailModel = new AccountTransactions();
            detailModel.ListOfTransactions = _accountService.GetAccountTransactions(accountId);
            return View(detailModel);
        }
    }
}