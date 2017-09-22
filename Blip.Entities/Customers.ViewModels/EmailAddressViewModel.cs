﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blip.Entities.Customers.ViewModels
{
    class EmailAddressViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(38)]
        public string Guid { get; set; }
    }
}