using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.Entities.Customers.ViewModels
{
    public class PostalAddressListViewModel
    {
        [StringLength(38)]
        public string CustomerID { get; set; }

        public ICollection<PostalAddressViewModel> PostalAddresses { get; set; }
    }
}
