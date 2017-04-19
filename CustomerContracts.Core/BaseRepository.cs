
using System;
using System.Data;
using System.Data.SqlClient;

namespace CustomerContracts.Core
{
    public abstract class BaseRepository: IDisposable
    {
        public string ConnectionString { get; private set; }

        protected IDbConnection connection = null;

        public BaseRepository(string connectionString)
        {
            this.ConnectionString = connectionString;

            this.connection = new SqlConnection(this.ConnectionString);

            this.connection.Open();
        }

        public void Dispose()
        {
            if(this.connection != null)
            {
                if (this.connection.State != ConnectionState.Closed)
                    this.connection.Close();
                this.connection.Dispose();
            }
        }
    }
}
