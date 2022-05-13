using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Data.Repository
{
    public class DataProviderOneRepository : IDataProviderOneRepository
    {
        private readonly IDbConnection _connection;
        public DataProviderOneRepository(IDataProvider dataProvider)
        {
            _connection = dataProvider.GetConnection();                
        }
        public async Task<int> Create(DataProviderOneEntity entity)
        {
            try
            {
                string query = $@"
                    insert into RawData(
                        vid
                    ) 
                    values(
                        @vid
                    ) returning RawDataID;
                ";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@vid", entity.vid);

                IEnumerable<int> res = await _connection.QueryAsync<int>(query, p);
                return res.First();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
