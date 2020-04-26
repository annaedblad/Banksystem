using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banksystem.Models;
using Banksystem.Services.Interfaces;
using Banksystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

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

        public IActionResult SelectCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectCustomer(CustomerDetailsViewModel id)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerById(id.CustomerId);
                var accounts = _accountService.ShowAccountDetails(id.CustomerId);
                if (customer == null)
                {
                    ViewBag.Message = "Hittar ingen kund med det kundnumret";
                    return View();
                }
                var customerModel = new CustomerDetailsViewModel
                {
                    Givenname = customer.Givenname,
                    Surname = customer.Surname,
                    Streetaddress = customer.Streetaddress,
                    Zipcode = customer.Zipcode,
                    City = customer.City,
                    Birthday = customer.Birthday.ToString(),
                    CountryCode = customer.Telephonecountrycode,
                    Telephonenumber = customer.Telephonenumber,
                    Emailaddress = customer.Emailaddress,
                    Gender = customer.Gender,
                    Country = customer.Country,
                    TotalBalance = accounts.TotalBalance,
                    ListOfAccounts = accounts.ListOfAccounts
                };
                return View("CustomerDetails", customerModel);
            }
            return View();

        }
    }
} 