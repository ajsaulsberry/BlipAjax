using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.Entities.Customers.ViewModels
{
    public class PostalAddressViewModel
    {
        public int PostalAddressID { get; set; }

        public string CustomerID { get; set; }

        [Display(Name = "Address Line 1")]
        [StringLength(100)]
        public string StreetAddress1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(100)]
        public string StreetAddress2 { get; set; }

        [Display(Name = "City / Town")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Zip / Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Display(Name = "State / Region")]
        public string RegionNameEnglish { get; set; }

        [Display(Name = "Country")]
        public string CountryNameEnglish { get; set; }
    }
}
