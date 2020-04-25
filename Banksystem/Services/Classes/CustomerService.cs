using Banksystem.Models;
using Banksystem.Repositories.Interfaces;
using Banksystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerrepository;
        public CustomerService (ICustomerRepository customer)
        {
            _customerrepository = customer;
        }
        public int TotalAmountOfCustomers()
        {
            var amountCustomers = _customerrepository.GetCustomers().Count();
            return amountCustomers;
        }

        public Customers GetCustomerById(int id)
        {
            var customer = _customerrepository.GetCustomers().Where(o => o.CustomerId == id).FirstOrDefault();
            return customer;
        }
    }
}
