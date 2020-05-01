using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.ViewModels
{
    public class SearchParametersViewModel
    {
        public string Name { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Field is mandatory")]
        [RegularExpression(@"(\s*[0-9]{0,6})", ErrorMessage = "Field must be a number")]
        public int CustomerId { get; set; }

        public string NationalId { get; set; }

        public string Adress { get; set; }
    }
}
