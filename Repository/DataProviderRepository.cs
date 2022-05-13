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
    public class DataProviderRepository : IDataProviderRepository
    {
        private readonly IDbConnection _connection;
        public DataProviderRepository(IDataProvider dataProvider)
        {
            _connection = dataProvider.GetConnection();                
        }

        public async Task<IEnumerable<DataProvider>> All()
        {
            try
            {
                string query = $@"select * from DataProviders";

                IEnumerable<DataProvider> res = await _connection.QueryAsync<DataProvider>(query, null);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataProvider> Create(DataProvider entity)
        {
            try
            {
                string query = $@"  insert into dataproviders(
                                        DataProviderName, BaseFolderPath, CreatedAt) 
                                    values(
                                        @DataProviderName, @BaseFolderPath, @CreatedAt) returning DataProviderID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataProviderName", entity.DataProviderName);
                p.Add("@BaseFolderPath", entity.BaseFolderPath);
                p.Add("@CreatedAt", entity.CreatedAt);

                IEnumerable<int> res = await _connection.QueryAsync<int>(query, p);
                var element = await this.Find(res.First());
                return element;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete(int EntityID)
        {
            try
            {
                string query = $@"delete from DataProviders where DataProviderID = @DataProviderID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataProviderID", EntityID);

                int res = await _connection.ExecuteAsync(query, p);

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataProvider> Find(int EntityID)
        {
            try
            {
                string query = $@"select * from DataProviders where DataProviderID = @DataProviderID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataProviderID", EntityID);

                IEnumerable<DataProvider> res = await _connection.QueryAsync<DataProvider>(query, p);
                return res.ToList().Count() != 0 ? res.First() : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataProvider> Update(DataProvider entity)
        {
            try
            {
                string query = $@"
                            update DataProviders 
                            set 
                                DataProviderName = @DataProviderName, 
                                BaseFolderPath = @BaseFolderPath
                            where 
                                DataProviderID = @DataProviderID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataProviderID", entity.DataProviderID);
                p.Add("@DataProviderName", entity.DataProviderName);
                p.Add("@BaseFolderPath", entity.BaseFolderPath);

                int res = await _connection.ExecuteAsync(query, p);
                var element = await this.Find(entity.DataProviderID);
                return element;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
