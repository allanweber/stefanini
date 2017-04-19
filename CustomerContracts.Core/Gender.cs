using Dapper;
using System.Collections.Generic;
using System.Configuration;

namespace CustomerContracts.Core
{
    public class Gender
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class GenderRepository : BaseRepository
    {
        public GenderRepository()
            : base(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString) { }

        public IEnumerable<Gender> GetAll()
        {
            return this.connection.Query<Gender>("select Id, Name from Gender");
        }
    }
}
