using Banksystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Birthday { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        public string CountryCode { get; set; }

        [Required (ErrorMessage = "Field is mandatory")]
        [RegularExpression(@"(\s*[0-9]{0,6})", ErrorMessage = "Field must be a number")]
        public int CustomerId { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public decimal TotalBalance { get; set; }

        public IQueryable<Accounts> ListOfAccounts { get; set; }
    }
}




