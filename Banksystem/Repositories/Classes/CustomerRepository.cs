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
        public CustomerRepository(BankDBContext context)
        {
            _bankdbcontext = context;
        }

        public Customers GetCustomerByAccountId(int id)
        {
            var customerId = _bankdbcontext.Dispositions.Where(o => o.AccountId == id).Select(x => x.CustomerId).FirstOrDefault();
            return GetCustomersById(customerId);
        }

        public List<Customers> GetCustomers()
        {
            return _bankdbcontext.Customers.ToList();
        }
        public Customers GetCustomersById(int id)
        {
            return _bankdbcontext.Customers.Where(o => o.CustomerId == id).FirstOrDefault();
        }

    }
}

