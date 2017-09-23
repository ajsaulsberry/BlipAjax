using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blip.Data;

namespace Blip.Data.Countries
{
    public class CountriesRepository
    {
        public IEnumerable<SelectListItem> GetCountries()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> countries = context.Countries.AsNoTracking()
                    .OrderBy(n => n.CountryNameEnglish)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Iso3.ToString(),
                            Text = n.CountryNameEnglish
                        }).ToList();
                var countrytip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select country ---"
                };
                countries.Insert(0, countrytip);
                return new SelectList(countries, "Value", "Text");
            }
        }

        public string GetCountryNameEnglish(string iso3)
        {
            if(!String.IsNullOrWhiteSpace(iso3))
            {
                using (var context = new ApplicationDbContext())
                {
                    var countryName = context.Countries.AsNoTracking()
                        .Where(x => x.Iso3 == iso3)
                        .SingleOrDefault();
                    if (countryName != null)
                    {
                        var countryNameEnglish = countryName.CountryNameEnglish.Trim();
                        return countryNameEnglish;
                    }
                }
            }
            return null;
        }
    }
}