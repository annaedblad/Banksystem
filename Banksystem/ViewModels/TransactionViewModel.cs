using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banksystem.ViewModels
{
    public class TransactionViewModel
    {
        public int AccountId { get; set; }

        public decimal Balance { get; set; }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        [Required (ErrorMessage = "Field is required")]
        [Range(typeof(decimal),"0.1", "1000000000000", ErrorMessage = "Value must be larger than 0")]
        public decimal TransferAmount { get; set; }

        public List<SelectListItem> OperationList { get; set; }

        [Required (ErrorMessage ="A selection is required")]
        public string ChosenOperation { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Input must be a number")]
        public int ? ReceivingAccount { get; set; }
    }
}
