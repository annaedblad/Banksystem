using Banksystem.Models;
using Banksystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Repositories.Classes
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankDBContext _bankdbcontext;
        public CustomerRepository (BankDBContext context)
        {
            _bankdbcontext = context;
        }

        public List<Customers> GetCustomers()
        {
            return _bankdbcontext.Customers.ToList();
        }
    }
}
