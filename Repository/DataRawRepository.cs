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
    public class DataRawRepository : IDataRawRepository
    {
        private readonly IDbConnection _connection;
        public DataRawRepository(IDataProvider dataProvider)
        {
            _connection = dataProvider.GetConnection();                
        }

        public async Task<IEnumerable<DataFile>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<DataFile> Create(DataFile entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(int EntityID)
        {
            throw new NotImplementedException();
        }

        public async Task<DataFile> Find(int EntityID)
        {
            throw new NotImplementedException();
        }

        public async Task<DataFile> Update(DataFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
