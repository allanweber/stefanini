using CustomerContracts.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerContracts.Models
{
    public class CustomersViewModel
    {
        [Display(Name = "Name:")]
        public string SearchName { get; set; }

        [Display(Name = "Gender:")]
        public int SearchGenderId { get; set; }

        [Display(Name = "City:")]
        public int SearchCityId { get; set; }

        [Display(Name = "Region:")]
        public int SearchRegionId { get; set; }

        [Display(Name = "Last Purchase:")]
        public string SearchInitialDate { get; set; }

        [Display(Name = "until:")]
        public string SearchFinalDate { get; set; }

        [Display(Name = "Classification:")]
        public int SearchClassificationId { get; set; }

        [Display(Name = "Seller:")]
        public int SearchUserId { get; set; }

        public string Order { get; set; }

        public IEnumerable<Gender> Genders { get; set; }

        public IEnumerable<Region> Regions { get; set; }

        public IEnumerable<City> Cities { get; set; }

        public IEnumerable<Classification> Classifications { get; set; }

        public IEnumerable<User> Sellers { get; set; }
    }

    public class CustomerView
    {
        public int Id { get; set; }
        public string Classification { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string LastPurchase { get; set; }
        public string Seller { get; set; }
    }

}

