using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customers> GetCustomers();
        Customers GetCustomersById(int id);

        Customers GetCustomerByAccountId(int id);
    }
}
