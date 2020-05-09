﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Banksystem.Services.Interfaces;
using Banksystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Newtonsoft.Json;

namespace Banksystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;

        public TransactionController(IAccountService account, ICustomerService customer, ITransactionService service)
        {
            _accountService = account;
            _customerService = customer;
            _transactionService = service;
        }

        public IActionResult TransferMoney(int accountId)
        {
            var account = _accountService.GetAccountById(accountId);
            var customer = _customerService.GetCustomerByAccountId(account.AccountId);

            var transactionModel = new TransactionViewModel
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Name = customer.Givenname + " " + customer.Surname,
                OperationList = LoadOperations(),
            };

            return View(transactionModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TransferMoney(TransactionViewModel model)
        {
            if (model.ChosenOperation != "Transfer")
            {
                ModelState.Remove("ReceivingAccount");
            }
            if (ModelState.IsValid)
            {
                if (model.ChosenOperation == "Deposit")
                {
                    _transactionService.CreateDepositTransaction(model.AccountId, model.Balance, model.TransferAmount);
                }

                else if (model.ChosenOperation == "Withdrawal")
                {
                    if (model.TransferAmount > model.Balance)
                    {
                        ViewBag.Message = "Operation failed: Withdrawal amount can't be larger than the balance";
                        model.OperationList = LoadOperations();
                        return View(model);
                    }
                    else
                    {
                        _transactionService.CreateWithdrawalTransaction(model.AccountId, model.Balance, model.TransferAmount);
                    }
                }

                else
                {
                    if (model.TransferAmount > model.Balance)
                    {
                        ViewBag.Message = "Operation failed: Transaction amount can't be larger than the balance";
                        model.OperationList = LoadOperations();
                        return View(model);
                    }
                    else
                    {
                        var receiverBalance = _accountService.GetAccountBalance(Convert.ToInt32(model.ReceivingAccount));
                        _transactionService.CreateTransferTransaction(model.AccountId, Convert.ToInt32(model.ReceivingAccount), model.TransferAmount, model.Balance, receiverBalance);
                        var receiver = _customerService.GetCustomerByAccountId(Convert.ToInt32(model.ReceivingAccount));
                        ViewBag.Message = model.TransferAmount + " sek has now been transfered to account " + model.ReceivingAccount + " owned by " + receiver.Givenname + " " + receiver.Surname;
                        model.OperationList = LoadOperations();
                        model.Balance = _accountService.GetAccountBalance(model.AccountId);
                        return View(model);
                    }
                }
            }
            ViewBag.Message = "Operation has been carried out";
            model.OperationList = LoadOperations();
            model.Balance = _accountService.GetAccountBalance(model.AccountId);
            return View(model);
        }

        public List<SelectListItem> LoadOperations()
        {
            var operationsSession = HttpContext.Session.GetString("operations");

            if (String.IsNullOrEmpty(operationsSession))
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "--Select operation--", Value = "" });
                list.Add(new SelectListItem { Text = "Withdrawal", Value = "Withdrawal" });
                list.Add(new SelectListItem { Text = "Deposit", Value = "Deposit" });
                list.Add(new SelectListItem { Text = "Transfer", Value = "Transfer" });
                var serialize = JsonConvert.SerializeObject(list);
                HttpContext.Session.SetString("operations", serialize);
                return list;
            }
            else
            {
                var dezerialise = JsonConvert.DeserializeObject<List<SelectListItem>>(operationsSession);
                return dezerialise;
            }
        }
    }
}