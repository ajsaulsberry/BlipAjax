using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blip.Data;

namespace Blip.Data.Metadata
{
    public class MetadataRepository
    {
        IEnumerable<SelectListItem> GetAddressTypes()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> addresstypes = context.AddressTypes.AsNoTracking()
                    .OrderBy(x => x.AddressTypeID)
                    .Select(x =>
                    new SelectListItem
                    {
                        Value = x.AddressTypeID,
                        Text = x.AddressTypeID
                    }).ToList();
                return new SelectList(addresstypes, "Value", "Text");
            }
        }

    }
}
