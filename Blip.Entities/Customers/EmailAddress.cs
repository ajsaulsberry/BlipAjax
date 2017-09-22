using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.Entities.Customers
{
    public class EmailAddress
    {
        [Key]
        [MaxLength(128)]
        public string Email { get; set; }

        public Guid CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
