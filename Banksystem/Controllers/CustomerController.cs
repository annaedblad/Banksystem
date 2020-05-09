using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banksystem.Models;
using Banksystem.Services.Interfaces;
using Banksystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using X.PagedList;

namespace Banksystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private int pageSize = 50;
        public CustomerController(IAccountService account, ICustomerService customer)
        {
            _accountService = account;
            _customerService = customer;
        }

        public IActionResult SelectCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectCustomer(SearchParametersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customerModel = CreateCustomerViewModel(model.CustomerId);
                if (customerModel == null)
                {
                    ViewBag.Message = "No customer found by that id number";
                    return View();
                }
                return View("CustomerDetails", customerModel);
            }
            return View();
        }

        public IActionResult SearchForCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchForCustomer(SearchParametersViewModel model)
        {
            if (model.City == null && model.Name == null)
            {
                ViewBag.Message = "At least one field is mandatory";
                return View();
            }
            var result = _customerService.GetCustomersByCityOrName(model.Name, model.City);
          

            var resultViewModel = new List<SearchParametersViewModel>();
            foreach (var item in result)
            {
                var newItem = new SearchParametersViewModel();
                newItem.City = item.City;
                newItem.CustomerId = item.CustomerId;
                newItem.Name = item.Givenname + " " + item.Surname;
                newItem.NationalId = item.NationalId;
                newItem.Adress = item.Streetaddress;
                resultViewModel.Add(newItem);
            }
            var jsonList = JsonConvert.SerializeObject(resultViewModel);
            HttpContext.Session.SetString("SearchResults", jsonList);

            return View("SearchResults", resultViewModel.ToPagedList(1, pageSize));
        }

        public IActionResult SearchResults (int? page)
        {
            var jsonSearchResult = HttpContext.Session.GetString("SearchResults");
            var searchResults = JsonConvert.DeserializeObject<List<SearchParametersViewModel>>(jsonSearchResult);

            var pageNumber = page ?? 1;
            return View("SearchResults", searchResults.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult CustomerDetails(int id)
        {
            var customerModel = CreateCustomerViewModel(id);
            return View(customerModel);
        }

        public CustomerDetailsViewModel CreateCustomerViewModel(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            var accounts = _accountService.ShowAccountsDetails(id);

            if (customer == null)
            {
                return null;
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
            return customerModel;
        }
    }
}