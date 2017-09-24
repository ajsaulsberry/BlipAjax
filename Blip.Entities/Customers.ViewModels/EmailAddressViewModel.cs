using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.Entities.Customers.ViewModels
{
    public class EmailAddressViewModel
    {
        [StringLength(38)]
        public string CustomerID { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
