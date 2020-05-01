using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.Services.Interfaces
{
    public interface ICustomerService
    {
        int TotalAmountOfCustomers();

        Customers GetCustomerById(int id);

        List<Customers> GetCustomersByCityOrName(string name, string city);
    }
}
