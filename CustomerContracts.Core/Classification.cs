using System;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CustomerContracts.Core
{
    public class Classification
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ClassificationRepository : BaseRepository
    {
        public ClassificationRepository()
            : base(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString) { }

        public IEnumerable<Classification> GetAll()
        {
            return this.connection.Query<Classification>("select Id, Name from Classification");
        }
    }
}
