using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.Entities.Metadata
{
    public class AddressType
    {
        [Key]
        [Required]
        [MaxLength(10), MinLength(2)]
        public string AddressTypeID { get; set; }
    }
}
