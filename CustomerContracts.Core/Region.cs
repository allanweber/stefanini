using Dapper;
using System.Collections.Generic;
using System.Configuration;

namespace CustomerContracts.Core
{
    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class RegionRepository : BaseRepository
    {
        public RegionRepository()
            : base(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString) { }

        public IEnumerable<Region> GetAll()
        {
            return this.connection.Query<Region>("select Id, Name from Region");
        }
    }
}
