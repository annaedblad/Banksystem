using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Banksystem.Models;
using Banksystem.ViewModels;
using Banksystem.Services.Interfaces;

namespace Banksystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;

        public HomeController(ILogger<HomeController> logger, IAccountService account, ICustomerService customer)
        {
            _logger = logger;
            _accountService = account;
            _customerService = customer;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            var statistics = new StatisticsViewModel();
            statistics.AmountOfAccounts = _accountService.TotalAccountAmount();
            statistics.AmountOfCustomers = _customerService.TotalAmountOfCustomers();
            statistics.SumOfAccountBalances = _accountService.TotalAccountBalanceSum();
            return View(statistics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
