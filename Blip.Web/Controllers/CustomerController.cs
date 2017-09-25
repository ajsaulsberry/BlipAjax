using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Blip.Data.Customers;
using Blip.Data.Metadata;
using Blip.Data.Regions;
using Blip.Entities.Customers.ViewModels;

namespace Blip.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var repo = new CustomersRepository();
            var customerList = repo.GetCustomers();
            return View(customerList);
        }

        [HttpGet]
        public ActionResult GetRegions(string iso3)
        {
            if (!string.IsNullOrWhiteSpace(iso3) && iso3.Length == 3)
            {
                var repo = new RegionsRepository();

                IEnumerable<SelectListItem> regions = repo.GetRegions(iso3);
                return Json(regions, JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var repo = new CustomersRepository();
            var customer = repo.CreateCustomer();
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerID, CustomerName, SelectedCountryIso3, SelectedRegionCode")] CustomerEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var repo = new CustomersRepository();
                    bool saved = repo.SaveCustomer(model);
                    if (saved)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                bool isGuid = Guid.TryParse(id, out Guid customerId);
                if (isGuid && customerId != Guid.Empty)
                {
                    return View();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [ChildActionOnly]
        public ActionResult EditCustomerPartial(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                bool isGuid = Guid.TryParse(id, out Guid customerId);
                if (isGuid && customerId != Guid.Empty)
                {
                    var repo = new CustomersRepository();
                    var model = repo.GetCustomer(customerId);
                    return View(model);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomerPartial(CustomerEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var repo = new CustomersRepository();
                bool saved = repo.SaveCustomer(model);
                if (saved)
                {
                    bool isGuid = Guid.TryParse(model.CustomerID, out Guid customerId);
                    if (isGuid)
                    {
                        var modelUpdate = repo.GetCustomer(customerId);
                        return PartialView(modelUpdate);
                    }
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [ChildActionOnly]
        public ActionResult AddressTypePartial(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                bool isGuid = Guid.TryParse(id, out Guid customerId);
                if (isGuid && customerId != Guid.Empty)
                {
                    var repo = new MetadataRepository();
                    var model = new AddressTypeViewModel()
                    {
                        CustomerID = id,
                        AddressTypes = repo.GetAddressTypes()
                    };
                    return PartialView("AddressTypePartial", model);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddressTypePartial(AddressTypeViewModel model)
        {
            if (ModelState.IsValid && !String.IsNullOrWhiteSpace(model.CustomerID))
            {
                switch (model.SelectedAddressType)
                {
                    case "Email":
                        var emailAddressModel = new EmailAddressViewModel()
                        {
                            CustomerID = model.CustomerID
                        };
                        return PartialView("CreateEmailAddressPartial", emailAddressModel);
                    case "Postal":
                        var postalAddressModel = new PostalAddressEditViewModel()
                        {
                            CustomerID = model.CustomerID
                        };
                        return PartialView("CreatePostalAddressPartial", postalAddressModel);
                    default:
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [ChildActionOnly]
        public ActionResult CreateEmailAddressPartial(Guid customerid)
        {
            var emailAddressModel = new EmailAddressViewModel()
            {
                CustomerID = customerid.ToString()
            };
            return PartialView("CreateEmailAddressPartial", emailAddressModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmailAddressPartial(EmailAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new CustomersRepository();
                bool saved = repo.SaveEmailAddress(model);
                if (saved)
                {
                    // refresh email address list
                    return PartialView("CreateEmailAddressPartial", model);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [ChildActionOnly]
        public ActionResult CreatePostalAddressPartial(string customerid)
        {
            var postalAddressModel = new PostalAddressEditViewModel()
            {
                CustomerID = customerid
            };
            return PartialView("CreatePostalAddressPartial", postalAddressModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePostalAddressPartial(PostalAddressEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new CustomersRepository();
                var updatedModel = repo.SavePostalAddress(model);
                if (updatedModel != null)
                {
                    return PartialView("CreatePostalAddressPartial", updatedModel);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

}