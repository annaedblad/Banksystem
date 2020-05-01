using Banksystem.Models;
using Banksystem.Repositories.Interfaces;
using Banksystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public List<Customers> GetCustomersByCityOrName (string name, string city)
        {
            var allCustomers = _customerrepository.GetCustomers();
            List<Customers> searchResults = new List<Customers>();
            name = name?.ToLower();
            city = city?.ToLower();
            if (name == null)
            {
                searchResults = allCustomers.Where(o => o.City.ToLower().Contains(city)).ToList();
            }
            else if (city == null)
            {
                searchResults = allCustomers.Where(o => o.Givenname.ToLower().Contains(name) || o.Surname.ToLower().Contains(name)).ToList();
            }
            else
            {
                searchResults = allCustomers.Where(o => o.City.ToLower().Contains(city) && (o.Givenname.ToLower().Contains(name) || o.Surname.ToLower().Contains(name))).ToList();
            }
           
            return searchResults;
      
        }
    }
}
