using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banksystem.Models;
using Banksystem.Services.Interfaces;
using Banksystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Banksystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;

        public CustomerController (IAccountService account, ICustomerService customer)
        {
            _accountService = account;
            _customerService = customer;
        }
        public IActionResult Index()
        {
            var statistics = new StatisticsViewModel();
            statistics.AmountOfAccounts = _accountService.TotalAccountAmount();
            statistics.AmountOfCustomers = _customerService.TotalAmountOfCustomers();
            statistics.SumOfAccountBalances = _accountService.TotalAccountBalanceSum();
            return View(statistics);
        }
    }
} 