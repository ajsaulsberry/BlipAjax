using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Blip.Data.Countries;
using Blip.Data.Regions;
using Blip.Entities.Customers;
using Blip.Entities.Customers.ViewModels;

namespace Blip.Data.Customers
{
    public class CustomersRepository
    {
        public List<CustomerDisplayViewModel> GetCustomers()
        {
            using (var context = new ApplicationDbContext())
            {
                List<Customer> customers = new List<Customer>();
                customers = context.Customers.AsNoTracking()
                    .Include(x => x.Country)
                    .Include(x => x.Region)
                    .ToList();

                if (customers != null)
                {
                    List<CustomerDisplayViewModel> customersDisplay = new List<CustomerDisplayViewModel>();
                    foreach (var x in customers)
                    {
                        var customerDisplay = new CustomerDisplayViewModel()
                        {
                            CustomerID = x.CustomerID,
                            CustomerName = x.CustomerName,
                            CountryName = x.Country.CountryNameEnglish,
                            RegionName = x.Region.RegionNameEnglish
                        };
                        customersDisplay.Add(customerDisplay);
                    }
                    return customersDisplay;
                }
                return null;
            }
        }

        public CustomerEditViewModel GetCustomer(Guid customerid)
        {
            if (customerid != Guid.Empty)
            {
                using (var context = new ApplicationDbContext())
                {
                    var customer = context.Customers.AsNoTracking()
                        .Where(x => x.CustomerID == customerid)
                        .SingleOrDefault();
                    if (customer != null)
                    {
                        var customerEditVm = new CustomerEditViewModel()
                        {
                            CustomerID = customer.CustomerID.ToString("D"),
                            CustomerName = customer.CustomerName.Trim(),
                            SelectedCountryIso3 = customer.CountryIso3,
                            SelectedRegionCode = customer.RegionCode
                        };
                        var countriesRepo = new CountriesRepository();
                        customerEditVm.Countries = countriesRepo.GetCountries();
                        var regionsRepo = new RegionsRepository();
                        customerEditVm.Regions = regionsRepo.GetRegions(customer.CountryIso3);

                        return customerEditVm;
                    }
                }
            }
            return null;
        }

        public CustomerEditViewModel CreateCustomer()
        {
            var cRepo = new CountriesRepository();
            var rRepo = new RegionsRepository();
            var customer = new CustomerEditViewModel()
            {
                CustomerID = Guid.NewGuid().ToString(),
                Countries = cRepo.GetCountries(),
                Regions = rRepo.GetRegions()
            };
            return customer;
        }

        public bool SaveCustomer(CustomerEditViewModel customeredit)
        {
            if (customeredit != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    if (Guid.TryParse(customeredit.CustomerID, out Guid newGuid))
                    {
                        var customer = new Customer()
                        {
                            CustomerID = newGuid,
                            CustomerName = customeredit.CustomerName,
                            CountryIso3 = customeredit.SelectedCountryIso3,
                            RegionCode = customeredit.SelectedRegionCode
                        };
                        customer.Country = context.Countries.Find(customeredit.SelectedCountryIso3);
                        customer.Region = context.Regions.Find(customeredit.SelectedRegionCode);

                        context.Customers.Add(customer);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            // Return false if customeredit == null or CustomerID is not a guid
            return false;
        }

        public EmailAddressListViewModel GetEmailAddressList(Guid customerid)
        {
            if (customerid != Guid.Empty)
            {
                EmailAddressListViewModel emailAddressListViewModel = null;

                using (var context = new ApplicationDbContext())
                {
                    var emailAddresses = context.EmailAddresses.AsNoTracking()
                        .Where(x => x.CustomerID == customerid)
                        .OrderBy(x => x.Email);

                    if (emailAddresses != null)
                    {
                        foreach (var email in emailAddresses)
                        {
                            emailAddressListViewModel.EmailAddresses.Add(email.Email);
                        }
                        return emailAddressListViewModel;
                    }
                }
            }
            return null;
        }

        public EmailAddressViewModel GetEmailAddress(Guid customerid)
        {
            if (customerid != Guid.Empty)
            {
                using (var context = new ApplicationDbContext())
                {
                    var emailAddress = context.EmailAddresses.AsNoTracking()
                        .Where(x => x.CustomerID == customerid)
                        .SingleOrDefault();

                    if (emailAddress != null && !String.IsNullOrWhiteSpace(emailAddress.Email))
                    {
                        var emailAddressVm = new EmailAddressViewModel()
                        {
                            CustomerID = customerid.ToString("D"),
                            Email = emailAddress.Email
                        };
                        return emailAddressVm;
                    }
                }
            }
            return null;
        }

        public bool SaveEmailAddress(EmailAddressViewModel model)
        {
            if (model != null)
            {
                if (Guid.TryParse(model.CustomerID, out Guid newGuid) && !String.IsNullOrWhiteSpace(model.Email))
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var emailAddress = new EmailAddress()
                        {
                            CustomerID = newGuid,
                            Email = model.Email
                        };
                        return true;
                    }
                }
            }
            return false;
        }

