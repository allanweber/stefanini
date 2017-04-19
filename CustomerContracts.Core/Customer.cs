using System;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CustomerContracts.Core
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int GenderId { get; set; }

        public int CityId { get; set; }

        public int RegionId { get; set; }

        public DateTime LastPurchase { get; set; }

        public int ClassificationId { get; set; }

        public int UserId { get; set; }

        public Classification Classification { get; set; }

        public City City { get; set; }

        public Region Region { get; set; }

        public Gender Gender { get; set; }

        public User Seller { get; set; }
    }

    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository()
            : base(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString) { }

        public IEnumerable<Customer> GetCustomers(string name, int genderId, int cityId, int regionId, DateTime inital, DateTime final, int classId, int sellerid)
        {
            #region command
            string sql = @"select
                            Customer.Id
                            ,Customer.Name
                            ,Customer.Phone
                            ,Customer.GenderId
                            ,Customer.CityId
                            ,Customer.RegionId
                            ,Customer.LastPurchase
                            ,Customer.ClassificationId
                            ,Customer.UserId
                            ,Classification.Id
                            ,Classification.Name
                            ,City.Id
                            ,City.Name
                            ,City.RegionId
                            ,Region.Id
                            ,Region.Name
                            ,Gender.Id
                            ,Gender.Name
                            ,UserSys.Id
                            ,UserSys.Login
                            ,UserSys.Email
                            ,UserSys.UserRoleId
                        from Customer 
                        inner join Classification on Classification.Id = Customer.ClassificationId
                        inner join City on City.Id = Customer.CityId
                        inner join Region on Region.Id = Customer.RegionId
                        inner join Gender on Gender.Id = Customer.GenderId
                        inner join UserSys on UserSys.Id = Customer.UserId
                        where 1=1";
            #endregion

            DynamicParameters parameters = new DynamicParameters();

            if(!string.IsNullOrEmpty(name))
            {
                sql += " and Customer.Name = @name";
                parameters.Add("name", name);
            }

            if (genderId > 0)
            {
                sql += " and Gender.Id = @genderId";
                parameters.Add("genderId", genderId);
            }

            if (cityId > 0)
            {
                sql += " and City.Id = @cityId";
                parameters.Add("cityId", cityId);
            }

            if (regionId > 0)
            {
                sql += " and Region.Id = @regionId";
                parameters.Add("regionId", regionId);
            }

            if (classId > 0)
            {
                sql += " and Classification.Id = @classId";
                parameters.Add("classId", classId);
            }

            if (sellerid > 0)
            {
                sql += " and UserSys.Id = @sellerid";
                parameters.Add("sellerid", sellerid);
            }

            if(inital > DateTime.MinValue)
            {
                sql += " and Customer.LastPurchase >= @inital";
                parameters.Add("inital", inital);
            }

            if (final > DateTime.MinValue)
            {
                sql += " and Customer.LastPurchase <= @final";
                parameters.Add("final", final);
            }

            var query =  this.connection.Query<Customer, Classification, City, Region, Gender, User, Customer>
                (sql, (customer, classification, city, region, gender, user) => 
                {   customer.Classification = classification;
                    customer.City = city;
                    customer.Region = region;
                    customer.Gender = gender;
                    customer.Seller = user;
                    return customer;
                }, parameters);
            return query;
        }
    }
}
