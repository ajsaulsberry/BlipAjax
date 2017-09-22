using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blip.Entities.Geographies;

namespace Blip.Entities.Customers
{
    public class PostalAddress
    {
        [Key]
        public int PostalAddressID { get; set; }

        public Guid CustomerID { get; set; }

        [MaxLength(3)]
        public string Iso3 { get; set; }

        [MaxLength(100)]
        public string StreetAddress1 { get; set; }

        [MaxLength(100)]
        public string StreetAddress2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(3)]
        public string RegionCode { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Country Country { get; set; }

        public virtual Region Region { get; set; }
    }
}