        public PostalAddressListViewModel GetPostalAddressList(Guid customerid)
        {
            if (customerid != Guid.Empty)
            {
                using (var context = new ApplicationDbContext())
                {
                    var postalAddresses = context.PostalAddresses.AsNoTracking()
                        .Where(x => x.CustomerID == customerid)
                        .OrderBy(x => x.PostalAddressID);

                    if (postalAddresses != null)
                    {
                        var postalAddressListVm = new PostalAddressListViewModel();
                        foreach (var address in postalAddresses)
                        {
                            var postalAddressVm = new PostalAddressViewModel()
                            {
                                CustomerID = address.CustomerID.ToString("D"),
                                PostalAddressID = address.PostalAddressID,
                                StreetAddress1 = address.StreetAddress1,
                                StreetAddress2 = address.StreetAddress2,
                                City = address.City

                            };
                            var regionsRepo = new RegionsRepository();
                            postalAddressVm.RegionNameEnglish = regionsRepo.GetRegionNameEnglish(address.RegionCode);
                            var countryRepo = new CountriesRepository();
                            postalAddressVm.CountryNameEnglish = countryRepo.GetCountryNameEnglish(address.Iso3);
                            postalAddressListVm.PostalAddresses.Add(postalAddressVm);
                        }
                        return postalAddressListVm;
                    }
                }
            }
            return null;
        }

        public PostalAddressViewModel GetPostalAddress(Guid customerid, int postaladdressid)
        {
            if (customerid != Guid.Empty)
            {
                using (var context = new ApplicationDbContext())
                {
                    var postalAddress = context.PostalAddresses.AsNoTracking()
                        .Where(x => x.CustomerID == customerid && x.PostalAddressID == postaladdressid)
                        .SingleOrDefault();

                    if (postalAddress != null)
                    {
                        var postalAddressVm = new PostalAddressViewModel()
                        {
                            CustomerID = postalAddress.CustomerID.ToString("D"),
                            StreetAddress1 = postalAddress.StreetAddress1.Trim(),
                            StreetAddress2 = postalAddress.StreetAddress2.Trim(),
                            City = postalAddress.City.Trim()
                        };
                        var countriesRepo = new CountriesRepository();
                        postalAddressVm.CountryNameEnglish = countriesRepo.GetCountryNameEnglish(postalAddress.Iso3);
                        var regionsRepo = new RegionsRepository();
                        postalAddressVm.RegionNameEnglish = regionsRepo.GetRegionNameEnglish(postalAddress.RegionCode);

                        return postalAddressVm;
                    }
                }
            }
            return null;
        }

        public PostalAddressEditViewModel SavePostalAddress(PostalAddressEditViewModel model)
        {
            if (model !=null)
            {
                using (var context = new ApplicationDbContext())
                {
                    var postalAddress = new PostalAddress()
                    {
                        StreetAddress1 = model.StreetAddress1.Trim(),
                        StreetAddress2 = model.StreetAddress2.Trim(),
                        City = model.City.Trim(),
                        PostalCode = model.PostalCode,
                        RegionCode = model.SelectedRegionCode,
                        Iso3 = model.SelectedCountryIso3
                    };
                    Guid.TryParse(model.CustomerID, out Guid customerid);
                    postalAddress.CustomerID = customerid;
                    postalAddress.Region = context.Regions.Find(postalAddress.RegionCode);
                    postalAddress.Country = context.Countries.Find(postalAddress.Iso3);

                    context.PostalAddresses.Add(postalAddress);
                    context.SaveChanges();

                    var countriesRepo = new CountriesRepository();
                    model.Countries = countriesRepo.GetCountries();
                    var regionsRepo = new RegionsRepository();
                    model.Regions = regionsRepo.GetRegions(model.SelectedCountryIso3);
                    return model;
                }
            }
            return null;
        }
    }
}