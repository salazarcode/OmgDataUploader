using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PostgresProvider : IDataProvider
    {
        private readonly IConfiguration _conf;
        protected string _connectionString { get; set; }
        protected IDbConnection _conection { get; set; }

        public PostgresProvider(IConfiguration conf)
        {
            _conf = conf;
            _connectionString = _conf.GetConnectionString("prod");
        }

        public IDbConnection GetConnection() 
        {
            IDbConnection connection = new NpgsqlConnection(_connectionString);
            return connection;
        }
    }
}
