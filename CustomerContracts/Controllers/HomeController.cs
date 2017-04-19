using CustomerContracts.Core;
using CustomerContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerContracts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Login", "Account");
            }

            using (UserRepository user = new UserRepository())
            {
                ViewBag.IsAdmin = user.IsAdmin(User.Identity.Name);
            }

            ViewBag.Title = "Customer List";

            CustomersViewModel model = new CustomersViewModel();

            using (GenderRepository gender = new GenderRepository())
                model.Genders = gender.GetAll();

            using (RegionRepository region = new RegionRepository())
                model.Regions = region.GetAll();

            using (CityRepository city = new CityRepository())
                model.Cities = city.GetAll();

            using (ClassificationRepository classification = new ClassificationRepository())
                model.Classifications = classification.GetAll();

            using (UserRepository user = new UserRepository())
                model.Sellers = user.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult LoadData(string name, string gender, string city, string region, string inital, string final, string classification, string seller)
        {
            try
            {
                int userId = 0;
                using (UserRepository userRepository = new UserRepository())
                {
                    var user = userRepository.GetUser(User.Identity.Name);
                    ViewBag.IsAdmin = user.Role.IsAdmin;
                    userId = user.Id;
                }

                int genderId, cityId, regionId, classId, sellerid = 0;
                DateTime initalDate, finalDate = DateTime.MinValue;

                genderId = gender.ToInt();

                cityId = city.ToInt();

                regionId = region.ToInt();

                initalDate = inital.ToDateTime();

                finalDate = final.ToDateTime();

                classId = classification.ToInt();

                sellerid = seller.ToInt();

                IEnumerable<Customer> customers = null;
                using (CustomerRepository customer = new CustomerRepository())
                    customers = customer.GetCustomers(name, genderId, cityId, regionId, initalDate, finalDate, classId, ViewBag.IsAdmin ? sellerid : userId);

                List<CustomerView> result = new List<CustomerView>();
                foreach (Customer item in customers)
                {
                    result.Add
                        (
                        new CustomerView
                        {
                            Id = item.Id,
                            Classification = item.Classification.Name,
                            Name = item.Name,
                            Phone = item.Phone,
                            Gender = item.Gender.Name,
                            City = item.City.Name,
                            Region = item.Region.Name,
                            LastPurchase = item.LastPurchase.ToString("dd/MM/yyyy"),
                        }
                        );
                    if (ViewBag.IsAdmin) result[result.Count - 1].Seller = item.Seller.Login;
                }

                return this.ReturnData(result);
            }
            catch (Exception ex)
            {
                return this.ReturnException(ex);
            }
        }

        [HttpPost]
        public ActionResult GetRegionByCity(string id)
        {
            try
            {
                int cityId = 0;
                bool isValid = Int32.TryParse(id, out cityId);

                IEnumerable<Region> regions = null;
                if (cityId == 0)
                {
                    using (RegionRepository region = new RegionRepository())
                        regions = region.GetAll();
                }
                else
                {
                    using (CityRepository city = new CityRepository())
                        regions = new List<Region> { city.GetRegionByCityId(cityId) };
                }


                var result = (from r in regions
                              select new
                              {
                                  id = r.Id,
                                  name = r.Name
                              }).ToList();

                if (cityId == 0)
                    result.Insert(0, new { id = 0, name = "Choose... " });

                return this.ReturnData(result);
            }
            catch (Exception ex)
            {
                return this.ReturnException(ex);
            }
        }

        private JsonResult ReturnData(object dataObj)
        {
            return new JsonResult
            {
                Data = new { data = dataObj, success = true },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private JsonResult ReturnException(Exception ex)
        {
            return new JsonResult
            {
                Data = new { error = ex.Message, success = false },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}
