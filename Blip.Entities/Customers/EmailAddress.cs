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
        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        public Guid CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
