using Dapper;
using System.Collections.Generic;
using System.Configuration;

namespace CustomerContracts.Core
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }
    }

    public class CityRepository : BaseRepository
    {
        public CityRepository()
            : base(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString) { }

        public IEnumerable<City> GetAll()
        {
            return this.connection.Query<City>("select Id, Name, RegionId from City");
        }

        public Region GetRegionByCityId(int cityId)
        {
            return this.connection.QuerySingle<Region>
                (@"select Region.Id, Region.Name from City 
                    inner join Region on Region.Id = City.RegionId
                where City.Id = @cityId", new { cityId });
        }
    }
}
